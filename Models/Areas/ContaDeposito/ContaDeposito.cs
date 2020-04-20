using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEL_JAMIRA_WEB_APP.Models.Areas.ContaDeposito
{
    public class ContaDeposito
    {
        public int Id { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public int IdEstacionamento { get; set; }
        public int IdBanco { get; set; }
        public int IdTipoConta { get; set; }
    }
}