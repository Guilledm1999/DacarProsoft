using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class DetallePallet
    {
        public int PackingDtlId { get; set; }
        public string ItemCode { get; set; }
        public string DescriptionItem { get; set; }
        public int CantidadItem { get; set; }
        public int Pallet { get; set; }
        public int TotalItem { get; set; }
        public int SaldoItem { get; set; }
        public int TotalItem2 { get; set; }
        public int SaldoItem2 { get; set; }
        public string Status { get; set; }


    }
}