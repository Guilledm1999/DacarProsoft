using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class PesoPrecioArticulo
    {
        public string CodigoItem { set; get; }
        public string ItemName { set; get; }

        public decimal UltimoPrecioCompra { set; get; }
        public decimal PesoArticulo { set; get; }

    }
}