using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FEL_JAMIRA_WEB_APP.Models.Areas.Autenticacao
{
    public class LoginRequisicao
    {
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "O Email inserido não é válido.")]
        [Required(ErrorMessage = "Login field is required")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Password field is required")]
        public string Senha { get; set; }
    }
}