$(document).ready(function () {
    var url = location.href;

    if (url.indexOf("Endereco") === -1) {
        if (url.indexOf("RegistrarEstacionamento") !== -1) {
            var $Cnpj = $("#CNPJ");
            $Cnpj.mask('00.000.000/0000-00', { reverse: true });
            var $Conta = $("#Conta");
            $Conta.mask('0000000-0', { reverse: true });
        }

        var $Cpf = $("#CPF");
        $Cpf.mask('000.000.000-00', { reverse: true });

        var $Rg = $("#RG");
        $Rg.mask('00.000.000-0', { reverse: true });

        var $Cep = $("#IdCep");
        $Cep.mask('00000-000', { reverse: true });
    }

    function limpa_formulário_cep() {
        // Limpa valores do formulário de cep.
        $("#Rua").val("");
        $("#Bairro").val("");
        $("#IdCep").val("");
        $("#Complemento").val("");
        $("#Cidade").val(99999);
        $("#Estado").val(99999);
    }

    //Quando o campo cep perde o foco.
    $("#IdCep").blur(function () {
        $('#aguardeCEP').show();
        //Nova variável "cep" somente com dígitos.
        var cep = $(this).val().replace(/\D/g, '');

        //Verifica se campo cep possui valor informado.
        if (cep !== "") {

            //Expressão regular para validar o CEP.
            var validacep = /^[0-9]{8}$/;

            //Valida o formato do CEP.
            if (validacep.test(cep)) {

                //Preenche os campos com "..." enquanto consulta webservice.
                $("#Rua").val("...");
                $("#Bairro").val("...");
                $("#Complemento").val("...");
                $("#IdCep").val("...");
                //$("#uf").val("...");

                //Consulta o webservice viacep.com.br/
                $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?", function (dados) {

                    if (!("erro" in dados)) {
                        //Atualiza os campos com os valores da consulta.
                        $("#Rua").val(dados.logradouro);
                        $("#Bairro").val(dados.bairro);
                        $("#Complemento").val(dados.complemento);
                        $("#IdCep").val(dados.cep);

                        jQuery("#IdCidade option").each(function () {
                            var value = jQuery(this).val();
                            if (jQuery(this)[0].innerText === dados.localidade) {
                                $('#IdCidade').val(value);
                            }
                        });

                        jQuery("#IdEstado option").each(function () {
                            var value = jQuery(this).val();
                            if (jQuery(this)[0].innerText === dados.uf) {
                                $('#IdEstado').val(value);
                            }
                        });
                        
                        $('#aguardeCEP').hide();
                    } //end if.
                    else {
                        //CEP pesquisado não foi encontrado.
                        limpa_formulário_cep();
                        $('#aguardeCEP').hide();
                        alert("CEP não encontrado.");
                    }
                });
            } //end if.
            else {
                //cep é inválido.
                limpa_formulário_cep();
                alert("Formato de CEP inválido.");
                $('#aguardeCEP').hide();
            }
        } //end if.
        else {
            //cep sem valor, limpa formulário.
            limpa_formulário_cep();
            $('#aguardeCEP').hide();
        }
    });
});