$(document).ready(function ($) {
    var $CVV = $("#CVV");
    $CVV.mask('000', { reverse: true });

    var $Vencimento = $("#Vencimento");
    $Vencimento.mask('00/0000', { reverse: true });

    var $Numero = $("#NumeroCartao");
    $Numero.mask('0000 0000 0000 0000', { reverse: true });

    $("#comprarCredito").click(function (event) {
        $('#Pagamentos').show("slow");
    });
});
$(document).ready(function () {
    $("#RealizarPagamento").click(function (event) {
        $('#aguardar').show();
        var value = $('#demo')[0].innerText;
        $.post("ComprarCreditos", { value: value }, function (response) {
            if (response && response.Data) {
                if (response.Sucesso === true) {
                    $('#sucesso').modal('show');
                    $('#aguardar').hide();
                } else {
                    $('#fracasso').modal('show');
                    $('#aguardar').hide();
                }
            }
            else {
                $('#fracasso').modal('show');
                $('#aguardar').hide();
            }
        });
    });
});