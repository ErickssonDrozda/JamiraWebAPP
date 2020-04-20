using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEL_JAMIRA_WEB_APP.Models.Areas.Modelagem_do_Sistema
{
    public class Cliente : Pessoa
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public double Saldo { get; set; }
        public bool TemCarro { get; set; }
    }
}