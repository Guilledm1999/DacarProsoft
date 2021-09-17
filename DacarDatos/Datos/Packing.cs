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
    
    public partial class Packing
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Packing()
        {
            this.PackingDtl = new HashSet<PackingDtl>();
            this.PalletPacking = new HashSet<PalletPacking>();
        }
    
        public int PackingId { get; set; }
        public Nullable<int> NumeroDocumento { get; set; }
        public string NumeroOrden { get; set; }
        public string NombreCliente { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public Nullable<int> CantidadPallet { get; set; }
        public string Tipo { get; set; }
        public string DetalleIngresado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PackingDtl> PackingDtl { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PalletPacking> PalletPacking { get; set; }
    }
}
