using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class GarantiaDetalle
    {
        public int IngresoGarantiaId { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Distribuidor { get; set; }
        public string Ciudad { get; set; }
        public string ModeloBateria { get; set; }
        public string NumeroBateria { get; set; }
        public string NumeroGarantia { get; set; }
        public string RegistroGarantia { get; set; }
    }
}