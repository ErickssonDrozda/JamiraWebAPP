using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FEL_JAMIRA_WEB_APP.Models.Areas.Estacionamento
{
    public class EnderecoEstacionamento
    {
        [Required(ErrorMessage = "Street is required")]
        [Display(Name = "Street:")]
        public string Rua { get; set; }
        [Required(ErrorMessage = "Street number is required")]
        [Display(Name = "nºStreet:")]
        public int Numero { get; set; }
        [Required(ErrorMessage = "Neighborhood is required")]
        [Display(Name = "Neighborhood: ")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "Zip code is required")]
        [Display(Name = "Zip Code(CEP):")]
        public string CEP { get; set; }
        [Display(Name = "Complement:")]
        public string Complemento { get; set; }
        [Required(ErrorMessage = "City is required")]
        [Display(Name = "City:")]
        public int IdCidade { get; set; }
        [Required(ErrorMessage = "State is required")]
        [Display(Name = "State:")]
        public int IdEstado { get; set; }
        public int IdPessoa { get; set; }
    }
}