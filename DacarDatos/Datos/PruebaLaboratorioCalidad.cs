//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DacarDatos.Datos
{
    using System;
    using System.Collections.Generic;
    
    public partial class PruebaLaboratorioCalidad
    {
        public int PruebaLaboratorioCalidadId { get; set; }
        public Nullable<System.DateTime> FechaIngreso { get; set; }
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
        public Nullable<System.DateTime> FechaRegistro { get; set; }
    }
}