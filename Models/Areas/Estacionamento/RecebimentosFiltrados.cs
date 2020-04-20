using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEL_JAMIRA_WEB_APP.Models.Areas.Estacionamento
{
    public class RecebimentosFiltrados
    {
        public int MesAno { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public double Valor { get; set; }
    }
}