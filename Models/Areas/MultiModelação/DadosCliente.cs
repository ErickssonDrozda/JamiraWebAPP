using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FEL_JAMIRA_WEB_APP.Models.Areas.MultiModelação
{
    public class DadosCliente
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
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "O Email inserido não é válido.")]
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Street is required")]
        [Display(Name = "Street(Anything)")]
        public string Rua { get; set; }
        [Required(ErrorMessage = "Strret number is required")]
        [Display(Name = "nºStreet(Anything)")]
        public int Numero { get; set; }
        [Required(ErrorMessage = "Neighborhood")]
        [Display(Name = "Neighborhood(Anything)")]
        public string Bairro { get; set; }
        [Display(Name = "ZIP Code(CEP)")]
        public string CEP { get; set; }
        [Display(Name = "Complement")]
        public string Complemento { get; set; }
        [Display(Name = "City")]
        public int IdCidade { get; set; }
        [Display(Name = "Province")]
        public int IdEstado { get; set; }
    }
}