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
    
    public partial class IngresoRevisionGarantiaCabecera
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IngresoRevisionGarantiaCabecera()
        {
            this.IngresoRevisionGarantiaInspeccionInicial = new HashSet<IngresoRevisionGarantiaInspeccionInicial>();
            this.IngresoRevisionGarantiaDiagnostico = new HashSet<IngresoRevisionGarantiaDiagnostico>();
            this.IngresoRevisionGarantiaTrabajoRealizado = new HashSet<IngresoRevisionGarantiaTrabajoRealizado>();
        }
    
        public int IngresoRevisionGarantiaId { get; set; }
        public string Cliente { get; set; }
        public string Cedula { get; set; }
        public string NumeroGarantia { get; set; }
        public Nullable<int> NumeroComprobante { get; set; }
        public Nullable<int> NumeroRevision { get; set; }
        public string Provincia { get; set; }
        public string Direccion { get; set; }
        public string Vendedor { get; set; }
        public string FacturaCliente { get; set; }
        public string TestBateria { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Lote { get; set; }
        public Nullable<System.DateTime> FechaVenta { get; set; }
        public Nullable<System.DateTime> FechaIngreso { get; set; }
        public Nullable<decimal> Prorrateo { get; set; }
        public Nullable<int> Meses { get; set; }
        public Nullable<decimal> PorcentajeVenta { get; set; }
        public Nullable<decimal> Voltaje { get; set; }
        public string ModoIngreso { get; set; }
        public string AplicaGarantia { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IngresoRevisionGarantiaInspeccionInicial> IngresoRevisionGarantiaInspeccionInicial { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IngresoRevisionGarantiaDiagnostico> IngresoRevisionGarantiaDiagnostico { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IngresoRevisionGarantiaTrabajoRealizado> IngresoRevisionGarantiaTrabajoRealizado { get; set; }
    }
}
