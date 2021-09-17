using DacarDatos.Datos;
using DacarProsoft.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DacarProsoft.Controllers
{
    public class LogisticaPedidosController : Controller
    {

        private DaoCliente daocliente { get; set; } = null;
        private DaoLogisticaPedidos daopedidos { get; set; } = null;
        private DaoArticulos daoArticulos { get; set; } = null;



        // GET: LogisticaPedidos
        [HttpGet]
        public ActionResult LogisticaPedidos()
        {
            if (Session["usuario"] != null)
            {
                List<SelectListItem> lst = new List<SelectListItem>();
         

                ViewBag.JavaScript = "General/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
                ViewBag.dxdevweb = "1";

                ViewBag.MenuAcceso = Session["Menu"];

                daocliente = new DaoCliente();
                daoArticulos = new DaoArticulos();
                var dat = daocliente.ConsultarListaClientes();
                lst = daoArticulos.ConsultarCategoria();
                ViewBag.cliente = dat;
                return View(lst);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }

        public JsonResult ConsultaDeArticulos()
        {
            try
            {
                daoArticulos = new DaoArticulos();
                var Result = daoArticulos.ConsultarListaArticulos();
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        [HttpGet]
        public JsonResult ConsultaDeArticulosPorCategorias(String Categoria,String Subcategoria)
        {
            try
            {
                daoArticulos = new DaoArticulos();
                var Result = daoArticulos.ListadoArticulosPorCategoria(Categoria,Subcategoria);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


        //public JsonResult ConsultarPedido()
        //{
        //    try
        //    {
        //        daopedidos = new DaoLogisticaPedidos();
        //        var Result = daopedidos.ConsultarPedidos();
        //       return Json(Result, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }


        //}
        [HttpGet]
           public JsonResult ConsultarSubCategoria(String Categoria){
            List<ElementJsonIntKey> lst = new List<ElementJsonIntKey>();
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
               
                lst = (from d in DB.vConsultaArticulos
                       where d.Categoria == Categoria
                       select new ElementJsonIntKey
                       {
                           Value = d.SubCategoria,
                           Text = d.SubCategoria
                       }
                         ).Distinct().ToList();

                return Json(lst, JsonRequestBehavior.AllowGet);
            }

        }
        public class ElementJsonIntKey
        {
            public string Value { get; set; }
            public string Text { get; set; }
        }

        //[HttpGet]
        //public JsonResult ConsultaInternaDeClientes(String nombreCliente)
        //{
        //    try
        //    {
        //        daocliente = new DaoCliente();
        //        var Result = daocliente.ConsultarCliente(nombreCliente);
        //        return Json(Result, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        throw;
        //    }
        //}

        //[HttpPost]
        //public JsonResult GuardarPedido(PedidoCliente pc)
        //{
        //    daopedidos = new DaoLogisticaPedidos();
        //    bool crearPedido = daopedidos.ingresarPedido(pc.ClienteId,Convert.ToDateTime(pc.FechaEmision), pc.OrdenCompra,pc.LugarEntrega, Convert.ToInt32(pc.ArticuloId), Convert.ToBoolean(pc.EstadoPedido));
        //    return Json(crearPedido, JsonRequestBehavior.AllowGet);

        //}

        //[HttpPost]
        //public JsonResult GuardarCliente(Cliente cl)
        //{
        //    daocliente = new DaoCliente();
        //    bool crearCliente =daocliente.ingresarCliente( cl.NombreCliente, cl.Ruc, cl.Telefono, cl.Direccion, cl.Pais, cl.Provincia, cl.Ciudad, cl.Email, Convert.ToDateTime(cl.FechaInicio)) ;
        //    return Json(crearCliente, JsonRequestBehavior.AllowGet);

        //}



    }
}