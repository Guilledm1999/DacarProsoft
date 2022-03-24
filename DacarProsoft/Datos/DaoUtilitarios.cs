using DacarDatos.Datos;
using DacarProsoft.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DacarProsoft.Datos
{
    public class DaoUtilitarios
    {


        public List<EstadosOrdenProduccion> ConsultarEstadosDeOrdenesProduccion()
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoEstadosOrdenProduccion = DB.EstadosOrdenProduccion.ToList();
                return ListadoEstadosOrdenProduccion;
            }
        }
        public List<SelectListItem> TipoUsuario()
        {
            List<SelectListItem> lst2 = new List<SelectListItem>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoTipoUsuario = from d in DB.TipoUsuario
                                     select new
                                     {
                                         d.TipoUsuarioId,
                                         d.DescripcionTipoUsuario,
                                     };

                foreach (var x in ListadoTipoUsuario)
                {

                    lst2.Add(new SelectListItem()
                    {
                        Text = x.DescripcionTipoUsuario,
                        Value = Convert.ToString(x.TipoUsuarioId)
                    });
                }
                return lst2;
            }
        }
        public List<SelectListItem> TipoMenu()
        {
            List<SelectListItem> lst2 = new List<SelectListItem>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoMenu = from d in DB.Menus
                                         select new
                                         {
                                             d.MenuId,
                                             d.DescripcionMenu
                                         };

                foreach (var x in ListadoMenu)
                {

                    lst2.Add(new SelectListItem()
                    {
                        Text = x.DescripcionMenu,
                        Value = Convert.ToString(x.MenuId)
                    });
                }
                return lst2;
            }
        }
        public bool ConsultarExistenciaAcceso(int tipoUsuario, int tipoMenu, int estado)
        {
            int? valor=null;
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoAccesos = (from d in DB.Accesos
                                         where d.TipoUsuarioId==tipoUsuario &&d.MenuId==tipoMenu &&d.Estado==estado
                                         select new
                                         {
                                             d.IdAccesos,
                                         });
                foreach (var x in ListadoAccesos) {
                    valor = x.IdAccesos;
                }

                if (valor == null) {
                    return false;
                }

                else {
                    return true;
                }
            }
        }
        public bool ingresarAcceso(string tipoUsuario, string tipoMenu, string estado)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var acceso = new Accesos();

                    acceso.TipoUsuarioId = Convert.ToInt32(tipoUsuario);
                    acceso.MenuId = Convert.ToInt32(tipoMenu);
                    acceso.Estado = Convert.ToInt32(estado);

                    DB.Accesos.Add(acceso);
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
        public bool ActualizarAccesos(int idAcceso, int Estado)
        {
            List<IngresosChatarras> lst = new List<IngresosChatarras>();

            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var regi = (from d in DB.Accesos
                            where d.IdAccesos == idAcceso
                            select d).FirstOrDefault();
                try
                {
                    regi.Estado = Estado;
                    DB.SaveChanges();
                    return true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;

                }

            }

        }
        public List<AccesosMenu> ConsultarAccesoMenu()
        {
            List<AccesosMenu> lst2 = new List<AccesosMenu>();

            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                string estado = null;
                var ListadoModelos = from d in DB.Accesos
                                     join e in DB.TipoUsuario on d.TipoUsuarioId equals e.TipoUsuarioId
                                     join f in DB.Menus on d.MenuId equals f.MenuId
                                     select new
                                     {
                                         d.IdAccesos,
                                         e.DescripcionTipoUsuario,
                                         f.DescripcionMenu,
                                         d.Estado
                                     };

                foreach (var x in ListadoModelos)
                {
                    if (x.Estado == 1)
                    {
                        estado = "Activo";
                    }
                    else {
                        estado = "Inactivo";
                    }
                    lst2.Add(new AccesosMenu()
                    {
                        idAcceso=x.IdAccesos,
                        TipoUsuario=x.DescripcionTipoUsuario,
                        MenuDescr=x.DescripcionMenu,
                        EstadoMenu=estado       
                    });
                }
                return lst2;
            }
        }

        public bool EliminarAcceso(int IdAcceso)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try {
                    DB.Accesos.RemoveRange(DB.Accesos.Where(x => x.IdAccesos == IdAcceso));
                    DB.SaveChanges();
                    return true;
                }
                catch (Exception ex) {
                    Console.WriteLine(ex);
                    return false;
                }
                
            }

        }

        public List<Menus> ConsultarMenuPrincipal()
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoMenuPrincipal = DB.Menus.ToList();
                return ListadoMenuPrincipal;
            }
        }
        public List<MenuOpciones> ConsultarMenuOpciones()
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoMenuOpciones = DB.MenuOpciones.ToList();
                return ListadoMenuOpciones;
            }
        }

        public List<SubMenuOpciones> ConsultarSubMenuOpciones()
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoSubMenuOpciones = DB.SubMenuOpciones.ToList();
                return ListadoSubMenuOpciones;
            }
        }
        public List<IngresoMercanciasTipoDescripcion> ConsultarBusquedaIngresoMercanciasTipo()
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoTipoBusquedaIngresoMercancias = DB.IngresoMercanciasTipoDescripcion.ToList();
                return ListadoTipoBusquedaIngresoMercancias;
            }
        }
        public List<SelectListItem> GrupoCliente()
        {
            List<SelectListItem> lst2 = new List<SelectListItem>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoModelos = from d in DB.GrupoClientes
                                     select new
                                     {
                                         d.GrupoClienteId,
                                         d.GroupCode,
                                         d.GroupName
                                     };

                foreach (var x in ListadoModelos)
                {

                    lst2.Add(new SelectListItem()
                    {
                       Text = x.GroupName,
                       Value = Convert.ToString(x.GroupCode)
                    });
                }
                return lst2;
            }
        }

        public string ControlCambios()
        {
            string contrasena="";

            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoControl = DB.ControlCambios.ToList();
                foreach (var x in ListadoControl)
                {
                    contrasena = x.Pass;
                }
                    return contrasena;
            }
        }

        public bool IngresarModelos(string Nombre, decimal Peso)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var modelos = new Modelos();
                    modelos.Nombre = Nombre;
                    modelos.PesoTeorico = Peso;

                    DB.Modelos.Add(modelos);
                    DB.SaveChanges();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            return true;
        }

        public List<CorreoElectronicoEnviosReportes> ConsultarCorreoElectronico()
        {
                List<CorreoElectronicoEnviosReportes> lst2 = new List<CorreoElectronicoEnviosReportes>();
                using (DacarProsoftEntities DB = new DacarProsoftEntities())
                {
                    var ListadoCorreo = (from d in DB.CorreoElectronicoEnviosReportes
                                        select new
                                         {
                                             d.DireccionCorreo,
                                             d.ClaveCorreo,
                                             d.Estado
                                         }).FirstOrDefault();

                lst2.Add(new CorreoElectronicoEnviosReportes()
                {
                    DireccionCorreo = ListadoCorreo.DireccionCorreo,
                    ClaveCorreo = ListadoCorreo.ClaveCorreo
                });

                //foreach (var x in ListadoCorreo)
                //    {
                //    if (x.Estado==true) {
                //        lst2.Add(new CorreoElectronicoEnviosReportes()
                //        {
                //            DireccionCorreo = x.DireccionCorreo,
                //            ClaveCorreo = x.ClaveCorreo
                //        });
                //    }            
                //    }
                    return lst2;
                }                  
        }

    }
}