using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class RevisionesTecnicasGarantias
    {
        public int IngresoRevisionGarantiaId { get; set; }
        public string Cliente { get; set; }
        public string Cedula { get; set; }
        public string NumeroGarantia { get; set; }
        public int NumeroComprobante { get; set; }
        public string NumeroRevision { get; set; }
        public string Provincia { get; set; }
        public string Direccion { get; set; }
        public string Vendedor { get; set; }
        public string FacturaCliente { get; set; }
        public string TestBateria { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Lote { get; set; }
        public string LoteEnsamble { get; set; }
        public string FechaVenta { get; set; }
        public string FechaIngreso { get; set; }
        public decimal Prorrateo { get; set; }
        public int Meses { get; set; }
        public decimal PorcentajeVenta { get; set; }
        public decimal Voltaje { get; set; }
        public string ModoIngreso { get; set; }
        public string AplicaGarantia { get; set; }


    }
}