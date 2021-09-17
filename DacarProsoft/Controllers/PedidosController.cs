using System;
using DacarProsoft.Datos;
using DacarProsoft.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAPbobsCOM;
using DacarDatos.Datos;

namespace DacarProsoft.Controllers
{
    public class PedidosController : Controller
    {
        private DaoUtilitarios daoUtilitarios { get; set; } = null;
        private DaoPedidos daoPedidos { get; set; } = null;

        // GET: Pedidos
        public ActionResult PedidosExterior()
        {
            if (Session["usuario"] != null)
            {
                ViewBag.JavaScript = "General/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
                ViewBag.dxdevweb = "1";

                daoUtilitarios = new DaoUtilitarios();

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

        public JsonResult ObtenerPalletIngresados()
        {
            try
            {

                daoPedidos = new DaoPedidos();
                var Result = daoPedidos.ConsultarPackingIngreseados();
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ObtenerDetallaFinalPedido(int PedidoId)
        {
            try
            {

                daoPedidos = new DaoPedidos();
                var Result = daoPedidos.ConsultarPedidoDetalleFinal(PedidoId);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public JsonResult ObtenerDetallaFinalPedidoGenerak(int PedidoId)
        {
            try
            {

                daoPedidos = new DaoPedidos();
                var Result = daoPedidos.ConsultarPedidoDetalleFinalAprobada(PedidoId);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ObtenerDetallePedido(int PedidoId)
        {
            try
            {

                daoPedidos = new DaoPedidos();
                var Result = daoPedidos.ConsultarPedidoDetalle(PedidoId);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ObtenerDetallePedidoConfirmado(int PedidoId)
        {
            try
            {

                daoPedidos = new DaoPedidos();
                var Result = daoPedidos.ConsultarPedidoDetalleConfirmado(PedidoId);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpPost]
        public bool CancelarPedido(int PedidoId)
        {
            try
            {

                daoPedidos = new DaoPedidos();
                var Result = daoPedidos.GuardarActualizacionEstado(PedidoId,3);
                return Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpPost]
        public bool RegistrarPedidoEnSap(int PedidoId, string Orden, DateTime FechaDocumento, DateTime FechaDespacho,string TipoVenta, string Vendedor, 
            string Observaciones, List<PedidoClienteDetalle> array,int CantidadNueva, Decimal PrecioNuevo, Decimal PesoNuevo) {

            daoPedidos = new DaoPedidos();

            string cardCode = null;
            string cardname = null;
            DateTime fechaEmisionCliente = DateTime.Now;
            DateTime fechaDespachoCliente =DateTime.Now;
            string incoterm = null;
            
            var PedidoCabecera = daoPedidos.ConsultarPedidoCabecera(PedidoId);

            foreach (var x in PedidoCabecera) {
                cardCode = x.CardCode;
                cardname = x.NombreCliente;
                fechaEmisionCliente = x.FechaEmision.Value;
                fechaDespachoCliente = x.FechaRequerida.Value;
                incoterm = x.TerminoImportacion;
            }

            //var PedidoDetalle = daoPedidos.ConsultarPedidoDetalle(PedidoId);
            
            var ComprobarOrden = daoPedidos.ComprobarExistenciaOrdenEnSap(Orden);

            if (ComprobarOrden == false)
            {
                if (ConexionApiSap.Open())
                {
                    ConexionApiSap.myCompany.StartTransaction();
                    Documents MyDoc = ConexionApiSap.myCompany.GetBusinessObject(BoObjectTypes.oOrders);

                    MyDoc.CardCode = cardCode;
                    MyDoc.CardName = cardname;
                    MyDoc.DocDate = FechaDocumento;
                    MyDoc.DocDueDate = fechaDespachoCliente;
                    MyDoc.TaxDate = fechaEmisionCliente;
                    //Tipo Venta
                    MyDoc.UserFields.Fields.Item("U_BPP_MDMT").Value = TipoVenta;
                    ////Vendedor
                    //MyDoc.UserFields.Fields.Item("SalesPersonCode").Value = Vendedor;
                    MyDoc.UserFields.Fields.Item("U_SYP_NUMOCCL").Value = Orden;
                    MyDoc.UserFields.Fields.Item("U_SYP_INCOTERM").Value = incoterm;
                    MyDoc.UserFields.Fields.Item("U_U_SYS_FECHADESPACHO").Value = FechaDespacho;
                    MyDoc.Comments = Observaciones;
                    MyDoc.DocType = BoDocumentTypes.dDocument_Items;
                    foreach (var y in array) {
                        MyDoc.Lines.ItemCode = y.ItemCode;
                        MyDoc.Lines.ItemDescription = y.ModeloBateria;
                        MyDoc.Lines.Quantity = Convert.ToDouble(y.CantidadConfirmada);
                        MyDoc.Lines.UnitPrice = Convert.ToDouble(y.PrecioUnitario);
                        MyDoc.Lines.DiscountPercent = 0.00;
                        MyDoc.Lines.UserFields.Fields.Item("U_SYP_HORAED").Value=12;
                        MyDoc.Lines.TaxCode = "EXE_IVA";
                        MyDoc.Lines.ProjectCode ="PLANTA";
                        MyDoc.Lines.WarehouseCode = "06";
                        MyDoc.Lines.Add();
                    }

                    if (MyDoc.Add() != 0)
                    {
                        string pr = ConexionApiSap.myCompany.GetLastErrorDescription();
                        Console.WriteLine(pr);
                        Console.WriteLine(ConexionApiSap.myCompany.GetLastErrorDescription());
                        ConexionApiSap.myCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                        return false;
                    }
                    else {
                        ConexionApiSap.myCompany.EndTransaction(BoWfTransOpt.wf_Commit);
                        daoPedidos.GuardarActualizacionObservaciones(PedidoId, Observaciones, FechaDocumento, FechaDespacho,CantidadNueva,PrecioNuevo,PesoNuevo);
                        daoPedidos.GuardarActualizacionEstado(PedidoId,2);
                        foreach (var z in array) {
                            daoPedidos.GuardarActualizacionDetallePedido(PedidoId,z.ItemCode,z.CantidadConfirmada.Value);
                        }
                        Console.WriteLine("Se agrego correctamente el pedido con folio: "+ConexionApiSap.myCompany.GetNewObjectKey());
                    }
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else {

                return false;
            }

        }


        public ActionResult PedidosConfirmados()
        {
            if (Session["usuario"] != null)
            {
                ViewBag.JavaScript = "General/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
                ViewBag.dxdevweb = "1";

                daoUtilitarios = new DaoUtilitarios();
                daoPedidos = new DaoPedidos();


                ViewBag.MenuAcceso = Session["Menu"];

                var datMenu = daoUtilitarios.ConsultarMenuPrincipal();
                ViewBag.MenuPrincipal = datMenu;
                var datMenuOpciones = daoUtilitarios.ConsultarMenuOpciones();
                ViewBag.MenuOpciones = datMenuOpciones;
                var datSubMenuOpciones = daoUtilitarios.ConsultarSubMenuOpciones();
                ViewBag.SubMenuOpciones = datSubMenuOpciones;
                var fechasIngreso = daoPedidos.ListaFechasPedidos();
                ViewBag.FechasPedido = fechasIngreso;

                var SestadoPedidos = daoPedidos.StatusPedidos();
                ViewBag.EstadosPedido = SestadoPedidos;
                

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public JsonResult ObtenerPedidosGenerales(int estado)
        {
            try
            {

                daoPedidos = new DaoPedidos();
                var Result = daoPedidos.ConsultarPedidosGeneral(estado);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }



        public ActionResult PedidosCancelados()
        {
            if (Session["usuario"] != null)
            {
                ViewBag.JavaScript = "General/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
                ViewBag.dxdevweb = "1";

                daoUtilitarios = new DaoUtilitarios();

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
        public JsonResult ObtenerPedidosCanceladosGenerales(int estado)
        {
            try
            {

                daoPedidos = new DaoPedidos();
                var Result = daoPedidos.ConsultarPedidosCanceladosGeneral(estado);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public bool ActualizarFechas(string FechaCargaLista, string DespachoPuerto, string Zarpe, string Arribo, string Entrega)
        {
            try
            {
                daoPedidos = new DaoPedidos();
                //var Result = daoPedidos.ConsultarPedidosCanceladosGeneral(estado);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public JsonResult SelectFechasPedido()
        {
            try
            {
                daoPedidos = new DaoPedidos();
                var Result = daoPedidos.ListaFechasPedidos();
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public string BusquedaFechasPedido(int PedidoId, int FechaId)
        {
            try
            {
                daoPedidos = new DaoPedidos();
                string Result = null;
                if (FechaId == 1) {
                    Result = daoPedidos.ConsultaFechaCargaLista(PedidoId);
                }
                if (FechaId == 2)
                {
                    Result = daoPedidos.ConsultaFechaDespachoPuerto(PedidoId);
                }
                if (FechaId == 3)
                {
                    Result = daoPedidos.ConsultaFechaZarpe(PedidoId);
                }
                if (FechaId == 4)
                {
                    Result = daoPedidos.ConsultaFechaArribo(PedidoId);
                }
                if (FechaId == 5)
                {
                    Result = daoPedidos.ConsultaFechaEntrega(PedidoId);
                }
                return Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        [HttpPost]
        public bool GuardarFecha(int PedidoId, int FechaId ,DateTime FechaIng)
        {
            bool Result = false;

            try
            {
                daoPedidos = new DaoPedidos();
                if (FechaId == 1)
                {
                    Result = daoPedidos.GuardarActualizacionFechaCargaLista(PedidoId,FechaIng);
                }
                if (FechaId == 2)
                {
                    Result = daoPedidos.GuardarActualizacionDespachoPuerto(PedidoId, FechaIng);
                }
                if (FechaId == 3)
                {
                    Result = daoPedidos.GuardarActualizacionZarpe(PedidoId,FechaIng);
                }
                if (FechaId == 4)
                {
                    Result = daoPedidos.GuardarActualizacionArribo(PedidoId, FechaIng);
                }
                if (FechaId == 5)
                {
                    Result = daoPedidos.GuardarActualizacionEntrega(PedidoId, FechaIng);
                }
                return Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public bool ActualizarEstado(int PedidoId, int Estado)
        {
            try
            {
                daoPedidos = new DaoPedidos();
                var Result = daoPedidos.GuardarActualizacionEstadoPedidoConfirmado(PedidoId,Estado);
                return Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}