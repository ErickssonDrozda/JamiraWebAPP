function Finalizar(idSolicitacao) {
    valor = String(idSolicitacao);
    $.ajax({
        url: "/Estacionamento/FinalizarSolicitacao",
        type: 'post',
        data: {
            id: valor
        },
        beforeSend : function () {
               $("#--" + idSolicitacao).show();
            }
     })
    .done(function (msg) {
        $('#sucesso').modal('show');
        $('#' + idSolicitacao + '--').remove();
    })
    .fail(function (jqXHR, textStatus, msg) {
        $('#fracasso').modal('show');
        $("#--" + idSolicitacao).hide();
    }); 
}