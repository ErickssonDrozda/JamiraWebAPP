$(document).ready(function () {

    $("#cadastrar").click(function (event) {
        event.preventDefault();
        var $this = $("#cadastrar");
        var form = document.getElementById("formFaleConosco");
        var thisData = $(form).serialize();

        $.ajax({
            url: form.action,
            type: 'post',
            data: thisData,
            beforeSend: function () {
                $("#loading").show();
            }
        })
        .done(function (msg) {
            $("#loading").hide();
            $('#IdCategoria').val(99999);
            $('#Texto').val('');
            if (msg.Sucesso === true) {
                $('#sucesso').modal('show');
            } else {
                $('#fracasso').modal('show');
            }
           
        })
        .fail(function (jqXHR, textStatus, msg) {
            $("#loading").hide();
            $('#IdCategoria').val(99999);
            $('#Texto').val('');
            debugger;
            $('#fracasso').modal('show');
        });
    });
});
