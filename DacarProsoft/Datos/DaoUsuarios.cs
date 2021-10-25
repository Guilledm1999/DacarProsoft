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
                                         select new { 
                                         d.CardCode,
                                         d.CardName
                                         };
                foreach (var x in ListadoClientesSap) {

                    //var comp=ConsultarExiistenciaUsuarioCliente(x.CardCode);
                    //if (comp == false) {
                        lst.Add(new ClienteSap
                        {
                            CardCode = x.CardCode,
                            NombreCliente = x.CardName
                        });
                    //}
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
    }

}