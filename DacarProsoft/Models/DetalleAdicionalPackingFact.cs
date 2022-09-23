using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class DetalleAdicionalPackingFact
    {
        public decimal PorcentajeDescuento { set; get; }
        public decimal Descuento { set; get; }
        public int CodigoCostosAdicionales { set; get; }
        public decimal ValorCostoAdicional { set; get; }
        public string direccionFact { set; get; }
        public string payToCode { set; get; }
        public string shipTo { set; get; }
        public string addressShipTo { set; get; }
    }
}