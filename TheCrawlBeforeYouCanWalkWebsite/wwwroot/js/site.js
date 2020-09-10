// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(":checkbox").change(function () {
    var id = $(this).attr('id');
    var isChecked = $(this).is(':checked');
    updateTestResult(id, isChecked, this);
});


function updateTestResult(id, value, checkBox) {
    $.ajax({
        url: paidStatusUrl,
        data: { 'id': id, 'hasPaid': value }
    }).done(function () {
        styleCardBackground(checkBox);
    }).fail(function () {
        $('#dbError').text('Databasefejl, ikke opdateret');
    });
}
