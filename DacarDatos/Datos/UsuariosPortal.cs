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
    
    public partial class UsuariosPortal
    {
        public int UsuarioPortalId { get; set; }
        public string NombreCliente { get; set; }
        public string UsuarioPortal { get; set; }
        public byte[] ClavePortal { get; set; }
        public string ReferenciaUsuario { get; set; }
        public string EstadoUsuario { get; set; }
        public string ImagenPerfil { get; set; }
        public Nullable<bool> Validaciones { get; set; }
    }
}
