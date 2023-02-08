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
    public class ListadoPacking
    {
        public string CUSTOMER { get; set; }
        public string DATE { get; set; }
        public string CONTAINER { get; set; }
        public string BOOKING { get; set; }
        public string INTERCHANGE { get; set; }
        public string INVOICE { get; set; }
        public string PO { get; set; }
        public string VESSEL { get; set; }
        public string PRODUCT { get; set; }
        public string CustomerReference { get; set; }
        public string DacarPartNumber { get; set; }
        public string Description { get; set; }
        public string Qty { get; set; }
        public string L { get; set; }
        public string W { get; set; }
        public string H { get; set; }
        public string Volume { get; set; }
       
        public string Gross { get; set; }
        public string Net { get; set; }



    }
}