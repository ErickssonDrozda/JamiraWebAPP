using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FEL_JAMIRA_WEB_APP.Models.Areas.Cliente
{
    public class CarroCliente
    {
        [Required(ErrorMessage = "Plate car is required")]
        [Display(Name = "Plate")]
        public string Placa { get; set; }
        [Required(ErrorMessage = "Car size is required")]
        [Display(Name = "Size")]
        public string Porte { get; set; }
        [Required(ErrorMessage = "Car model is required")]
        [Display(Name = "Model")]
        public string Modelo { get; set; }
        [Display(Name = "Brand")]
        [Required(ErrorMessage = "Brand car is required")]
        public int IdMarca { get; set; }
        public int IdCliente { get; set; }
    }
}