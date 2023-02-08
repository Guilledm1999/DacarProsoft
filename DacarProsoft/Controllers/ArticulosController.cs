using DacarDatos.Datos;
using DacarProsoft.Datos;
using DacarProsoft.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace DacarProsoft.Controllers
{
    public class ArticulosController : Controller
    {

        private DaoArticulos daoArticulos { get; set; } = null;
        private DaoUtilitarios daoUtilitarios { get; set; } = null;


        [HttpGet]
        public ActionResult ConsultarArticulos()
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

                daoArticulos = new DaoArticulos();
            var dat = daoArticulos.ConsultarCategoria();
            ViewBag.articulos = dat;

            return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }


        [HttpGet]
        public ActionResult InventarioBaterias()
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
                daoArticulos = new DaoArticulos();
                var dat = daoArticulos.ConsultarCategoria();
                ViewBag.articulos = dat;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpGet]
        public ActionResult InventarioBateriasAlmacenes()
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
                daoArticulos = new DaoArticulos();
          //      var dat = daoArticulos.ConsultarCategoria();
               // ViewBag.articulos = dat;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public JsonResult ConsultaDeArticulos()
        {
            try
            {
                daoArticulos = new DaoArticulos();
                var Result = daoArticulos.ConsultarListaArticulos();
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex )
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public JsonResult ConsultaBateriasPlanta()
        {
            try
            {
                daoArticulos = new DaoArticulos();
                var Result = daoArticulos.ConsultarListaBateriasPlanta();
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ConsultaBateriasAlmacenes()
        {
            try
            {
                daoArticulos = new DaoArticulos();
                var Result = daoArticulos.ConsultarListaBateriasAlmacenes();
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        //[HttpPost]
        //public JsonResult GuardarArticulosPedidos(ArticulosPedidos ap)
        //{

        //    daoArticulos = new DaoArticulos();
        //    bool crearArticulo = daoArticulos.ingresarArticulo(Convert.ToInt32(ap.ClienteId), ap.CodigoArticulo, ap.DescripcionArticulo, ap.Categoria, ap.SubCategoria, Convert.ToInt32(ap.Cantidad));
        //    return Json(crearArticulo, JsonRequestBehavior.AllowGet);

        //}

        [HttpPost]
        public ActionResult Verify(Articulos acc)
        {
            
            return null;
        }

        [HttpGet]
        public ActionResult ConsultarItemsChatarra()
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

        [HttpGet]
        public JsonResult ConsultarPesosChatarra()
        {
            daoArticulos = new DaoArticulos();

            var art = daoArticulos.ConsultarPesos();
            return Json(art, JsonRequestBehavior.AllowGet);

        }
    }
}