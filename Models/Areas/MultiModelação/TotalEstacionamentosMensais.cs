using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEL_JAMIRA_WEB_APP.Models.Areas.MultiModelação
{
    public class TotalEstacionamentosMensais
    {
        public int IdEstacionamento { get; set; }
        public string NomeEstacionamento { get; set; }
        public double ValorAPagar { get; set; }
        public string ContaParaPagar { get; set; }
        public string AgenciaParaPagar { get; set; }
        public string Banco { get; set; }
        public string TipoConta { get; set; }
    }
}