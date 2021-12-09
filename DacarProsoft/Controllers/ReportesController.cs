using DacarProsoft.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DacarProsoft.Controllers
{
    public class ReportesController : Controller
    {
        private DaoUtilitarios daoUtilitarios { get; set; } = null;
        private DaoReportes daoReportes { get; set; } = null;
        private DaoGarantias daoGarantias { get; set; } = null;
        private DaoUsuarios daoUsuarios { get; set; } = null;


        // GET: Reportes
        public ActionResult ReporteGeneralVentasExterior()
        {
            if (Session["usuario"] != null)
            {
                ViewBag.JavaScript = "General/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
                ViewBag.dxdevweb = "1";

                daoUtilitarios = new DaoUtilitarios();
                daoReportes = new DaoReportes();


                ViewBag.MenuAcceso = Session["Menu"];

                var datMenu = daoUtilitarios.ConsultarMenuPrincipal();
                ViewBag.MenuPrincipal = datMenu;
                var datMenuOpciones = daoUtilitarios.ConsultarMenuOpciones();
                ViewBag.MenuOpciones = datMenuOpciones;
                var datSubMenuOpciones = daoUtilitarios.ConsultarSubMenuOpciones();
                ViewBag.SubMenuOpciones = datSubMenuOpciones;

                var SestadoPedidos = daoReportes.StatusPedidos();
                ViewBag.EstadosPedido = SestadoPedidos;

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public JsonResult ReporteGeneralPedidos(int Tipo, DateTime FechaInicio, DateTime FechaFin )
        {
            try
            {
                daoReportes = new DaoReportes();
                var Result = daoReportes.ReporteGeneralPedidosExterior(Tipo, FechaInicio,FechaFin);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public ActionResult ReporteDeControl()
        {
            if (Session["usuario"] != null)
            {
                ViewBag.JavaScript = "General/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
                ViewBag.dxdevweb = "1";

                daoUtilitarios = new DaoUtilitarios();
                daoReportes = new DaoReportes();


                ViewBag.MenuAcceso = Session["Menu"];

                var datMenu = daoUtilitarios.ConsultarMenuPrincipal();
                ViewBag.MenuPrincipal = datMenu;
                var datMenuOpciones = daoUtilitarios.ConsultarMenuOpciones();
                ViewBag.MenuOpciones = datMenuOpciones;
                var datSubMenuOpciones = daoUtilitarios.ConsultarSubMenuOpciones();
                ViewBag.SubMenuOpciones = datSubMenuOpciones;

                var SestadoPedidos = daoReportes.StatusPedidos();
                ViewBag.EstadosPedido2 = SestadoPedidos;

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public ActionResult ReporteDeGarantias()
        {
            if (Session["usuario"] != null)
            {
                ViewBag.JavaScript = "General/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
                ViewBag.dxdevweb = "1";

                daoUtilitarios = new DaoUtilitarios();
                daoReportes = new DaoReportes();


                ViewBag.MenuAcceso = Session["Menu"];

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

        public JsonResult ReporteGeneralDeControl(int Tipo, DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                daoReportes = new DaoReportes();
                var Result = daoReportes.ReporteGeneralDeControl(Tipo, FechaInicio, FechaFin);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public JsonResult ReporteGeneralDeGarantias(/*DateTime FechaInicio, DateTime FechaFin*/)
        {
            try
            {
                daoGarantias = new DaoGarantias();
                var Result = daoGarantias.ReporteGeneralPedidosExterior(/*FechaInicio, FechaFin*/);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public ActionResult ReporteAnalisisGarantias()
        {
            if (Session["usuario"] != null)
            {

                ViewBag.JavaScript = "General/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
                ViewBag.dxdevweb = "1";

                ViewBag.MenuAcceso = Session["Menu"];

                daoUtilitarios = new DaoUtilitarios();
                daoReportes = new DaoReportes();

                var datFiltroGarantia= daoReportes.ConsultarFiltrosCategoriaGarantias();
                ViewBag.FiltroBusqueda = datFiltroGarantia;
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
        public JsonResult ReporteAnalisisDeGarantias(int Filtro, DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                daoReportes = new DaoReportes();
                if (Filtro==1) {
                    var Result = daoReportes.ReporteAnalisisGarantiaPorCausales(FechaInicio, FechaFin);
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
                if (Filtro == 2) {
                    var Result = daoReportes.ReporteAnalisisGarantiaPorMeses(FechaInicio, FechaFin);
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
                if (Filtro == 3)
                {
                    var Result = daoReportes.ReporteAnalisisGarantiaPorArea(FechaInicio, FechaFin);
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
                if (Filtro == 4)
                {
                    var Result = daoReportes.ReporteAnalisisGarantiaPorModelo(FechaInicio, FechaFin);
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
                if (Filtro == 5)
                {
                    var Result = daoReportes.ReporteAnalisisGarantiaPorAplicacion(FechaInicio, FechaFin);
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }


                else {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public ActionResult ReporteAnalisisGarantiasPorAnio()
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
        public JsonResult ReporteAnalisisDeGarantiasPorAnio1(string Anio)
        {
            try
            {
                daoReportes = new DaoReportes();
                       
                    var Result = daoReportes.ReporteAnalisisGarantiaPorAnio1(Convert.ToInt32(Anio));
                    return Json(Result, JsonRequestBehavior.AllowGet);
                

             
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ReporteAnalisisDeGarantiasPorAnio2(string Anio)
        {
            try
            {
                daoReportes = new DaoReportes();

                var Result = daoReportes.ReporteAnalisisGarantiaPorAnio2(Convert.ToInt32(Anio));
                return Json(Result, JsonRequestBehavior.AllowGet);



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public JsonResult ReporteDetalleGarantiaPorCausales(string Anio, string Mes)
        {
            try
            {
                daoReportes = new DaoReportes();

                var Result = daoReportes.ReporteDetalleAnalisisCausalesMeses(Convert.ToInt32(Anio),Mes);
                return Json(Result, JsonRequestBehavior.AllowGet);



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ReporteDetalleGarantiaPorModelo(string Anio, string Mes)
        {
            try
            {
                daoReportes = new DaoReportes();

                var Result = daoReportes.ReporteDetalleAnalisisModelosMeses(Convert.ToInt32(Anio), Mes);
                return Json(Result, JsonRequestBehavior.AllowGet);



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        


        public ActionResult ReporteAnalisisGarantiasPorCliente()
        {
            if (Session["usuario"] != null)
            {

                ViewBag.JavaScript = "General/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
                ViewBag.dxdevweb = "1";

                ViewBag.MenuAcceso = Session["Menu"];

                daoUtilitarios = new DaoUtilitarios();

                daoUsuarios = new DaoUsuarios();

                var datClientesSap = daoUsuarios.ConsutaClientesSap();
                ViewBag.ClientesSap = datClientesSap;

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


        public JsonResult ReporteDetalleGarantiaPorNombreClienteMeses(string NombreCliente, string AnioConsulta)
        {
            try
            {
                daoReportes = new DaoReportes();

                var Result = daoReportes.ReporteAnalisisGarantiaPorNombreClienteMeses(NombreCliente, Convert.ToInt32(AnioConsulta));
                return Json(Result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ReporteDetalleGarantiaPorMesesPorCliente(string Anio, string Mes, string Cliente)
        {
            try
            {
                daoReportes = new DaoReportes();

                var Result = daoReportes.ReporteDetalleAnalisisCausalesMesesPorCliente(Convert.ToInt32(Anio), Mes, Cliente);
                return Json(Result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public JsonResult ReporteDetalleGarantiaPorMesesPorClientePorModelo(string Anio, string Mes, string Cliente)
        {
            try
            {
                daoReportes = new DaoReportes();

                var Result = daoReportes.ReporteDetalleAnalisisModelosMesesPorCliente(Convert.ToInt32(Anio), Mes, Cliente);
                return Json(Result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public ActionResult ReporteAnalisisGarantiasPorTipoCliente()
        {
            if (Session["usuario"] != null)
            {

                ViewBag.JavaScript = "General/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
                ViewBag.dxdevweb = "1";

                ViewBag.MenuAcceso = Session["Menu"];

                daoUtilitarios = new DaoUtilitarios();
                daoReportes = new DaoReportes();

                var datGrupoCliente = daoReportes.ListadoGrupoDeCliente();
                ViewBag.GrupoCliente = datGrupoCliente;

                var datClienteLinea = daoReportes.ListadoClienteLinea();
                ViewBag.ClienteLinea = datClienteLinea;

                var datClienteClase = daoReportes.ListadoClienteClase();
                ViewBag.ClienteClase = datClienteClase;

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

        public JsonResult ReporteDetalleGarantiaPorTipoDeCliente(string tipoCliente, string ClienteClase, string ClienteLinea, string Anio)
        {
            try
            {
                daoReportes = new DaoReportes();

                var Result = daoReportes.ReporteDetalleAnalisisModelosPorTipoCliente(tipoCliente, ClienteClase, ClienteLinea, Convert.ToInt32(Anio));
                return Json(Result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}