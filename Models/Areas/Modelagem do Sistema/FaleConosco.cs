using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FEL_JAMIRA_WEB_APP.Models.Areas.Modelagem_do_Sistema
{
    public class FaleConosco
    {
        public int IdPessoa { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category:")]
        public int IdCategoria { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description:")]
        public string Texto { get; set; }
        public DateTime DataCriacao { get; set; }
       
    }
}