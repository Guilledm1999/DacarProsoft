using DacarProsoft.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DacarProsoft.Controllers
{
    public class PrincipalController : Controller
    {
        private DaoUtilitarios daoUtilitarios { get; set; } = null;

        // GET: Principal
        public ActionResult VistaPrincipal()
        {
          
            if (Session["usuario"] != null)
            {
                ViewBag.JavaScript = "General/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
                ViewBag.dxdevweb = "1";
                daoUtilitarios = new DaoUtilitarios();
            var datMenu = daoUtilitarios.ConsultarMenuPrincipal();

                ViewBag.MenuAcceso= Session["Menu"] ;


                ViewBag.MenuPrincipal = datMenu;


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

        public ActionResult RedirigirPagina(String OpcionPrincipal)
        {
           
                if (OpcionPrincipal == "Administrar") {
                    return RedirectToAction("CrearUsuarios", "CrearUsuario");

                }
                if (OpcionPrincipal == "Ventas")
                {
                    return RedirectToAction("InformeVentas", "Ventas");

                }
                if (OpcionPrincipal == "Produccion")
                {
                    return RedirectToAction("OrdenesDeProduccion", "OrdenesProduccion");

                }

            else
            {
                return RedirectToAction("VistaPrincipal", "Principal");
            }

        }

        public JsonResult RevisarContrasena( )
        {
            try
            {
                var daoIngresoMercancias = new DaoMenu();
                
                    var Result = daoIngresoMercancias.RevisarDiasContrasena(Convert.ToString(Session["usuarioIng"]));
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