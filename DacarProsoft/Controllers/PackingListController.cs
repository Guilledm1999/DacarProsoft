﻿using DacarProsoft.Datos;
using DacarProsoft.Models;
using QRCoder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Image;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Kernel.Events;
using System.Drawing;
using iText.Layout.Borders;
using iText.Kernel.Colors;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Geom;
using System.Text;
using System.Security.Cryptography;
using System.Globalization;

namespace DacarProsoft.Controllers
{
    public class PackingListController : Controller
    {
        private DaoUtilitarios daoUtilitarios { get; set; } = null;
        private DaoPackingList daoPackilist { get; set; } = null;
        private DaoOrdenesVentas daoOrdenesVentas { get; set; } = null;
        private EventoPagina evePag { get; set; } = null;
        string password = "$$Dacar123.*";

        // GET: PackingList
        public ActionResult IngresosPackingList()
        {
            if (Session["usuario"] != null)
            {
                var prueba = ConexionApiSap.Open();

                ViewBag.JavaScript = "General/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
                ViewBag.dxdevweb = "1";
                ViewBag.MenuAcceso = Session["Menu"];

                daoUtilitarios = new DaoUtilitarios();
                daoPackilist = new DaoPackingList();

                var datPaises = daoPackilist.ConsultarPaises();
                ViewBag.Paises = datPaises;

                var datPaisOrigen = daoPackilist.ConsultarPaisOrigen();
                ViewBag.PaisOrigen = datPaisOrigen;

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

        public ActionResult ListadoPackingList()
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

        public JsonResult ObtenerPalletIngresados(string tipo)
        {
            try
            {

                daoPackilist = new DaoPackingList();

                var Result = daoPackilist.ConsultarPackingIngreseados(tipo);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ObtenerPalletIngresadosComext()
        {
            try
            {
                daoPackilist = new DaoPackingList();

                var Result = daoPackilist.ConsultarPackingIngreseadosComext();
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ObtenerDetallePalletIngresados(int PackingId, int PalletId)
        {
            try
            {
                daoPackilist = new DaoPackingList();

                var Result = daoPackilist.ConsultarPalletsIngreseados(PackingId, PalletId);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ConsultarPalletsDetalleIngreseados(int PackingId, int PalletId)
        {
            try
            {
                daoPackilist = new DaoPackingList();

                var Result = daoPackilist.ConsultarPalletsDetalleIngreseados(PackingId, PalletId);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public bool ConsultarEstadoPacking(int PackingId)
        {
            try
            {
                daoPackilist = new DaoPackingList();

                var Result = daoPackilist.ConsultarEstadoPacking(PackingId);
                return Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


        [HttpPost]
        public int RegistrarPallet(string NumeroDocumento, string NumeroOrden, string NombreCliente, string Origen, string Destino, int IdentificadorDetalle, string tipo, int vari, 
            string Sucursal, int numeroContenedor)
        {
            int identificador1=0;
            daoPackilist = new DaoPackingList();
            daoOrdenesVentas = new DaoOrdenesVentas();
            try
            {
                for (int i = 1; i <= numeroContenedor; i++)
                {
                    var result = daoPackilist.IngresarPacking(Convert.ToInt32(NumeroDocumento), NumeroOrden, NombreCliente, Origen, Destino, 20, tipo, Sucursal, i);
                    identificador1 = result;

                    if (NumeroOrden == "PHE/6609" || NumeroOrden == "DAC-USA-01" || NumeroOrden == "45110" || NumeroOrden == "PHE/7062") {
                        var PackingDetalle = daoOrdenesVentas.ListadoDetalleFacturasReservaSap(IdentificadorDetalle);
                        foreach (var x in PackingDetalle)
                        {
                            var resultado = daoPackilist.IngresarPackingDtl(result, x.ItemCode, x.Descripcion, x.Cantidad);
                        }
                    }
                    else {
                        if (vari == 1)
                        {
                            var PackingDetalle = daoOrdenesVentas.ListadoDetalleOrdenesVentasSap(IdentificadorDetalle);
                            foreach (var x in PackingDetalle)
                            {
                                var resultado = daoPackilist.IngresarPackingDtl(result, x.ItemCode, x.Descripcion, x.Cantidad);
                            }
                        }
                        if (vari == 2)
                        {
                            var PackingDetalle = daoOrdenesVentas.ListadoDetalleFacturasReservaSap(IdentificadorDetalle);
                            foreach (var x in PackingDetalle)
                            {
                                var resultado = daoPackilist.IngresarPackingDtl(result, x.ItemCode, x.Descripcion, x.Cantidad);
                            }
                        }
                    }                  
                }

                return identificador1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 0;

            }
        }
        //Obtiene el detalle del pallet
        public JsonResult ObtenerDetallePallet(int IdentificadorPacking)
        {
            try
            {
                daoPackilist = new DaoPackingList();

                var Result = daoPackilist.ConsultarPackingDtl(IdentificadorPacking);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public int ObtenerNumeroPallet(int IdentificadorPacking)
        {
            try
            {
                daoPackilist = new DaoPackingList();

                var Result = daoPackilist.ConsultarNumeroPallet(IdentificadorPacking);
                return Result; }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ObtenerPalletList(int PackingId)
        {
            try
            {
                daoPackilist = new DaoPackingList();

                var Result = daoPackilist.ConsultarPalletCant(PackingId);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        [HttpPost]
        public JsonResult ValidarIngresoCantidad(int PackingId, string CodeItem, int valor)
        {
            int resta = 0;
            daoPackilist = new DaoPackingList();

            var detalleAcumPackingLocal = daoPackilist.ConsultarAcumItem(PackingId, CodeItem);
            var detallePackingLocal = daoPackilist.ConsultarTotalItem(PackingId, CodeItem);

            resta = detallePackingLocal - detalleAcumPackingLocal;

            if (valor <= resta)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ObtenerDetallePalletList(int PackingId, int PalletPackingId)
        {
            try
            {
                daoPackilist = new DaoPackingList();

                var Result = daoPackilist.ConsultarPalletDetalle(PackingId, PalletPackingId);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpPost]
        public bool EliminarPallet(int PalletId)
        {
            daoPackilist = new DaoPackingList();

            var res = daoPackilist.EliminarPallet(PalletId);
            return res;
        }
        [HttpPost]
        public bool ActualizarCantidadPallet(int PackingId, int NuevaCantidad)
        {
            daoPackilist = new DaoPackingList();

            var res = daoPackilist.ActualizarCantidadPallet(PackingId, NuevaCantidad);
            return res;
        }

        [HttpPost]
        public bool EliminarPackingCompleto(int PackingId)
        {
            daoPackilist = new DaoPackingList();

            var res = daoPackilist.EliminarPacking(PackingId);
            return res;
        }
        [HttpPost]
        public bool RegistrarPalletPacking(List<DetallePallet> Array2, List<DetallePallet> Array, int idPacking, int PalletNumber, string LargoPallet, string AltoPallet, string AnchoPallet, string VolumenPallet, string PesoNeto, string PesoBruto)
        {
            string PackOrden = null;
            string PackCliente = null;
            string PackOrigen = null;
            string PackDestino = null;

            daoPackilist = new DaoPackingList();
            //foreach (var x in Array)
            //{
            //    var detalleAcumPackingLocal = daoPackilist.ConsultarAcumItem(idPacking, x.ItemCode);
            //    //var detallePackingLocal = daoPackilist.ConsultarPalletPackingPorItem(PackingId, CodeItem);
            //    var detallePackingLocal = daoPackilist.ConsultarTotalItem(idPacking, x.ItemCode);
            //    resta = detallePackingLocal- detalleAcumPackingLocal;
            //    if (x.Pallet>resta) {
            //        return false;
            //    }
            //}
            var packingDetalle = daoPackilist.ConsultarNumeroOrden(idPacking);

            foreach (var x in packingDetalle) {
                PackOrden = x.NumeroOrden;
                PackCliente = x.NombreCliente;
                PackOrigen = x.Origen;
                PackDestino = x.Destino;
            }
            var codigoQr = GenerarQr(Convert.ToString(PalletNumber), PackOrden, PackCliente, PesoBruto, PesoNeto, PackOrigen, PackDestino, Array);
            try
            {
                daoPackilist = new DaoPackingList();

                var Result = daoPackilist.IngresarPackingPallet(idPacking, PalletNumber, Convert.ToDecimal(LargoPallet), Convert.ToDecimal(AltoPallet), Convert.ToDecimal(AnchoPallet), Convert.ToDecimal(VolumenPallet), Convert.ToDecimal(PesoNeto), Convert.ToDecimal(PesoBruto), codigoQr);
                var codigoQr2 = GenerarQrPdf(idPacking, Result);
                var actualizar = daoPackilist.ActualizarPackingPallet(Result, codigoQr2);

                foreach (var x in Array) {
                    if (x.Pallet > 0) {
                        daoPackilist.IngresarPackingPalletDetalle(Result, idPacking, x.ItemCode, x.DescriptionItem, x.Pallet);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
                throw;
            }
        }
        public JsonResult ConsultaPackingListCabecera()
        {
            try
            {
                daoPackilist = new DaoPackingList();

                var Result = daoPackilist.ConsultarPackingListCabecera();
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public JsonResult ConsultaPackingListDetalle(int PackingListId)
        {
            try
            {
                daoPackilist = new DaoPackingList();

                var Result = daoPackilist.ConsultarPackingListDetalle(PackingListId);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public JsonResult ConsultaOrdenVentaListCabecera(String Exportacion)
        {
            try
            {
                daoOrdenesVentas = new DaoOrdenesVentas();

                var Result = daoOrdenesVentas.ListadoCabeceraOrdenesVentasSap(Exportacion);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ConsultaFacturaReservaListCabecera(String factReserv)
        {
            try
            {
                daoOrdenesVentas = new DaoOrdenesVentas();

                var Result = daoOrdenesVentas.ListadoCabeceraFacturasReservaSap(factReserv);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ConsultaFacturaReservaListCabeceraCancelada(String factReserv)
        {
            try
            {
                daoOrdenesVentas = new DaoOrdenesVentas();

                var Result = daoOrdenesVentas.ListadoCabeceraFacturasReservaSapCanceladas(factReserv);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public JsonResult ConsultaOrdenVentaDetalle(int DocEntry)
        {
            try
            {
                daoOrdenesVentas = new DaoOrdenesVentas();

                var Result = daoOrdenesVentas.ListadoDetalleOrdenesVentasSap(DocEntry);
                if (Result.Count == 0) {
                    Result = daoOrdenesVentas.ListadoDetalleFacturasReservaSap(DocEntry);
                }
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ConsultaFactReservaDetalle(int DocEntry)
        {
            try
            {
                daoOrdenesVentas = new DaoOrdenesVentas();

                var Result = daoOrdenesVentas.ListadoDetalleFacturasReservaSap(DocEntry);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        [HttpPost]
        public int ObtenerNumeroPallet(String Orden)
        {
            daoPackilist = new DaoPackingList();

            var res = daoPackilist.ConsultarPackingListxPallet(Orden);
            return res;
        }

        [HttpGet]
        public string GenerarQrPorCodigo(int PakcingId, int PalletId)
        {
            string orden = null;
            string cliente = null;
            decimal pesoBruto = 0;
            decimal pesoNeto = 0;
            string origen = null;
            string destino = null;
            int pallet = 0;

            daoPackilist = new DaoPackingList();

            var Cabecera = daoPackilist.ConsultarInfoPallets(PakcingId, PalletId);
            var Detalle = daoPackilist.ConsultarPalletsDetalleIngreseados(PakcingId, PalletId);
            List<ItemsPallet> lst = new List<ItemsPallet>();

            foreach (var x in Detalle)
            {
                lst.Add(new ItemsPallet
                {
                    cantidad = Convert.ToString(x.CantidadItem),
                    codigo = Convert.ToString(x.ItemCode),
                    descripcion = Convert.ToString(x.DescriptionCode)
                });
            }
            foreach (var y in Cabecera)
            {
                orden = y.NumeroOrden;
                cliente = y.NombreCliente;
                pesoBruto = y.PesoBruto;
                pesoNeto = y.PesoNeto;
                origen = y.Origen;
                destino = y.Destino;
                pallet = y.PalletNumber;
            }
            Byte[] prueba;
            String codigo;
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lst);

            ViewBag.txtQRCode = orden;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            //QRCodeData qrCodeData = qrGenerator.CreateQrCode("{Orden:" + orden + ", Cliente:" + cliente + ", GrossWeight:" + pesoBruto + ", NetWeight:" + pesoNeto + ", Origen:" +
            //    origen + ", Destino:" + destino + "} Items:" + json, QRCodeGenerator.ECCLevel.Q);
            QRCodeData qrCodeData = qrGenerator.CreateQrCode("{Orden:" + orden + ", Cliente:" + cliente + ", GrossWeight:" + pesoBruto + ", NetWeight:" + pesoNeto + ", Origen:" +
                origen + ", Destino:" + destino + "}", QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            using (Bitmap bitMap = qrCode.GetGraphic(20))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    ViewBag.imageBytes = ms.ToArray();
                    prueba = ms.ToArray();
                    codigo = "data:image/png;base64," + Convert.ToBase64String(prueba);
                }
            }
            var base64String = Convert.ToBase64String(prueba);
            return base64String;
        }

        [HttpPost]
        public string GenerarQr(string PalletNumero, String Orden, String Cliente, String GrossWeight, String NetWeight, String Origen, String Destino, List<DetallePallet> items)
        {
            List<ItemsPallet> lst = new List<ItemsPallet>();

            foreach (var x in items) {
                lst.Add(new ItemsPallet {
                    cantidad = Convert.ToString(x.CantidadItem),
                    codigo = Convert.ToString(x.ItemCode),
                    descripcion = Convert.ToString(x.DescriptionItem)
                });
            }
            Byte[] prueba;
            String codigo;
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(lst);

            ViewBag.txtQRCode = Orden;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            //QRCodeData qrCodeData = qrGenerator.CreateQrCode("{Orden:" + Orden + ", Cliente:" + Cliente + ", GrossWeight:" + GrossWeight + ", NetWeight:" + NetWeight + ", Origen:" +
            //    Origen + ", Destino:" + Destino + "} Items:" + json, QRCodeGenerator.ECCLevel.Q);
            QRCodeData qrCodeData = qrGenerator.CreateQrCode("{Orden:" + Orden + ", Cliente:" + Cliente + ", GrossWeight:" + GrossWeight + ", NetWeight:" + NetWeight + ", Origen:" +
              Origen + ", Destino:" + Destino + "}", QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            using (Bitmap bitMap = qrCode.GetGraphic(20))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    ViewBag.imageBytes = ms.ToArray();
                    prueba = ms.ToArray();
                    codigo = "data:image/png;base64," + Convert.ToBase64String(prueba);
                }
            }
            var base64String = Convert.ToBase64String(prueba);

            return base64String;
        }
        public string Encrypt(string plainText)
        {
            if (plainText == null)
            {
                return null;
            }
            // Get the bytes of the string
            var bytesToBeEncrypted = Encoding.UTF8.GetBytes(plainText);
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            // Hash the password with SHA256
            passwordBytes = SHA512.Create().ComputeHash(passwordBytes);
            var bytesEncrypted = Encrypt(bytesToBeEncrypted, passwordBytes);

            return Convert.ToBase64String(bytesEncrypted);
        }

        private static byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }

                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        [HttpPost]
        public string GenerarQrPdf(int packingIdDestino, int palletId)
        {
            daoPackilist = new DaoPackingList();

            var encriptarpackingIdDestino = Encrypt(Convert.ToString(packingIdDestino));
            var encriptarpalletId = Encrypt(Convert.ToString(palletId));
            var encriptarString = Encrypt("SI");

            var url = daoPackilist.ConsultarUrlImprimirPalletPdf();

            Byte[] prueba;
            String codigo;
          
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            //QRCodeData qrCodeData = qrGenerator.CreateQrCode("{Orden:" + Orden + ", Cliente:" + Cliente + ", GrossWeight:" + GrossWeight + ", NetWeight:" + NetWeight + ", Origen:" +
            //    Origen + ", Destino:" + Destino + "} Items:" + json, QRCodeGenerator.ECCLevel.Q);
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(url + "PackingId=" + encriptarpackingIdDestino + "&PalletId=" + encriptarpalletId + "&Fondo="+ encriptarString, QRCodeGenerator.ECCLevel.Q);
            
            //QRCodeData qrCodeData = qrGenerator.CreateQrCode("https://21.0.1.241/PackingList/PalletPdf?" + "PackingId=" + packingIdDestino + "&PalletId=" + palletId + "&&Fondo=SI", QRCodeGenerator.ECCLevel.Q);
            //QRCodeData qrCodeData = qrGenerator.CreateQrCode("http://bateriasdacar.com/", QRCodeGenerator.ECCLevel.Q);

            QRCode qrCode = new QRCode(qrCodeData);

            using (Bitmap bitMap = qrCode.GetGraphic(20))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    ViewBag.imageBytes = ms.ToArray();
                    prueba = ms.ToArray();
                    codigo = "data:image/png;base64," + Convert.ToBase64String(prueba);
                }
            }
            var base64String = Convert.ToBase64String(prueba);

            return base64String;
        }

        public bool GuardarPackingList(int Pallet, string Orden, string Cliente, string Volumen, string GrossWeight, string NetWeight, string Origen, string Destino, List<DetallePallet> items, string Codigo) {

            List<ItemsPallet> lst = new List<ItemsPallet>();

            foreach (var x in items)
            {
                lst.Add(new ItemsPallet
                {
                    cantidad = Convert.ToString(x.CantidadItem),
                    codigo = Convert.ToString(x.ItemCode),
                    descripcion = Convert.ToString(x.DescriptionItem)
                });
            }

            bool detalle = false;
            daoPackilist = new DaoPackingList();
            var IdCabecera = daoPackilist.IngresarPackingListCabecera(Pallet, Convert.ToDecimal(GrossWeight), Convert.ToDecimal(NetWeight), Cliente, Orden, Origen, Destino, Convert.ToDecimal(Volumen), Codigo);
            foreach (var x in lst)
            {
                detalle = daoPackilist.IngresarPackingListDetalle(IdCabecera, x.descripcion, Convert.ToInt32(x.cantidad));
            }

            if (detalle == true && IdCabecera != 0)
            {
                return true;
            }
            else {
                return false;
            }

        }
       
        [HttpGet]
        public ActionResult PalletPdf3(int PackingId, int PalletId, string Fondo)
        {
            string Orden = null;
            string Cliente = null;
            decimal PesoBruto = 0;
            decimal PesoNeto = 0;
            string Origen = null;
            string Destino = null;
            string CodigoQr = null;
            int CantidadPallet = 0;
            string Sucursal = null;

            int primerFila = 0;
            int segundaFila = 0;
            int tercerFila = 0;
            int cuartaFila = 0;
            int quintaFila = 0;

            int palletNumber = 0;
            daoPackilist = new DaoPackingList();
            var Cabecera = daoPackilist.ConsultarInfoPallets(PackingId, PalletId);
            var Detalle = daoPackilist.ConsultarPalletsDetalleIngreseados(PackingId, PalletId);

            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            PdfFont No_Bold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            List<ItemsPallet> lst = new List<ItemsPallet>();
            foreach (var y in Cabecera)
            {
                Orden = y.NumeroOrden;
                Cliente = y.NombreCliente;
                PesoBruto = Decimal.Round(y.PesoBruto,0);
                PesoNeto = Decimal.Round(y.PesoNeto,0);
                Origen = y.Origen;
                Destino = y.Destino;
                Sucursal = y.Sucursal;
                CantidadPallet = y.CantidadPallet;
                palletNumber = y.PalletNumber;
                CodigoQr = y.CodigoQr;
            }

            byte[] bytes = Convert.FromBase64String(CodigoQr);


            foreach (var x in Detalle)
            {
                lst.Add(new ItemsPallet
                {
                    cantidad = Convert.ToString(x.CantidadItem),
                    codigo = Convert.ToString(x.ItemCode),
                    descripcion = Convert.ToString(x.DescriptionCode)
                });
            }


            MemoryStream stream = new MemoryStream();
            PdfWriter writer = new PdfWriter(stream);

            var path = System.IO.Path.Combine(Server.MapPath("~/Images/PackingNe.png"));

            iText.Layout.Element.Image BackPack = new iText.Layout.Element.Image(ImageDataFactory.Create(path));

            PdfDocument pdf = new PdfDocument(writer);
            //Document document = new Document(pdf, iText.Kernel.Geom.PageSize.A5, true);
            //Obtener valores de cm a pulgadas y de ahi multiplicar x 72
            //iText.Kernel.Geom.Rectangle rectangle13x21 = new iText.Kernel.Geom.Rectangle(368.50f, 595.28f);

            //iText.Kernel.Geom.Rectangle rectangle11x16 = new iText.Kernel.Geom.Rectangle(325.98f, 453.54f);
            iText.Kernel.Geom.Rectangle rectangle10x15 = new iText.Kernel.Geom.Rectangle(283.46f, 425.20f);

            //Document document = new Document(pdf, iText.Kernel.Geom.PageSize.A4, true);
            Document document = new Document(pdf, new PageSize(rectangle10x15));

            iText.Layout.Element.Image img = new iText.Layout.Element.Image(ImageDataFactory
             .Create(bytes))
             .SetTextAlignment(TextAlignment.CENTER).SetWidth(325).SetHeight(325);

            pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new FooterHandleEvent3(img, Fondo));

            if (Fondo == "SI") {
                pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new BackgroundPageEvent(BackPack));
                primerFila = 316;
                segundaFila = 275;
                tercerFila = 225;
                cuartaFila = 78;
                quintaFila = 98;
            }

            if (Fondo == "NO") {
                primerFila = 308;
                segundaFila = 265;
                tercerFila = 215;
                cuartaFila = 63;
               quintaFila = 88;
            }          

            Table product = new Table(2);

            product.SetHorizontalAlignment(HorizontalAlignment.CENTER);
            Cell cell = new Cell().Add(new Paragraph("Product").SetFont(bold).SetTextAlignment(TextAlignment.CENTER)).SetBackgroundColor(new DeviceRgb(2, 22, 240));
            product.AddCell(cell);
            Cell cell2 = new Cell().Add(new Paragraph("Unit").SetFont(bold).SetTextAlignment(TextAlignment.CENTER)).SetBackgroundColor(new DeviceRgb(2, 22, 240));
            product.AddCell(cell2);
            List<string> itemspallet = new List<string>();
            int CantidadTotal = 0;

            foreach (var x in Detalle)
            {
                CantidadTotal = CantidadTotal + x.CantidadItem.Value;
            }

            document.Add(new Paragraph("\n"));

            Paragraph palletNumb = new Paragraph("" + palletNumber+"/"+CantidadPallet).SetFont(bold).SetFontSize(14).SetBorder(Border.NO_BORDER).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER);
            PdfCanvas canvasNumbP = new PdfCanvas(pdf.GetFirstPage());
            iText.Kernel.Geom.Rectangle rectNumbP = new iText.Kernel.Geom.Rectangle(24, primerFila, 70, 24);
            new Canvas(canvasNumbP, rectNumbP)
                    .Add(palletNumb);
            //canvasNumbP.Rectangle(rectNumbP);
            canvasNumbP.Stroke();

            Paragraph ParaPesoBruto = new Paragraph("" + PesoBruto+" kg").SetFontSize(13).SetFont(bold).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER);
            PdfCanvas canvasPb = new PdfCanvas(pdf.GetFirstPage());
            iText.Kernel.Geom.Rectangle rectPb = new iText.Kernel.Geom.Rectangle(110, primerFila, 70, 24);
            new Canvas(canvasPb, rectPb)
                    .Add(ParaPesoBruto);
            //canvasPb.Rectangle(rectPb);
            canvasPb.Stroke();

            Paragraph ParaPesoNeto = new Paragraph("" + PesoNeto + " kg").SetFontSize(13).SetFont(bold).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER);
            PdfCanvas canvasPn = new PdfCanvas(pdf.GetFirstPage());
            iText.Kernel.Geom.Rectangle rectPn = new iText.Kernel.Geom.Rectangle(200, primerFila, 70, 24);
            new Canvas(canvasPn, rectPn)
                    .Add(ParaPesoNeto);
            //canvasPn.Rectangle(rectPn);
            canvasPn.Stroke();

            Paragraph p = new Paragraph("" + Orden).SetFont(bold).SetFontSize(14).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER);
            PdfCanvas canvas = new PdfCanvas(pdf.GetFirstPage());
            iText.Kernel.Geom.Rectangle rect = new iText.Kernel.Geom.Rectangle(170, segundaFila, 107, 35);
            new Canvas(canvas, rect)
                    .Add(p);
            //canvas.Rectangle(rect);
            canvas.Stroke();

            Paragraph p2 = new Paragraph("" + Cliente).SetFont(bold).SetFontSize(9).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER);
            PdfCanvas canvas2 = new PdfCanvas(pdf.GetFirstPage());
            iText.Kernel.Geom.Rectangle rect2 = new iText.Kernel.Geom.Rectangle(30, segundaFila, 110, 35);
            new Canvas(canvas2, rect2)
                    .Add(p2);
            //canvas.Rectangle(rect2);
            canvas2.Stroke();

            Paragraph p3 = new Paragraph(Sucursal+"/ " + Destino).SetFixedLeading(10).SetFont(bold).SetFontSize(11).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER);
            PdfCanvas canvas3 = new PdfCanvas(pdf.GetFirstPage());
            iText.Kernel.Geom.Rectangle rect3 = new iText.Kernel.Geom.Rectangle(170, tercerFila-5, 107, 42);
            new Canvas(canvas3, rect3)
                    .Add(p3);
            //canvas.Rectangle(rect3);
            canvas3.Stroke();

            Paragraph p4 = new Paragraph("" + Origen).SetFont(bold).SetFontSize(11).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER);
            PdfCanvas canvas4 = new PdfCanvas(pdf.GetFirstPage());
            iText.Kernel.Geom.Rectangle rect4 = new iText.Kernel.Geom.Rectangle(30, tercerFila, 110, 35);
            new Canvas(canvas4, rect4)
                    .Add(p4);
            //canvas.Rectangle(rect4);
            canvas4.Stroke();

            Paragraph CantTotal = new Paragraph("" + CantidadTotal).SetFont(bold).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER);
            PdfCanvas canvasCantTotal = new PdfCanvas(pdf.GetFirstPage());
            iText.Kernel.Geom.Rectangle rectCantTotal = new iText.Kernel.Geom.Rectangle(145, cuartaFila, 130, 23);
            new Canvas(canvasCantTotal, rectCantTotal)
                    .Add(CantTotal);
            //canvas.Rectangle(rectCantTotal);
            canvasCantTotal.Stroke();

            float[] columnWidth = { 117, 52 };
            Table tabla = new Table(columnWidth);

            Table tabla2 = new Table(columnWidth);

            tabla.SetPaddingLeft(1);

            int j = 0;
            foreach (var x in lst)
            {
                if (j <= 5)
                {
                    tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph(x.descripcion).SetFont(bold).SetFontSize(9.1f)).SetHeight(11.9f).SetBorder(Border.NO_BORDER));
                    tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph("  " + x.cantidad).SetFont(bold).SetFontSize(9.1f)).SetHeight(11.9f).SetBorder(Border.NO_BORDER));
                }
                else {
                    tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph(x.descripcion).SetFont(bold).SetFontSize(9.1f)).SetHeight(11.9f).SetBorder(Border.NO_BORDER));
                    tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE)
                        .Add(new Paragraph("  " + x.cantidad).SetFont(bold).SetFontSize(9.1f)).SetHeight(11.9f).SetBorder(Border.NO_BORDER));
                }
                j = j + 1;
            }

            PdfCanvas canvasTabla1 = new PdfCanvas(pdf.GetFirstPage());
            iText.Kernel.Geom.Rectangle rectTabla1 = new iText.Kernel.Geom.Rectangle(8, quintaFila, 135, 99);
            new Canvas(canvasTabla1, rectTabla1)
            .Add(tabla);
            //canvasTabla1.Rectangle(rectTabla1);
            canvasTabla1.Stroke();

            PdfCanvas canvasTabla2 = new PdfCanvas(pdf.GetFirstPage());
            iText.Kernel.Geom.Rectangle rectTabla2 = new iText.Kernel.Geom.Rectangle(145, quintaFila, 135, 99);
            new Canvas(canvasTabla2, rectTabla2)
            .Add(tabla2);
            //canvasTabla2.Rectangle(rectTabla2);
            canvasTabla2.Stroke();

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
            public BackgroundPageEvent(iText.Layout.Element.Image imgBackPage) {
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

        public class FooterHandleEvent3 : IEventHandler
        {
            iText.Layout.Element.Image imge;
            string etiq;
            int x = 0;
            int y = 0;

            public FooterHandleEvent3(iText.Layout.Element.Image img, string etiqueta)
            {
                imge = img;
                etiq = etiqueta;
            }

            public void HandleEvent(Event @event)
            {
                if (etiq=="SI") {
                    x = 40;
                    y = 81;
                }
                if (etiq=="NO")
                {
                    x = 40;
                    y = 70;
                }
                PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
                PdfDocument pdfDoc = docEvent.GetDocument();
                PdfPage page = docEvent.GetPage();
                PdfCanvas canvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);
                new Canvas(canvas, new iText.Kernel.Geom.Rectangle(37, -3, page.GetPageSize().GetRight() - x, y)).
                    Add(getTable2(docEvent));
            }
            private Table getTable2(PdfDocumentEvent docEvent)
            {
                float[] cellWidth = { 30f, 10f };
                Table tablaEvent = new Table(UnitValue.CreatePercentArray(cellWidth)).UseAllAvailableWidth();
                Style styleCell = new Style().SetBorder(Border.NO_BORDER);
                Style styletext = new Style().SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10f);

                imge.SetWidth(325).SetHeight(325);

                Cell cell = new Cell().SetHeight(92f).Add(new Paragraph("")/*.SetBorder(Border.NO_BORDER)*/);
                tablaEvent.AddCell(cell.SetTextAlignment(TextAlignment.RIGHT).SetBorder(Border.NO_BORDER));
                //tablaEvent.AddCell(cell.SetTextAlignment(TextAlignment.RIGHT));

                cell = new Cell().SetHeight(82f).Add(imge.SetWidth(69).SetHeight(69).SetTextAlignment(TextAlignment.LEFT).AddStyle(styleCell)).SetBorder(Border.NO_BORDER);
                //cell = new Cell().SetHeight(92f).Add(imge.SetAutoScale(true).SetTextAlignment(TextAlignment.LEFT).AddStyle(styleCell));
                tablaEvent.AddCell(cell);
                return tablaEvent;
            }
        }

        public JsonResult ConsultaItemsPackingList(int PackingId)
        {
            try
            {
                daoOrdenesVentas = new DaoOrdenesVentas();

                var Result = daoOrdenesVentas.ListadoItemsPackingList(PackingId);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult DetallesPalletsPackingList(int PackingId)
        {
            try
            {
                daoOrdenesVentas = new DaoOrdenesVentas();

                var Result = daoOrdenesVentas.ListadoDetallesPalletsPackingList(PackingId);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public bool GuardarDetallesPalletsPackingList(int PackingId, string Cliente, string Contenedor, DateTime fecha, string Reserva, string Factura, string Pedido, string Embarcacion, string IntercambioEir, string Referencias, string Productos)
        {
            try
            {
                daoPackilist = new DaoPackingList();

                var Result = daoPackilist.IngresarDetalleGeneralPackingList(PackingId,Cliente, Contenedor, fecha, Reserva, Factura, Pedido, Embarcacion, IntercambioEir, Referencias, Productos);
                return Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        //public bool GuardarDetallesGuiaPackingList(int PackingId, string Cliente, string Contenedor, DateTime fecha, string Reserva, string Factura, string Pedido, string Embarcacion, string IntercambioEir, string Referencias, string Productos)
        //{
        //    try
        //    {
        //        daoPackilist = new DaoPackingList();

        //        var Result = daoPackilist.IngresarDetalleGeneralPackingList(PackingId, Cliente, Contenedor, fecha, Reserva, Factura, Pedido, Embarcacion, IntercambioEir, Referencias, Productos);
        //        return Result;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        throw;
        //    }
        //}
        public bool ActualizarDetallesPalletsPackingList(int PackingId, string Contenedor, DateTime fecha, string Reserva, string Factura, string Embarcacion, string IntercambioEir, string Referencias, string Productos)
        {
            try
            {
                daoPackilist = new DaoPackingList();

                var Result = daoPackilist.ActualizarDetallePackingList(PackingId, Contenedor, fecha, Reserva, Factura, Embarcacion, IntercambioEir, Referencias, Productos);
                return Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public JsonResult DetallesGeneralesPalletsPackingList(int PackingId)
        {
            try
            {
                daoPackilist = new DaoPackingList();

                var Result = daoPackilist.ConsultarDetalleGeneralPackingList(PackingId);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpGet]
        public ActionResult InformePackingList(int PackingId, string Fondo)
        {
            daoOrdenesVentas = new DaoOrdenesVentas();
            int totalUnidades = 0;
            decimal totalVolumen = 0;
            decimal totalPBruto = 0;
            decimal totalPNeto = 0;
            daoPackilist = new DaoPackingList();
            string nombreForaneo = null;
            string nombreCliente = null;
            string contenedor = null;
            string fecha = null;
            string reserva = null;
            string factura = null;
            string pedido = null;
            string embarcacion = null;
            string intercambioEir = null;
            string referencia = null;
            string productos = null;
            string datosTecnicos = null;
            string polaridad = null;
            string generico = null;
            var detalleGeneralPackingList = daoPackilist.ConsultarDetalleGeneralPackingList(PackingId);
            var pallet = daoPackilist.ConsultarPalletPackingCompleto(PackingId);
            var nombreAutorizado = daoPackilist.ConsultarNombreAutorizado();
            foreach (var x in detalleGeneralPackingList)
            {
                nombreCliente = x.ClientePackingList;
                contenedor = x.ContenedorPackingList;
                fecha = x.FechaDePackingList;
                reserva = x.ReservaPackingList;
                factura = x.FacturaPedido;
                pedido = x.PedidoPackingList;
                embarcacion = x.EmbarcacionPackingList;
                intercambioEir = x.IntercambioEirPackingList;
                referencia = x.ReferenciasPackingList;
                productos = x.ProductosPackingList;
            }
            var NumCont = daoPackilist.numeroContenedor(PackingId);

            var NumContTotales=daoPackilist.numeroContenedores(pedido);

            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
            PdfFont No_Bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            iText.Kernel.Colors.Color lineColor = new DeviceRgb(164, 164, 164);

            MemoryStream stream = new MemoryStream();
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf, PageSize.A4, true);
            LineSeparator ls = new LineSeparator(new SolidLine());
            //pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new FooterHandleEventFirmas(referencia,nombreAutorizado));


            PdfPage page = pdf.AddNewPage();
            iText.Kernel.Geom.Rectangle NumeroContenedor = new iText.Kernel.Geom.Rectangle(540, 730, 30, 30);
            PdfCanvas pdfCanvas = new PdfCanvas(page);

            //pdfCanvas.Rectangle(NumeroContenedor);
            pdfCanvas.Stroke();
            Canvas canvas = new Canvas(pdfCanvas, NumeroContenedor);
            Paragraph p = new Paragraph(NumCont+"/"+ NumContTotales).SetBold().SetFontSize(14);   // Negrita se establece en negrita
            canvas.Add(p);


            float[] cellWidth = { 20f, 20f };
            Table tablaEvent = new Table(UnitValue.CreatePercentArray(3)).UseAllAvailableWidth();

          
            Cell cell5 = new Cell().Add(new Paragraph("_________________________"));
            tablaEvent.AddCell(cell5.SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER));
            Cell cell9 = new Cell().Add(new Paragraph(""));
            tablaEvent.AddCell(cell9.SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER));
            Cell cell6 = new Cell().Add(new Paragraph("_________________________"));
            tablaEvent.AddCell(cell6.SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER));
            Cell cell10 = new Cell().Add(new Paragraph("Produced by: " + referencia));
            tablaEvent.AddCell(cell10.SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER));
            Cell cell7 = new Cell().Add(new Paragraph(""));
            tablaEvent.AddCell(cell7.SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER));
            Cell cell2 = new Cell().Add(new Paragraph("Authorized by: " + nombreAutorizado));
            tablaEvent.AddCell(cell2.SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER));
            Cell cell3 = new Cell().Add(new Paragraph(" " /*+ referencia*/));
            tablaEvent.AddCell(cell3.SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER));
            Cell cell8 = new Cell().Add(new Paragraph(""));
            tablaEvent.AddCell(cell8.SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER));
            Cell cell4 = new Cell().Add(new Paragraph(" " /*+ nombreAutorizado*/));
            tablaEvent.AddCell(cell4.SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER));
            pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new TableFooterEventHandler(tablaEvent));

            //float topMargin = 20 + tablaEvent.GetTableHeight();
            document.SetMargins(80, 36, 130, 36);

            //document.Add(new AreaBreak(new PageSize()));

            if (Fondo == "SI")
            {
                var path = System.IO.Path.Combine(Server.MapPath("~/Images/HojaMembretada.jpg"));
                iText.Layout.Element.Image BackPack = new iText.Layout.Element.Image(ImageDataFactory.Create(path))/*.SetOpacity(0.1f)*/;
                pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new BackgroundPageEvent(BackPack));
            }
           // Paragraph header = new Paragraph("PACKING LIST")
           //.SetTextAlignment(TextAlignment.CENTER)
           //.SetFontSize(16).SetFontColor(ColorConstants.BLACK).SetFont(bold);
            Paragraph header = new Paragraph("PACKING LIST")
         .SetTextAlignment(TextAlignment.CENTER)
         .SetFontSize(16).SetBold();
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            List list = new List().SetSymbolIndent(12)
            .SetFont(font);

            Paragraph Espacio = new Paragraph(" ").SetTextAlignment(TextAlignment.CENTER)
              .SetFontSize(12).SetFontColor(ColorConstants.BLACK);

            document.Add(header);
            //document.Add(ls);
            //document.Add(Espacio);

            float[] columnWidth = { 85f, 185f, 104f, 150f };
            Table tabla = new Table(columnWidth);

            tabla.AddCell(new Cell(1,1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("CUSTOMER:").SetFontSize(8)).SetBold().SetHeight(12f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell(1,3).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + nombreCliente).SetFontSize(8)).SetHeight(12f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("DATE:").SetFontSize(8)).SetBold().SetHeight(12f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + fecha).SetFontSize(8)).SetHeight(12f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("INVOICE #:").SetFontSize(8)).SetBold().SetHeight(12f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + factura).SetFontSize(8)).SetHeight(12f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("CONTAINER #:").SetFontSize(8)).SetBold().SetHeight(12f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + contenedor).SetFontSize(8)).SetHeight(12f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("P.O:").SetFontSize(8)).SetBold().SetHeight(12f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + pedido).SetFontSize(8)).SetHeight(12f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("BOOKING #:").SetFontSize(8)).SetBold().SetHeight(12f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + reserva).SetFontSize(8)).SetHeight(12f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("VESSEL:").SetFontSize(8)).SetBold().SetHeight(12f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + embarcacion).SetFontSize(8)).SetHeight(12f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("INTERCHANGE EIR:").SetFontSize(8)).SetBold().SetHeight(12f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + intercambioEir).SetFontSize(8)).SetHeight(12f).SetBorder(Border.NO_BORDER));
            //tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("REFERENCE:").SetFontSize(8)).SetBold().SetHeight(16f).SetBorder(Border.NO_BORDER));
            //tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + referencia).SetFontSize(8)).SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("PRODUCT:").SetFontSize(8)).SetBold().SetHeight(12f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + productos).SetFontSize(8)).SetHeight(12f).SetBorder(Border.NO_BORDER));

            float[] columnWidth2 = { 10f, 75f, 60f, 60f, 60f, 7f, 10f, 10f, 10f, 40f, 40f, 40f };

            Table tabla2 = new Table(columnWidth2).UseAllAvailableWidth();

            Cell cell = new Cell(2, 1)
           .SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER)
           .Add(new Paragraph("Pallet").SetFontSize(8)).SetBold().SetHeight(16f).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetBorder(new SolidBorder(lineColor, 1));
            tabla2.AddCell(cell);
            cell = new Cell(2, 1)
            .SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER)
           .Add(new Paragraph("Customer Reference").SetFixedLeading(10).SetFontSize(8)).SetBold().SetHeight(20f).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetBorder(new SolidBorder(lineColor, 1));
            tabla2.AddCell(cell);
            cell = new Cell(2, 1)
           .SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER)
          .Add(new Paragraph("Dacar Part Number").SetFixedLeading(10).SetFontSize(8)).SetBold().SetHeight(20f).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetBorder(new SolidBorder(lineColor, 1));
            tabla2.AddCell(cell);
            cell = new Cell(2, 2)
           .SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER)
          .Add(new Paragraph("Description").SetFontSize(8)).SetBold().SetHeight(16f).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetBorder(new SolidBorder(lineColor, 1));
            tabla2.AddCell(cell);
            cell = new Cell(2, 1)
           .SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER)
          .Add(new Paragraph("Qty").SetFontSize(8)).SetBold().SetHeight(16f).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetBorder(new SolidBorder(lineColor, 1));
            tabla2.AddCell(cell);
            cell = new Cell(1, 3)
           .SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER)
          .Add(new Paragraph("Dimensions(m)").SetFontSize(8)).SetBold().SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetHeight(16f).SetBorder(new SolidBorder(lineColor, 1));
            tabla2.AddCell(cell);
            cell = new Cell(2, 1)
          .SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER)
          .Add(new Paragraph().Add("Volume\n(m³)").SetFontSize(8)).SetBold().SetHeight(16f).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetBorder(new SolidBorder(lineColor, 1));
            tabla2.AddCell(cell.SetHeight(23f));

            cell = new Cell(1, 2)
          .SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER)
         .Add(new Paragraph("Weight(kg)").SetFontSize(8)).SetBold().SetHeight(16f).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetBorder(new SolidBorder(lineColor, 1));
            tabla2.AddCell(cell);
            tabla2.AddCell(new Cell().Add(new Paragraph("L.").SetFontSize(8)).SetBold().SetHeight(16f).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().Add(new Paragraph("W.").SetFontSize(8)).SetBold().SetHeight(16f).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().Add(new Paragraph("H.").SetFontSize(8)).SetBold().SetHeight(16f).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().Add(new Paragraph("Gross").SetFontSize(8)).SetBold().SetHeight(16f).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().Add(new Paragraph("Net").SetFontSize(8)).SetBold().SetHeight(16f).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));

            float[] columnWidth3 = { 10f, 75f, 60f, 60f, 60f, 7f, 10f, 10f, 10f, 40f, 40f };
            Table tabla3 = new Table(columnWidth3);

            foreach (var x in pallet) {
                var palletDetalle = daoPackilist.ConsultarPalletPalletDetalleItemsCompleto(PackingId, x.PalletPacking1);
                totalVolumen = totalVolumen + x.VolumenPallet.Value;
                totalPBruto = totalPBruto + x.PesoBruto.Value;
                totalPNeto = totalPNeto + x.PesoNeto.Value;

                if (palletDetalle.Count == 1)
                {
                    foreach (var y in palletDetalle) {
                        nombreForaneo = daoPackilist.ConsultarNombreForaneo(y.ItemCode);
                        datosTecnicos= daoOrdenesVentas.ConsultarDatosTecnicos(y.ItemCode);
                        polaridad = daoOrdenesVentas.ConsultarPolaridad(y.ItemCode);                    
                        generico = daoOrdenesVentas.ConsultarGenerico(y.ItemCode);
  
                        tabla2.AddCell(new Cell().Add(new Paragraph("" + x.PalletNumber).SetFontSize(7)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                        tabla2.AddCell(new Cell().Add(new Paragraph(""+ nombreForaneo).SetFixedLeading(8).SetFontSize(7)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                        tabla2.AddCell(new Cell().Add(new Paragraph("" + generico).SetFixedLeading(8).SetFontSize(7)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                        tabla2.AddCell(new Cell(1, 2).Add(new Paragraph("" + datosTecnicos+" - "+polaridad).SetFixedLeading(10).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                        tabla2.AddCell(new Cell().Add(new Paragraph("" + y.CantidadItem).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                        tabla2.AddCell(new Cell().Add(new Paragraph("" + x.LargoPallet).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                        tabla2.AddCell(new Cell().Add(new Paragraph("" + x.AnchoPallet).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                        tabla2.AddCell(new Cell().Add(new Paragraph("" + x.AltoPallet).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                        tabla2.AddCell(new Cell().Add(new Paragraph("" + x.VolumenPallet).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                        tabla2.AddCell(new Cell().Add(new Paragraph("" + String.Format("{0:n}", x.PesoBruto)).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                        tabla2.AddCell(new Cell().Add(new Paragraph("" + String.Format("{0:n}", x.PesoNeto)).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                        totalUnidades = totalUnidades + y.CantidadItem.Value;
                    }
                }
                else
                {
                    int i = 1;
                    foreach (var y in palletDetalle)
                    {
                        nombreForaneo = daoPackilist.ConsultarNombreForaneo(y.ItemCode);
                        datosTecnicos = daoOrdenesVentas.ConsultarDatosTecnicos(y.ItemCode);
                        polaridad = daoOrdenesVentas.ConsultarPolaridad(y.ItemCode);
                        generico = daoOrdenesVentas.ConsultarGenerico(y.ItemCode);
                        int contador = palletDetalle.Count;

                        if (i == 1)
                        {
                            tabla2.AddCell(new Cell(contador, 1).Add(new Paragraph("" + x.PalletNumber).SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));
                            tabla2.AddCell(new Cell(1, 1).Add(new Paragraph("" + nombreForaneo).SetFontSize(7)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));
                            tabla2.AddCell(new Cell(1, 1).Add(new Paragraph("" + generico).SetFontSize(7)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));
                            tabla2.AddCell(new Cell(1, 2).Add(new Paragraph("" + datosTecnicos + " - " + polaridad).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));
                            tabla2.AddCell(new Cell(1, 1).Add(new Paragraph("" + y.CantidadItem).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));
                            tabla2.AddCell(new Cell(contador, 1).Add(new Paragraph("" + x.LargoPallet).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));
                            tabla2.AddCell(new Cell(contador, 1).Add(new Paragraph("" + x.AnchoPallet).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));
                            tabla2.AddCell(new Cell(contador, 1).Add(new Paragraph("" + x.AltoPallet).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));
                            tabla2.AddCell(new Cell(contador, 1).Add(new Paragraph("" + x.VolumenPallet).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));
                            tabla2.AddCell(new Cell(contador, 1).Add(new Paragraph("" + String.Format("{0:n}", x.PesoBruto)).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));
                            tabla2.AddCell(new Cell(contador, 1).Add(new Paragraph("" + String.Format("{0:n}", x.PesoNeto)).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));
                        }
                        if (i > 1) {
                            tabla2.AddCell(new Cell(1, 1).Add(new Paragraph("" + nombreForaneo).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));
                            tabla2.AddCell(new Cell(1, 1).Add(new Paragraph("" + generico).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));
                            tabla2.AddCell(new Cell(1, 2).Add(new Paragraph("" + datosTecnicos + " - " + polaridad).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));
                            tabla2.AddCell(new Cell(1, 1).Add(new Paragraph("" + y.CantidadItem).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));
                        }
                        i = i + 1;
                        totalUnidades = totalUnidades + y.CantidadItem.Value;
                    }
                }
            }
            tabla2.AddCell(new Cell(1, 5).Add(new Paragraph("Total").SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell(1, 1).Add(new Paragraph("" + String.Format("{0:n0}", totalUnidades)).SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell(1, 3).Add(new Paragraph("").SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell(1, 1).Add(new Paragraph("" + totalVolumen).SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell(1, 1).Add(new Paragraph("" + String.Format("{0:n}", totalPBruto)).SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell(1, 1).Add(new Paragraph("" + String.Format("{0:n}", totalPNeto)).SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));
            
            document.Add(Espacio);
            document.Add(tabla).SetBorder(Border.NO_BORDER);
            document.Add(Espacio);
            document.Add(tabla2);

            document.Add(tabla3);
            document.Add(Espacio);
            document.Add(Espacio);

            document.Close();

            byte[] bytesStreams = stream.ToArray();
            stream = new MemoryStream();
            stream.Write(bytesStreams, 0, bytesStreams.Length);
            stream.Position = 0;
            return new FileStreamResult(stream, "application/pdf");
        }
        public class BackgroundPageEvent2 : IEventHandler
        {
            iText.Layout.Element.Image imgBack;
            public BackgroundPageEvent2(iText.Layout.Element.Image imgBackPage)
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
                new Canvas(pdfCanvas, page.GetPageSize()).Add(imgBack.ScaleAbsolute(pageSize.GetWidth(), pageSize.GetHeight())).Close();

                pdfCanvas.RestoreState();
                pdfCanvas.Release();
            }
        }
        private class TableFooterEventHandler : IEventHandler
        {
            private Table table;

            public TableFooterEventHandler(Table table)
            {
                this.table = table;
            }

            public void HandleEvent(Event currentEvent)
            {
                PdfDocumentEvent docEvent = (PdfDocumentEvent)currentEvent;
                PdfDocument pdfDoc = docEvent.GetDocument();
                PdfPage page = docEvent.GetPage();
                PdfCanvas canvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);

                new Canvas(canvas, new iText.Kernel.Geom.Rectangle(35, 38, page.GetPageSize().GetRight() - 55, 81))
                    .Add(table)
                    .Close();
            }
        }

        public ActionResult ComextListadoPackingList()
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

        public JsonResult DetallesGuiasPackingList(int PackingId)
        {
            try
            {
                daoPackilist = new DaoPackingList();

                var Result = daoPackilist.ConsultarGuiaPackingList(PackingId);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public bool GuardarDetallesGuiaPackingList(int PackingId, decimal pesoBruto, decimal pesoTara, string razonSocial, string ruc, string direccion, string placa, string selloA, string selloB, string selloC, string selloD,
            string elaboradoPor, string autorizadoPor, string puntoPartida)
        {
            try
            {
                daoPackilist = new DaoPackingList();

                var Result = daoPackilist.IngresarDetalleGuiaPackingList(PackingId, pesoBruto, pesoTara, razonSocial, ruc, direccion, placa, selloA, selloB, selloC, selloD,
            elaboradoPor, autorizadoPor, puntoPartida);
                return Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public bool ActualizarDetallesGuiaPalletsPackingList(int PackingId, decimal pesoBruto, decimal pesoTara, string razonSocial, string ruc, string direccion, string placa, string selloA, string selloB, string selloC, string selloD,
            string elaboradoPor, string autorizadoPor, string puntoPartida)
        {
            try
            {
                daoPackilist = new DaoPackingList();

                var Result = daoPackilist.ActualizarDetalleGuiaPackingList(PackingId, pesoBruto, pesoTara, razonSocial, ruc, direccion, placa, selloA, selloB, selloC, selloD,
                elaboradoPor, autorizadoPor, puntoPartida);
                return Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult BuscarDatosFactPacking(string numeroOrden)
        {
            try
            {
                daoPackilist = new DaoPackingList();

                var Result = daoPackilist.BusquedaFacturaReserva(numeroOrden);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult BuscarDatosDetalleFactPacking(int docEntry)
        {
            try
            {
                daoPackilist = new DaoPackingList();
                var Result = daoPackilist.BusquedaFacturaDetalleReserva(docEntry);

                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public FileStreamResult pdf()
        {
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf, PageSize.A4, true);

            document.Add(new Paragraph("Hello World"));
            document.Add(new Paragraph(DateTime.Now.ToString()));
            document.Close();

            byte[] bytesStreams = stream.ToArray();
            stream = new MemoryStream();
            stream.Write(bytesStreams, 0, bytesStreams.Length);
            stream.Position = 0;

            Response.AppendHeader("content-disposition", "inline; filename=file.pdf");
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult ImprimirFact(string numeroFactura, string numeroOrden, DateTime fecha, string cliente, string enviar, string telefono, string vendedor, string destino, int contenedor, string metodo
            , int valorEntry, int packingId, string formaPago)
        {
            daoPackilist = new DaoPackingList();
            var ResultDetalle = daoPackilist.BusquedaFacturaDetalleReserva(valorEntry);
            string contenedorDetalle = daoPackilist.ConsultarDetalleGeneralPackingListContenedor(packingId);
            DateTime fechaDoc = Convert.ToDateTime(fecha, CultureInfo.InvariantCulture);
            string fechaDocumento = fechaDoc.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
        
            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
            PdfFont No_Bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            iText.Kernel.Colors.Color fondoCelda = new DeviceRgb(199, 224, 255);
            iText.Kernel.Colors.Color lineColor = new DeviceRgb(39, 110, 198);

            MemoryStream stream = new MemoryStream();
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf, PageSize.A4, true);
            LineSeparator ls = new LineSeparator(new SolidLine());

            float[] cellWidth = { 20f, 20f };
            Table tablaEvent = new Table(UnitValue.CreatePercentArray(3)).UseAllAvailableWidth();

            Cell cell5 = new Cell().Add(new Paragraph("_________________________"));
            tablaEvent.AddCell(cell5.SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER));
            Cell cell9 = new Cell().Add(new Paragraph(""));
            tablaEvent.AddCell(cell9.SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER));
            Cell cell6 = new Cell().Add(new Paragraph("_________________________"));
            tablaEvent.AddCell(cell6.SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER));
            Cell cell10 = new Cell().Add(new Paragraph("SALES DEPARTAMENT").SetFontSize(10));
            tablaEvent.AddCell(cell10.SetTextAlignment(TextAlignment.CENTER).SetBorder(Border.NO_BORDER));
            Cell cell7 = new Cell().Add(new Paragraph(""));
            tablaEvent.AddCell(cell7.SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER));
            Cell cell2 = new Cell().Add(new Paragraph("CUSTOMER APPROVAL").SetFontSize(10));
            tablaEvent.AddCell(cell2.SetTextAlignment(TextAlignment.CENTER).SetBorder(Border.NO_BORDER));
            Cell cell3 = new Cell().Add(new Paragraph(" " /*+ referencia*/));
            tablaEvent.AddCell(cell3.SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER));
            Cell cell8 = new Cell().Add(new Paragraph(""));
            tablaEvent.AddCell(cell8.SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER));
            Cell cell4 = new Cell().Add(new Paragraph(" " /*+ nombreAutorizado*/));
            tablaEvent.AddCell(cell4.SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER));
            pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new TableFooterEventHandler(tablaEvent));

            document.SetMargins(80, 36, 130, 36);          
                var path = System.IO.Path.Combine(Server.MapPath("~/Images/HojaMembretada.jpg"));
                iText.Layout.Element.Image BackPack = new iText.Layout.Element.Image(ImageDataFactory.Create(path))/*.SetOpacity(0.1f)*/;
                pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new BackgroundPageEvent(BackPack));
                     
            Paragraph header = new Paragraph("INVOICE")     
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(16).SetBold();
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            List list = new List().SetSymbolIndent(12)
            .SetFont(font);

            Paragraph Espacio = new Paragraph(" ").SetTextAlignment(TextAlignment.CENTER)
              .SetFontSize(12).SetFontColor(ColorConstants.BLACK);
            float[] columnWidth = { 50f, 300f, 40f, 80f };

            Table tablaEncabezado = new Table(columnWidth);

            tablaEncabezado.AddCell(new Cell(1,1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("INVOICE:").SetFontSize(10)).SetBold().SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1,1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + numeroFactura).SetFontSize(10)).SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1,1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("DATE:").SetFontSize(10)).SetBold().SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1,1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + fechaDocumento).SetFontSize(10)).SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1,1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("TO:").SetFontSize(10)).SetBold().SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1,1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + cliente).SetFontSize(10)).SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("PHONE:").SetFontSize(10)).SetBold().SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + telefono).SetFontSize(10)).SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1,1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("SHIP TO:").SetFontSize(10)).SetBold().SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1,3).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + enviar).SetFontSize(10)).SetHeight(14f).SetBorder(Border.NO_BORDER));

            float[] columnWidth2 = { 100f, 100f, 100f, 100f, 100f ,100f};

            Table tablaEncabezadoDetalle = new Table(columnWidth2).UseAllAvailableWidth();
            tablaEncabezadoDetalle.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("SALES PERSON").SetFontSize(8)).SetBold().SetHeight(12f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(fondoCelda).SetBorder(new SolidBorder(lineColor, 1)));
            tablaEncabezadoDetalle.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("ORIGIN").SetFontSize(8)).SetBold().SetHeight(12f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(fondoCelda).SetBorder(new SolidBorder(lineColor, 1)));
            tablaEncabezadoDetalle.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("DESTINATION").SetFontSize(8)).SetBold().SetHeight(12f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(fondoCelda).SetBorder(new SolidBorder(lineColor, 1)));
            tablaEncabezadoDetalle.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("SHIPPING METHOD").SetFontSize(8)).SetBold().SetHeight(12f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(fondoCelda).SetBorder(new SolidBorder(lineColor, 1)));
            tablaEncabezadoDetalle.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("# CONTAINER").SetFontSize(8)).SetBold().SetHeight(12f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(fondoCelda).SetBorder(new SolidBorder(lineColor, 1)));
            tablaEncabezadoDetalle.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("PURCHASE ORDER").SetFontSize(8)).SetBold().SetHeight(12f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(fondoCelda).SetBorder(new SolidBorder(lineColor, 1)));
            tablaEncabezadoDetalle.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + vendedor).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
            tablaEncabezadoDetalle.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("ECUADOR").SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
            tablaEncabezadoDetalle.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + destino).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
            tablaEncabezadoDetalle.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + metodo).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
            tablaEncabezadoDetalle.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + contenedorDetalle).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
            tablaEncabezadoDetalle.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + numeroOrden).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));

            float[] columnWidth3 = { 30f, 80f, 80f, 80f, 80f };
            Table tablaDetalleFact = new Table(columnWidth3);

            tablaDetalleFact.SetHorizontalAlignment(HorizontalAlignment.CENTER);
            tablaDetalleFact.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("ITEM").SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(fondoCelda).SetBorder(new SolidBorder(lineColor, 1)));
            tablaDetalleFact.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("CUSTOMER PART NUMBER").SetFontSize(8).SetFixedLeading(10)).SetBold().SetHeight(20f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(fondoCelda).SetBorder(new SolidBorder(lineColor, 1)));
            tablaDetalleFact.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("QUANTITY").SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(fondoCelda).SetBorder(new SolidBorder(lineColor, 1)));
            tablaDetalleFact.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("PRICE").SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(fondoCelda).SetBorder(new SolidBorder(lineColor, 1)));
            tablaDetalleFact.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("AMOUNT").SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(fondoCelda).SetBorder(new SolidBorder(lineColor, 1)));

            int contadorItem=0;
            decimal contadorTotal = 0;


            foreach (var x in ResultDetalle)
            {
                tablaDetalleFact.AddCell(new Cell().Add(new Paragraph("" +x.numeroItem).SetFontSize(8)).SetHeight(13f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                tablaDetalleFact.AddCell(new Cell().Add(new Paragraph("" + x.Description).SetFontSize(8)).SetHeight(13f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                tablaDetalleFact.AddCell(new Cell().Add(new Paragraph("" + x.Quantity).SetFontSize(8)).SetHeight(13f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                tablaDetalleFact.AddCell(new Cell().Add(new Paragraph("$" + String.Format("{0:n}", x.Price)).SetFontSize(8)).SetHeight(13f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                tablaDetalleFact.AddCell(new Cell().Add(new Paragraph("$" + String.Format("{0:n}", x.TotalPrice)).SetFontSize(8)).SetHeight(13f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                contadorItem = contadorItem + x.Quantity;
                contadorTotal = contadorTotal + x.TotalPrice;
            }
            tablaDetalleFact.AddCell(new Cell(1, 2).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Total").SetFontSize(8)).SetBold().SetHeight(12f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(fondoCelda).SetBorder(new SolidBorder(lineColor, 1)));
            tablaDetalleFact.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(""+contadorItem+" unids.").SetFontSize(8)).SetBold().SetHeight(12f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
            tablaDetalleFact.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Subtotal").SetFontSize(8)).SetBold().SetHeight(12f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(fondoCelda).SetBorder(new SolidBorder(lineColor, 1)));
            tablaDetalleFact.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("$" + String.Format("{0:n}", contadorTotal)).SetFontSize(8)).SetBold().SetHeight(12f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
           
            tablaDetalleFact.AddCell(new Cell(2, 3).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("PAYMENT TERMS: "+ formaPago + " Currency in US Dollars").SetFontSize(8)).SetHeight(12f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
            tablaDetalleFact.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(""+ metodo+" Cost").SetFontSize(8)).SetBold().SetHeight(12f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(fondoCelda).SetBorder(new SolidBorder(lineColor, 1)));
            tablaDetalleFact.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("$" + String.Format("{0:n}", contadorTotal)).SetFontSize(8)).SetBold().SetHeight(12f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
            //tablaDetalleFact.AddCell(new Cell(1, 3).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("").SetFontSize(8)).SetBold().SetHeight(12f).SetTextAlignment(TextAlignment.CENTER).SetBorder(Border.NO_BORDER));
            tablaDetalleFact.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Total "+metodo).SetFontSize(8)).SetBold().SetHeight(12f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(fondoCelda).SetBorder(new SolidBorder(lineColor, 1)));
            tablaDetalleFact.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("$" + String.Format("{0:n}", contadorTotal)).SetFontSize(8)).SetBold().SetHeight(12f).SetTextAlignment(TextAlignment.CENTER));

            document.Add(header);
            document.Add(Espacio);
            document.Add(Espacio);
            document.Add(Espacio);
            document.Add(tablaEncabezado);
            document.Add(Espacio);
            document.Add(Espacio);
            document.Add(tablaEncabezadoDetalle);
            document.Add(Espacio);
            document.Add(Espacio);
            document.Add(tablaDetalleFact);

            document.Close();

            byte[] bytesStreams = stream.ToArray();
            stream = new MemoryStream();
            stream.Write(bytesStreams, 0, bytesStreams.Length);
            stream.Position = 0;
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult ImprimirGuia(string Cliente, int packingId, string reserva, string pedido, string contenedorDetalle)
        {
            daoPackilist = new DaoPackingList();
            daoOrdenesVentas = new DaoOrdenesVentas();
            var detGuia = daoPackilist.ConsultarGuiaPackingList(packingId);
            //string contenedorDetalle = daoPackilist.ConsultarDetalleGeneralPackingListContenedor(packingId);
            string nombreForaneo = "";
            string datosTecnicos = "";
            string polaridad = "";
            string generico = "";
            string fechaEmision="";
            string puntoPartida="";
            decimal pesoBruto= 0;
            decimal pesotara = 0;
            string razonSocial = "";
            string ruc = "";
            string direccion = "";
            string placa = "";
            string selloA = "";
            string selloB = "";
            string selloC = "";
            string selloD = "";
            string elaboradoPor = "";
            string autorizadoPor = "";
            int totalUnidades = 0;

            foreach (var y in detGuia) {
                DateTime fecAct = Convert.ToDateTime(y.FechaActualizacion, CultureInfo.InvariantCulture);
                fechaEmision = fecAct.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
                puntoPartida = y.PuntoPartida;
                pesoBruto = y.PesoBruto.Value;
                pesotara = y.PesoTara.Value;
                razonSocial = y.RazonSocial;
                ruc = y.Ruc;
                direccion = y.Direccion;
                placa = y.Placa;
                selloA = y.SelloA;
                selloB = y.SelloB;
                selloC = y.SelloC;
                selloD = y.SelloD;
                elaboradoPor = y.ElaboradoPor;
                autorizadoPor = y.AutorizadoPor;
            }

            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
            PdfFont No_Bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            iText.Kernel.Colors.Color fondoCelda = new DeviceRgb(199, 224, 255);
            iText.Kernel.Colors.Color lineColor = new DeviceRgb(39, 110, 198);

            MemoryStream stream = new MemoryStream();
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf, PageSize.A4, true);
            LineSeparator ls = new LineSeparator(new SolidLine());

            float[] cellWidth = { 20f, 20f  };
            Table tablaEvent = new Table(UnitValue.CreatePercentArray(3)).UseAllAvailableWidth();

            Cell cell5 = new Cell().Add(new Paragraph("_________________________"));
            tablaEvent.AddCell(cell5.SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER));
            Cell cell9 = new Cell().Add(new Paragraph("_________________________"));
            tablaEvent.AddCell(cell9.SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER));
            Cell cell6 = new Cell().Add(new Paragraph("_________________________"));
            tablaEvent.AddCell(cell6.SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER));
            Cell cell10 = new Cell().Add(new Paragraph("Autorizado por").SetFontSize(10));
            tablaEvent.AddCell(cell10.SetTextAlignment(TextAlignment.CENTER).SetBorder(Border.NO_BORDER));
            Cell cell7 = new Cell().Add(new Paragraph("Entregado por").SetFontSize(10));
            tablaEvent.AddCell(cell7.SetTextAlignment(TextAlignment.CENTER).SetBorder(Border.NO_BORDER));
            Cell cell2 = new Cell().Add(new Paragraph("Recibí conforme").SetFontSize(10));
            tablaEvent.AddCell(cell2.SetTextAlignment(TextAlignment.CENTER).SetBorder(Border.NO_BORDER));
            Cell cell3 = new Cell().Add(new Paragraph(" " /*+ referencia*/));
            tablaEvent.AddCell(cell3.SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER));
            Cell cell8 = new Cell().Add(new Paragraph(""));
            tablaEvent.AddCell(cell8.SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER));
            Cell cell4 = new Cell().Add(new Paragraph(" " /*+ nombreAutorizado*/));
            tablaEvent.AddCell(cell4.SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER));
            pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new TableFooterEventHandler(tablaEvent));

            document.SetMargins(80, 36, 130, 36);
            var path = System.IO.Path.Combine(Server.MapPath("~/Images/HojaMembretada.jpg"));
            iText.Layout.Element.Image BackPack = new iText.Layout.Element.Image(ImageDataFactory.Create(path))/*.SetOpacity(0.1f)*/;
            pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new BackgroundPageEvent(BackPack));

            Paragraph header = new Paragraph("GUIA DE REMISION")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(16).SetBold();
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            List list = new List().SetSymbolIndent(12)
            .SetFont(font);

            Paragraph text1 = new Paragraph("Datos del transportista")
              .SetTextAlignment(TextAlignment.LEFT)
              .SetFontSize(12).SetBold();

            Paragraph text2 = new Paragraph("Sellos")
            .SetTextAlignment(TextAlignment.LEFT)
            .SetFontSize(12).SetBold();

            Paragraph Espacio = new Paragraph(" ").SetTextAlignment(TextAlignment.CENTER)
              .SetFontSize(12).SetFontColor(ColorConstants.BLACK);
            float[] columnWidth = { 80f, 270f, 40f, 80f };

            Table tablaEncabezado = new Table(columnWidth);

            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Cliente:").SetFontSize(10)).SetBold().SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + Cliente).SetFontSize(10)).SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Fecha Emision:").SetFontSize(10)).SetBold().SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + fechaEmision).SetFontSize(10)).SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Punto Partida:").SetFontSize(10)).SetBold().SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + puntoPartida).SetFontSize(10)).SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Reserva:").SetFontSize(10)).SetBold().SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + reserva).SetFontSize(10)).SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Pedido").SetFontSize(10)).SetBold().SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + pedido).SetFontSize(10)).SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Contenedor").SetFontSize(10)).SetBold().SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + contenedorDetalle).SetFontSize(10)).SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Peso Bruto").SetFontSize(10)).SetBold().SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + pesoBruto).SetFontSize(10)).SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Peso Tara").SetFontSize(10)).SetBold().SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + pesotara).SetFontSize(10)).SetHeight(14f).SetBorder(Border.NO_BORDER));

            float[] columnWidth2 = { 80f, 270f, 40f, 80f };

            Table tablaEncabezadoDetalle = new Table(columnWidth2);
            tablaEncabezadoDetalle.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Razon Social").SetBold().SetFontSize(10)).SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezadoDetalle.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + razonSocial).SetFontSize(10)).SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezadoDetalle.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Ruc").SetBold().SetFontSize(10)).SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezadoDetalle.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + ruc).SetFontSize(10)).SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezadoDetalle.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Direccion").SetBold().SetFontSize(10)).SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezadoDetalle.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + direccion).SetFontSize(10)).SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezadoDetalle.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Placa").SetBold().SetFontSize(10)).SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezadoDetalle.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + placa).SetFontSize(10)).SetHeight(14f).SetBorder(Border.NO_BORDER));


            float[] columnWidth4 = { 80f, 270f, 40f, 80f };

            Table tablaEncabezadoDetalleSellos = new Table(columnWidth4);
            tablaEncabezadoDetalleSellos.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Sello A").SetBold().SetFontSize(10)).SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezadoDetalleSellos.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + selloA).SetFontSize(10)).SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezadoDetalleSellos.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Sello B").SetBold().SetFontSize(10)).SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezadoDetalleSellos.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + selloB).SetFontSize(10)).SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezadoDetalleSellos.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Sello C").SetBold().SetFontSize(10)).SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezadoDetalleSellos.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + selloC).SetFontSize(10)).SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezadoDetalleSellos.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Sello D").SetBold().SetFontSize(10)).SetHeight(14f).SetBorder(Border.NO_BORDER));
            tablaEncabezadoDetalleSellos.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + selloD).SetFontSize(10)).SetHeight(14f).SetBorder(Border.NO_BORDER));


            var pallet = daoPackilist.ConsultarPalletPackingCompleto(packingId);

            float[] columnWidthItems = { 10f, 100f, 100f, 100f};

            Table tablaDescripcionItems = new Table(columnWidthItems).SetHorizontalAlignment(HorizontalAlignment.CENTER);

            Cell cell = new Cell(1, 1)
           .SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER)
           .Add(new Paragraph("Item").SetFontSize(8)).SetBold().SetHeight(16f).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetBorder(new SolidBorder(lineColor, 1));
            tablaDescripcionItems.AddCell(cell);
            cell = new Cell(1, 1)
            .SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER)
           .Add(new Paragraph("Referencia del cliente").SetFontSize(8)).SetBold().SetHeight(20f).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetBorder(new SolidBorder(lineColor, 1));
            tablaDescripcionItems.AddCell(cell);
            cell = new Cell(1, 1)
           .SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER)
          .Add(new Paragraph("Dacar Part Number").SetFontSize(8)).SetBold().SetHeight(20f).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetBorder(new SolidBorder(lineColor, 1));
            tablaDescripcionItems.AddCell(cell);
          //  cell = new Cell(1, 1)
          // .SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER)
          //.Add(new Paragraph("Descripcion").SetFontSize(8)).SetBold().SetHeight(16f).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetBorder(new SolidBorder(lineColor, 1));
          //  tablaDescripcionItems.AddCell(cell);
            cell = new Cell(1, 1)
           .SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER)
          .Add(new Paragraph("Cantidad").SetFontSize(8)).SetBold().SetHeight(16f).SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetBorder(new SolidBorder(lineColor, 1));
            tablaDescripcionItems.AddCell(cell);

            int j = 1;

            foreach (var z in pallet) {
                var palletDetalle = daoPackilist.ConsultarPalletPalletDetalleItemsCompleto(packingId, z.PalletPacking1);
                
                if (palletDetalle.Count == 1)
                {
                    foreach (var y in palletDetalle)
                    {
                        nombreForaneo = daoPackilist.ConsultarNombreForaneo(y.ItemCode);
                        datosTecnicos = daoOrdenesVentas.ConsultarDatosTecnicos(y.ItemCode);
                        polaridad = daoOrdenesVentas.ConsultarPolaridad(y.ItemCode);
                        generico = daoOrdenesVentas.ConsultarGenerico(y.ItemCode);
                        tablaDescripcionItems.AddCell(new Cell().Add(new Paragraph("" + j).SetFontSize(8)).SetBold().SetHeight(13f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                        tablaDescripcionItems.AddCell(new Cell().Add(new Paragraph("" + nombreForaneo).SetFontSize(8)).SetHeight(13f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                        tablaDescripcionItems.AddCell(new Cell().Add(new Paragraph("" + generico).SetFontSize(8)).SetHeight(13f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                        //tablaDescripcionItems.AddCell(new Cell(1, 1).Add(new Paragraph("" + datosTecnicos + " - " + polaridad).SetFontSize(8)).SetHeight(13f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                        tablaDescripcionItems.AddCell(new Cell().Add(new Paragraph("" + y.CantidadItem).SetFontSize(8)).SetHeight(13f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                        totalUnidades = totalUnidades + y.CantidadItem.Value;
                        j = j + 1;
                    }
                }
                else {
                    int i = 1;
                    foreach (var y in palletDetalle)
                    {
                        nombreForaneo = daoPackilist.ConsultarNombreForaneo(y.ItemCode);
                        datosTecnicos = daoOrdenesVentas.ConsultarDatosTecnicos(y.ItemCode);
                        polaridad = daoOrdenesVentas.ConsultarPolaridad(y.ItemCode);
                        generico = daoOrdenesVentas.ConsultarGenerico(y.ItemCode);
                        int contador = palletDetalle.Count;
                        if (i == 1)
                        {
                            tablaDescripcionItems.AddCell(new Cell().Add(new Paragraph("" + j).SetFontSize(8)).SetBold().SetHeight(13f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                            //tablaDescripcionItems.AddCell(new Cell(contador, 1).Add(new Paragraph("" + z.PalletNumber).SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));
                            tablaDescripcionItems.AddCell(new Cell(1, 1).Add(new Paragraph("" + nombreForaneo).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));
                            tablaDescripcionItems.AddCell(new Cell(1, 1).Add(new Paragraph("" + generico).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));
                            //tablaDescripcionItems.AddCell(new Cell(1, 1).Add(new Paragraph("" + datosTecnicos + " - " + polaridad).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));
                            tablaDescripcionItems.AddCell(new Cell(1, 1).Add(new Paragraph("" + y.CantidadItem).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));
                            j = j + 1;
                        }
                        if (i > 1)
                        {
                            tablaDescripcionItems.AddCell(new Cell().Add(new Paragraph("" + j).SetFontSize(8)).SetBold().SetHeight(13f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                            tablaDescripcionItems.AddCell(new Cell(1, 1).Add(new Paragraph("" + nombreForaneo).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));
                            tablaDescripcionItems.AddCell(new Cell(1, 1).Add(new Paragraph("" + generico).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));
                            //tablaDescripcionItems.AddCell(new Cell(1, 1).Add(new Paragraph("" + datosTecnicos + " - " + polaridad).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));
                            tablaDescripcionItems.AddCell(new Cell(1, 1).Add(new Paragraph("" + y.CantidadItem).SetFontSize(8)).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));
                            j = j + 1;
                        }
                        i = i + 1;
                        totalUnidades = totalUnidades + y.CantidadItem.Value;
                    }
                }               
            }
            tablaDescripcionItems.AddCell(new Cell(1, 3).Add(new Paragraph("Total").SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));
            tablaDescripcionItems.AddCell(new Cell(1, 1).Add(new Paragraph(""+ totalUnidades).SetFontSize(8).SetBold()).SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBorder(new SolidBorder(lineColor, 1)));


            document.Add(header);
            document.Add(Espacio);
            document.Add(Espacio);
            document.Add(Espacio);
            document.Add(tablaEncabezado);
            document.Add(Espacio);
            document.Add(Espacio);
            document.Add(text1);
            document.Add(tablaEncabezadoDetalle);
            //document.Add(text2);
            //document.Add(tablaEncabezadoDetalleSellos);         
            document.Add(Espacio);
            document.Add(tablaDescripcionItems);
            document.Add(Espacio);
            //document.Add(tablaDetalleFact);

            document.Close();

            byte[] bytesStreams = stream.ToArray();
            stream = new MemoryStream();
            stream.Write(bytesStreams, 0, bytesStreams.Length);
            stream.Position = 0;
            return new FileStreamResult(stream, "application/pdf");
        }
    }
}



