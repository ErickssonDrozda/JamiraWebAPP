using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEL_JAMIRA_WEB_APP.Models.Areas.Estacionamento
{
    public class DadosRapidosEstacionamento
    {
        public int idEstacionamento { get; set; }
        public byte[] Foto { get; set; }
        public string Nome { get; set; }
        public int ValorHr { get; set; }
        public string Endereco { get; set; }
    }
}