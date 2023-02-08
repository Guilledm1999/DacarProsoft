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

        public bool ingresarUsuarios(String Nombre, String Usuario,String Contrasena, int TipoUsuario, string UsuarioCreador)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                 DB.RegistrarNuevoUsuario(Nombre,Usuario,Contrasena,TipoUsuario, UsuarioCreador);
                 return true;
            }

        }
        public int Intentos()
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var retorno = DB.SeguridadUsuario.Where(x=> x.Estado=="A").Select(x => x.Intentos).FirstOrDefault()??0;
                return retorno;
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
        public int ConsultarNivelDificultadContrasena()
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoTipoUsuario = DB.SeguridadUsuario.Where(x => x.Estado == "A").Select(x => x.DificultadContrasena).FirstOrDefault()??3;
                return ListadoTipoUsuario;
            }
        }


        public List<ClienteSap> ConsutaClientesSap()
        {
            List<ClienteSap> lst = new List<ClienteSap>();
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                var ListadoClientesSap = from d in DB.OCRD
                                         where d.U_SYP_DIVISION== "02 COMERCIAL" && d.CardType == "C" && d.validFor =="Y"
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
                string cardcode = DB.UsuariosPortal.Where(x => x.UsuarioPortalId == IdUsuario).Select(x => x.ReferenciaUsuario).FirstOrDefault();
                var query = (from a in DB.NombreListaPrecioCliente
                             where a.CardCode == cardcode && a.Estado == true
                             select a).ToList();
                foreach (var x in query)
                {
                    x.Estado = false;
                }
                var query1 = (from a in DB.ListaPrecioCliente
                             where a.CardCode == cardcode && a.Estado == true
                             select a).ToList();
                foreach (var x in query1)
                {
                    x.Estado = false;
                }

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
            int cantPorPallet = 0;
            int cantPisos=0;
            List<ListaPrecioCliente> lst = new List<ListaPrecioCliente>();
           /* using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoUsuariosPortal = from d in DB.ListaPrecioGenerica
                                            where d.Estado == true
                                            select new
                                            {
                                               // d.ItemCode,
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
                                                d.Categoria,
                                            };
                foreach (var x in ListadoUsuariosPortal)
                {
                    cantPorPallet = ConsultarCantidadPorPallet(x.ModeloGenerico);
                    cantPisos = ConsultarCantidadPisos(x.ModeloGenerico);
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
                        CantidadPorPallet= cantPorPallet,//por consultar
                        Pisos= cantPisos//por consultar
                    });
                }

                return lst;
            }*/

            return lst;
        }
        public List<ListaPrecioCliente> ConsutarListaPrecioGenerica()
        {
            int cantPorPallet = 0;
            int cantPisos = 0;
            List<ListaPrecioCliente> lst = new List<ListaPrecioCliente>();
             using (DacarProsoftEntities DB = new DacarProsoftEntities())
             {
                 var ListadoUsuariosPortal = from d in DB.ListaPrecioGenerica
                                             where d.Estado == true
                                             select new
                                             {
                                                // d.ItemCode,
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
                                                 d.Categoria,
                                             };
                 foreach (var x in ListadoUsuariosPortal)
                 {
                     cantPorPallet = ConsultarCantidadPorPallet(x.ModeloGenerico);
                     cantPisos = ConsultarCantidadPisos(x.ModeloGenerico);
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
                         CantidadPorPallet= cantPorPallet,//por consultar
                         Pisos= cantPisos//por consultar
                     });
                 }

                 return lst;
             }

           
        }

        public List<EMarca> ConsutarModelo()
        {

            List<EMarca> lst = new List<EMarca>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                lst = (from d in DB.ListaPrecioGenerica

                       select new EMarca
                       {
                           ItemCode = d.DacarPartNumber,
                           Descripcion = d.DacarPartNumber,
                       }).ToList();

            }
            lst = lst.GroupBy(x => x.Descripcion).Select(y => y.FirstOrDefault()).ToList();
            return lst;
        }

        public String EncontrarItemCode(string NombreForaneo, string Modelo, string Marca)
        {

            
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                string Itemcode = DB.OITM.Where(x => x.FrgnName == NombreForaneo && x.U_MARCA == Marca && x.validFor == "Y").Select(x => x.ItemCode).FirstOrDefault();
             
                if (string.IsNullOrEmpty(Itemcode))
                {
                    Itemcode = DB.OITM.Where(x => x.U_DAC_MARCA == Modelo && x.U_MARCA == Marca && x.validFor == "Y").Select(x => x.ItemCode).FirstOrDefault();
                   
                }
                if (string.IsNullOrEmpty(Itemcode))
                {
                    Itemcode = "";
                }


                return Itemcode;

            }
          
        }



        public List<EMarca> ConsutarMarca(string CardCode)
        {
            //Encontrar mediante tabla MarcaClientePortal en Prosoft
            List<EMarca> lst = new List<EMarca>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            { 
                lst = (from d in DB.MarcaClientePortal
                       join m in DB.MarcaPortal on d.IdMarca equals m.IdMarca
                       where d.Estado == "A" && CardCode ==d.CardCode
                       select new EMarca
                       {
                           ItemCode = m.Descripcion,
                           Descripcion = m.Descripcion
                       }).ToList();


                return lst;
            }

            /*
             * ENCONTRAR MARCA MEDIANTE SAP
            using (SBODACARPRODEntities1 DBSap = new SBODACARPRODEntities1())
            {
                


                //Para Mostrar todas las marcas
                lst = (from d in DBSap.OITM
                       where d.validFor == "Y" && d.U_DAC_MARCA != null
                       select new EMarca
                       {
                           ItemCode = d.U_MARCA,
                           Descripcion = d.U_MARCA
                       }).ToList();
             //   var distinctItems = lst.GroupBy(x => x.ItemCode).Select(y => y.First());


                lst = lst.GroupBy(a => a.Descripcion).Select(a => a.FirstOrDefault()).ToList();
                return lst;
            }*/
          //  return lst;
        }


        public string ConsultarVersion(string CardCode)
        {
            //Encontrar mediante tabla MarcaClientePortal en Prosoft
            
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                string version = DB.ListaPrecioCliente.Where(x => x.CardCode == CardCode && x.Estado == true).Select(x => x.Version).FirstOrDefault();
                

                return version;
            }

        }

        //Buscar marcas a partir del modelogenerico
        public List<EMarca> ConsutarMarcaModeloGenerico(string CardCode, string NombreForaneo  )
        {

            List<EMarca> lst = new List<EMarca>();
            //  List<EMarca> Lista = ConsutarMarca();

            /*
            using (SBODACARPRODEntities1 DBSap = new SBODACARPRODEntities1())
            {


                lst = (from d in DBSap.OITM
                       where  d.validFor =="Y"  && d.FrgnName == NombreForaneo
                       select new EMarca
                       {
                           ItemCode = d.U_MARCA,
                           Descripcion = d.U_MARCA
                       }).ToList();
                if (lst.Count <= 0)
                {
                    lst = (from d in DBSap.OITM
                           where d.validFor == "Y" && d.U_DAC_MARCA == ModeloGenerico
                           select new EMarca
                           {
                               ItemCode = d.U_MARCA,
                               Descripcion = d.U_MARCA
                           }).ToList();
                }


                lst = lst.GroupBy(a => a.Descripcion).Select(a => a.FirstOrDefault()).ToList();
                return lst;
            }*/
            return lst;
        }

        public int ConsultarCantidadPorPallet(string modelo) {
            int  result=0;
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var valor = (from d in DB.DatosTecnicosBatExpor
                             where d.Modelo == modelo
                             select new
                             {
                                 d.CantidadPorPallet
                             }).FirstOrDefault();
                if (valor != null)
                {
                    result = valor.CantidadPorPallet.Value;
                }
                return result;
            }
        }
        public int ConsultarCantidadPisos(string modelo)
        {
            int result = 0;
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var valor = (from d in DB.DatosTecnicosBatExpor
                             where d.Modelo == modelo
                             select new
                             {
                                 d.Pisos
                             }).FirstOrDefault();
                if (valor != null)
                {
                    result = valor.Pisos.Value;
                }
                return result;
            }
        }
        //metodo para buscar valor de cantidad por pallet y pisos
        public List<DatosTecnicosBatExpor> ConsutarDatosGenericosBateria()
        {
            List<DatosTecnicosBatExpor> lst = new List<DatosTecnicosBatExpor>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoUsuariosPortal = from d in DB.DatosTecnicosBatExpor
                                            where d.Estado == true
                                            select new
                                            {
                                                d.DatosTecnicosBatExporId,
                                                d.Modelo,
                                                d.C20,
                                                d.CA0C,
                                                d.CantidadPiso,
                                                d.CantidadPorPallet,
                                                d.CAP,
                                                d.CCAMenos18CExpo,
                                                d.CCAMenos18CLocal,
                                                d.PesoHumedaKg,
                                                d.PesoSellada,
                                                d.Pisos,                                           
                                            };
                foreach (var x in ListadoUsuariosPortal)
                {
                    lst.Add(new DatosTecnicosBatExpor
                    {
                        DatosTecnicosBatExporId=x.DatosTecnicosBatExporId,
                        C20=x.C20,
                        Pisos=x.Pisos,
                        PesoSellada=x.PesoSellada,
                        PesoHumedaKg=x.PesoHumedaKg,
                        CCAMenos18CLocal=x.CCAMenos18CLocal,
                        CCAMenos18CExpo=x.CCAMenos18CExpo,
                        CAP=x.CAP,
                        CantidadPorPallet=x.CantidadPorPallet,
                        CA0C=x.CA0C,
                        CantidadPiso=x.CantidadPiso,
                        Modelo=x.Modelo
                    });
                }
                return lst;
            }
        }
        public bool RegistrarListaPrecio(string itemCode, string Marca ,string CustomerReference, string DacarPartNumber, string ModeloGenerico, int DimensionsLenght, int DimensionsWidht, int DimensionsHeight, string Assembly, int NominalCap,
            int ReservaCap, int CCAMenos18, int Ca0, decimal WeightKg, int QuantityLayer, int Categoria, string CardCode, decimal PrecioProducto, decimal PrecioEnvio, int NombreListaId, int CantidadXPallet, int Pisos, string version)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var lstPrecioCliente = new ListaPrecioCliente();

                    lstPrecioCliente.ItemCode = itemCode;
                    lstPrecioCliente.Marca = Marca;
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
                    lstPrecioCliente.Pisos = Pisos;
                    lstPrecioCliente.CantidadPorPallet=CantidadXPallet;
                    lstPrecioCliente.Version = version;

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
        public bool RegistrarDatosGenericosBateriaClientes(string modelo, int c20, int pisos, decimal pesoSellada, decimal PesoHumedaKg, int CCAMenos18CLocal
            ,int CCAMenos18CExpo, int CAP, int CantidadPorPallet, int CA0C, int CantidadPiso)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var lst = new DatosTecnicosBatExporListCli();
                    lst.Modelo = modelo;
                    lst.C20 = c20;
                    lst.Pisos = pisos;
                    lst.PesoSellada = pesoSellada;
                    lst.PesoHumedaKg = PesoHumedaKg;
                    lst.CCAMenos18CLocal = CCAMenos18CLocal;
                    lst.CCAMenos18CExpo = CCAMenos18CExpo;
                    lst.CAP = CAP;
                    lst.CantidadPorPallet = CantidadPorPallet;
                    lst.CA0C = CA0C;
                    lst.CantidadPiso = CantidadPiso;

                    DB.DatosTecnicosBatExporListCli.Add(lst);
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

                var ListadoUsuariosPortal = (from d in DB.ListaPrecioCliente
                                            where d.Estado == true && d.CardCode==CardCode && d.NombreListaId==id && d.Estado==true
                                            select new
                                            {
                                                d.ListaPrecioClienteId,
                                                d.NombreListaId,
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
                                                d.PrecioEnvio,
                                                d.Pisos,//por ingresar
                                                d.CantidadPorPallet,//por ingresar
                                                d.Marca,
                                                d.Version
                                            }).ToList();
                foreach (var x in ListadoUsuariosPortal)
                {
                    lst.Add(new ListaPrecioCliente
                    {
                        ListaPrecioClienteId = x.ListaPrecioClienteId,
                        NombreListaId = x.NombreListaId,
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
                        Pisos=x.Pisos,
                        CantidadPorPallet=x.CantidadPorPallet,
                        Marca = x.Marca,
                        Version = x.Version
                        
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
                               where d.CardCode == CardCode && d.Estado== true
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
        public bool ActualizarRegistroBateria(List<ListaPrecioCliente> generico, string Version, string CardCode)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var result1 = (from a in DB.ListaPrecioCliente
                                  where  a.Estado == true && a.Version != Version && a.CardCode == CardCode
                                  select a).ToList();
                    if (result1.Count >0)
                    {
                        foreach (var item1 in result1)
                        {
                            item1.Estado = false;
                            item1.FechaActualizacion = DateTime.Now;
                        }
                    }
                    var Lista = generico.Where(x=> x.ListaPrecioClienteId != 0).Select(x => x.ListaPrecioClienteId).ToList();
                   
                    var ListaEliminar = DB.ListaPrecioCliente.Where(x => !Lista.Contains(x.ListaPrecioClienteId) && x.Version == Version && x.Estado == true && x.CardCode == CardCode).Select(x => x).ToList();
                    foreach (var item in ListaEliminar)
                    {
                        item.Estado = false;
                        item.FechaActualizacion = DateTime.Now;
                    }
                    foreach (var item in generico)
                    {
                        var result = (from a in DB.ListaPrecioCliente
                                      where a.ListaPrecioClienteId == item.ListaPrecioClienteId && a.Estado == true && a.Version == Version
                                      select a).FirstOrDefault();
                        //generico.GrupoGenericoItem, generico.ModeloDacar, generico.NumeroParteCliente, generico.EtiquetaDatosTecnicos, generico.Polaridad, generico.TipoTerminal, generico.CantidadPiso.Value,
                        //generico.PisoMaximo.Value, generico.BateriasPallet.Value, generico.PesoTara.Value
                        if (result !=null)
                        {

                            if (item.CustomerReference != null)
                            {
                                result.CustomerReference = item.CustomerReference;
                            }
                            if (item.DacarPartNumber != null)
                            {
                                result.DacarPartNumber = item.DacarPartNumber;
                            }
                            if (item.ModeloGenerico != null)
                            {
                                result.ModeloGenerico = item.ModeloGenerico;
                            }
                            if (item.DimensionsHeight != null)
                            {
                                result.DimensionsHeight = item.DimensionsHeight;
                            }
                            if (item.DimensionsLenght != null)
                            {
                                result.DimensionsLenght = item.DimensionsLenght;
                            }
                            if (item.DimensionWidth != null)
                            {
                                result.DimensionWidth = item.DimensionWidth;
                            }
                            if (item.AssemblyBci != null)
                            {
                                result.AssemblyBci = item.AssemblyBci;
                            }
                            if (item.SpecificationsNominalCapacity != null)
                            {
                                result.SpecificationsNominalCapacity = item.SpecificationsNominalCapacity;
                            }
                            if (item.ReserveCap != null)
                            {
                                result.ReserveCap = item.ReserveCap;
                            }
                            if (item.CCAMenos18 != null)
                            {
                                result.CCAMenos18 = item.CCAMenos18;
                            }
                            if (item.CA0 != null)
                            {
                                result.CA0 = item.CA0;
                            }
                            if (item.WeightKg != null)
                            {
                                result.WeightKg = item.WeightKg;
                            }
                            if (item.QuantityXLayer != null)
                            {
                                result.QuantityXLayer = item.QuantityXLayer;
                            }
                            if (item.Categoria != null)
                            {
                                result.Categoria = item.Categoria;
                            }
                            if (item.PrecioProducto != null)
                            {
                                result.PrecioProducto = item.PrecioProducto;
                            }
                            if (item.PrecioEnvio != null)
                            {
                                result.PrecioEnvio = item.PrecioEnvio;
                            }
                            if (item.CantidadPorPallet != null)
                            {
                                result.CantidadPorPallet = item.CantidadPorPallet;
                            }
                            if (item.Pisos != null)
                            {
                                result.Pisos = item.Pisos;
                            }
                            if (item.Marca != null)
                            {
                                result.Marca = item.Marca;
                            }
                            result.FechaActualizacion = DateTime.Now;


                            result.FechaRegistro = DateTime.Now;
                        }
                        else
                        {
                            var NombreLista = DB.NombreListaPrecioCliente.Where(x => x.CardCode == CardCode && x.Estado == true).Select(x => x.NombreListaPrecioClienteId).FirstOrDefault();
                            result = new ListaPrecioCliente();
                            result.NombreListaId = item.NombreListaId ?? NombreLista;
                            result.CustomerReference = item.CustomerReference;
                          
                            result.DacarPartNumber = item.DacarPartNumber;
                            result.ModeloGenerico = item.ModeloGenerico;
                            result.DimensionsLenght = item.DimensionsLenght;
                            result.DimensionWidth = item.DimensionWidth;
                            result.DimensionsHeight = item.DimensionsHeight;
                            result.SpecificationsNominalCapacity = item.SpecificationsNominalCapacity;
                            result.AssemblyBci = item.AssemblyBci;
                            result.ReserveCap = item.ReserveCap;
                            result.CCAMenos18 = item.CCAMenos18;
                            result.CA0 = item.CA0;
                            result.WeightKg = item.WeightKg;
                            result.QuantityXLayer = item.QuantityXLayer;
                            result.Categoria = item.Categoria;
                            result.Estado = true;

                            result.CardCode = CardCode;
                            result.PrecioProducto = item.PrecioProducto;
                            result.PrecioEnvio = item.PrecioEnvio;
                            
                            result.FechaRegistro = DateTime.Now;
                          
                            result.Pisos = item.Pisos;
                            result.CantidadPorPallet = item.CantidadPorPallet;
                            result.Marca = item.Marca;

                            result.Version = Version;

                            DB.ListaPrecioCliente.Add(result);

                        }

                    }
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