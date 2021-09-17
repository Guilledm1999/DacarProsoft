﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DacarProsoftEntities : DbContext
    {
        public DacarProsoftEntities()
            : base("name=DacarProsoftEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<MenuOpciones> MenuOpciones { get; set; }
        public virtual DbSet<Menus> Menus { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<Meses> Meses { get; set; }
        public virtual DbSet<EstadosOrdenProduccion> EstadosOrdenProduccion { get; set; }
        public virtual DbSet<Bodega> Bodega { get; set; }
        public virtual DbSet<TipoDocumento> TipoDocumento { get; set; }
        public virtual DbSet<TipoIngreso> TipoIngreso { get; set; }
        public virtual DbSet<Modelos> Modelos { get; set; }
        public virtual DbSet<Vehiculos> Vehiculos { get; set; }
        public virtual DbSet<Vendedores> Vendedores { get; set; }
        public virtual DbSet<TipoCliente> TipoCliente { get; set; }
        public virtual DbSet<Marcas> Marcas { get; set; }
        public virtual DbSet<IngresoMercanciasTipoDescripcion> IngresoMercanciasTipoDescripcion { get; set; }
        public virtual DbSet<DiasBusqueda> DiasBusqueda { get; set; }
        public virtual DbSet<Chatarra> Chatarra { get; set; }
        public virtual DbSet<PackingListDetalle> PackingListDetalle { get; set; }
        public virtual DbSet<Accesos> Accesos { get; set; }
        public virtual DbSet<Paises> Paises { get; set; }
        public virtual DbSet<PaisOrigen> PaisOrigen { get; set; }
        public virtual DbSet<PackingListCabecera> PackingListCabecera { get; set; }
        public virtual DbSet<ChatarraPesos> ChatarraPesos { get; set; }
        public virtual DbSet<ChatarraDetalle> ChatarraDetalle { get; set; }
        public virtual DbSet<ChatarraDetalleIndividual> ChatarraDetalleIndividual { get; set; }
        public virtual DbSet<ControlCambios> ControlCambios { get; set; }
        public virtual DbSet<GrupoClientes> GrupoClientes { get; set; }
        public virtual DbSet<Packing> Packing { get; set; }
        public virtual DbSet<PackingDtl> PackingDtl { get; set; }
        public virtual DbSet<PalletPacking> PalletPacking { get; set; }
        public virtual DbSet<PalletPackingDetalle> PalletPackingDetalle { get; set; }
        public virtual DbSet<DetalleGeneralPackingList> DetalleGeneralPackingList { get; set; }
        public virtual DbSet<SubMenuOpciones> SubMenuOpciones { get; set; }
        public virtual DbSet<UsuariosPortal> UsuariosPortal { get; set; }
        public virtual DbSet<PedidoClienteCabecera> PedidoClienteCabecera { get; set; }
        public virtual DbSet<PedidoClienteDetalleFinal> PedidoClienteDetalleFinal { get; set; }
        public virtual DbSet<PedidoClienteDetalle> PedidoClienteDetalle { get; set; }
        public virtual DbSet<EstadosPedidos> EstadosPedidos { get; set; }
        public virtual DbSet<FechasPedidos> FechasPedidos { get; set; }
        public virtual DbSet<EstadosPedidosSelect> EstadosPedidosSelect { get; set; }
        public virtual DbSet<UrlImprimirPdfPallet> UrlImprimirPdfPallet { get; set; }
    
        public virtual int RegistrarNuevoUsuario(string nombres, string username, string llave, Nullable<int> tipo)
        {
            var nombresParameter = nombres != null ?
                new ObjectParameter("nombres", nombres) :
                new ObjectParameter("nombres", typeof(string));
    
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var llaveParameter = llave != null ?
                new ObjectParameter("llave", llave) :
                new ObjectParameter("llave", typeof(string));
    
            var tipoParameter = tipo.HasValue ?
                new ObjectParameter("tipo", tipo) :
                new ObjectParameter("tipo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("RegistrarNuevoUsuario", nombresParameter, usernameParameter, llaveParameter, tipoParameter);
        }
    
        public virtual ObjectResult<spConsultaUsuarios> spConsultaUsuarios()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spConsultaUsuarios>("spConsultaUsuarios");
        }
    
        public virtual ObjectResult<LoginUsuario_Result> LoginUsuario(string username, string llave)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var llaveParameter = llave != null ?
                new ObjectParameter("llave", llave) :
                new ObjectParameter("llave", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<LoginUsuario_Result>("LoginUsuario", usernameParameter, llaveParameter);
        }
    
        public virtual int spActualizarUsuario(Nullable<int> idUsuario, string usuario, string nombre, string clave, Nullable<int> tipoUsuario)
        {
            var idUsuarioParameter = idUsuario.HasValue ?
                new ObjectParameter("IdUsuario", idUsuario) :
                new ObjectParameter("IdUsuario", typeof(int));
    
            var usuarioParameter = usuario != null ?
                new ObjectParameter("usuario", usuario) :
                new ObjectParameter("usuario", typeof(string));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("nombre", nombre) :
                new ObjectParameter("nombre", typeof(string));
    
            var claveParameter = clave != null ?
                new ObjectParameter("clave", clave) :
                new ObjectParameter("clave", typeof(string));
    
            var tipoUsuarioParameter = tipoUsuario.HasValue ?
                new ObjectParameter("tipoUsuario", tipoUsuario) :
                new ObjectParameter("tipoUsuario", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spActualizarUsuario", idUsuarioParameter, usuarioParameter, nombreParameter, claveParameter, tipoUsuarioParameter);
        }
    
        public virtual ObjectResult<spLoginUsuarioPortal_Result> spLoginUsuarioPortal(string username, string clave)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var claveParameter = clave != null ?
                new ObjectParameter("clave", clave) :
                new ObjectParameter("clave", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spLoginUsuarioPortal_Result>("spLoginUsuarioPortal", usernameParameter, claveParameter);
        }
    
        public virtual int spRegistrarNuevoUsuarioPortal(string nombre, string usuario, string clave, string referencia, string estado)
        {
            var nombreParameter = nombre != null ?
                new ObjectParameter("nombre", nombre) :
                new ObjectParameter("nombre", typeof(string));
    
            var usuarioParameter = usuario != null ?
                new ObjectParameter("usuario", usuario) :
                new ObjectParameter("usuario", typeof(string));
    
            var claveParameter = clave != null ?
                new ObjectParameter("clave", clave) :
                new ObjectParameter("clave", typeof(string));
    
            var referenciaParameter = referencia != null ?
                new ObjectParameter("referencia", referencia) :
                new ObjectParameter("referencia", typeof(string));
    
            var estadoParameter = estado != null ?
                new ObjectParameter("estado", estado) :
                new ObjectParameter("estado", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spRegistrarNuevoUsuarioPortal", nombreParameter, usuarioParameter, claveParameter, referenciaParameter, estadoParameter);
        }
    
        public virtual int spActualizarUsuarioPortal(Nullable<int> idUsuario, string usuario, string clave, string tipo)
        {
            var idUsuarioParameter = idUsuario.HasValue ?
                new ObjectParameter("IdUsuario", idUsuario) :
                new ObjectParameter("IdUsuario", typeof(int));
    
            var usuarioParameter = usuario != null ?
                new ObjectParameter("usuario", usuario) :
                new ObjectParameter("usuario", typeof(string));
    
            var claveParameter = clave != null ?
                new ObjectParameter("clave", clave) :
                new ObjectParameter("clave", typeof(string));
    
            var tipoParameter = tipo != null ?
                new ObjectParameter("tipo", tipo) :
                new ObjectParameter("tipo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spActualizarUsuarioPortal", idUsuarioParameter, usuarioParameter, claveParameter, tipoParameter);
        }
    }
}
