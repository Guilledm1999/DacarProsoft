using DacarProsoft.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DacarProsoft.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        private DaoMenu daomenu { get; set; } = null;

        [HttpGet]
        public ActionResult _Layout()
        {

            try
            {
                ViewBag.JavaScript = "General/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
                ViewBag.dxdevweb = "1";

                ViewBag.MenuAcceso = Session["Menu"];

                daomenu = new DaoMenu();
                var dat = daomenu.ConsultarMenu();
                ViewBag.menu = dat;
                var dat2 = daomenu.ConsultarSubmenu();
                ViewBag.submenu = dat2;

                return View("Views/Compartida/_Layout.cshtml");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }


        [HttpGet]
        public ActionResult LoginFail()
        {
            return View();
        }


    }
}