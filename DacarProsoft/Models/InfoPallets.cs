using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class InfoPallets
    {
        public string NumeroOrden { get; set; }
        public string NombreCliente { get; set; }
        public string Destino { get; set; }
        public string Origen { get; set; }
        public int CantidadPallet { get; set; }

        public int PalletNumber { get; set; }
        public decimal PesoBruto { get; set; }
        public decimal PesoNeto { get; set; }
        public string CodigoQr { get; set; }

    }
}