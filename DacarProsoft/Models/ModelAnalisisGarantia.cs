using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class ModelAnalisisGarantia
    {
        public int AnalisisRegistrosGarantiasId { get; set; }
        public Nullable<int> IngresoRevisionGarantiaId { get; set; }
        public string NumeroComprobante { get; set; }
        public string LoteEnsamble { get; set; }
        public string LoteCarga { get; set; }
        public string ModeloBateria { get; set; }
        public Nullable<decimal> Voltaje { get; set; }
        public Nullable<decimal> CCA { get; set; }
        public Nullable<decimal> DencidadCelda1 { get; set; }
        public Nullable<decimal> DencidadCelda2 { get; set; }
        public Nullable<decimal> DencidadCelda3 { get; set; }
        public Nullable<decimal> DencidadCelda4 { get; set; }
        public Nullable<decimal> DencidadCelda5 { get; set; }
        public Nullable<decimal> DencidadCelda6 { get; set; }
        public string ResumenAnalisis { get; set; }
        public string AreaResponsable { get; set; }
        public string Observaciones { get; set; }
        public string FechaRegistroAnalisis { get; set; }
    }
}