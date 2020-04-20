using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEL_JAMIRA_WEB_APP.Models.Areas.Estacionamento
{
    public class DadosSolicitantes
    {
        public FEL_JAMIRA_WEB_APP.Models.Areas.Modelagem_do_Sistema.Estacionamento Estacionamento { get; set; }
        public List<Solicitantes> Solicitantes{ get; set; }
    }
}