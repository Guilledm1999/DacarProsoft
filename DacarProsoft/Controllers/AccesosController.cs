using DacarProsoft.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DacarProsoft.Controllers
{
    public class AccesosController : Controller
    {
        private DaoUtilitarios daoUtilitarios { get; set; } = null;

        private DaoMenu daoMenu { get; set; } = null;
        // GET: Accesos
        public ActionResult Accesos()
        {
            if (Session["usuario"] != null)
            {
                ViewBag.JavaScript = "General/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
                ViewBag.dxdevweb = "1";

                ViewBag.MenuAcceso = Session["Menu"];
                daoUtilitarios = new DaoUtilitarios();
              

                var datMenu = daoUtilitarios.ConsultarMenuPrincipal();
                ViewBag.MenuPrincipal = datMenu;
                var datMenuOpciones = daoUtilitarios.ConsultarMenuOpciones();
                ViewBag.MenuOpciones = datMenuOpciones;
                var datSubMenuOpciones = daoUtilitarios.ConsultarSubMenuOpciones();
                ViewBag.SubMenuOpciones = datSubMenuOpciones;

                var datListMenuOp = daoUtilitarios.TipoMenu();
                ViewBag.ListMenuOp = datListMenuOp;
                var datListTipoUsu = daoUtilitarios.TipoUsuario();
                ViewBag.ListUsuarioTip = datListTipoUsu;

                var grupoClientes = daoUtilitarios.GrupoCliente();
                ViewBag.gruposClientes = grupoClientes;

             
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public JsonResult ConsultaAccesos()
        {
            try
            {
                daoUtilitarios = new DaoUtilitarios();
                var Result = daoUtilitarios.ConsultarAccesoMenu();
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpPost]
        public bool EliminarAcceso(int idAcceso)
        {
            try
            {
                daoUtilitarios = new DaoUtilitarios();
                var Result = daoUtilitarios.EliminarAcceso(idAcceso);
                return Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        [HttpPost]
        public bool AgregarAcceso(string tipoUsuario,string tipoMenu,string estado)
        {
            try
            {
                daoUtilitarios = new DaoUtilitarios();

                var comprobar = daoUtilitarios.ConsultarExistenciaAcceso(Convert.ToInt32(tipoUsuario), Convert.ToInt32(tipoMenu), Convert.ToInt32(estado));
                if (comprobar == false)
                {
                    var Result = daoUtilitarios.ingresarAcceso(tipoUsuario, tipoMenu, estado);
                    return Result;
                }
                else {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpPost]
        public bool ActualizarAcceso(string idAcceso,string estado)
        {
            try
            {
                daoUtilitarios = new DaoUtilitarios();

                var actualizar = daoUtilitarios.ActualizarAccesos(Convert.ToInt32(idAcceso), Convert.ToInt32(estado));              
                return actualizar;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}