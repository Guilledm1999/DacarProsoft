using System;
using DacarProsoft.Datos;
using DacarProsoft.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAPbobsCOM;
using DacarDatos.Datos;
using System.Net.Mail;
using System.Net;
using System.Globalization;

namespace DacarProsoft.Controllers
{
    public class PedidosController : Controller
    {
        private DaoUtilitarios daoUtilitarios { get; set; } = null;
        private DaoPedidos daoPedidos { get; set; } = null;
        private DaoPackingList daoPackingList { get; set; } = null;

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

        public JsonResult ObtenerPedidosIngresados()
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

        public JsonResult ObtenerPedidosModificados()
        {
            try
            {
                daoPedidos = new DaoPedidos();
                var Result = daoPedidos.ConsultarPackingIngreseadosModificados(7);
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
        public JsonResult ObtenerDetalleActualizadoPedido(int PedidoId)
        {
            try
            {

                daoPedidos = new DaoPedidos();
                var Result = daoPedidos.ConsultarPedidoActualizadoDetalle(PedidoId);
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
        public bool AprobacionCliente(int PedidoId)
        {
            try
            {

                daoPedidos = new DaoPedidos();
                var Result = daoPedidos.ConsultarAprobacionClienteExt(PedidoId);
                return Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpPost]
        public bool ModificarPedidoCliente(int pedidoId, List<PedidoClienteDetalle> array, int CantidadNueva, Decimal PrecioNuevo, Decimal PesoNuevo, string Observacion, string Orden) {
            string DirCorreo = "";
            string ClavCorreo = "";
            daoPedidos = new DaoPedidos();
            daoUtilitarios = new DaoUtilitarios();
            daoPedidos.GuardarModificacionPedidoCliente(pedidoId, Observacion, CantidadNueva, PrecioNuevo, PesoNuevo);
            
            foreach (var z in array)
            {
                daoPedidos.GuardarActualizacionDetallePedido(pedidoId, z.ItemCode, z.CantidadConfirmada.Value);
            }

            var result= daoPedidos.GuardarActualizacionEstado(pedidoId, 7);

            var cabeceraPedido = daoPedidos.InformacionCabeceraPedido(pedidoId);

            string ordenCompra = "";
            string carCode = "";
            string sucursal = "";
            string fechaEmision="";
            foreach (var y in cabeceraPedido)
            {
                ordenCompra = y.OrdenCompra;
                carCode = y.CardCode;
                sucursal = y.Sucursal;
                DateTime fechaDoc = Convert.ToDateTime(y.FechaEmision, CultureInfo.InvariantCulture);
                fechaEmision = fechaDoc.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);

            }

            var CorreoBase = daoUtilitarios.ConsultarCorreoElectronico();
            var CorreoCliente = daoPedidos.ConsultarCorreoCliente(carCode, sucursal);

            foreach (var x in CorreoBase)
            {
                DirCorreo = x.DireccionCorreo;
                ClavCorreo = x.ClaveCorreo;
            }
            try
            {
                MailMessage mm = new MailMessage("bateriasdacar1975@gmail.com", CorreoCliente);
                mm.Subject = "Order Update";
                //MailAddress copy = new MailAddress("Notification_List@contoso.com");
                //mm.CC.Add(CorreoCopia);
                mm.Body = "An adjustment has been made in order "+Orden+" that was registered on "+fechaEmision+", for approval enter the platform: http://app2.bateriasdacar.com:8033/Pedidos/ConsultarPedidos .";
                //mm.Attachments.Add(new Attachment(new MemoryStream(bytesStreams), NombreCliente + "-" + Order + ".pdf"));
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("El error:" + ex);
            }

            return result;
        }

        [HttpPost]
        public string RegistrarPedidoEnSap(int PedidoId, string Orden, DateTime FechaDocumento, DateTime FechaDespacho,string TipoVenta, string Vendedor, 
            string Observaciones, List<PedidoClienteDetalle> array,int CantidadNueva, Decimal PrecioNuevo, Decimal PesoNuevo) {

            daoPedidos = new DaoPedidos();
            daoUtilitarios = new DaoUtilitarios();

            string DirCorreo="";
            string ClavCorreo = "";
            string cardCode = null;
            string cardname = null;
            DateTime fechaEmisionCliente = DateTime.Now;
            DateTime fechaDespachoCliente =DateTime.Now;
            string incoterm = null;
            try
            {
                var PedidoCabecera = daoPedidos.ConsultarPedidoCabecera(PedidoId);

                foreach (var x in PedidoCabecera)
                {
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
                        foreach (var y in array)
                        {
                            MyDoc.Lines.ItemCode = y.ItemCode;
                            MyDoc.Lines.ItemDescription = y.ModeloBateria;
                            MyDoc.Lines.Quantity = Convert.ToDouble(y.CantidadConfirmada);
                            MyDoc.Lines.UnitPrice = Convert.ToDouble(y.PrecioUnitario);
                            MyDoc.Lines.DiscountPercent = 0.00;
                            MyDoc.Lines.UserFields.Fields.Item("U_SYP_HORAED").Value = 12;
                            MyDoc.Lines.TaxCode = "EXE_IVA";
                            MyDoc.Lines.ProjectCode = "PLANTA";
                            MyDoc.Lines.WarehouseCode = "06";
                            MyDoc.Lines.Add();
                        }

                        if (MyDoc.Add() != 0)
                        {
                            string pr = ConexionApiSap.myCompany.GetLastErrorDescription();
                            Console.WriteLine(pr);
                            Console.WriteLine(ConexionApiSap.myCompany.GetLastErrorDescription());
                            //ConexionApiSap.myCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                            //ConexionApiSap.myCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                            return pr;
                        }
                        else
                        {
                            ConexionApiSap.myCompany.EndTransaction(BoWfTransOpt.wf_Commit);
                            daoPedidos.GuardarActualizacionObservaciones(PedidoId, Observaciones, FechaDocumento, FechaDespacho, CantidadNueva, PrecioNuevo, PesoNuevo);
                            daoPedidos.GuardarActualizacionEstado(PedidoId, 2);
                            foreach (var z in array)
                            {
                                daoPedidos.GuardarActualizacionDetallePedido(PedidoId, z.ItemCode, z.CantidadConfirmada.Value);
                            }
                            Console.WriteLine("Se agrego correctamente el pedido con folio: " + ConexionApiSap.myCompany.GetNewObjectKey());

                            var cabeceraPedido = daoPedidos.InformacionCabeceraPedido(PedidoId);

                            string nombreCliente = "";
                            string ordenCompra = "";
                            string carCode = "";
                            int estadoPedido = 0;
                            string sucursal = "";
                            foreach (var y in cabeceraPedido) {
                                nombreCliente = y.NombreCliente;
                                ordenCompra = y.OrdenCompra;
                                carCode = y.CardCode;
                                estadoPedido = y.EstadoPedido.Value;
                                sucursal = y.Sucursal;
                            }
                            var CorreoBase = daoUtilitarios.ConsultarCorreoElectronico();
                            var CorreoCliente = daoPedidos.ConsultarCorreoCliente(carCode,sucursal);

                            foreach (var x in CorreoBase)
                            {
                                DirCorreo = x.DireccionCorreo;
                                ClavCorreo = x.ClaveCorreo;
                            }
                            try
                            {
                                MailMessage mm = new MailMessage("bateriasdacar1975@gmail.com", CorreoCliente);
                                mm.Subject = "Order confirmation";
                                //MailAddress copy = new MailAddress("Notification_List@contoso.com");
                                //mm.CC.Add(CorreoCopia);
                   

                                mm.Body = "Dear customer, your order "+ ordenCompra + "was confirmed and will be shipped on "+fechaDespachoCliente+", you can check the status through the platform "+ "http://app2.bateriasdacar.com:8033/Pedidos/ConsultarPedidos";
                                //mm.Attachments.Add(new Attachment(new MemoryStream(bytesStreams), NombreCliente + "-" + Order + ".pdf"));
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
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("El error:" + ex);
                            }
                            return "True";
                        }
                    }
                    else
                    {
                        return "Error";
                    }

                }
                else
                {

                    return "Registrada";
                }

            }
            catch(Exception ex) {
                return ex.ToString();
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
        public ActionResult PedidosModificados()
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

        public ActionResult CronogramaExportacion()
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

        public JsonResult EventosCalendarioPedidos() {

            daoPedidos = new DaoPedidos();
            var result = daoPedidos.InformacionEventosMes();

            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult IngresoCronogramaExportacion()
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
        public JsonResult ConsultarEventosMes()
        {
            daoPedidos = new DaoPedidos();
            var result = daoPedidos.ConsultarEventosMes();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public bool InsertarEventoMes(CronogramaExportacion crono)
        {

            daoPedidos = new DaoPedidos();

            var result = daoPedidos.IngresarEventosMes(crono.Orden, crono.Cliente, crono.FechaPedido.Value, crono.FechaDespacho.Value, Convert.ToString(crono.FechaZarpe), crono.Booking.Value, crono.TotalContenedores.Value, crono.CardCode, crono.Destino);

            return result;
        }
        public bool ActualizarEventoMes(CronogramaExportacion crono, int Key)
        {

            daoPedidos = new DaoPedidos();

            var result = daoPedidos.ActualizarEventosMes(crono, Key);

            return result;
        }
        public bool EliminarEventoMes(CronogramaExportacion crono)
        {

            daoPedidos = new DaoPedidos();
            var result = daoPedidos.EliminarEventosMes(crono.CronogramaExportacionId);
            return result;
        }
        public string ConvertirPdf(string cardCode, string numeroOrden) {

            daoPedidos = new DaoPedidos();

            int atcEntry = daoPedidos.BusquedaAtcEntry(cardCode, numeroOrden);
            if (atcEntry != 0)
            {
                string ruta = daoPedidos.ConsultarRutaAnexo(atcEntry);
                Byte[] bytes = System.IO.File.ReadAllBytes(@ruta);
                String file = Convert.ToBase64String(bytes);
                return file;
            }
            else {
                return "";
            }
        }

        public JsonResult ConsultarDsecripFact(string cardCode, string numeroOrden)
        {
            daoPackingList = new DaoPackingList();
            daoPedidos = new DaoPedidos();

            int docEntry = daoPedidos.BusquedaFactura(cardCode, numeroOrden);
            var result = daoPackingList.BusquedaFacturaDetalleReserva(docEntry);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}