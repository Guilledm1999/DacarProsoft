using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class CabeceraOrdenVenta
    {
        public int DocEntry { get; set; }
        public int DocNum { get; set; }
        public string DocDate { get; set; }
        public string TaxDate { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string NumeroOrden { get; set; }
        public string Mes { get; set; }
        public string SypExportacion { get; set; }
        public decimal DocTotal { get; set; }

    }
}