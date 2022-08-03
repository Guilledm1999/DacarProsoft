using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class DetalleOrdenVenta
    {
        public int DocEntry { get; set; }
        public string WhsCode { get; set; }
        public int Cantidad { get; set; }
        public string ItemCode { get; set; }
        public string Descripcion { get; set; }
        public string NombreForaneo { get; set; }
        public string NombreGenerico { get; set; }
        public string Text { get; set; }
        public decimal Precio { get; set; }
        public decimal PrecioTotal { get; set; }
    }
}