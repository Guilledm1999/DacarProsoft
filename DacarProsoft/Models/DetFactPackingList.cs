using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class DetFactPackingList
    {
        public int numeroItem { set; get; }
        public string DacarPArtNumber { set; get; }
        public string CustomerPartNumber { set; get; }
        public string Description { set; get; }
        public int Quantity { set; get; }
        public decimal Price { set; get; }
        public decimal TotalPrice { set; get; }
        


    }
}