using DacarProsoft.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DacarProsoft.Controllers
{
    public class CalidadController : Controller
    {
        private DaoUtilitarios daoUtilitarios { get; set; } = null;
        private DaoCalidad daoCalidad { get; set; } = null;

        // GET: Calidad
        public ActionResult RegistrosVisuaLCN()
        {
            if (Session["usuario"] != null)
            {
                ViewBag.JavaScript = "General/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
                ViewBag.dxdevweb = "1";

                ViewBag.MenuAcceso = Session["Menu"];

                //ConexionAccess conexion = new ConexionAccess();

                daoCalidad = new DaoCalidad();
                daoUtilitarios = new DaoUtilitarios();

                var datMenu = daoUtilitarios.ConsultarMenuPrincipal();
                ViewBag.MenuPrincipal = datMenu;
                var datMenuOpciones = daoUtilitarios.ConsultarMenuOpciones();
                ViewBag.MenuOpciones = datMenuOpciones;
                var datSubMenuOpciones = daoUtilitarios.ConsultarSubMenuOpciones();
                ViewBag.SubMenuOpciones = datSubMenuOpciones;
                var dat = daoUtilitarios.ConsultarEstadosDeOrdenesProduccion();
                ViewBag.EstadoOrden = dat;

                var baseDatos = daoCalidad.ConsultarBaseDeDatos();
                ViewBag.BaseDatos = baseDatos;

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public JsonResult ConsultarRegistrosLcn(string nombre)
        {
            try
            {
                ConexionAccess conexion = new ConexionAccess();
                var conexionAcc= conexion.open(nombre);

                if (conexionAcc == true)
                {

                    var result = conexion.ConsultarFechasBaseDeDatos();
                    conexion.close();
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;       
            }
        }
        public JsonResult ConsultarDetalleRegistrosLcn(string nombreBase, int testUnique)
        {
            try
            {
                ConexionAccess conexion = new ConexionAccess();
                var conexionAcc = conexion.open(nombreBase);

                if (conexionAcc == true)
                {

                    var result = conexion.ConsultarDetalleRegistrosLcnAccess(testUnique);
                    conexion.close();
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}