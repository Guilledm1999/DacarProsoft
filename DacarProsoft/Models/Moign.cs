using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class Moign
    {
        public int DocEntry { set; get; }
        public string DocNum { set; get; }
        public string DocDate { set; get; }
        public string CedulaCliente { set; get; }
        public string NombreCliente { set; get; }
        public string NumeroPedido { set; get; }
        public string GrupoName { set; get; }
        public int CardCode { set; get; }
        public string ClienteLinea { set; get; }
        public string ClienteClase { set; get; }
        public decimal KilosReales { set; get; }
        public string Comments { set; get; }
        public string TipoIngreso { set; get; }
        public int Bodega { set; get; }



    }
}