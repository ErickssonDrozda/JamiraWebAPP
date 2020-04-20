using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEL_JAMIRA_WEB_APP.Models.Areas.Modelagem_do_Sistema
{
    public class CompraCreditos
    {
        public int IdCliente { get; set; }
        public DateTime DataTransacao { get; set; }
        public double Credito { get; set; }
    }
}