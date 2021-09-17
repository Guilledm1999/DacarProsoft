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
    
    public partial class Chatarra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Chatarra()
        {
            this.ChatarraDetalle = new HashSet<ChatarraDetalle>();
            this.ChatarraDetalleIndividual = new HashSet<ChatarraDetalleIndividual>();
        }
    
        public int ChatarraId { get; set; }
        public Nullable<int> DocEntry { get; set; }
        public Nullable<int> NumeroDocumento { get; set; }
        public string Fecha { get; set; }
        public string Identificacion { get; set; }
        public string Cliente { get; set; }
        public string TipoIngreso { get; set; }
        public string Comentarios { get; set; }
        public string BodegaId { get; set; }
        public Nullable<System.DateTime> FechaRegistro { get; set; }
        public Nullable<int> CardCode { get; set; }
        public string ClienteLinea { get; set; }
        public string ClienteClase { get; set; }
        public string NumeroPedido { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChatarraDetalle> ChatarraDetalle { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChatarraDetalleIndividual> ChatarraDetalleIndividual { get; set; }
    }
}
