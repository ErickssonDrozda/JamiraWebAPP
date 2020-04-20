(function ($) {
    $(function () {
        var $Cep = $("#IdCep");
        $Cep.mask('00000-000', { reverse: true });
    });
})(jQuery);

$("#cadastrar").click(function (event) {
    event.preventDefault();
    $('#aguardeCEP').show();

    var $this = $("#cadastrar");
    var form = document.getElementById("formEstacionamento");
    var data = $(form).serialize();
    $.post(form.action, data, function (response) {
        if (response && response.Data) {
            if (response.Sucesso === true) {
                $('#sucesso').modal('show');
                $('#alertas').css('display', 'none');
            }
            else {
                $('#fracasso').modal('show');
            }
            $('#aguardeCEP').hide();
        }
        else {
            $('#fracasso').modal('show');
            $('#aguardeCEP').hide();
        }
    });
   
});
