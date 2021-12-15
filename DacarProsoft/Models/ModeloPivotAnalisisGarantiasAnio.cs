using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class ModeloPivotAnalisisGarantiasAnio
    {
        public int AnalisisRegistrosGarantiasId { get; set; }
        public string AreaResponsable { get; set; }
        public int Cantidad { get; set; }
        public string ResumenAnalisis { get; set; }
        public string GrupoBateria { get; set; }
        public string ModeloBateria { get; set; }
        public string FechaRegistro { get; set; }



    }
}