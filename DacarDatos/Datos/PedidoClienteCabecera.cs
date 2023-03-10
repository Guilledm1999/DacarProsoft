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
    
    public partial class PedidoClienteCabecera
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PedidoClienteCabecera()
        {
            this.PedidoClienteDetalle = new HashSet<PedidoClienteDetalle>();
        }
    
        public int NumeroPedidoId { get; set; }
        public string CardCode { get; set; }
        public string NombreCliente { get; set; }
        public Nullable<System.DateTime> FechaEmision { get; set; }
        public string OrdenCompra { get; set; }
        public string Sucursal { get; set; }
        public string PaisCiudad { get; set; }
        public string Direccion { get; set; }
        public string CodigoPostal { get; set; }
        public string TerminoImportacion { get; set; }
        public Nullable<System.DateTime> FechaRequerida { get; set; }
        public Nullable<int> EstadoPedido { get; set; }
        public Nullable<System.DateTime> FechaCargaLista { get; set; }
        public Nullable<System.DateTime> FechaDespachoPuerto { get; set; }
        public Nullable<System.DateTime> FechaZarpe { get; set; }
        public Nullable<System.DateTime> FechaArribo { get; set; }
        public Nullable<System.DateTime> FechaEntrega { get; set; }
        public string NombreListaPrecio { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PedidoClienteDetalle> PedidoClienteDetalle { get; set; }
    }
}
