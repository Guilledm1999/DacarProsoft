using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class DetalleGeneralDePackingListcs
    {
        public int DetalleGeneralPackingListId { get; set; }
        public int PackingId { get; set; }
        public string ClientePackingList { get; set; }
        public string ContenedorPackingList { get; set; }
        public DateTime FechaPackingList { get; set; }
        public String FechaDePackingList { get; set; }

        public string ReservaPackingList { get; set; }
        public string FacturaPedido { get; set; }
        public string PedidoPackingList { get; set; }
        public string EmbarcacionPackingList { get; set; }
        public string IntercambioEirPackingList { get; set; }
        public string ReferenciasPackingList { get; set; }
        public string ProductosPackingList { get; set; }
    }
}