// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    let placeholder = $('#modal-container');

    $('button[data-toggle="ajax-modal"]').click(function (event) {
        let url = $(this).data('url');

        console.log(url)

        $.get(url).done((data) => {
            placeholder.html(data);
            placeholder.find('.modal').modal('show');
        })
    })
})

$(function () {
    $('[data-toggle="popover"]').popover()
})