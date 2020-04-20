using FEL_JAMIRA_WEB_APP.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace FEL_JAMIRA_WEB_APP.Models.Areas.Autenticacao
{
    public class CadastroCliente
    {
        [Required(ErrorMessage = "Fullname is required")]
        [Display(Name = "Fullname")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Nickname is required")]
        [Display(Name = "Nickname")]
        public string Nickname { get; set; }
        [Display(Name = "Individual Registration(Anything)")]
        public string CPF { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Birthday")]
        public DateTime? Nascimento { get; set; }
        [Display(Name = "General Register(Anything)")]
        public string RG { get; set; }
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "This email isn't valid.")]
        [Required(ErrorMessage = "Email is required.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [Display(Name = "Password")]
        public string Senha { get; set; }
        [Display(Name = "Confirm password")]
        [Compare("Senha", ErrorMessage = "The password and password confirmation are different.")]
        public string ConfirmaSenha { get; set; }
        [Required(ErrorMessage = "Street is required")]
        [Display(Name = "Street(Anything)")]
        public string Rua { get; set; }
        [Required(ErrorMessage = "Street number is required")]
        [Display(Name = "Street number(Anything)")]
        public int Numero { get; set; }
        [Required(ErrorMessage = "Neighborhood is required")]
        [Display(Name = "Neighborhood (Anything)")]
        public string Bairro { get; set; }
        [Display(Name = "ZIP Code(CEP)")]
        public string CEP { get; set; }
        [Display(Name = "Complement")]
        public string Complemento { get; set; }
        [Display(Name = "Resident City")]
        public int IdCidade { get; set; }
        [Display(Name = "Province")]
        public int IdEstado { get; set; }

        [UploadFoto(ErrorMessage = "Error. Submit a valid photo.")]
        [Required(ErrorMessage = "Please select the user's photo.")]
        [Display(Name = "Send your photo")]
        public HttpPostedFileBase Arquivo { get; set; }
        public byte[] Foto { get; set; }
    }
}