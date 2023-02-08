using DacarDatos.Datos;
using DacarProsoft.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Datos
{
    public class DaoMenu
    {
        public List<Menus> ConsultarMenu()
        {

            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoMenus = DB.Menus.ToList();
                return ListadoMenus;
            }

        }
        public List<MenuOpciones> ConsultarSubmenu()
        {

            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadosubMenus = DB.MenuOpciones.ToList();
                return ListadosubMenus;
            }

        }
        public List<MenuAcceso> AccesosMenu(int tipoUsuario)
        {
            List<MenuAcceso> listmenu = new List<MenuAcceso>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadosubMenus = from d in DB.Accesos
                                      where d.TipoUsuarioId == tipoUsuario
                                      select new
                                      {
                                          d.MenuId,


                                      };
                foreach (var x in ListadosubMenus)
                {
                    listmenu.Add(new MenuAcceso
                    {
                        MenuId = x.MenuId.Value,

                    });

                }
                return listmenu;

            }
        }

        public List<MenuAcceso> AccesosMenu2(int tipoUsuario)
        {
            List<MenuAcceso> listmenu = new List<MenuAcceso>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadosubMenus = (from d in DB.Accesos join f in DB.Menus on d.MenuId equals f.MenuId
                                      where  d.TipoUsuarioId == tipoUsuario && d.MenuId==f.MenuId 
                                      select new 
                                      {
                                          d.MenuId,
                                          d.Estado,
                                          f.DescripcionMenu,
                                          f.EstadoMenu
                                      });
                foreach (var x in ListadosubMenus)
                {
                    listmenu.Add(new MenuAcceso
                    {
                        MenuId = x.MenuId.Value,
                        Estado=x.Estado.Value,
                        Descripcion=x.DescripcionMenu,
                        EstadoMenu=x.EstadoMenu.Value
                    });

                }
                return listmenu;

            }
        }

        public bool RevisarDiasContrasena(string Usuario)
        {
            
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                bool respuesta = false;
                var TiempoContrasena = DB.SeguridadUsuario.Where(x=> x.Estado=="A").Select(x => x.CambioContrasenaDias).FirstOrDefault();
                if (TiempoContrasena!=null)
                {
                    int IdUsuario = DB.Usuarios.Where(x => x.NombreUsuario == Usuario).Select(x => x.IdUsuario).FirstOrDefault();
                    DateTime FechaContrasena = DB.Contrasena.Where(x => x.IdUsuario == IdUsuario && x.Estado == "A").Select(x => x.FechaCreacion).FirstOrDefault();
                    var dias = ((DateTime.Now - FechaContrasena).TotalHours) / 24;
                    if (dias >= TiempoContrasena)
                    {
                        respuesta = true;
                    }
                    if (TiempoContrasena == -1)
                    {
                        respuesta = false;
                    }
                }


                return respuesta;

            }
        }
    }
}