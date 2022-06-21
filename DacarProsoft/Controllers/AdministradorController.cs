using DacarDatos.Datos;
using DacarProsoft.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DacarProsoft.Controllers
{
    public class AdministradorController : Controller
    {
        private DaoUtilitarios daoUtilitarios { get; set; } = null;
        private DaoAdministrar daoAdministrar { get; set; } = null;

        // GET: Administrador
        public ActionResult AdministrarGenericosItem()
        {
            if (Session["usuario"] != null)
            {
                ViewBag.JavaScript = "General/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
                ViewBag.dxdevweb = "1";

                ViewBag.MenuAcceso = Session["Menu"];

                //ConexionAccess conexion = new ConexionAccess();

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
        public JsonResult ConsultarGenericosItem() {
            daoAdministrar = new DaoAdministrar();
            var result = daoAdministrar.ConsultarGenericosItem();
            return Json(result, JsonRequestBehavior.AllowGet);

        }
       
        public bool InsertarGenerico(GenericosItem generico) {

            daoAdministrar = new DaoAdministrar();
            
            var result = daoAdministrar.IngresarGenericoItem(generico.GrupoGenericoItem, generico.ModeloDacar, generico.NumeroParteCliente, generico.EtiquetaDatosTecnicos, generico.Polaridad, generico.TipoTerminal, generico.CantidadPiso.Value,
            generico.PisoMaximo.Value, generico.BateriasPallet.Value, generico.PesoTara.Value);

            return result;
        }
        public bool ActualizarGenerico(GenericosItem generico, int Key)
        {

            daoAdministrar = new DaoAdministrar();
            //var result = daoAdministrar.ActualizarGenericoItem(generico.GenericoItemId, generico.GrupoGenericoItem, generico.ModeloDacar, generico.NumeroParteCliente, generico.EtiquetaDatosTecnicos, generico.Polaridad, generico.TipoTerminal, generico.CantidadPiso.Value,
            //generico.PisoMaximo.Value, generico.BateriasPallet.Value, generico.PesoTara.Value);
            var result = daoAdministrar.ActualizarGenericoItem(generico, Key);

            var r = true;
            return r;
        }

        public bool EliminarGenerico(GenericosItem generico)
        {

            daoAdministrar = new DaoAdministrar();
            var result = daoAdministrar.EliminarGenericoItem(generico.GenericoItemId);
            var r = true;
            return r;
        }
    }
}