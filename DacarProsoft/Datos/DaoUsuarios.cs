using DacarDatos.Datos;
using DacarProsoft.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DacarProsoft.Datos
{
    public class DaoUsuarios
    {
        public Conexion conexion = new Conexion();

        public DataTable consultarUsuarios(String Nombre, String Contrasena) {
            DataTable dt = new DataTable();

            try
            {
                //ME CONECTO
                conexion.Conectar();
                using (conexion.conn)
                {
                    //CREO EL COMANDO
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "dbo.LoginUsuario";
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Connection = conexion.conn;
                    comando.Parameters.Add("@username", SqlDbType.NVarChar).Value = Nombre;
                    comando.Parameters.Add("@llave", SqlDbType.NVarChar).Value = Contrasena;
                    //CREO EL ADAPTADOR
                    SqlDataAdapter da = new SqlDataAdapter();
 
                    da.SelectCommand = comando;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dt;
        }


        public List<spConsultaUsuarios> ConsultarUsuarios()
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoTipoUsuario = DB.spConsultaUsuarios().ToList();
                Console.WriteLine(ListadoTipoUsuario);
                return ListadoTipoUsuario;
            }
        }

        public bool ingresarUsuarios(String Nombre, String Usuario,String Contrasena, int TipoUsuario)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                 DB.RegistrarNuevoUsuario(Nombre,Usuario,Contrasena,TipoUsuario);
                 return true;
            }

        }
        public bool ActualizacionUsuarios(int IdUsuario,string Nombre,string Usuario,string contrasena, int TipoUsuario)
        {

            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
        
                try
                {
                    DB.spActualizarUsuario(IdUsuario, Usuario, Nombre, contrasena, TipoUsuario);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;

                }

            }

        }
        public bool EliminarUsuarios(int TipoUsuario)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                DB.Usuarios.RemoveRange(DB.Usuarios.Where(x => x.IdUsuario == TipoUsuario));
                DB.SaveChanges();
                return true;
            }

        }

        public List<TipoUsuario> tipoUsuario()
        {

            using(DacarProsoftEntities  DB = new DacarProsoftEntities())
            {
                var ListadoTipoUsuario = DB.TipoUsuario.ToList();
                return ListadoTipoUsuario;
            }
        }




        public List<LoginUsuario_Result> InicioSesion(String usuario, String Clave)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoTipoUsuario = DB.LoginUsuario(usuario,Clave).ToList();
                return ListadoTipoUsuario;
            }
        }

        public List<ClienteSap> ConsutaClientesSap()
        {
            List<ClienteSap> lst = new List<ClienteSap>();
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                var ListadoClientesSap = from d in DB.OCRD
                                         where d.U_SYP_DIVISION== "02 COMERCIAL"
                                         select new { 
                                         d.CardCode,
                                         d.CardName
                                         };
                foreach (var x in ListadoClientesSap) {
                    var comp = ConsultarExiistenciaUsuarioCliente(x.CardCode);
                    if (comp == false)
                    {
                        lst.Add(new ClienteSap
                        {
                            CardCode = x.CardCode,
                            NombreCliente = x.CardName
                        });
                    }
                }

                return lst;
            }
        }


        public bool ConsultarExiistenciaUsuarioCliente(string referencia)
        {

            bool comprobar = false;
            List<UsuariosPortal> lst = new List<UsuariosPortal>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var Listado = (from d in DB.UsuariosPortal
                                   where d.ReferenciaUsuario == referencia
                                   select new
                                   {
                                       d.UsuarioPortal,

                                   });
                    foreach (var x in Listado)
                    {
                        if (x.UsuarioPortal != null)
                        {
                            comprobar = true;
                        }
                    }

                    return comprobar;
                }
                catch
                {
                    return false;

                }

            }
        }

        public bool ConsultarExiistenciaUsuarioEnbase(int IdUsuario, string referencia)
        {

            bool comprobar = false;
            List<UsuariosPortal> lst = new List<UsuariosPortal>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var Listado = (from d in DB.UsuariosPortal
                                   where d.UsuarioPortalId!=IdUsuario && d.UsuarioPortal == referencia 
                                   select new
                                   {
                                       d.NombreCliente,

                                   });
                    foreach (var x in Listado)
                    {
                        if (x.NombreCliente != null)
                        {
                            comprobar = true;
                        }
                    }

                    return comprobar;
                }
                catch
                {
                    return false;

                }

            }
        }

        public bool ingresarUsuariosPortal(string Nombre, string Usuario, string Contrasena, string Referencia, bool Validacion)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var consultar = ConsultarExiistenciaUsuarioCliente(Referencia);
                    if (consultar == false)
                    {
                        DB.spRegistrarNuevoUsuarioPortal(Nombre, Usuario, Contrasena, Referencia, "1", Validacion);
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                catch {
                    return false;
                }
                
            }

        }

        public List<UsuariosPortal> ConsutaUsuariosRegistradosPortal()
        {
            List<UsuariosPortal> lst = new List<UsuariosPortal>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoUsuariosPortal = from d in DB.UsuariosPortal
                                         select new
                                         {
                                             d.UsuarioPortalId,
                                             d.NombreCliente,
                                             d.UsuarioPortal,
                                             d.ReferenciaUsuario,
                                             d.EstadoUsuario,
                                             d.Validaciones
                                         };
                foreach (var x in ListadoUsuariosPortal)
                {

                    //var comp=ConsultarExiistenciaUsuarioCliente(x.CardCode);
                    //if (comp == false) {
                    lst.Add(new UsuariosPortal
                    {
                      UsuarioPortalId=x.UsuarioPortalId,
                      NombreCliente=x.NombreCliente,
                      UsuarioPortal=x.UsuarioPortal,
                      ReferenciaUsuario=x.ReferenciaUsuario,
                      EstadoUsuario=x.EstadoUsuario,
                      Validaciones=x.Validaciones
                      
                    });
                    //}
                }

                return lst;
            }
        }

        public bool EliminarUsuariosPortal(int IdUsuario)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                DB.UsuariosPortal.RemoveRange(DB.UsuariosPortal.Where(x => x.UsuarioPortalId == IdUsuario));
                DB.SaveChanges();
                return true;
            }

        }

        public bool ActualizacionUsuariosPortal(int IdUsuarioPortal, string Usuario, string Clave, string tipo, bool validacion)
        {

            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {

                try
                {
                    var consultar = ConsultarExiistenciaUsuarioEnbase(IdUsuarioPortal, Usuario);
                    if (consultar == false)
                    {
                        DB.spActualizarUsuarioPortal(IdUsuarioPortal, Usuario, Clave, tipo, validacion);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;

                }

            }

        }
        public List<ListaPrecioCliente> ConsutarListaGenerica()
        {
            List<ListaPrecioCliente> lst = new List<ListaPrecioCliente>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoUsuariosPortal = from d in DB.ListaPrecioGenerica
                                            where d.Estado == true
                                            select new
                                            {
                                                d.ListaPrecioGenericaId,
                                                d.CustomerReference,
                                                d.DacarPartNumber,
                                                d.ModeloGenerico,
                                                d.DimensionsHeight,
                                                d.DimensionsLenght,
                                                d.DimensionWidth,
                                                d.AssemblyBci,
                                                d.SpecificationsNominalCapacity,
                                                d.ReserveCap,
                                                d.CCAMenos18,
                                                d.CA0,
                                                d.WeightKg,
                                                d.QuantityXLayer,
                                                d.Categoria
                                            };
                foreach (var x in ListadoUsuariosPortal)
                {
                    lst.Add(new ListaPrecioCliente
                    {
                        ListaPrecioClienteId=x.ListaPrecioGenericaId,
                        CustomerReference=x.CustomerReference,
                        DacarPartNumber=x.DacarPartNumber,
                        ModeloGenerico=x.ModeloGenerico,
                        DimensionsHeight=x.DimensionsHeight,
                        DimensionsLenght=x.DimensionsLenght,
                        DimensionWidth=x.DimensionWidth,
                        AssemblyBci=x.AssemblyBci,
                        SpecificationsNominalCapacity=x.SpecificationsNominalCapacity,
                        ReserveCap=x.ReserveCap,
                        CCAMenos18=x.CCAMenos18,
                        CA0=x.CA0,
                        WeightKg=x.WeightKg,
                        QuantityXLayer=x.QuantityXLayer,
                        Categoria=x.Categoria,
                        PrecioProducto=0,
                        PrecioEnvio=0,
                    });
                }

                return lst;
            }
        }
        public bool RegistrarListaPrecio(string CustomerReference, string DacarPartNumber, string ModeloGenerico, int DimensionsLenght, int DimensionsWidht, int DimensionsHeight, string Assembly, int NominalCap,
            int ReservaCap, int CCAMenos18, int Ca0, decimal WeightKg, int QuantityLayer, int Categoria, string CardCode, decimal PrecioProducto, decimal PrecioEnvio, int NombreListaId)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var lstPrecioCliente = new ListaPrecioCliente();

                    lstPrecioCliente.CustomerReference = CustomerReference;
                    lstPrecioCliente.DacarPartNumber = DacarPartNumber;
                    lstPrecioCliente.ModeloGenerico = ModeloGenerico;
                    lstPrecioCliente.DimensionsLenght = DimensionsLenght;
                    lstPrecioCliente.DimensionWidth = DimensionsWidht;
                    lstPrecioCliente.DimensionsHeight = DimensionsHeight;
                    lstPrecioCliente.AssemblyBci = Assembly;
                    lstPrecioCliente.SpecificationsNominalCapacity = NominalCap;
                    lstPrecioCliente.ReserveCap = ReservaCap;
                    lstPrecioCliente.CCAMenos18 = CCAMenos18;
                    lstPrecioCliente.CA0 = Ca0;
                    lstPrecioCliente.WeightKg = WeightKg;
                    lstPrecioCliente.QuantityXLayer = QuantityLayer;
                    lstPrecioCliente.Categoria = Categoria;
                    lstPrecioCliente.CardCode = CardCode;
                    lstPrecioCliente.PrecioProducto = PrecioProducto;
                    lstPrecioCliente.PrecioEnvio = PrecioEnvio;
                    lstPrecioCliente.Estado = true;
                    lstPrecioCliente.FechaRegistro = DateTime.Now;
                    lstPrecioCliente.NombreListaId = NombreListaId;

                    DB.ListaPrecioCliente.Add(lstPrecioCliente);
                    DB.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
        public int RegistrarNombreLista(string CardCode)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var lst = new NombreListaPrecioCliente();

                    lst.NombreLista = DateTime.Now.Day+"/"+ DateTime.Now.Month + "/" + DateTime.Now.Year;
                    lst.CardCode = CardCode;
                    lst.Estado = true;
                    lst.FechaRegistro = DateTime.Now;

                    DB.NombreListaPrecioCliente.Add(lst);
                    DB.SaveChanges();
                    int prodId = lst.NombreListaPrecioClienteId;
                    return prodId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }
        public List<ListaPrecioCliente> ConsutarListaPrecioCLiente(string CardCode)
        {
            List<ListaPrecioCliente> lst = new List<ListaPrecioCliente>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                int id=ObtenerIdUltimaListaPrecioCliente(CardCode);

                var ListadoUsuariosPortal = from d in DB.ListaPrecioCliente
                                            where d.Estado == true && d.CardCode==CardCode && d.NombreListaId==id
                                            select new
                                            {
                                                d.ListaPrecioClienteId,
                                                d.CustomerReference,
                                                d.DacarPartNumber,
                                                d.ModeloGenerico,
                                                d.DimensionsHeight,
                                                d.DimensionsLenght,
                                                d.DimensionWidth,
                                                d.AssemblyBci,
                                                d.SpecificationsNominalCapacity,
                                                d.ReserveCap,
                                                d.CCAMenos18,
                                                d.CA0,
                                                d.WeightKg,
                                                d.QuantityXLayer,
                                                d.Categoria,
                                                d.PrecioProducto,
                                                d.PrecioEnvio
                                            };
                foreach (var x in ListadoUsuariosPortal)
                {
                    lst.Add(new ListaPrecioCliente
                    {
                        ListaPrecioClienteId = x.ListaPrecioClienteId,
                        CustomerReference = x.CustomerReference,
                        DacarPartNumber = x.DacarPartNumber,
                        ModeloGenerico = x.ModeloGenerico,
                        DimensionsHeight = x.DimensionsHeight,
                        DimensionsLenght = x.DimensionsLenght,
                        DimensionWidth = x.DimensionWidth,
                        AssemblyBci = x.AssemblyBci,
                        SpecificationsNominalCapacity = x.SpecificationsNominalCapacity,
                        ReserveCap = x.ReserveCap,
                        CCAMenos18 = x.CCAMenos18,
                        CA0 = x.CA0,
                        WeightKg = x.WeightKg,
                        QuantityXLayer = x.QuantityXLayer,
                        Categoria = x.Categoria,
                        PrecioProducto = x.PrecioProducto,
                        PrecioEnvio = x.PrecioEnvio,
                    });
                }

                return lst;
            }
        }
        public int ObtenerIdUltimaListaPrecioCliente(string CardCode)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try {
                    int valor = 0;

                    var res = (from d in DB.NombreListaPrecioCliente
                               where d.CardCode == CardCode
                               orderby d.NombreListaPrecioClienteId descending
                               select new
                               {
                                   d.NombreListaPrecioClienteId
                               }).FirstOrDefault();
                    if (res != null) {
                        valor = res.NombreListaPrecioClienteId;
                    }
                    return valor;
                }
                catch (Exception ex){
                    Console.WriteLine(ex);
                    return 0;
                }
              
            }
        }
        public bool ActualizarRegistroBateria(ListaPrecioCliente generico, int Key)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var result = (from a in DB.ListaPrecioCliente
                                  where a.ListaPrecioClienteId == Key
                                  select a).FirstOrDefault();

                    //generico.GrupoGenericoItem, generico.ModeloDacar, generico.NumeroParteCliente, generico.EtiquetaDatosTecnicos, generico.Polaridad, generico.TipoTerminal, generico.CantidadPiso.Value,
                    //generico.PisoMaximo.Value, generico.BateriasPallet.Value, generico.PesoTara.Value
                    if (generico.CustomerReference != null)
                    {
                        result.CustomerReference = generico.CustomerReference;
                    }
                    if (generico.DacarPartNumber != null)
                    {
                        result.DacarPartNumber = generico.DacarPartNumber;
                    }
                    if (generico.ModeloGenerico != null)
                    {
                        result.ModeloGenerico = generico.ModeloGenerico;
                    }
                    if (generico.DimensionsHeight != null)
                    {
                        result.DimensionsHeight = generico.DimensionsHeight;
                    }
                    if (generico.DimensionsLenght != null)
                    {
                        result.DimensionsLenght = generico.DimensionsLenght;
                    }
                    if (generico.DimensionWidth != null)
                    {
                        result.DimensionWidth = generico.DimensionWidth;
                    }
                    if (generico.AssemblyBci != null)
                    {
                        result.AssemblyBci = generico.AssemblyBci;
                    }
                    if (generico.SpecificationsNominalCapacity != null)
                    {
                        result.SpecificationsNominalCapacity = generico.SpecificationsNominalCapacity;
                    }
                    if (generico.ReserveCap != null)
                    {
                        result.ReserveCap = generico.ReserveCap;
                    }
                    if (generico.CCAMenos18 != null)
                    {
                        result.CCAMenos18 = generico.CCAMenos18;
                    }
                    if (generico.CA0 != null)
                    {
                        result.CA0 = generico.CA0;
                    }
                    if (generico.WeightKg != null)
                    {
                        result.WeightKg = generico.WeightKg;
                    }
                    if (generico.QuantityXLayer != null)
                    {
                        result.QuantityXLayer = generico.QuantityXLayer;
                    }
                    if (generico.Categoria != null)
                    {
                        result.Categoria = generico.Categoria;
                    }
                    if (generico.PrecioProducto != null)
                    {
                        result.PrecioProducto = generico.PrecioProducto;
                    }
                    if (generico.PrecioEnvio != null)
                    {
                        result.PrecioEnvio = generico.PrecioEnvio;
                    }
                    result.FechaActualizacion = DateTime.Now;


                    result.FechaRegistro = DateTime.Now;

                    DB.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
    }
}