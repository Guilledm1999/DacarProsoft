using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class PedidosRegistradosSap
    {
        public int NumeroPedidoId { get; set; }
        public string CardCode { get; set; }
        public string NombreCliente { get; set; }
        public string FechaIngresadaSap { get; set; }
        public string FechaNuevaDespacho { get; set; }
        public string OrdenCompra { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public string Direccion { get; set; }
        public string TerminoImportacion { get; set; }
        public int Estado { get; set; }
        public string FechaEmision { get; set; }

        public string FechaCargaLista { get; set; }
        public string FechaDespachoPuerto { get; set; }
        public string FechaZarpe { get; set; }
        public string FechaArribo { get; set; }
        public string FechaEntrega { get; set; }


    }
}