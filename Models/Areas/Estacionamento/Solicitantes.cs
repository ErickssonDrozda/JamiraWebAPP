using FEL_JAMIRA_WEB_APP.Models.Areas.Modelagem_do_Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEL_JAMIRA_WEB_APP.Models.Areas.Estacionamento
{
    public class Solicitantes
    {
        public string Nickname { get; set; }
        public bool InsereAlerta { get; set; }
        public int IdSolicitacao { get; set; }
        public int IdCliente { get; set; }
        public string NomeCliente { get; set; }
        public string PlacaCarro { get; set; }
        public string Carro { get; set; }
        public int Status { get; set; }
        public DateTime? PeriodoDe { get; set; }
        public DateTime? PeriodoAte { get; set; }
    }
}