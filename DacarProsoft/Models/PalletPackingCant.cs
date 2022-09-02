using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class PalletPackingCant
    {
        public int PalletPacking1 { get; set; }
        public int PackingId { get; set; }
        public int PalletNumber { get; set; }
        public decimal LargoPallet { get; set; }
        public decimal AnchoPallet { get; set; }
        public decimal AltoPallet { get; set; }
        public decimal VolumenPallet { get; set; }
        public decimal PesoNeto { get; set; }
        public decimal PesoBruto { get; set; }
        public int Cantidad { get; set; }
        public int CantidadMediciones { get; set; }



    }
}