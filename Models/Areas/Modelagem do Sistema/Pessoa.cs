using FEL_JAMIRA_WEB_APP.Models.Areas.Localizacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEL_JAMIRA_WEB_APP.Models.Areas.Modelagem_do_Sistema
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Nome { get; set; }
        public DateTime? Nascimento { get; set; }
        public DateTime DataCriacao { get; set; }
        public int IdEndereco { get; set; }
        public virtual Endereco EnderecoPessoa { get; set; }
    }
}