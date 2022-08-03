using DacarProsoft.Datos;
using DacarProsoft.Models;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace DacarProsoft.Controllers
{
    public class ChatarraController : Controller
    {
        private DaoUtilitarios daoUtilitarios { get; set; } = null;
        private DaoChatarra daoChatarra { get; set; } = null;

        private DaoCliente daoCliente { get; set; } = null;
        private DaoIngresoMercanciasSap daoIngresoMercanciasSap { get; set; } = null;
        private DaoMenu daoMenu { get; set; } = null;



        // GET: Chatarra
        public ActionResult IngresoChatarraSap()
        {
            if (Session["usuario"] != null)
            {
                ViewBag.JavaScript = "General/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
                ViewBag.dxdevweb = "1";

                ViewBag.MenuAcceso = Session["Menu"];
                daoUtilitarios = new DaoUtilitarios();
                daoChatarra = new DaoChatarra();
                daoCliente = new DaoCliente();

                var datMenu = daoUtilitarios.ConsultarMenuPrincipal();
                ViewBag.MenuPrincipal = datMenu;
                var datMenuOpciones = daoUtilitarios.ConsultarMenuOpciones();
                ViewBag.MenuOpciones = datMenuOpciones;
                var datSubMenuOpciones = daoUtilitarios.ConsultarSubMenuOpciones();
                ViewBag.SubMenuOpciones = datSubMenuOpciones;

                var grupoClientes = daoUtilitarios.GrupoCliente();
                ViewBag.gruposClientes = grupoClientes;

                var clienteLinea = daoCliente.ClienteLinea();
                ViewBag.clienteLinea = clienteLinea;

                var clienteClase = daoCliente.ClienteClase();
                ViewBag.clienteClase = clienteClase;

                var datBodega = daoChatarra.ConsultarBodega();
                ViewBag.Bodega = datBodega;

                var datTipoIngreso = daoChatarra.ConsultarTipoIngreso();
                ViewBag.TipoIngreso = datTipoIngreso;
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
                daoChatarra = new DaoChatarra();
                var Result = daoChatarra.ConsultarChatarraDetalle();
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public JsonResult ConsultaDeMarcas()
        {
            try
            {
                daoChatarra = new DaoChatarra();
                var Result = daoChatarra.ConsultarMarcas();
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ConsultaDeModelos()
        {

            try
            {
                daoChatarra = new DaoChatarra();
                var Result = daoChatarra.ConsultarModelos();
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public JsonResult ConsultaCabeceraIngresoMercanciasSap(string tipoIngreso)
        {

            try
            {
                daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
                var Result = daoIngresoMercanciasSap.ListadoCabeceraChatarraSap(tipoIngreso);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ConsultaCabeceraIngresoMercanciasPorTipoClienteSap(string tipoIngreso, int codigoCliente)
        {
            if (codigoCliente==0) {
                try
                {
                    daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
                    var Result = daoIngresoMercanciasSap.ListadoCabeceraChatarraSap(tipoIngreso);
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }else
            {
                try
                {
                    daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
                    var Result = daoIngresoMercanciasSap.ListadoCabeceraChatarraSapDescCliente(tipoIngreso, codigoCliente);
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
        }

        public JsonResult ConsultaCompraCabeceraIngresoMercanciasSap(string tipoIngreso)
        {

            try
            {
                daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
                var Result = daoIngresoMercanciasSap.ListadoCompraCabeceraChatarraSap(tipoIngreso);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ConsultaCompraCabeceraIngresoMercanciasPorTipoClienteSap(string tipoIngreso, int codigoCliente)
        {
            if (codigoCliente == 0)
            {
                try
                {
                    daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
                    var Result = daoIngresoMercanciasSap.ListadoCompraCabeceraChatarraSap(tipoIngreso);
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
            else
            {
                try
                {
                    daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
                    var Result = daoIngresoMercanciasSap.ListadoCompraCabeceraChatarraSapDescCliente(tipoIngreso, codigoCliente);
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
          
        }
        public JsonResult ConsultaCompraCabeceraIngresoMercanciasSapPorTipoCliente(string tipoIngreso, string codigoCliente, string codigos, string clienteLinea, string clienteClase)
        {
            try
            {
                daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
                var Result = daoIngresoMercanciasSap.ListadoCompraCabeceraChatarraSap(tipoIngreso);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ConsultaDetalleIngresoMercanciasSapSinFu(int DocEntry)
        {
            try
            {
                daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
                var Result = daoIngresoMercanciasSap.ListadoDetalleChatarraSapSinFu(DocEntry);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public JsonResult ConsultaCompraDetalleIngresoMercanciasSapSinFu(int DocEntry)
        {
            try
            {
                daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
                var Result = daoIngresoMercanciasSap.ListadoCompraDetalleChatarraSapSinFu(DocEntry);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ConsultaDetalleIngresoMercanciasSapSinFuIndividual(int DocEntry)
        {
            try
            {
                daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
                var Result = daoIngresoMercanciasSap.ListadoDetalleChatarraSapSinFuIndividual(DocEntry);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public JsonResult ConsultaCompraDetalleIngresoMercanciasSapSinFuIndividual(int DocEntry)
        {
            try
            {
                daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
                var Result = daoIngresoMercanciasSap.ListadoCompraDetalleChatarraSapSinFuIndividual(DocEntry);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ConsultaDetalleIngresoMercanciasSap(int DocEntry, decimal factorUnitario)
        {
            try
            {
                daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
                var Result = daoIngresoMercanciasSap.ListadoDetalleChatarraSap(DocEntry, factorUnitario);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ConsultaModificarDetalleIngresoMercanciasLocal(int DocEntry, decimal factorUnitario)
        {
            try
            {
                daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
                var Result = daoIngresoMercanciasSap.ListadoModificacionDetalleChatarraSap(DocEntry, factorUnitario);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ConsultaCompraDetalleIngresoMercanciasSap(int DocEntry, decimal factorUnitario)
        {
            try
            {
                daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
                var Result = daoIngresoMercanciasSap.ListadoCompraDetalleChatarraSap(DocEntry, factorUnitario);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


        public JsonResult ConsultaModelo(String Nombre)
        {
            try
            {
                daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
                var Result = daoIngresoMercanciasSap.ConsultarModelosPorDescripcion(Nombre);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpPost]
        public ActionResult GuardarIngresos(MdlOpdn cabecera, List<Mign1> Array,String pesoTotal,String PesoBulto, String PesoTotalAjustado, String Desviacion, String Bodega)
        {
            int acum = 0;
            daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();

                var IdCabecera = daoIngresoMercanciasSap.IngresarChatarra(cabecera.DocEntry, cabecera.DocNum,cabecera.NumeroPedido, cabecera.DocDate, cabecera.CedulaCliente, cabecera.NombreCliente,cabecera.ClienteLinea,cabecera.ClienteClase, cabecera.TipoIngreso, cabecera.Comments, Bodega);

                foreach (var x in Array)
                {
                acum = acum + x.Cantidad;
                    daoIngresoMercanciasSap.IngresarDetalleChatarra(IdCabecera, cabecera.DocEntry, x.ItemCode, x.Description, x.Cantidad, x.PesoTeoricoUnitario,x.PesoTeoricoSubtotal,x.PesoTeoricoAjustado,x.PesoTeoricoAjustadoTotal);
                }

                bool ingresoExitoso= daoIngresoMercanciasSap.IngresarPesosTotalesChatarras(cabecera.DocEntry, pesoTotal, PesoBulto, PesoTotalAjustado, Desviacion, acum,1);

            return Json(IdCabecera, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ConsultaIngresoChatarra()
        {
            if (Session["usuario"] != null)
            {
                ViewBag.JavaScript = "General/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
            ViewBag.dxdevweb = "1";
            ViewBag.MenuAcceso = Session["Menu"];


            daoUtilitarios = new DaoUtilitarios();
                daoCliente = new DaoCliente();

                daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();

            var dat = daoIngresoMercanciasSap.ConsultarAniosVentas();
            ViewBag.anos = dat;

                var grupoClientes = daoUtilitarios.GrupoCliente();
                ViewBag.gruposClientes = grupoClientes;

                var clienteLinea = daoCliente.ClienteLinea();
                ViewBag.clienteLinea = clienteLinea;

                var clienteClase = daoCliente.ClienteClase();
                ViewBag.clienteClase = clienteClase;

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

        public JsonResult ConsultaIngresoChatarraLocal(string anio/*,string codigoCliente,string codigos*/)
        {
            //string cod;
            //if (codigoCliente != null && codigoCliente != " ")
            //{
            //    cod = codigoCliente;
            //}
            //else {
            //    cod = "0";
            //}
            try
            {
                daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
                var Result = daoIngresoMercanciasSap.ConsultarIngresosChatarraLocal(Convert.ToInt32(anio)/*, Convert.ToInt32(cod), codigos*/);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ConsultaIngresoChatarraDetalleLocal(int DocEntry)
        {
            try
            {
                daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
                var Result = daoIngresoMercanciasSap.ConsultarIngresosChatarraDetalleLocal(DocEntry);
                if (Result.Count()==0) {
                     Result = daoIngresoMercanciasSap.ConsultarIngresosIndividualesChatarraDetalleLocal(DocEntry);
                }
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public string ConsultaBodega(int DocEntry)
        {
            try
            {
                daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
                var Result = daoIngresoMercanciasSap.Bodega(DocEntry);
                return Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public string ConsultaBodegaCompra(int DocEntry)
        {
            try
            {
                daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
                var Result = daoIngresoMercanciasSap.CompraBodega(DocEntry);
                return Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


        [HttpPost]
        public ActionResult CalcularPesosIndividuales(List<MdlPdn1Individual> Array)
        {
          

            daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
          var resul= daoIngresoMercanciasSap.ListadoCalculoIndividual(Array);
            return Json(resul, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult CalcularModificarPesosIndividuales(List<DetalleChatarraGuardado> Array)
        {
            int contador = 0;
            decimal total = 0;
            decimal variacionTotal = 0;
            daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
            var resul = daoIngresoMercanciasSap.ListadoModificarCalculoIndividual(Array);

            foreach (var x in resul)
            {
                total = total + x.DesviacionIndividual;
                contador = contador + 1;
            }

            variacionTotal = (total / contador);

            ViewBag.desviacion = decimal.Round(variacionTotal,2);

            return Json(resul, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public decimal calcdesv(List<DetalleChatarraGuardado> Array)
        {
            int contador = 0;
            decimal total = 0;
            decimal variacionTotal = 0;

            foreach (var x in Array)
            {
                total = total + x.DesviacionIndividual;
                contador = contador + 1;
            }

            variacionTotal = (total / contador);

            return decimal.Round(variacionTotal,2);

        }
        [HttpGet]
        public string ControlCambios()
        {
            daoUtilitarios = new DaoUtilitarios();

            var resul = daoUtilitarios.ControlCambios();
            return resul;

        }


        [HttpPost]
        public ActionResult GuardarIngresosChatarraIndividuales(MdlOpdn cabecera, List<MdlPdn1Individual> Array, String Bodega)
        {

            decimal contadorDesviacion = 0;
            decimal contadorPesoTeorico = 0;
            decimal contadorPesoTotalIngresado = 0;
            decimal contadorPesoTotalAjustado = 0;

            int acum = 0;

            int i = 0;
            decimal desviacionPromedio = 0;
            daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();

            var IdCabecera = daoIngresoMercanciasSap.IngresarChatarra(cabecera.DocEntry, cabecera.DocNum,cabecera.NumeroPedido, cabecera.DocDate, cabecera.CedulaCliente, cabecera.NombreCliente, cabecera.ClienteLinea, cabecera.ClienteClase, cabecera.TipoIngreso, cabecera.Comments, Bodega);
            foreach (var x in Array)
            {
                contadorDesviacion = contadorDesviacion + x.DesviacionIndividual;
                contadorPesoTeorico = contadorPesoTeorico + x.PesoTeoricoSubtotal;
                contadorPesoTotalIngresado = contadorPesoTotalIngresado + (x.PesoNetoTipo);
                contadorPesoTotalAjustado = contadorPesoTotalAjustado + x.PesoTeoricoAjustadoTotal;
                i = i + 1;
            }
            desviacionPromedio = contadorDesviacion / i;


            foreach (var x in Array)
            {
                acum = acum + x.Cantidad;
                daoIngresoMercanciasSap.IngresarDetalleChatarraIndividual(IdCabecera, cabecera.DocEntry, x.ItemCode, x.Description, x.Cantidad, x.PesoTeoricoUnitario, x.PesoTeoricoSubtotal, x.PesoNetoTipo, x.PesoTeoricoAjustado, x.PesoTeoricoAjustadoTotal, x.DesviacionIndividual);
            }

            bool ingresoExitoso = daoIngresoMercanciasSap.IngresarPesosTotalesChatarras(cabecera.DocEntry, Convert.ToString(contadorPesoTeorico), Convert.ToString(contadorPesoTotalIngresado), Convert.ToString(contadorPesoTotalAjustado), Convert.ToString(decimal.Round(desviacionPromedio, 2)), acum, 2);


            return Json(IdCabecera, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ReporteIngresosChatarra()
        {
            if (Session["usuario"] != null)
            {
                ViewBag.JavaScript = "General/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
                ViewBag.dxdevweb = "1";
                ViewBag.MenuAcceso = Session["Menu"];


                daoUtilitarios = new DaoUtilitarios();
                daoCliente = new DaoCliente();

                daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();

                var dat = daoIngresoMercanciasSap.ConsultarAniosVentas();

                ViewBag.anos = dat;
                var grupoClientes = daoUtilitarios.GrupoCliente();
                ViewBag.gruposClientes = grupoClientes;

                var clienteLinea = daoCliente.ClienteLinea();
                ViewBag.clienteLinea = clienteLinea;

                var clienteClase = daoCliente.ClienteClase();
                ViewBag.clienteClase = clienteClase;

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
                //return View("GenericRedirect", (object)"Account");
                return RedirectToAction("Login", "Account");
            }
        }

        public JsonResult ConsultaModificarIngresoChatarraLocal(string anio)
        {
        
            try
            {   
                daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
                var Result = daoIngresoMercanciasSap.ConsultarModificarIngresosChatarraLocal(Convert.ToInt32(anio));
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public JsonResult ConsultaIngresosChatarraLoc(int DocEntry,int ModoIngreso)
        {

            try
            {
                daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
                var Result = daoIngresoMercanciasSap.DetallaModificacionChatarra(DocEntry, ModoIngreso);
                     
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


        [HttpPost]
        public ActionResult GuardarActualizacionDetalles(int DocEntry,List<DetalleChatarraGuardado> detalleChatarra,string PesoTeoricoTotalCal,string PesoBultoIng,string PesoAjustadoTot,string desviacionTot)
        {          
            daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();

            foreach (var x in detalleChatarra)
            {
                var IdCabecera = daoIngresoMercanciasSap.GuardarActualizacionDetalle(x.ChatarraDetalleId, x.PesoTeoricoAjustado, x.PesoTeoricoAjustadoTotal);
            }

            bool ingresoExitoso = daoIngresoMercanciasSap.GuardarActualizacionPesosTotales(DocEntry, Convert.ToDecimal(PesoTeoricoTotalCal), Convert.ToDecimal(PesoBultoIng), Convert.ToDecimal(PesoAjustadoTot), Convert.ToString(Convert.ToDecimal(desviacionTot)));


            return Json(ingresoExitoso, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult IngresosIndividuales(int DocEntry, List<DetalleChatarraGuardado> Array)
        {
            decimal contadorDesviacion = 0;
            decimal contadorPesoTeorico = 0;
            decimal contadorPesoTotalIngresado = 0;
            decimal contadorPesoTotalAjustado = 0;

            int acum = 0;

            int i = 0;
            decimal desviacionPromedio = 0;
            daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();

            foreach (var x in Array)
            {
                contadorDesviacion = contadorDesviacion + x.DesviacionIndividual;
                contadorPesoTeorico = contadorPesoTeorico + x.PesoTeoricoTotal;
                contadorPesoTotalIngresado = contadorPesoTotalIngresado + x.PesoNetoTipo;
                contadorPesoTotalAjustado = contadorPesoTotalAjustado + x.PesoTeoricoAjustadoTotal;
                i = i + 1;
            }
            desviacionPromedio = contadorDesviacion / i;


            foreach (var x in Array)
            {
                acum = acum + x.Cantidad;
                daoIngresoMercanciasSap.GuardarActualizacionDetalleIndividual(x.ChatarraDetalleId, x.PesoNetoTipo, x.PesoTeoricoAjustado, x.PesoTeoricoAjustadoTotal, x.DesviacionIndividual);
            }

            bool ingresoExitoso = daoIngresoMercanciasSap.GuardarActualizacionPesosTotales(DocEntry, Convert.ToDecimal(contadorPesoTeorico), Convert.ToDecimal(contadorPesoTotalIngresado), Convert.ToDecimal(contadorPesoTotalAjustado), Convert.ToString(decimal.Round(desviacionPromedio, 2)));


            return Json(ingresoExitoso, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult GuardarIngresosIndividuales(int DocEntry, List<DetalleChatarraGuardado> Array)
        {
            decimal contadorDesviacion = 0;
            decimal contadorPesoTeorico = 0;
            decimal contadorPesoTotalIngresado = 0;
            decimal contadorPesoTotalAjustado = 0;

            int acum = 0;

            int i = 0;
            decimal desviacionPromedio = 0;
            daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();

            foreach (var x in Array)
            {
                contadorDesviacion = contadorDesviacion + x.DesviacionIndividual;
                contadorPesoTeorico = contadorPesoTeorico + x.PesoTeoricoTotal;
                contadorPesoTotalIngresado = contadorPesoTotalIngresado + x.PesoNetoTipo;
                contadorPesoTotalAjustado = contadorPesoTotalAjustado + x.PesoTeoricoAjustadoTotal;
                i = i + 1;
            }
            desviacionPromedio = contadorDesviacion / i;


            foreach (var x in Array)
            {
                acum = acum + x.Cantidad;
                daoIngresoMercanciasSap.GuardarActualizacionDetalleIndividual(x.ChatarraDetalleId,x.PesoNetoTipo,x.PesoTeoricoAjustado,x.PesoTeoricoAjustadoTotal,x.DesviacionIndividual);
            }

            bool ingresoExitoso = daoIngresoMercanciasSap.GuardarActualizacionPesosTotales(DocEntry, Convert.ToDecimal(contadorPesoTeorico), Convert.ToDecimal(contadorPesoTotalIngresado), Convert.ToDecimal(contadorPesoTotalAjustado), Convert.ToString(decimal.Round(desviacionPromedio, 2)));


            return Json(ingresoExitoso, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        public ActionResult ChatarraPdf(int CodDocEntry, int modIng)
        {


            var path = System.IO.Path.Combine(Server.MapPath("~/Images/DacarImg.png"));
            var path2 = System.IO.Path.Combine(Server.MapPath("~/Images/DacarProsoft.png"));

            iText.Layout.Element.Image img = new iText.Layout.Element.Image(ImageDataFactory.Create(path));
            // position in document
            img.SetFixedPosition(15, 760);
            img.SetHeight(55);
            img.SetWidth(90);

            iText.Layout.Element.Image img2 = new iText.Layout.Element.Image(ImageDataFactory.Create(path2));
            // position in document
            img2.SetFixedPosition(500, 760);
            img2.SetHeight(55);
            img2.SetWidth(90);




            if (modIng == 2)
            {
                daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
                var resultChatarra = daoIngresoMercanciasSap.ConsultarModificarIngresosChatarraImprimir(CodDocEntry);
                var result1 = daoIngresoMercanciasSap.DetallaModificacionChatarra(CodDocEntry, modIng);

                string ClienteCha = null;
                string numeroOrden = null;
                string fecha = null;
                string bodega = null;
                string CantidadTotal = null;
                string DesviacionTotal = null;
                string PesoIngresado = null;

                foreach (var x in resultChatarra)
                {
                    ClienteCha = x.NombreCliente;
                    numeroOrden = x.NumeroPedido;
                    fecha = x.FechaIngreso;
                    bodega = x.Bodega;
                    CantidadTotal = Convert.ToString(x.CantidadTotal);
                    DesviacionTotal = Convert.ToString(x.Desviacion);
                    PesoIngresado = Convert.ToString(x.PesoBultoIngresado);
                }
                PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
                PdfFont No_Bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
                MemoryStream stream = new MemoryStream();

                LineSeparator ls = new LineSeparator(new SolidLine());
                PdfWriter writer = new PdfWriter(stream);

                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf, iText.Kernel.Geom.PageSize.A4, true);

                Table itemsTable = new Table(8);

                itemsTable.SetHorizontalAlignment(HorizontalAlignment.CENTER);

                Cell cell1 = new Cell().Add(new Paragraph("Codigo").SetFont(bold).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER))/*.SetBackgroundColor(ColorConstants.BLUE)*/;
                itemsTable.AddCell(cell1);
                Cell cell2 = new Cell().Add(new Paragraph("Descripcion").SetFont(bold).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER))/*.SetBackgroundColor(ColorConstants.BLUE)*/;
                itemsTable.AddCell(cell2);
                Cell cell3 = new Cell().Add(new Paragraph("Cantidad").SetFont(bold).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER))/*.SetBackgroundColor(ColorConstants.BLUE)*/;
                itemsTable.AddCell(cell3);
                Cell cell4 = new Cell().Add(new Paragraph("Peso Teorico U.").SetFont(bold).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER))/*.SetBackgroundColor(ColorConstants.BLUE)*/;
                itemsTable.AddCell(cell4);
                Cell cell5 = new Cell().Add(new Paragraph("Peso Teorico T.").SetFont(bold).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER))/*.SetBackgroundColor(ColorConstants.BLUE)*/;
                itemsTable.AddCell(cell5);
                Cell cell6 = new Cell().Add(new Paragraph("Peso Ingresado U.").SetFont(bold).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER))/*.SetBackgroundColor(ColorConstants.BLUE)*/;
                itemsTable.AddCell(cell6);
                Cell cell7 = new Cell().Add(new Paragraph("Peso Ingresado T.").SetFont(bold).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER))/*.SetBackgroundColor(ColorConstants.BLUE)*/;
                itemsTable.AddCell(cell7);
                Cell cell8 = new Cell().Add(new Paragraph("Desviacion").SetFont(bold).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER))/*.SetBackgroundColor(ColorConstants.BLUE)*/;
                itemsTable.AddCell(cell8);

                foreach (var x in result1)
                {
                    itemsTable.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(x.ItemCode).SetFontSize(8)));
                    itemsTable.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(x.Description).SetFontSize(8)));
                    itemsTable.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(Convert.ToString(x.Cantidad)).SetFontSize(8)));
                    itemsTable.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(Convert.ToString(x.PesoTeoricoUnitario)).SetFontSize(8)));
                    itemsTable.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(Convert.ToString(x.PesoTeoricoTotal)).SetFontSize(8)));
                    itemsTable.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(Convert.ToString(x.PesoNetoTipo)).SetFontSize(8)));
                    itemsTable.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(Convert.ToString(x.PesoTeoricoAjustadoTotal)).SetFontSize(8)));
                    itemsTable.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(Convert.ToString(x.DesviacionIndividual)).SetFontSize(8)));
                }

                Table itemsTable2 = new Table(1);
                itemsTable2.SetHorizontalAlignment(HorizontalAlignment.CENTER);

                itemsTable2.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Cantidad Total:"+CantidadTotal).SetFontSize(8))
                    .Add(new Paragraph("Peso Ingresado:" + PesoIngresado).SetFontSize(8))
                    .Add(new Paragraph("Desviacion:" + DesviacionTotal+"%").SetFontSize(8)));

                Paragraph Encabezado = new Paragraph("INDUSTRIAS DACAR CIA LTDA.")
               .SetTextAlignment(TextAlignment.CENTER)
               .SetFontSize(16).SetFontColor(ColorConstants.BLACK).SetFont(bold);

                Paragraph header = new Paragraph("TABLA DE CALCULO DE PESO DE BATERIAS CHATARRAS")
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(12).SetFontColor(ColorConstants.BLACK).SetFont(bold);
                PdfFont font = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);

                List list = new List().SetSymbolIndent(12)
               .SetFont(font);


                Paragraph fechaRep = new Paragraph("FECHA:" + fecha + "                            #REP:" + numeroOrden).SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(10).SetFontColor(ColorConstants.BLACK);
                Paragraph cliente = new Paragraph("CLIENTE:     ").SetTextAlignment(TextAlignment.LEFT)
                         .SetFontSize(10).SetFont(bold).Add(new Paragraph(ClienteCha).SetFont(No_Bold)).SetFontColor(ColorConstants.BLACK);
                Paragraph bodegaCha = new Paragraph("BODEGA:     ").SetTextAlignment(TextAlignment.LEFT)
                         .SetFontSize(10).SetFont(bold).Add(new Paragraph(bodega).SetFont(No_Bold)).SetFontColor(ColorConstants.BLACK);

                Paragraph Espacio = new Paragraph("  ").SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(10).SetFontColor(ColorConstants.BLACK);

                //document.Add(Espacio);
                document.Add(Encabezado);
                document.Add(Espacio);
                //document.Add(img);
                //document.Add(img2);
                document.Add(header);
                document.Add(ls);
                document.Add(Espacio);
                document.Add(fechaRep);
                document.Add(cliente);
                document.Add(bodegaCha);
                document.Add(itemsTable);
                document.Add(Espacio);
                document.Add(itemsTable2);
                document.Close();

                byte[] bytesStreams = stream.ToArray();
                stream = new MemoryStream();
                stream.Write(bytesStreams, 0, bytesStreams.Length);
                stream.Position = 0;
                return new FileStreamResult(stream, "application/pdf");
            }
            else {

                daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
                var resultChatarra = daoIngresoMercanciasSap.ConsultarModificarIngresosChatarraImprimir(CodDocEntry);
                var result1 = daoIngresoMercanciasSap.DetallaModificacionChatarra(CodDocEntry, modIng);

                string ClienteCha = null;
                string numeroOrden = null;
                string fecha = null;
                string bodega = null;
                string CantidadTotal = null;
                string DesviacionTotal = null;
                string PesoIngresado = null;

                foreach (var x in resultChatarra)
                {
                    ClienteCha = x.NombreCliente;
                    numeroOrden = x.NumeroPedido;
                    fecha = x.FechaIngreso;
                    bodega = x.Bodega;
                    CantidadTotal = Convert.ToString(x.CantidadTotal);
                    DesviacionTotal = Convert.ToString(x.Desviacion);
                    PesoIngresado = Convert.ToString(x.PesoBultoIngresado);
                }
                PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
                PdfFont No_Bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
                MemoryStream stream = new MemoryStream();

                LineSeparator ls = new LineSeparator(new SolidLine());
                PdfWriter writer = new PdfWriter(stream);

                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf, iText.Kernel.Geom.PageSize.A4, true);

                Table itemsTable = new Table(7);

                itemsTable.SetHorizontalAlignment(HorizontalAlignment.CENTER);

                Cell cell1 = new Cell().Add(new Paragraph("Codigo").SetFont(bold).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER))/*.SetBackgroundColor(ColorConstants.BLUE)*/;
                itemsTable.AddCell(cell1);
                Cell cell2 = new Cell().Add(new Paragraph("Descripcion").SetFont(bold).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER))/*.SetBackgroundColor(ColorConstants.BLUE)*/;
                itemsTable.AddCell(cell2);
                Cell cell3 = new Cell().Add(new Paragraph("Cantidad").SetFont(bold).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER))/*.SetBackgroundColor(ColorConstants.BLUE)*/;
                itemsTable.AddCell(cell3);
                Cell cell4 = new Cell().Add(new Paragraph("Peso Teorico U.").SetFont(bold).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER))/*.SetBackgroundColor(ColorConstants.BLUE)*/;
                itemsTable.AddCell(cell4);
                Cell cell5 = new Cell().Add(new Paragraph("Peso Teorico T.").SetFont(bold).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER))/*.SetBackgroundColor(ColorConstants.BLUE)*/;
                itemsTable.AddCell(cell5);
                Cell cell6 = new Cell().Add(new Paragraph("Peso Ajustado U.").SetFont(bold).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER))/*.SetBackgroundColor(ColorConstants.BLUE)*/;
                itemsTable.AddCell(cell6);
                Cell cell7 = new Cell().Add(new Paragraph("Peso Ajustado T.").SetFont(bold).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER))/*.SetBackgroundColor(ColorConstants.BLUE)*/;
                itemsTable.AddCell(cell7);

                foreach (var x in result1)
                {
                    itemsTable.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(x.ItemCode).SetFontSize(8)));
                    itemsTable.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(x.Description).SetFontSize(8)));
                    itemsTable.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(Convert.ToString(x.Cantidad)).SetFontSize(8)));
                    itemsTable.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(Convert.ToString(x.PesoTeoricoUnitario)).SetFontSize(8)));
                    itemsTable.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(Convert.ToString(x.PesoTeoricoTotal)).SetFontSize(8)));
                    itemsTable.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(Convert.ToString(x.PesoTeoricoAjustado)).SetFontSize(8)));
                    itemsTable.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(Convert.ToString(x.PesoTeoricoAjustadoTotal)).SetFontSize(8)));
                }

                Table itemsTable2 = new Table(1);
                itemsTable2.SetHorizontalAlignment(HorizontalAlignment.CENTER);

                itemsTable2.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("Cantidad Total:" + CantidadTotal).SetFontSize(8))
                    .Add(new Paragraph("Peso Ingresado:" + PesoIngresado).SetFontSize(8))
                    .Add(new Paragraph("Desviacion:" + DesviacionTotal + "%").SetFontSize(8)));

                Paragraph Encabezado = new Paragraph("INDUSTRIAS DACAR CIA LTDA.")
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(16).SetFontColor(ColorConstants.BLACK).SetFont(bold);

                Paragraph header = new Paragraph("TABLA DE CALCULO DE PESO DE BATERIAS CHATARRAS")
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(14).SetFontColor(ColorConstants.BLACK).SetFont(bold);
                PdfFont font = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);

                List list = new List().SetSymbolIndent(12)
               .SetFont(font);


                Paragraph fechaRep = new Paragraph("FECHA:" + fecha + "                            #REP:" + numeroOrden).SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(10).SetFontColor(ColorConstants.BLACK);
                Paragraph cliente = new Paragraph("CLIENTE:     ").SetTextAlignment(TextAlignment.LEFT)
                         .SetFontSize(10).SetFont(bold).Add(new Paragraph(ClienteCha).SetFont(No_Bold)).SetFontColor(ColorConstants.BLACK);
                Paragraph bodegaCha = new Paragraph("BODEGA:     ").SetTextAlignment(TextAlignment.LEFT)
                         .SetFontSize(10).SetFont(bold).Add(new Paragraph(bodega).SetFont(No_Bold)).SetFontColor(ColorConstants.BLACK);

                Paragraph Espacio = new Paragraph("  ").SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(10).SetFontColor(ColorConstants.BLACK);

                //document.Add(Espacio);
                document.Add(Encabezado);
                document.Add(Espacio);
                //document.Add(img);
                //document.Add(img2);
                document.Add(header);
                document.Add(ls);
                document.Add(Espacio);
                document.Add(fechaRep);
                document.Add(cliente);
                document.Add(bodegaCha);
                document.Add(itemsTable);
                document.Add(Espacio);
                document.Add(itemsTable2);
                document.Close();

                byte[] bytesStreams = stream.ToArray();
                stream = new MemoryStream();
                stream.Write(bytesStreams, 0, bytesStreams.Length);
                stream.Position = 0;
                return new FileStreamResult(stream, "application/pdf");
            }
        }
        public ActionResult ReporteIngresosChatarraPorFechas()
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
                //return View("GenericRedirect", (object)"Account");
                return RedirectToAction("Login", "Account");
            }
        }

        public JsonResult ConsultaDatosIngresosChatarra(DateTime anioInicial, DateTime anioFinal, string OpcionFiltrado)
        {
            try
            {
                daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
                var Result = daoIngresoMercanciasSap.ConsultaDeRegistrosChatarrasGeneralPorFechas(anioInicial, anioFinal, Convert.ToInt32(OpcionFiltrado));

                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                serializer.MaxJsonLength = int.MaxValue;

                var json = Json(Result, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = int.MaxValue;
                return json;


               // return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ConsultaIngresosChatarraPorCliente(int anio, string cliente)
        {

            try
            {
                daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
                var Result = daoIngresoMercanciasSap.ReporteClienteChatarrasPorMeses(anio, cliente);

                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ConsultaIngresosChatarraGenerales(int anio)
        {

            try
            {
                daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
                var Result = daoIngresoMercanciasSap.ReporteGeneralChatarrasPorMeses(anio);

                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ConsultaIngresosChatarraGeneralesAnioAndTipo(int anio, string tipo)
        {

            try
            {
                daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
                var Result = daoIngresoMercanciasSap.ReporteGeneralChatarrasPorMesesPorTipo(anio,tipo);

                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ConsultaIngresosChatarraGeneralesAnioAndTipoCliente(int anio, string tipo)
        {

            try
            {
                daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
                var Result = daoIngresoMercanciasSap.ReporteGeneralChatarrasPorMesesPorTipoCliente(anio, Convert.ToInt32(tipo));

                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public string GuardarViewBagDetalleChatarra(string chart, List<IngresosChatarras> registros)
        {
            Session["GraficoChatarra"] = chart;
            Session["RegistrosChatarra"] = registros;

            return null;
        }

      
        public ActionResult GenerarPdfReporteChatarra(int Cantidad, string PesoTeorico, string PesoIngresado, string Desviacion)
        {
            string valoview = Session["GraficoChatarra"].ToString();
            List<IngresosChatarras> lst = new List<IngresosChatarras>();
            var valor = (List<IngresosChatarras>)Session["RegistrosChatarra"];

            iText.Kernel.Colors.Color lineColor = new DeviceRgb(164, 164, 164);


            var base64arr = valoview.Split(',');
            Paragraph Espacio = new Paragraph(" ").SetTextAlignment(TextAlignment.CENTER);
            byte[] bytes = Convert.FromBase64String(base64arr[1]);

            MemoryStream stream = new MemoryStream();
            PdfWriter writer = new PdfWriter(stream);

            PdfDocument pdf = new PdfDocument(writer);

            Document document = new Document(pdf, iText.Kernel.Geom.PageSize.A4, true);

            iText.Layout.Element.Image img = new iText.Layout.Element.Image(ImageDataFactory
             .Create(bytes))
             .SetTextAlignment(TextAlignment.CENTER).SetWidth(480).SetHeight(240).SetHorizontalAlignment(HorizontalAlignment.CENTER);

            document.SetMargins(112, 36, 90, 36);

            Paragraph header = new Paragraph("REPORTE CHATARRAS")
         .SetTextAlignment(TextAlignment.CENTER)
         .SetFontSize(16).SetBold();

            var path = System.IO.Path.Combine(Server.MapPath("~/Images/HojaMembretada.jpg"));
            iText.Layout.Element.Image BackPack = new iText.Layout.Element.Image(ImageDataFactory.Create(path))/*.SetOpacity(0.1f)*/;
            pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new BackgroundPageEvent(BackPack));


            float[] columnWidth2 = { 80f, 80f};
            Table tabla2 = new Table(columnWidth2);
            tabla2.AddCell(new Cell(1, 2).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("RESUMEN DE CHATARRAS").SetFontSize(10)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Cantidad").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Peso Teorico").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + Cantidad).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + PesoTeorico).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));

            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Peso Ingresado").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Desviacion").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));

           tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(""+PesoIngresado).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(""+Desviacion).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));

            document.Add(header);
            document.Add(Espacio);
            document.Add(tabla2.SetHorizontalAlignment(HorizontalAlignment.CENTER));

            document.Add(Espacio);
            document.Add(img);
            document.Add(Espacio);

            document.Close();
            byte[] bytesStreams = stream.ToArray();
            stream = new MemoryStream();
            stream.Write(bytesStreams, 0, bytesStreams.Length);
            stream.Position = 0;

            return new FileStreamResult(stream, "application/pdf");
        }

        public class BackgroundPageEvent : IEventHandler
        {
            iText.Layout.Element.Image imgBack;
            public BackgroundPageEvent(iText.Layout.Element.Image imgBackPage)
            {
                imgBack = imgBackPage;
            }
            public void HandleEvent(Event @event)
            {
                PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
                PdfDocument pdfDoc = docEvent.GetDocument();
                PdfPage page = docEvent.GetPage();
                iText.Kernel.Geom.Rectangle pageSize = page.GetPageSize();

                PdfCanvas pdfCanvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);
                //GetLastContentStream page.NewContentStreamBefore()
                pdfCanvas.SaveState();

                Canvas canvas = new Canvas(pdfCanvas, page.GetPageSize());
                canvas.Add(imgBack.ScaleAbsolute(pageSize.GetWidth(), pageSize.GetHeight()));

                pdfCanvas.RestoreState();
                pdfCanvas.Release();
            }
        }
        private class TableHeaderEventHandler : IEventHandler
        {
            private Table table;

            public TableHeaderEventHandler(Table table)
            {
                this.table = table;
            }
            public void HandleEvent(Event currentEvent)
            {
                PdfDocumentEvent docEvent = (PdfDocumentEvent)currentEvent;
                PdfDocument pdfDoc = docEvent.GetDocument();
                PdfPage page = docEvent.GetPage();
                PdfCanvas canvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);

                new Canvas(canvas, new iText.Kernel.Geom.Rectangle(35, 730, page.GetPageSize().GetRight() - 78, 81))
                    .Add(table)
                    .Close();
            }
        }
        public string EnviarPdfReporteChatarra(int Cantidad, string PesoTeorico, string PesoIngresado, string Desviacion, string Correo, string CorreoCopia)
        {
            try
            {
                daoUtilitarios = new DaoUtilitarios();
                string valoview = Session["GraficoChatarra"].ToString();
            List<IngresosChatarras> lst = new List<IngresosChatarras>();
            var valor = (List<IngresosChatarras>)Session["RegistrosChatarra"];

            iText.Kernel.Colors.Color lineColor = new DeviceRgb(164, 164, 164);


            var base64arr = valoview.Split(',');
            Paragraph Espacio = new Paragraph(" ").SetTextAlignment(TextAlignment.CENTER);
            byte[] bytes = Convert.FromBase64String(base64arr[1]);

            MemoryStream stream = new MemoryStream();
            PdfWriter writer = new PdfWriter(stream);

            PdfDocument pdf = new PdfDocument(writer);

            Document document = new Document(pdf, iText.Kernel.Geom.PageSize.A4, true);

            iText.Layout.Element.Image img = new iText.Layout.Element.Image(ImageDataFactory
             .Create(bytes))
             .SetTextAlignment(TextAlignment.CENTER).SetWidth(480).SetHeight(240).SetHorizontalAlignment(HorizontalAlignment.CENTER);

            document.SetMargins(112, 36, 90, 36);

            Paragraph header = new Paragraph("REPORTE CHATARRAS")
         .SetTextAlignment(TextAlignment.CENTER)
         .SetFontSize(16).SetBold();

            var path = System.IO.Path.Combine(Server.MapPath("~/Images/HojaMembretada.jpg"));
            iText.Layout.Element.Image BackPack = new iText.Layout.Element.Image(ImageDataFactory.Create(path))/*.SetOpacity(0.1f)*/;
            pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new BackgroundPageEvent(BackPack));

            float[] columnWidth2 = { 80f, 80f };
            Table tabla2 = new Table(columnWidth2);
            tabla2.AddCell(new Cell(1, 2).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("RESUMEN DE CHATARRAS").SetFontSize(10)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Cantidad").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Peso Teorico").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + Cantidad).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + PesoTeorico).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));

            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Peso Ingresado").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Desviacion").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));

            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + PesoIngresado).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + Desviacion).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));

            document.Add(header);
            document.Add(Espacio);
            document.Add(tabla2.SetHorizontalAlignment(HorizontalAlignment.CENTER));

            document.Add(Espacio);
            document.Add(img);
            document.Add(Espacio);

            document.Close();
            byte[] bytesStreams = stream.ToArray();
            stream = new MemoryStream();
            stream.Write(bytesStreams, 0, bytesStreams.Length);
            stream.Position = 0;


            var CorreoBase = daoUtilitarios.ConsultarCorreoElectronico();
            string DirCorreo = "";
            string ClavCorreo = "";
            foreach (var x in CorreoBase)
            {
                DirCorreo = x.DireccionCorreo;
                ClavCorreo = x.ClaveCorreo;
            }
         
            MailMessage mm = new MailMessage("bateriasdacar1975@gmail.com", Correo);
            mm.Subject = "Reporte Chatarras ";

            mm.CC.Add(CorreoCopia);

            mm.Body = "Resultados del reporte de chatarras , con fecha: " + DateTime.Now;
            mm.Attachments.Add(new Attachment(new MemoryStream(bytesStreams), "ReporteChatarras-" + DateTime.Now.Year + "-" + DateTime.Now.Month +"-"+DateTime.Now.Day+ ".pdf"));
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 25;
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential();
            NetworkCred.UserName = DirCorreo;
            NetworkCred.Password = ClavCorreo;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Send(mm);
            return "El envío fue realizado con éxito!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public ActionResult ReporteIngresosChatarraSap()
        {
            if (Session["usuario"] != null)
            {
                ViewBag.JavaScript = "General/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
                ViewBag.dxdevweb = "1";
                ViewBag.MenuAcceso = Session["Menu"];

                daoUtilitarios = new DaoUtilitarios();
                daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();

                var dat = daoIngresoMercanciasSap.ConsultarAniosVentas();
                ViewBag.anos = dat;

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
                //return View("GenericRedirect", (object)"ViewBag.anosAccount");
                return RedirectToAction("Login", "Account");
            }
        }

        public decimal ConsultarDesviacionChatarra()
        { 
                daoUtilitarios = new DaoUtilitarios();
                var Result = daoUtilitarios.ObtenerValorDesviacion();              
                return Result;
        }

        public JsonResult ConsultaIngresosChatarraConDesviacionSap(int anio)
        {
            try
            {
                daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
                var Result = daoIngresoMercanciasSap.ReporteGeneralChatarrasPorDesviacionesSap(anio);

                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                serializer.MaxJsonLength = int.MaxValue;

                var json = Json(Result, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = int.MaxValue;
                return json;
                //return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ConsultaDetalleIngresoChatarraSap(int docEntry, string tipo)
        {

            try
            {
                daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();
                if (tipo == "Compras")
                {
                    var Result = daoIngresoMercanciasSap.ListadoIngresoCompraDetalleChatarraSap(docEntry);

                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var Result = daoIngresoMercanciasSap.ListadoNotasCreditoDetalleChatarraSap(docEntry);

                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public ActionResult ReporteDashboardIngresosChatarraSap()
        {
            if (Session["usuario"] != null)
            {
                ViewBag.JavaScript = "General/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
                ViewBag.dxdevweb = "1";
                ViewBag.MenuAcceso = Session["Menu"];

                daoUtilitarios = new DaoUtilitarios();
                daoIngresoMercanciasSap = new DaoIngresoMercanciasSap();

                var dat = daoIngresoMercanciasSap.ConsultarAniosVentas();
                ViewBag.anos = dat;

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
                //return View("GenericRedirect", (object)"ViewBag.anosAccount");
                return RedirectToAction("Login", "Account");
            }
        }
    }
}