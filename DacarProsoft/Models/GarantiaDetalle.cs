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
        public int NumeroRevision { get; set; }
        public string Celular { get; set; }

        public string Provincia { get; set; }
        public string MarcaVehiculo { get; set; }
        public string ModeloVehiculo { get; set; }
        public int AnioFabricacion { get; set; }
        public decimal Kilometraje    { get; set; }

        public string NumeroFactura { get; set; }
        public string Mes { get; set; }
        public string Anio { get; set; }

    }

    public class PieChart
    {
        public string Linea { get; set; }
        public int Total { get; set; }

        
    }
    public class Chart
    {
        public string Descripion { get; set; }
        public int Total { get; set; }
    }

    public class Provincias
    {
        public int gu { get; set; }
        public int es { get; set; }
        public int cr { get; set; }
        public int im { get; set; }
        public int su { get; set; }
        public int se { get; set; }
        public int sd { get; set; }
        public int az { get; set; }
        public int eo { get; set; }
        public int lj { get; set; }
        public int zc { get; set; }
        public int cn { get; set; }
        public int bo { get; set; }
        public int ct { get; set; }
        public int lr { get; set; }
        public int mn { get; set; }
        public int cb { get; set; }
        public int ms { get; set; }
        public int pi { get; set; }
        public int pa { get; set; }
        public int numero { get; set; }
        public int na { get; set; }
        public int tu { get; set; }
        public int ga { get; set; }
     
    }
}