(function ($) {
    $(function () {
        var url = location.href;
        if (url.indexOf("Estacionamento") !== -1) {
            var $Conta = $("#Conta");
            $Conta.mask('0000000-0', { reverse: true });
        }
        else {
            var $Cep = $("#CEP");
            $Cep.mask('00000-000', { reverse: true });
        }
    });
})(jQuery);