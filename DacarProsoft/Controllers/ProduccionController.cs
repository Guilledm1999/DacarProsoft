using DacarProsoft.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DacarProsoft.Controllers
{
    public class ProduccionController : Controller
    {
        private DaoUtilitarios daoUtilitarios { get; set; } = null;
        private DaoProduccion daoProduccion { get; set; } = null;
        // GET: Produccion
        public ActionResult AnalisisRegistroGarantias()
        {
            if (Session["usuario"] != null)
            {

                ViewBag.JavaScript = "General/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
                ViewBag.dxdevweb = "1";

                ViewBag.MenuAcceso = Session["Menu"];

                daoUtilitarios = new DaoUtilitarios();
                daoProduccion = new DaoProduccion();
                var datCausales = daoProduccion.ConsultarCausalesGarantia();
                ViewBag.Causales = datCausales;

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
        public JsonResult ConsultarRegistrosGarantias() {

            daoProduccion = new DaoProduccion();
            var Result = daoProduccion.ConsultarRegistrosGarantias();

            return Json(Result,JsonRequestBehavior.AllowGet);
        
        }

        public string ConsultarAreaResponsable(int CodidoArea)
        {

            daoProduccion = new DaoProduccion();
            var Result = daoProduccion.ConsultarAreaResponsable(CodidoArea);

            return Result;

        }

        public bool RegistrarAnalisisDeGarantias(int IngresoRevisionGarantiaId, string LoteFabricacion, string LoteEnsamble, string LoteCarga, string ModeloBateria, decimal Voltaje, decimal? CCA,
            decimal? DencidadCelda1, decimal? DencidadCelda2, decimal? DencidadCelda3, decimal? DencidadCelda4, decimal? DencidadCelda5, decimal? DencidadCelda6, string ResumenAnalisis, string AreaResponsable,
            string Observaciones)
        {
            decimal valor1 = 0;
            decimal valor2 = 0;
            decimal valor3 = 0;
            decimal valor4 = 0;
            decimal valor5 = 0;
            decimal valor6 = 0;
            decimal valor7 = 0;

            if (CCA!=null) {
                valor1 = Convert.ToDecimal(CCA);
            }
            if (DencidadCelda1 != null)
            {
                valor2 = Convert.ToDecimal(DencidadCelda1);
            }
            if (DencidadCelda2 != null)
            {
                valor3 = Convert.ToDecimal(DencidadCelda2);
            }
            if (DencidadCelda3 != null)
            {
                valor4 = Convert.ToDecimal(DencidadCelda3);
            }
            if (DencidadCelda4 != null)
            {
                valor5 = Convert.ToDecimal(DencidadCelda4);
            }
            if (DencidadCelda5 != null)
            {
                valor6 = Convert.ToDecimal(DencidadCelda5);
            }
            if (DencidadCelda6 != null)
            {
                valor7 = Convert.ToDecimal(DencidadCelda6);
            }
            daoProduccion = new DaoProduccion();

             var Result = daoProduccion.RegistrarAnalisisGarantia( IngresoRevisionGarantiaId,  LoteFabricacion,  LoteEnsamble,  LoteCarga,  ModeloBateria,  Voltaje,  valor1,
             valor2, valor3, valor4, valor5, valor6, valor7,  ResumenAnalisis,  AreaResponsable,
             Observaciones);

            return Result;
        }
    }
}