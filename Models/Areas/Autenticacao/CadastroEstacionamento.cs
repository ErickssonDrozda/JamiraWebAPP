using FEL_JAMIRA_WEB_APP.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FEL_JAMIRA_WEB_APP.Models.Areas.Autenticacao
{
    public class CadastroEstacionamento
    {
        [Required(ErrorMessage = "Owner name is required.")]
        [Display(Name = "Owner")]
        public string NomeProprietario { get; set; }
        [Required(ErrorMessage = "Park name is required.")]
        [Display(Name = "Park name")]
        public string NomeEstacionamento { get; set; }
        [Required(ErrorMessage = "Value/Hr is required.")]
        [Display(Name = "Value/Hr")]
        public int Value { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Birthday is required.")]
        public DateTime? Nascimento { get; set; }
        [Display(Name = "Individual Registration (Anything)(CPF)")]
        public string CPF { get; set; }
        [Display(Name = "General Register (Anything)(RG)")]
        public string RG { get; set; }
        [Required(ErrorMessage = "Nickname is required.")]
        [Display(Name = "Nickname:")]
        public string Nickname { get; set; }
        [Display(Name = "Business Id (Anything)")]
        public string CNPJ { get; set; }
        [Display(Name = "State Registration (Anything)")]
        public string InscricaoEstadual { get; set; }
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "This email isn't valid.")]
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        public string Senha { get; set; }
        [Display(Name = "Confirm password")]
        [Compare("Senha", ErrorMessage = "The password and password confirmation are different.")]
        public string ConfirmaSenha { get; set; }
        [Required(ErrorMessage = "Street is required")]
        [Display(Name = "Street (Anything)")]
        public string Rua { get; set; }
        [Required(ErrorMessage = "Street number is required")]
        [Display(Name = "Street number (Anything)")]
        public int Numero { get; set; }
        [Required(ErrorMessage = "Neighborhood is required")]
        [Display(Name = "Neighborhood (Anything)")]
        public string Bairro { get; set; }
        [Display(Name = "Zip Code(CEP)")]
        public string CEP { get; set; }
        [Display(Name = "Complement")]
        public string Complemento { get; set; }
        [Display(Name = "Resident City")]
        public int IdCidade { get; set; }
        [Display(Name = "Province")]
        public int IdEstado { get; set; }
        [Required(ErrorMessage = "Agency is required.")]
        [Display(Name = "Agency")]
        public string Agencia { get; set; }
        [Required(ErrorMessage = "Account is required")]
        [Display(Name = "Account")]
        public string Conta { get; set; }
        [Required(ErrorMessage = "Bank is required")]
        [Display(Name = "Bank")]
        public int IdBanco { get; set; }
        [Required(ErrorMessage = "Account type is required")]
        [Display(Name = "Account type")]
        public int IdTipoConta { get; set; }
        [UploadFoto(ErrorMessage = "Error. Submit a valid photo.")]
        [Required(ErrorMessage = "Please select the user's photo.")]
        [Display(Name = "Send your photo")]
        public HttpPostedFileBase Arquivo { get; set; }
        public byte[] Foto{ get; set; }
    }
}