using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class ModelPruebaLaboratorioCalidadCCA
    {
        public int PruebasLaboratorioCCAId { get; set; }
        public string FechaPrueba { get; set; }
        public Nullable<int> CodigoIngreso { get; set; }
        public string TipoBateria { get; set; }
        public string Modelo { get; set; }
        public string Separador { get; set; }
        public string LoteEnsamble { get; set; }
        public string LoteCarga { get; set; }
        public Nullable<decimal> Peso { get; set; }
        public Nullable<decimal> Voltaje { get; set; }
        public Nullable<decimal> Temperatura { get; set; }
        public string DatoTeoricoPrueba { get; set; }
        public Nullable<decimal> ResultadoFinal { get; set; }
        public string Observaciones { get; set; }
        public string FechaRegistro { get; set; }
        public string CodigoBateria { get; set; }
        public string RutaAnexo { get; set; }
        public Nullable<decimal> Rendimiento { get; set; }

    }
}