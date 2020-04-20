using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FEL_JAMIRA_WEB_APP.Models.Areas.Modelagem_do_Sistema
{
    public class Carro
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public string Porte { get; set; }
        public string Modelo { get; set; }
        public int IdMarca { get; set; }
        public int IdCliente { get; set; }
        public Marca Marca { get; set; }
        public Cliente Cliente { get; set; }
    }
}