using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class CronogramaExp
    {
        public int CronogramaExportacionId { get; set; }
        public string Orden { get; set; }
        public string Cliente { get; set; }
        public string FechaPedido { get; set; }
        public string FechaDespacho { get; set; }
        public Nullable<int> Booking { get; set; }
        public Nullable<int> TotalContenedores { get; set; }
        public string CardCode { get; set; }
        public string Destino { get; set; }
        public string FechaZarpe { get; set; }
    }
}