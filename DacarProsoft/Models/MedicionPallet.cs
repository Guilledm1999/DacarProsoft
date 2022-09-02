using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class MedicionPallet
    {
        public int MedicionPalletPackingId { get; set; }
        public int PackingId { get; set; }
        public int PalletId { get; set; }
        public int NumeroMedicion { get; set; }
        public string NumeroLote { get; set; }
        public string Modelo { get; set; }
        public decimal Voltaje { get; set; }
        public bool nivel { get; set; }
        public bool Acabado { get; set; }
        public bool Limpieza { get; set; }
        public decimal CCA { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}