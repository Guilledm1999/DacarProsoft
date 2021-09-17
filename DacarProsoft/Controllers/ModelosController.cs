using DacarProsoft.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DacarProsoft.Controllers
{
    public class ModelosController : Controller
    {
        private DaoUtilitarios daoUtilitarios { get; set; } = null;
        private DaoChatarra daoChatarra { get; set; } = null;
        private DaoIngresoMercanciasSap daoIngresoMercancias { get; set; } = null;



        // GET: Modelos
        public ActionResult IngresoModelos()
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


        public JsonResult ConsultaDetalleChatarra()
        {
            try
            {
                daoIngresoMercancias = new DaoIngresoMercanciasSap();
                var Result = daoIngresoMercancias.ConsultarModelos();
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpPost]
        public ActionResult GuardarModeloChatarra(String Descripcion, Decimal PesoTeorico)
        {

            daoIngresoMercancias = new DaoIngresoMercanciasSap();

            var IngresoModelos=daoIngresoMercancias.GuardarModelos(Descripcion,PesoTeorico);
            return Json(IngresoModelos, JsonRequestBehavior.AllowGet);

        }

    }
}