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
    
    public partial class Contrasena
    {
        public int IdContraseña { get; set; }
        public int IdUsuario { get; set; }
        public byte[] Contrasena1 { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string Estado { get; set; }
    
        public virtual Contrasena Contrasena11 { get; set; }
        public virtual Contrasena Contrasena2 { get; set; }
    }
}