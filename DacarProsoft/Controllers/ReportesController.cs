using DacarProsoft.Datos;
using DacarProsoft.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public JsonResult ReporteIdGarantia(int Id)
        {
            try
            {
                daoGarantias = new DaoGarantias();
                var Result = daoGarantias.ReporteIdGarnatia(Id);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult LineaMarca(/*DateTime FechaInicio, DateTime FechaFin*/)
        {
            try
            {
                daoGarantias = new DaoGarantias();
                var Result = daoGarantias.PieLinea();
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult LineaMarcaActualizacion(List<GarantiaDetalle>Linea)
        {
            try
            {
               
                
                daoGarantias = new DaoGarantias();
                var Result = daoGarantias.PieLineaActualizacion(Linea);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public JsonResult MesesFiltro(List<GarantiaDetalle> Meses)
        {
            try
            {
                

                var groups = Meses.GroupBy(n => new { n.Mes, n.Anio })
                         .Select(n => new
                         {  MesNum = DateTime.ParseExact(n.Key.Mes, "MMMM", CultureInfo.CurrentCulture).Month,
                             Mes = n.Key.Mes,
                             Anio = n.Key.Anio,
                             Total = n.Count()
                         })
                         .OrderBy(n => n.MesNum).ThenByDescending(n=> n.Anio).ToList();
               
                return Json(groups, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public JsonResult Provincias()
        {
            try
            {
                daoGarantias = new DaoGarantias();
                var Result = daoGarantias.Provincia();
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ProvinciasActualizacion(List<GarantiaDetalle> Provincias)
        {
            try
            {

                Provincias lst = new Provincias();

                var Listado = (from d in Provincias

                               select new
                               {
                                   d.Provincia,
                               }).ToList();

                var pl = (from r in Listado
                          orderby r.Provincia
                          group r by r.Provincia into grp
                          select new { key = grp.Key, cnt = grp.Count() }).ToList();

                foreach (var item in pl)
                {
                    if (item.key == "Azuay")
                    {
                        lst.az = item.cnt;
                    }
                    if (item.key == "Bolívar")
                    {
                        lst.bo = item.cnt;
                    }
                    if (item.key == "Cañar")
                    {
                        lst.cn = item.cnt;
                    }
                    if (item.key == "Carchi")
                    {
                        lst.cr = item.cnt;
                    }
                    if (item.key == "Cotopaxi")
                    {
                        lst.ct = item.cnt;
                    }
                    if (item.key == "Chimborazo")
                    {
                        lst.cb = item.cnt;
                    }
                    if (item.key == "El Oro")
                    {
                        lst.eo = item.cnt;
                    }
                    if (item.key == "Esmeraldas")
                    {
                        lst.es = item.cnt;
                    }
                    if (item.key == "Guayas")
                    {
                        lst.gu = item.cnt;
                    }
                    if (item.key == "Imbabura")
                    {
                        lst.im = item.cnt;
                    }
                    if (item.key == "Loja")
                    {
                        lst.lj = item.cnt;
                    }
                    if (item.key == "Los Rios")
                    {
                        lst.lr = item.cnt;
                    }
                    if (item.key == "Manabi")
                    {
                        lst.mn = item.cnt;
                    }
                    if (item.key == "Morona Santiago")
                    {
                        lst.ms = item.cnt;
                    }
                    if (item.key == "Napo")
                    {
                        lst.numero = item.cnt;
                    }
                    if (item.key == "Pastaza")
                    {
                        lst.pa = item.cnt;
                    }
                    if (item.key == "Pichincha")
                    {
                        lst.pi = item.cnt;
                    }
                    if (item.key == "Tungurahua")
                    {
                        lst.tu = item.cnt;
                    }
                    if (item.key == "Zamora Chinchipe")
                    {
                        lst.zc = item.cnt;
                    }
                    if (item.key == "Galápagos")
                    {
                        lst.ga = item.cnt;
                    }
                    if (item.key == "Sucumbíos")
                    {
                        lst.su = item.cnt;
                    }
                    if (item.key == "Orellana")
                    {
                        lst.na = item.cnt;
                    }
                    if (item.key == "Santo Domingo de Los Tsáchilas")
                    {
                        lst.sd = item.cnt;
                    }
                    if (item.key == "Santa Elena")
                    {
                        lst.se = item.cnt;
                    }
                }
                return Json(lst, JsonRequestBehavior.AllowGet);
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
        public JsonResult ReporteDetalleGarantiaPorMesesPorTipoCliente(string tipoCliente, string ClienteClase, string ClienteLinea, string Anio, string Mes)
        {
            try
            {
                daoReportes = new DaoReportes();

                var Result = daoReportes.ReporteDetalleAnalisisCausalesMesesPorTipoCliente( tipoCliente,  ClienteClase,  ClienteLinea,  Convert.ToInt32(Anio),  Mes);
                return Json(Result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public JsonResult ReporteDetalleGarantiaPorMesesPorTipoClientePorModelo(string tipoCliente, string ClienteClase, string ClienteLinea, string Anio, string Mes)
        {
            try
            {
                daoReportes = new DaoReportes();

                var Result = daoReportes.ReporteDetalleAnalisisCausalesMesesPorTipoClientePorModelo(tipoCliente, ClienteClase, ClienteLinea, Convert.ToInt32(Anio), Mes);
                return Json(Result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public ActionResult ReporteAnalisisGarantiasPorDetalleFinal()
        {
            if (Session["usuario"] != null)
            {

                ViewBag.JavaScript = "General/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
                ViewBag.dxdevweb = "1";

                ViewBag.MenuAcceso = Session["Menu"];

                daoUtilitarios = new DaoUtilitarios();
                daoReportes = new DaoReportes();

                var Meses = daoReportes.Meses();
                var Area = daoReportes.AreaResponsable();
                var TipoBateria = daoReportes.TipoBateria();
                var Causales = daoReportes.Causales();
                var GrupoBateria = daoReportes.GrupoBateria();

                ViewBag.Meses = Meses;
                ViewBag.Area = Area;
                ViewBag.TipoBateria = TipoBateria;
                ViewBag.Causales = Causales;
                ViewBag.GrupoBateria = GrupoBateria;

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

        public JsonResult ReporteDetalleGeneralGarantias(string FechaAnalisis, string MesAnalisis, string AreaResponsable, string TipoBateria, string Causales, string GrupoBateria)
        {
            try
            {
                daoReportes = new DaoReportes();

                var Result = daoReportes.ReporteAnalisisGarantiaDetalleGeneral(Convert.ToInt32(FechaAnalisis), Convert.ToInt32(FechaAnalisis)-1, MesAnalisis, AreaResponsable, Causales, GrupoBateria);
                return Json(Result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


        public JsonResult PivotDeAnalisisGarantiasAnios(string anio1, string anio2)
        {
            try
            {
                daoReportes = new DaoReportes();

                var Result = daoReportes.ReportePivotDeAnalisisGarantiasAnios(Convert.ToInt32(anio1), Convert.ToInt32(anio2));
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