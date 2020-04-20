using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEL_JAMIRA_WEB_APP.Models.Areas.Cliente
{
    public class CarroRetorno
    {
        public int IdMarca { get; set; }
        public bool Alerta { get; set; }
        public string Placa { get; set; }
        public string Porte { get; set; }
        public string Modelo { get; set; }
        public int Level { get; set; }
        public string Nome{ get; set; }
    }
}