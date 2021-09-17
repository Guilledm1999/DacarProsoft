using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class ReporteGeneralPedidosExterior
    {
        public int NumeroPedidoId { get; set; }
        public string CardCode { get; set; }
        public string NombreCliente { get; set; }
        public string FechaEmision { get; set; }
        public string FechaRequerida { get; set; }
        public string OrdenCompra { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public string Direccion { get; set; }
        public string TerminoImportacion { get; set; }
        public string Estado { get; set; }
        public string FechaCargaLista { get; set; }
        public string FechaDespachoPuerto { get; set; }
        public string FechaZarpe { get; set; }
        public string FechaArribo { get; set; }
        public string FechaEntrega { get; set; }
        public int Cantidad { get; set; }
        public int CantidadNueva { get; set; }
        public decimal PrecioTotal { get; set; }
        public decimal PesoNetoTotal { get; set; }
        public string Observaciones { get; set; }
        public string FechaIngresadaSap { get; set; }
        public string FechaNuevoDespacho { get; set; }





    }
}