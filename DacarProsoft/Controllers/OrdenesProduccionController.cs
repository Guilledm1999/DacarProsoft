using DacarProsoft.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DacarProsoft.Controllers
{

    public class OrdenesProduccionController : Controller
    {
        private DaoOrdenesDeFabricacion daoOrdenesFabricacion { get; set; } = null;

        private DaoUtilitarios daoUtilitarios { get; set; } = null;


        // GET: OrdenesDeProduccion
        public ActionResult OrdenesDeProduccion()
        {
            if (Session["usuario"] != null)
            {
                ViewBag.JavaScript = "General/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
            ViewBag.dxdevweb = "1";

                ViewBag.MenuAcceso = Session["Menu"];

                //ConexionAccess conexion=new ConexionAccess();

                //conexion.open("27-75.mdb");
                //conexion.AccessData_O();
                //conexion.close();
                daoUtilitarios = new DaoUtilitarios();
                var datMenu = daoUtilitarios.ConsultarMenuPrincipal();
                ViewBag.MenuPrincipal = datMenu;
                var datMenuOpciones = daoUtilitarios.ConsultarMenuOpciones();
                ViewBag.MenuOpciones = datMenuOpciones;
                var datSubMenuOpciones = daoUtilitarios.ConsultarSubMenuOpciones();
                ViewBag.SubMenuOpciones = datSubMenuOpciones;
                var dat = daoUtilitarios.ConsultarEstadosDeOrdenesProduccion();
            ViewBag.EstadoOrden = dat;
            return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public JsonResult ConsultaDeOrdenesDeFabricacion()
        {
            try
            {
                daoOrdenesFabricacion = new DaoOrdenesDeFabricacion();
                var Result = daoOrdenesFabricacion.ListadoOrdenesFabricacion();
                var json = Json(Result, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = 50000000;
                return json;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ConsultaDeOrdenesDeFabricacionPorEstado(String estado)
        {
            try
            {
                daoOrdenesFabricacion = new DaoOrdenesDeFabricacion();
                var Result = daoOrdenesFabricacion.ListadoOrdenesFabricacionPorEstado(estado);
                var json = Json(Result, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = 50000000;
                return json;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }


}