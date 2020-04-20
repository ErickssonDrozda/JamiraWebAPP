function Aprovar(idSolicitacao)
{
    valor = String(idSolicitacao);
     $.ajax({
          url: "/Estacionamento/AprovarSolicitacao",
          type : 'post',
          data : {
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

function Reprovar(idSolicitacao) {
    valor = String(idSolicitacao);
    $.ajax({
        url: "/Estacionamento/ReprovarSolicitacao",
        type: 'post',
        data: {
            id: valor
        },
        beforeSend: function () {
            $("#--" + idSolicitacao).show();
        }
    })
    .done(function (msg) {
        $('#sucessoRemocao').modal('show');
        $('#' + idSolicitacao + '--').remove();
    })
    .fail(function (jqXHR, textStatus, msg) {
        $('#fracasso').modal('show');
        $("#--" + idSolicitacao).hide();
    }); 
}