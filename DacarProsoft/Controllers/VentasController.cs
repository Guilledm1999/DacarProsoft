using DacarProsoft.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DacarProsoft.Controllers
{


    public class VentasController : Controller
    {
        private DaoVentas daoVentas { get; set; } = null;
        private DaoUtilitarios daoUtilitarios { get; set; } = null;



        // GET: Ventas
        [HttpGet]
        public ActionResult InformeVentas()
        {
            if (Session["usuario"] != null)
            {
                ViewBag.JavaScript = "General/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
            ViewBag.dxdevweb = "1";

                ViewBag.MenuAcceso = Session["Menu"];


            daoVentas = new DaoVentas();
            daoUtilitarios = new DaoUtilitarios();
            var datMenu = daoUtilitarios.ConsultarMenuPrincipal();
            ViewBag.MenuPrincipal = datMenu;
            var datMenuOpciones = daoUtilitarios.ConsultarMenuOpciones();
            ViewBag.MenuOpciones = datMenuOpciones;
                var datSubMenuOpciones = daoUtilitarios.ConsultarSubMenuOpciones();
                ViewBag.SubMenuOpciones = datSubMenuOpciones;

                var dat = daoVentas.ConsultarAniosVentas();
            var dat2 = daoVentas.ConsultarMesesVentas();
          

            ViewBag.anios = dat;
            ViewBag.meses = dat2;

            return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public JsonResult ConsultaDeVentas()
        {
            try
            {
                daoVentas = new DaoVentas();
                var Result = daoVentas.ListadoVentasTotales();


                var json = Json(Result, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = 50000000;
                return json;

                //return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ConsultaDeVentasPorAnio(String anio, String mes)
        {
            var json = new JsonResult();
            try
            {        
                daoVentas = new DaoVentas();
                if (mes!=null&&mes=="--Todos--") {
                    var Result = daoVentas.ListadoVentasPorAnio(anio, mes);
                    //var Result = daoVentas.ListadoVentasPorMeses(anio, mes);

                    json = Json(Result, JsonRequestBehavior.AllowGet);
                    json.MaxJsonLength = 555000000;
                }
                else
                {
                    var Result = daoVentas.ListadoVentasPorMeses(anio, mes);

                    json = Json(Result, JsonRequestBehavior.AllowGet);
                    json.MaxJsonLength = 555000000;

                }
               
                return json;

                //return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

    }
}