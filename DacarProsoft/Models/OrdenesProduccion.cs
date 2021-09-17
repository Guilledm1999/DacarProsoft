using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class OrdenesProduccion
    {
        public int NumInterno { get; set; }
        public string SeriesName { get; set; }
        public string ItemCode { get; set; }
        public string Comments { get; set; }
        public string Status { get; set; }
        public Nullable<decimal> PlannedQty { get; set; }
        public Nullable<decimal> CmpltQty { get; set; }
        public Nullable<decimal> RjctQty { get; set; }
        public string CreacionOrden { get; set; }
        public string TipoProduccion { get; set; }
        public Nullable<int> Linea { get; set; }
        public string Expr1 { get; set; }
        public string ItemName { get; set; }
        public Nullable<decimal> BaseQty { get; set; }
        public Nullable<decimal> Expr2 { get; set; }
        public Nullable<decimal> IssuedQty { get; set; }
        public string WhsCode { get; set; }
        public string WhsName { get; set; }
    }
}