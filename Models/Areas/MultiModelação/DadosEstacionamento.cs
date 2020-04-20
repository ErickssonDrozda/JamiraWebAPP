using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FEL_JAMIRA_WEB_APP.Models.Areas.MultiModelação
{
    public class DadosEstacionamento
    {
        [Required(ErrorMessage = "Agency is required.")]
        [Display(Name = "Agency:")]
        public string Agencia { get; set; }
        [Required(ErrorMessage = "Account is required")]
        [Display(Name = "Account:")]
        public string Conta { get; set; }
        [Required(ErrorMessage = "Bank is required")]
        [Display(Name = "Bank:")]
        public int IdBanco { get; set; }
        [Required(ErrorMessage = "Account type is required")]
        [Display(Name = "Account type:")]
        public int IdTipoConta { get; set; }

        [Required(ErrorMessage = "Owner name is required")]
        [Display(Name = "Owner name:")]
        public string NomeProprietario { get; set; }
        [Required(ErrorMessage = "Park name is required")]
        [Display(Name = "Park name:")]
        public string NomeEstacionamento { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Birthday:")]
        public DateTime? Nascimento { get; set; }
        [Display(Name = "Individual Registration(CPF):")]
        public string CPF { get; set; }
        [Display(Name = "General Register(RG):")]
        public string RG { get; set; }
        [Required(ErrorMessage = "Value per hour is required")]
        [Range(1, int.MaxValue, ErrorMessage ="Value per hour must be 1 or more")]
        [Display(Name = "Value per Hour:")]
        public int ValorHora { get; set; }
        [Required(ErrorMessage = "Nickname is required")]
        [Display(Name = "Nickname:")]
        public string Nickname { get; set; }
        [Display(Name = "Business Id (CNPJ):")]
        public string CNPJ { get; set; }
        [Display(Name = "State Registration:")]
        public string InscricaoEstadual { get; set; }
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "O Email inserido não é válido.")]
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email:")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Street is required")]
        [Display(Name = "Street:")]
        public string Rua { get; set; }
        [Required(ErrorMessage = "Street number is required")]
        [Display(Name = "nºStreet:")]
        public int Numero { get; set; }
        [Required(ErrorMessage = "Neighborhood is required")]
        [Display(Name = "Neighborhood:")]
        public string Bairro { get; set; }
        [Display(Name = "Zip Code(CEP):")]
        public string CEP { get; set; }
        [Display(Name = "Complement:")]
        public string Complemento { get; set; }
        [Display(Name = "City:")]
        public int IdCidade { get; set; }
        [Display(Name = "Province:")]
        public int IdEstado { get; set; }

        //Endereço do Estabelecimento

        [Display(Name = "Street:")]
        public string RuaEstacionamento { get; set; }
        [Display(Name = "nºStreet:")]
        public int? NumeroEstacionamento { get; set; }
        [Display(Name = "Neighborhood:")]
        public string BairroEstacionamento { get; set; }
        [Display(Name = "Zip Code(CEP):")]
        public string CEPEstacionamento { get; set; }
        [Display(Name = "Complement:")]
        public string ComplementoEstacionamento { get; set; }
        [Display(Name = "City:")]
        public int? IdCidadeEstacionamento { get; set; }
        [Display(Name = "Province:")]
        public int? IdEstadoEstacionamento { get; set; }

    }
}