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
    
    public partial class PedidoClienteDetalle
    {
        public int PedidoClienteDetalleId { get; set; }
        public Nullable<int> NumeroPedidoId { get; set; }
        public string ItemCode { get; set; }
        public string ModeloBateria { get; set; }
        public string Marca { get; set; }
        public string CardCode { get; set; }
        public string NumeroParteCliente { get; set; }
        public string EtiquetaDatosTecnicos { get; set; }
        public string Polaridad { get; set; }
        public string TipoTerminal { get; set; }
        public Nullable<int> Cantidad { get; set; }
        public Nullable<int> CantidadConfirmada { get; set; }
        public Nullable<int> CantidadPiso { get; set; }
        public Nullable<int> PisoMaximo { get; set; }
        public Nullable<decimal> BateriasPallet { get; set; }
        public Nullable<decimal> CantidadPallets { get; set; }
        public Nullable<decimal> PrecioUnitario { get; set; }
        public Nullable<decimal> PrecioTotal { get; set; }
        public Nullable<decimal> PesoBateria { get; set; }
        public Nullable<decimal> PesoNeto { get; set; }
        public Nullable<decimal> PesoBruto { get; set; }
        public string MarcaBateria { get; set; }
    
        public virtual PedidoClienteCabecera PedidoClienteCabecera { get; set; }
    }
}
