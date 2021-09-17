using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class ItemsPackingList
    {
        public int PalletPackinId { set; get; }
        public int PalletPackingDetalleId { set; get;}
        public int NumeroPallet { set; get; }
        public string ItemCode { set; get; }
        public string Descripcion { set; get; }
        public int Cantidad { set; get; }
        public decimal Largo { set; get; }
        public decimal Alto { set; get; }
        public decimal Ancho { set; get; }
        public decimal Volumen { set; get; }
        public decimal PesoBruto { set; get; }
        public decimal PesoNeto { set; get; }
    }
}