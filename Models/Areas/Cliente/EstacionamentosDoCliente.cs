using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEL_JAMIRA_WEB_APP.Models.Areas.Cliente
{
    public class EstacionamentosDoCliente
    {
        public string NomeEstacionamento { get; set; }
        public DateTime PeriodoDe { get; set; }
        public DateTime PeriodoAte { get; set; }
        public double ValorTotal { get; set; }
    }
}