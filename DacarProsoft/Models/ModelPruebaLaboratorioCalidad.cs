using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class ModelPruebaLaboratorioCalidad
    {
        public int PruebaLaboratorioCalidadId { get; set; }
        public string FechaIngreso { get; set; }
        public Nullable<int> CodigoIngreso { get; set; }
        public string Marca { get; set; }
        public string TipoNorma { get; set; }
        public string Normativa { get; set; }
        public string PreAcondicionamiento { get; set; }
        public string TipoBateria { get; set; }
        public string Modelo { get; set; }
        public string Separador { get; set; }
        public string TipoEnsayo { get; set; }
        public string LoteEnsamble { get; set; }
        public string LoteCarga { get; set; }
        public Nullable<int> CCA { get; set; }
        public Nullable<decimal> Peso { get; set; }
        public Nullable<decimal> Voltaje { get; set; }
        public Nullable<decimal> DensidadIngreso { get; set; }
        public Nullable<decimal> DensidadPreAcondicionamiento { get; set; }
        public Nullable<decimal> TemperaturaIngreso { get; set; }
        public Nullable<decimal> TemperaturaPrueba { get; set; }
        public string DatoTeoricoPrueba { get; set; }
        public Nullable<decimal> ValorObjetivo { get; set; }
        public Nullable<decimal> ResultadoFinal { get; set; }
        public string Observaciones { get; set; }
        public Nullable<decimal> Calificacion { get; set; }
        public string FechaRegistro { get; set; }
        public int ContadorRegistros { get; set; }
        public string CodigoBateria { get; set; }


    }
}