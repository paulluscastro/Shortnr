// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('#tryFriendlyCheck').on('click', function () {
        if (!$('#tryFriendlyCheck').is(':checked'))
            $('#tryFriendly').attr('disabled', 'disabled');
        else
            $('#tryFriendly').removeAttr('disabled');
    });
});