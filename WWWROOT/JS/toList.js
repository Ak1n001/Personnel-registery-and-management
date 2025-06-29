$(document).ready(function () {
    
    loadData(function (data) {
        populateTable(data);
        $('#data-table').DataTable();
    });

    //new DataTable('#data-table');

   
   
});


function loadData(callback) {
    $.ajax({
        url: '/List/GetEmployees/', // Controller action
        type: 'GET',
        //dataType: 'json',
        success: function (data) {
            callback(data);
            
           
        },
        error: function (error) {
            console.error(error);
        }
    });
}

function populateTable(data) {
    var table = $('#data-table tbody');
    table.empty(); // Clear existing data
    var reversedData = data.personelKayits.slice().reverse();
    reversedData.forEach(function (item) {
        console.log(item.id);
         var cins = "";
        if (item.active == true) {

            if (item.cinsiyet == false) {
                cins = "kadın";
            }
            else {
                cins = "erkek";
            }
            var imageTag = '<img src="/Home/GetImage?id=' + item.id + '" alt="User Image" width="40" height="40" />';
            var row = '<tr>' +
                '<td>' + imageTag + '</td>' +
                '<td>' + item.ad + '</td>' +
                '<td>' + item.soyad + '</td>' +
                '<td>' + cins + '</td>' +
                '<td>' + data.ulkeSehirTable.find(u => u.id == item.ulke).sehir_ulke_name + '</td>' +
                '<td>' + data.ulkeSehirTable.find(u => u.id == item.sehir).sehir_ulke_name + '</td>' +
                '<td>' + item.aciklama + '</td>' +
                '<td>' + item.dogumTarihi.substring(0, 10) + '</td>' +
                '<td>' +
                "<button type='button' class='edit-button btn btn-primary' onclick='redirect(" + parseInt(item.id) + ")'>Edit</button> " +
                '</td>'+
                '<td>' +
                "<button type='button' id='edit-redirect' class='delete-button btn btn-danger' onclick='confirmDelete(" + parseInt(item.id) + ")'>Delete</button> " +
                '</td>' +
                '</tr>';
            table.append(row);
        }
        
    });
}



function delete_data(employeeId) {
    $.ajax({
        url: "/List/DeactivateEmployee/" + employeeId,
        type: "POST",
        success: function (response) {
            alert(response.message); // Show a success message
            location.reload(); // Refresh the page or update the UI as needed
        },
        error: function (error) {
            alert("Error: " + error.responseJSON.message); // Show an error message
        }
    });
}


function confirmDelete(employeeId) {
    var result = confirm("Are you sure you want to delete this employee?");

    if (result) {
        // If the user confirms, call the delete_data function
        delete_data(employeeId);
    }
}

function redirect(id) {
    window.location.href = "/Edit/Edit/" + id;
}