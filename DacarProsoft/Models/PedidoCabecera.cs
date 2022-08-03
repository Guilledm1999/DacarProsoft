using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class PedidoCabecera
    {
        public int DocEntry { get; set; }
        public int NumeroPedidoId   { get; set; }
        public string CardCode { get; set; }
        public string NombreCliente { get; set; }
        public string FechaEmision { get; set; }
        public string FechaRequerida { get; set; }
        public string OrdenCompra { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public string Direccion { get; set; }
        public string TerminoImportacion { get; set; }
        public int Estado { get; set; }
        public string Sucursal { get; set; }
        public string EstadoPed { get; set; }


    }
}