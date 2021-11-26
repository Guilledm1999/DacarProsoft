using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class IngresoGarantiasModel
    {
        public int IngresoGarantiaId { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string ModeloBateria { get; set; }
        public string NumeroGarantia { get; set; }
        public DateTime RegistroGarantia { get; set; }
        public string Provincia { get; set; }
        public string ModeloVehiculo { get; set; }
        public int NumeroRevision { get; set; }
        public int NumeroCombrobante { get; set; }
        public string NumeroFactura { get; set; }
        public decimal ValorBateria { get; set; }


    }
}