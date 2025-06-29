
$(document).ready(function () {
   
    var selectCountry = $("#ulkeDropdown");
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

    // Populate the country dropdown
    $.ajax({
        url: "/Home/UlkeGetir", // Replace with the correct URL for your countries data
        type: "GET",
        dataType: "json",
        success: function (result) {
            $.each(result, function (index, item) {
                selectCountry.append($('<option>', {
                    value: item.id,
                    text: item.sehir_ulke_name
                }));
            });

            // Handle country selection to populate city dropdown
            sehirGetir();
            //loadData();
        },
        
        error: function (xhr, status, error) {
            console.error("AJAX error:", status, error);
        }
    });
    
});
function sehirGetir() {
    var selectCity = $("#sehirDropdown");

    $("#ulkeDropdown").on("change", function () {
        var selectedCountryId = $(this).val();
        selectCity.empty();

        // Make an AJAX request to fetch cities based on the selected country
        $.ajax({
            url: "/Home/SehirGetir/" + selectedCountryId, // Replace with the correct URL for cities based on country
            type: "GET",
            dataType: "json",
            success: function (cities) {
                $.each(cities, function (index, city) {

                    selectCity.append($('<option>', {

                        value: city.id,
                        text: city.sehir_ulke_name
                    }));
                    
                });
                if (selectedSehir != 0)
                    selectCity.val(selectedSehir);
            },
            error: function (xhr, status, error) {
                console.error("AJAX error:", status, error);
            }
        });
    });
}
function get_data() {
    var Ad = $("#Ad").val();
    var Soyad = $("#Soyad").val();
    var DogumTarihi = $("#datepick").val();
    var Cinsiyet = $("input[name='season']:checked").val();
    var Aciklama = $("#Aciklama_text").val();
    var Ulke = $("#ulkeDropdown").val();
    var Sehir = $("#sehirDropdown").val();

    
    var image = $("#employeepic")[0].files[0];

    // Create a FormData object to send the data including the image
    var formData = new FormData();
    formData.append("Ad", Ad);
    formData.append("Soyad", Soyad);
    formData.append("DogumTarihi", DogumTarihi);
    formData.append("Cinsiyet", Cinsiyet);
    formData.append("Aciklama", Aciklama);
    formData.append("Ulke", Ulke);
    formData.append("Sehir", Sehir);
    formData.append("ImageFile", image); // Image file


        var userData = {
            ad: Ad,
            soyad: Soyad,
            dogumTarihi: DogumTarihi,
            cinsiyet: Cinsiyet,
            ulke: Ulke,
            sehir: Sehir,
            aciklama: Aciklama,
            active: true,
            
        };
        $.ajax({
            type: "POST",
            url: "/Home/InsertUserData/", // Adjust the URL to your controller method
            data: formData,
            contentType: false, // Required for sending FormData
            processData: false, // Required for sending FormData


            success: function (response) {
                window.location.href = "/List/List"
                console.log(response);
            },
            error: function (error) {
                console.error(error);
            }

        });
        
}





