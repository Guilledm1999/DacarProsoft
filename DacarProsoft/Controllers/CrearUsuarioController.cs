using DacarDatos.Datos;
using DacarProsoft.Datos;
using DacarProsoft.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DacarProsoft.Controllers
{
    public class CrearUsuarioController : Controller
    {        
        private DaoUsuarios daousuario { get; set; } = null;
        private DaoUtilitarios daoUtilitarios { get; set; } = null;


        // GET: CrearUsuario
        [HttpGet]
        public ActionResult CrearUsuarios()
        {

            if (Session["usuario"] != null)
            {
                ViewBag.JavaScript = "General/"+ RouteData.Values["controller"] + "/" + RouteData.Values["action"];
                ViewBag.dxdevweb = "1";

                ViewBag.MenuAcceso = Session["Menu"];

                daoUtilitarios = new DaoUtilitarios();
                var datMenu = daoUtilitarios.ConsultarMenuPrincipal();
                ViewBag.MenuPrincipal = datMenu;
                var datMenuOpciones = daoUtilitarios.ConsultarMenuOpciones();
                ViewBag.MenuOpciones = datMenuOpciones;
                var datSubMenuOpciones = daoUtilitarios.ConsultarSubMenuOpciones();
                ViewBag.SubMenuOpciones = datSubMenuOpciones;
                daousuario = new DaoUsuarios();
                var dat = daousuario.tipoUsuario();
                ViewBag.usuario = dat;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }

        [HttpPost]
        public JsonResult GuardarModificarUsuarios(Account acc)
        {
            
            daousuario = new DaoUsuarios();
            bool crearUsuario = daousuario.ingresarUsuarios(acc.NombreCompleto, acc.NombreUsuario, acc.Contrasena, Int32.Parse(acc.TipoUsuario));
            return Json(crearUsuario, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult ActualizarUsuarios(int IdUsuario, string NombreCompleto,string NombreUsuario,string contrasena ,int TipoUsuario)
        {

            daousuario = new DaoUsuarios();
            bool crearUsuario = daousuario.ActualizacionUsuarios(IdUsuario, NombreCompleto, NombreUsuario, contrasena, TipoUsuario);
            return Json(crearUsuario, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult EliminarUsuarios(int UserId)
        {

            daousuario = new DaoUsuarios();
            bool crearUsuario = daousuario.EliminarUsuarios(UserId);
            return Json(crearUsuario, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ConsultarUsuarios()
        {
            try
            {
                daousuario = new DaoUsuarios();
                var Result = daousuario.ConsultarUsuarios();
                return Json(Result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {

                throw;
            }
         

        }

        [HttpGet]
        public ActionResult CrearUsuariosPortal()
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
              
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }

        public JsonResult ConsultarClientesSap()
        {
            try
            {
                daousuario = new DaoUsuarios();
                var Result = daousuario.ConsutaClientesSap();
                return Json(Result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool IngresarUsuarioClientesSap(string NombreCliente, string Usuario, string Clave, string Referencia, bool validacion)
        {
            try
            {
                daousuario = new DaoUsuarios();
                var Result = daousuario.ingresarUsuariosPortal(NombreCliente,Usuario,Clave,Referencia, validacion);
                return Result;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpGet]
        public ActionResult ConsultarUsuariosPortal()
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

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
        public JsonResult ConsultarUsuariosRegistradosPortal()
        {
            try
            {
                daousuario = new DaoUsuarios();
                var Result = daousuario.ConsutaUsuariosRegistradosPortal();
                return Json(Result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public JsonResult EliminarUsuariosPortal(int UserId)
        {

            daousuario = new DaoUsuarios();
            bool crearUsuario = daousuario.EliminarUsuariosPortal(UserId);
            return Json(crearUsuario, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public bool ActualizarUsuariosPortal(int IdUsuarioPortal, string Usuario, string Clave, string Tipo, bool Validacion)
        {

            daousuario = new DaoUsuarios();
            bool actualizar = daousuario.ActualizacionUsuariosPortal(IdUsuarioPortal, Usuario, Clave, Tipo, Validacion);
            return actualizar;

        }
    }
}