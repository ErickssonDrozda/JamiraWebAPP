using FEL_JAMIRA_WEB_APP.Models.Areas.Modelagem_do_Sistema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FEL_JAMIRA_WEB_APP.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public int Level { get; set; }
        public int IdPessoa { get; set; }
        public Pessoa Pessoa { get; set; }
        public string AuxSenha { get; set; }
        public Byte[] Foto { get; set; }

    }
}