// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function deleteToDo(index) {
    $.ajax({
        url: 'Home/Delete',
        type: 'POST',
        data: {
            Id: index
        },
        success: function () {
            window.location.reload();
        }
    });
}

function updateToDo(index) {

    $.ajax({
        url: 'Home/UpdateToFinished',
        type: 'PUT',
        data: {
            Id: index
        },
        dataType: 'json',
        success: function () {
            window.location.reload();            
        }
    });
}