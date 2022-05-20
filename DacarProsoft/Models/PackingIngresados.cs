using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class PackingIngresados
    {
        public int PackingId { set; get; }
        public int NumeroDocumento { set; get; }
        public string NumeroOrden { set; get; }
        public string NombreCliente { set; get; }
        public string DetalleIngresado { set; get; }
        public string Origen { set; get; }
        public string Destino { set; get; }
        public int CantidadPallet { set; get; }
        public int PalletFaltantes { set; get; }
        public string Estado { set; get; }
        public string EstadoPackingList { set; get; }
        public string FechaRegistro { set; get; }
        public string Mes { set; get; }
        public int NumeroContenedor { set; get; }

    }
}