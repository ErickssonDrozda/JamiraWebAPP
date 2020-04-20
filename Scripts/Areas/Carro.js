$(document).ready(function () {

    $("#cadastrar").click(function (event) {
        $('#aguarde').show();
        event.preventDefault();
        var $this = $("#cadastrar");
        var form = document.getElementById("formCarro");
        var data = $(form).serialize();
        $.post(form.action, data, function (response) {
            if (response && response.Sucesso) {
                if (response.Sucesso === true) {
                    $('#sucesso').modal('show');
                    $('#alertas').css('display', 'none');
                } else {
                    $('#fracasso').modal('show');
                }
            }
            else
            {
                $('#fracasso').modal('show');
            }
            $('#cadcarro').remove();
            $('#aguarde').hide();
        });
    });
});