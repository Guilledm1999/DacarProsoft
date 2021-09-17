using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class ReporteGeneralDeControl
    {
        public int NumeroPedidoId { get; set; }
        public string NombreCliente { get; set; }
        public string Estado { get; set; }
        public string OrdenCompra { get; set; }
        public string FechaEmision { get; set; }
        public string FechaIngresoSap { get; set; }
        public string TiempoAtencion { get; set; }
        public string FechaRequerida { get; set; }
        public string FechaDespacho { get; set; }
        public string FechaPlazoEntrega { get; set; }
        public string FechaCargaLista { get; set; }
        public string FechaPlazoCargaLista { get; set; }



    }
}