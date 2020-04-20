using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEL_JAMIRA_WEB_APP.Models.Areas.Util
{
    public class ResponseViewModelGeneric
    {
        public object Data { get; set; }
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public bool Serializado { get; set; } = true;
    }
}