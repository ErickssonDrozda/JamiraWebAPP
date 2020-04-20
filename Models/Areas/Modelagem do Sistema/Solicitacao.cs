using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEL_JAMIRA_WEB_APP.Models.Areas.Modelagem_do_Sistema
{
    public class Solicitacao
    {
        public Double ValorTotal { get; set; }
        public Double ValorTotalEstacionamento { get; set; }
        public Double ValorGanho { get; set; }
        public int Status { get; set; }
        public int IdCliente { get; set; }
        public int IdEstacionamento { get; set; }
    }
}