$(document).ready(function () {
    $('#dataTable1').DataTable();

    $("#valProceed").click(function (event) {
        event.preventDefault();
        $('#aguarde').show();
        $("#setRetorno").empty();

        var idEstabelecimentoN = $("#valProceed").val();

        $.post("RequisitarEstacionamento", { idEstabelecimento: idEstabelecimentoN }, function (response) {
            if (response && response.Sucesso) {
                if (response.Sucesso === true) {
                    $("#setRetorno").html("</br><b><p style='color: green;'>" + response.Mensagem + "</p></b>");
                } else {
                    $("#setRetorno").html("</br><b><p style='color: red;'>" + response.Mensagem + "</p></b>");
                }
            }
            else {
                $("#setRetorno").html("</br><b><p style='color: red;'>" + response.Mensagem + "</p></b>");
            }
            $('#aguarde').hide();
        });
    });
});

function ConfirmarSequencia(idEstabelecimento, Nome)
{
    $('#setBody')['0'].innerText = "Are you sure you want to go to '" + Nome +"'?";
    $('#modalConfirm').modal('show');
    $('#valProceed')['0'].value = idEstabelecimento;
}
function RequisitarFim(solicitacao)
{
    event.preventDefault();
    $('#aguarde').show();

    $.post("FinalizarEstacionamento", { idSolicitacao: solicitacao }, function (response) {
        if (response && response.Sucesso) {
            if (response.Sucesso === true)
            {
                $('#sucesso').modal('show');
            } else
            {
                $('#fracasso').modal('show');
            }
            $('#aguarde').hide();
        }
        else {
            $('#fracasso').modal('show');
            $('#aguarde').hide();
        }
    });
}