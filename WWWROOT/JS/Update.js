 // Define a global variable to store the selected "sehir" value

$(document).ready(function () {
    var selectedSehir;
    var x = window.location.href.split('/');
    idgelen = x[x.length - 1];

    if (idgelen != "List") {
        Edit(idgelen);
    }
    $("#employeepic").change(function () {

        var File = this.files;

        if (File && File[0]) {
            ReadImage(File[0]);
        }

    })
    var ReadImage = function (file) {

        var reader = new FileReader;
        var image = new Image;

        reader.readAsDataURL(file);
        reader.onload = function (_file) {

            image.src = _file.target.result;
            image.onload = function () {

                var height = this.height;
                var width = this.width;
                var type = file.type;
                var size = ~~(file.size / 1024) + "KB";

                $("#targetImg").attr('src', _file.target.result);
                $("#description").text("Size:" + size + ", " + height + "X " + width + ", " + type + "");
                $("#imgPreview").show();

            }

        }

    }
 
});

function Edit(id) {
    $.ajax({
        url: "/Edit/Update/" + id,
        type: "Get",

        success: function (response) {
            var originalUlke = response.personelKayits.find(u => u.id == id).ulke;
            var originalSehir = response.personelKayits.find(u => u.id == id).sehir;

            $("#Ad").val(response.personelKayits.find(u => u.id == id).ad);
            $("#Soyad").val(response.personelKayits.find(u => u.id == id).soyad);
            $("#datepick").val(response.personelKayits.find(u => u.id == id).dogumTarihi.substring(0, 10));
            $("input[name='season1'][value='" + response.personelKayits.find(u => u.id == id).cinsiyet + "']").prop('checked', true);
            $("#Aciklama_text").val(response.personelKayits.find(u => u.id == id).aciklama);
            selectedSehir = originalSehir;
            $("#ulkeDropdown").val(originalUlke).change(); // Set the original value for ulkeDropdown

            var userImageSrc = "/Home/GetImage?id=" + id;
            $("#targetImg").attr('src', userImageSrc);
            $("#imgPreview").show();
        },
        error: function () {
            // window.location.href = "/List/Update/";
            alert("Data not found");
        }
    });
}

function get_data_updated() {
    var x = window.location.href.split('/');
    idgelen = x[x.length - 1];
    var id = idgelen
    var Ad = $("#Ad").val();
    var Soyad = $("#Soyad").val();
    var DogumTarihi = $("#datepick").val();
    var Cinsiyet = $("input[name='season1']:checked").val();
    var Aciklama = $("#Aciklama_text").val();
    var Ulke = $("#ulkeDropdown").val();

   var image = $("#employeepic")[0].files[0];

    var Sehir = $("#sehirDropdown").val();;

    var formData = new FormData();

    formData.append("id", id);
    formData.append("Ad", Ad);
    formData.append("Soyad", Soyad);
    formData.append("DogumTarihi", DogumTarihi);
    formData.append("Cinsiyet", Cinsiyet);
    formData.append("Aciklama", Aciklama);
    formData.append("Ulke", Ulke);
    formData.append("Sehir", Sehir);
    formData.append("ImageFile", image); // Image file

    // Use the selectedSehir value here
    

    var userData = {
        id: id,
        ad: Ad,
        soyad: Soyad,
        dogumTarihi: DogumTarihi,
        cinsiyet: Cinsiyet,
        ulke: Ulke,
        sehir: Sehir,
        aciklama: Aciklama,
        active: true
    };
    $.ajax({
        type: "POST",
        url: "/Edit/InsertUserDataUpdated/" + idgelen, // Adjust the URL to your controller method
        data: formData,
        contentType: false, // Required for sending FormData
        processData: false, // Required for sending FormData

        success: function (response) {
            console.log(response);
        },
        error: function (error) {
            console.error(error);
        }
    });
}
function redirectToHome() {
    window.location.href = "/Home/Index";
}

