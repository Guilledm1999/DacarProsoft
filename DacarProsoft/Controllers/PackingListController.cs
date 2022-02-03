
using DacarProsoft.Datos;
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
using System.Globalization;
using System.Text;
using System.Security.Cryptography;

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
        public int RegistrarPallet(string NumeroDocumento, string NumeroOrden, string NombreCliente, string Origen, string Destino, int CantidadPallet, int IdentificadorDetalle, string tipo, int vari)
        {
            int identificador;
            daoPackilist = new DaoPackingList();
            daoOrdenesVentas = new DaoOrdenesVentas();
            try {
                var result = daoPackilist.IngresarPacking(Convert.ToInt32(NumeroDocumento), NumeroOrden, NombreCliente, Origen, Destino, CantidadPallet, tipo);
                identificador = result;

                if (vari == 1) {
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



                return identificador;
            } catch (Exception ex)
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
            //var detallePackingLocal = daoPackilist.ConsultarPalletPackingPorItem(PackingId, CodeItem);
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
                Console.WriteLine(idPacking);
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

            Console.WriteLine(prueba);

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

            Console.WriteLine(prueba);

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
            //var encriptar = Encrypt("ValorENcriptado");
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

            Console.WriteLine(prueba);

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
        //[HttpGet]
        //public ActionResult PalletPdf2(int PackingId, int PalletId)
        //{
        //    string Orden = null;
        //    string Cliente = null;
        //    decimal PesoBruto = 0;
        //    decimal PesoNeto = 0;
        //    string Origen = null;
        //    string Destino = null;
        //    string CodigoQr = null;

        //    int palletNumber = 0;
        //    daoPackilist = new DaoPackingList();
        //    var Cabecera = daoPackilist.ConsultarInfoPallets(PackingId, PalletId);
        //    var Detalle = daoPackilist.ConsultarPalletsDetalleIngreseados(PackingId, PalletId);

        //    PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
        //    PdfFont No_Bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
        //    List<ItemsPallet> lst = new List<ItemsPallet>();
        //    foreach (var y in Cabecera)
        //    {
        //        Orden = y.NumeroOrden;
        //        Cliente = y.NombreCliente;
        //        PesoBruto = y.PesoBruto;
        //        PesoNeto = y.PesoNeto;
        //        Origen = y.Origen;
        //        Destino = y.Destino;
        //        palletNumber = y.PalletNumber;
        //        CodigoQr = y.CodigoQr;
        //    }

        //    byte[] bytes = Convert.FromBase64String(CodigoQr);


        //    foreach (var x in Detalle)
        //    {
        //        lst.Add(new ItemsPallet
        //        {
        //            cantidad = Convert.ToString(x.CantidadItem),
        //            codigo = Convert.ToString(x.ItemCode),
        //            descripcion = Convert.ToString(x.DescriptionCode)
        //        });
        //    }


        //    MemoryStream stream = new MemoryStream();

        //    decimal suma = 0;
        //    LineSeparator ls = new LineSeparator(new SolidLine());
        //    var path = System.IO.Path.Combine(Server.MapPath("~/Images/LogoDacar.png"));

        //    iText.Layout.Element.Image img1 = new iText.Layout.Element.Image(ImageDataFactory.Create(path));

        //    var path2 = System.IO.Path.Combine(Server.MapPath("~/Images/ETPACKINGLIST-02.png"));

        //    iText.Layout.Element.Image img2 = new iText.Layout.Element.Image(ImageDataFactory.Create(path2)).SetWidth(325).SetHeight(325);
        //    PdfWriter writer = new PdfWriter(stream);

        //    var path3 = System.IO.Path.Combine(Server.MapPath("~/Images/Imagen1.png"));

        //    iText.Layout.Element.Image img3 = new iText.Layout.Element.Image(ImageDataFactory.Create(path3));

        //    PdfDocument pdf = new PdfDocument(writer);

        //    iText.Kernel.Geom.Rectangle rectangle13x21 = new iText.Kernel.Geom.Rectangle(368.50f, 595.28f);


        //    //Document document = new Document(pdf, iText.Kernel.Geom.PageSize.A5, true);
        //    Document document = new Document(pdf, new PageSize(rectangle13x21));


        //    Table itemsTable = new Table(2);

        //    itemsTable.SetHorizontalAlignment(HorizontalAlignment.CENTER);

        //    Cell cell = new Cell().Add(new Paragraph("Product").SetFont(bold).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER)).SetBackgroundColor(ColorConstants.BLUE);
        //    itemsTable.AddCell(cell);
        //    Cell cell2 = new Cell().Add(new Paragraph("Unit").SetFont(bold).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER)).SetBackgroundColor(ColorConstants.BLUE);
        //    itemsTable.AddCell(cell2);

        //    foreach (var x in lst)
        //    {
        //        itemsTable.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(x.descripcion).SetFontSize(8)));
        //        itemsTable.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("  " + x.cantidad).SetFontSize(8)));
        //    }

        //    iText.Layout.Element.Image img = new iText.Layout.Element.Image(ImageDataFactory
        //   .Create(bytes))
        //   .SetTextAlignment(TextAlignment.CENTER).SetWidth(325).SetHeight(325);

        //    pdf.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderHandleEvent(img1, Convert.ToString(palletNumber), img3));
        //    pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new FooterHandleEvent(img, img2));
        //    pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new BackgroundCuerpoEvent());
        //    pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new BackgroundColorEvent());



        //    Paragraph header = new Paragraph("PACKING LIST")
        //       .SetTextAlignment(TextAlignment.CENTER)
        //       .SetFontSize(20).SetFontColor(ColorConstants.BLACK).SetFont(bold);
        //    PdfFont font = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);

        //    List list = new List().SetSymbolIndent(12)
        //   .SetFont(font);

        //    foreach (var x in lst)
        //    {
        //        suma = suma + Convert.ToDecimal(x.cantidad);
        //    }

        //    Paragraph Weight = new Paragraph("Gross Weight:" + PesoBruto + "                    Net Weight:" + PesoNeto).SetTextAlignment(TextAlignment.CENTER)
        //       .SetFontSize(12).SetFontColor(ColorConstants.BLACK);
        //    Paragraph cliente = new Paragraph("Customer:     ").SetTextAlignment(TextAlignment.LEFT)
        //             .SetFontSize(14).SetFont(bold).Add(new Paragraph(Cliente).SetFont(No_Bold)).SetFontColor(ColorConstants.BLACK);

        //    Paragraph orden = new Paragraph("Order:      ").SetTextAlignment(TextAlignment.LEFT)
        //             .SetFontSize(14).SetFont(bold).Add(new Paragraph(Orden).SetFont(No_Bold)).SetFontColor(ColorConstants.BLACK);

        //    Paragraph origen = new Paragraph("Origin:     ").SetTextAlignment(TextAlignment.LEFT)
        //             .SetFontSize(12).SetFont(bold).Add(new Paragraph(Origen).SetFont(No_Bold)).SetFontColor(ColorConstants.BLACK);

        //    Paragraph destino = new Paragraph("Destination:    ").SetTextAlignment(TextAlignment.LEFT)
        //             .SetFontSize(12).SetFont(bold).Add(new Paragraph(Destino).SetFont(No_Bold)).SetFontColor(ColorConstants.BLACK);

        //    Paragraph cantidad = new Paragraph("Quantity:  ").SetTextAlignment(TextAlignment.LEFT)
        //             .SetFontSize(12).SetFont(bold).Add(new Paragraph("" + suma).SetFont(No_Bold)).SetFontColor(ColorConstants.BLACK);

        //    Paragraph Espacio = new Paragraph("  ").SetTextAlignment(TextAlignment.CENTER)
        //       .SetFontSize(12).SetFontColor(ColorConstants.BLACK);

        //    Paragraph PalletNumber = new Paragraph("Number Pallet:").SetTextAlignment(TextAlignment.RIGHT)
        //       .SetFontSize(15).SetFontColor(ColorConstants.BLACK);


        //    document.Add(Espacio);
        //    document.Add(Espacio);
        //    document.Add(Espacio);
        //    document.Add(Espacio);
        //    document.Add(Espacio);
        //    document.Add(header);
        //    document.Add(Espacio);
        //    document.Add(Weight);
        //    document.Add(cliente);
        //    document.Add(orden);
        //    document.Add(origen);
        //    document.Add(destino);
        //    document.Add(cantidad);
        //    document.Add(itemsTable);
        //    document.Close();

        //    byte[] bytesStreams = stream.ToArray();
        //    stream = new MemoryStream();
        //    stream.Write(bytesStreams, 0, bytesStreams.Length);
        //    stream.Position = 0;



        //    return new FileStreamResult(stream, "application/pdf");


        //}

        //[HttpPost]
        //public string CrearPdf(String NumeroPallet,String Orden, String Cliente, String Volumen, String GrossWeight, String NetWeight, String Origen, String Destino, List<DetallePallet> items,string Qr) {
        //    List<ItemsPallet> lst = new List<ItemsPallet>();
        //    //byte[] bytes = Encoding.ASCII.GetBytes(Qr);

        //    byte[] bytes = Convert.FromBase64String(Qr);


        //    foreach (var x in items)
        //    {
        //        lst.Add(new ItemsPallet
        //        {
        //            cantidad = Convert.ToString(x.CantidadItem),
        //            codigo = Convert.ToString(x.ItemCode),
        //            descripcion = Convert.ToString(x.DescriptionItem)
        //        });
        //    }

        //    MemoryStream stream = new MemoryStream();

        //    decimal suma = 0;
        //    LineSeparator ls = new LineSeparator(new SolidLine());
        //    var path = System.IO.Path.Combine(Server.MapPath("~/Images/LogoDacar.png"));



        //    iText.Layout.Element.Image img1 = new iText.Layout.Element.Image(ImageDataFactory.Create(path));


        //    var path2 = System.IO.Path.Combine(Server.MapPath("~/Images/SeguridadIndustrial.png"));

        //    iText.Layout.Element.Image img2 = new iText.Layout.Element.Image(ImageDataFactory.Create(path2));

        //    //System.Drawing.Image image1 = System.Drawing.Image.FromFile(path);

        //    //PdfWriter writer = new PdfWriter("C:\\Prueba\\"+ Orden + "Pallet"+NumeroPallet + ".pdf");
        //    PdfWriter writer = new PdfWriter(stream);

        //    PdfDocument pdf = new PdfDocument(writer);
        //    //pdf.SetDefaultPageSize(iText.Kernel.Geom.PageSize.A4);
        //    Document document = new Document(pdf, iText.Kernel.Geom.PageSize.A5, true);

        //    //PdfPTable table = new PdfPTable(3);
        //    Table itemsTable = new Table(2);

        //    itemsTable.SetHorizontalAlignment(HorizontalAlignment.CENTER);


        //    foreach (var x in lst)
        //    {
        //        itemsTable.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(x.descripcion)));
        //        itemsTable.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("  "+x.cantidad)));
        //    }

        //    iText.Layout.Element.Image img = new iText.Layout.Element.Image(ImageDataFactory
        //   .Create(bytes))
        //   .SetTextAlignment(TextAlignment.CENTER).SetWidth(325).SetHeight(325);
        //    //EventoPagina evento = new EventoPagina(document,Qr);

        //    pdf.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderHandleEvent(img1, NumeroPallet));
        //    pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new FooterHandleEvent(img,img2));
        //    pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new BackgroundColorEvent());




        //    //pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, evento);

        //    Paragraph header = new Paragraph("PACKING LIST")
        //       .SetTextAlignment(TextAlignment.CENTER)
        //       .SetFontSize(16);
        //    PdfFont font = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);

        //    List list = new List().SetSymbolIndent(12)
        //   //.SetListSymbol("\u2022")
        //   .SetFont(font);

        //    PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
        //    PdfFont No_Bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);


        //    //foreach (var x in items)
        //    //{
        //    //    list
        //    //   .Add(new ListItem(x.descripcion+"         "+x.cantidad));
        //    //}

        //        foreach (var x in lst)
        //    {
        //        suma = suma+Convert.ToDecimal(x.cantidad)  ;
        //    }


        //    Paragraph Weight = new Paragraph("Gross Weight:" + GrossWeight + "                    Net Weight:" + NetWeight).SetTextAlignment(TextAlignment.CENTER)
        //       .SetFontSize(12);
        //    Paragraph cliente = new Paragraph("Customer:     ").SetTextAlignment(TextAlignment.JUSTIFIED)
        //             .SetFontSize(12).SetFont(bold).Add(new Paragraph(Cliente).SetFont(No_Bold));

        //    Paragraph orden = new Paragraph("Order:      ").SetTextAlignment(TextAlignment.JUSTIFIED)
        //             .SetFontSize(12).SetFont(bold).Add(new Paragraph(Orden).SetFont(No_Bold));

        //    Paragraph volumen = new Paragraph("Volumen:   ").SetTextAlignment(TextAlignment.JUSTIFIED)
        //             .SetFontSize(12).SetFont(bold).Add(new Paragraph(Volumen).SetFont(No_Bold));

        //    Paragraph origen = new Paragraph("Origin:     ").SetTextAlignment(TextAlignment.JUSTIFIED)
        //             .SetFontSize(12).SetFont(bold).Add(new Paragraph(Origen).SetFont(No_Bold));

        //    Paragraph destino = new Paragraph("Destination:    ").SetTextAlignment(TextAlignment.JUSTIFIED)
        //             .SetFontSize(12).SetFont(bold).Add(new Paragraph(Destino).SetFont(No_Bold));

        //    Paragraph cantidad = new Paragraph("Quantity:  ").SetTextAlignment(TextAlignment.JUSTIFIED)
        //             .SetFontSize(12).SetFont(bold).Add(new Paragraph(""+suma).SetFont(No_Bold));

        //    Paragraph Espacio = new Paragraph("  ").SetTextAlignment(TextAlignment.CENTER)
        //       .SetFontSize(12);

        //    Paragraph PalletNumber = new Paragraph("Number Pallet:").SetTextAlignment(TextAlignment.RIGHT)
        //       .SetFontSize(15);

        //    // iText.Layout.Element.Image Imgdacar = new iText.Layout.Element.Image(ImageDataFactory
        //    //.Create(imagenDc))
        //    //.SetTextAlignment(TextAlignment.LEFT).SetWidth(155).SetHeight(80);


        //    //document.Add(Imgdacar).Add(PalletNumber);
        //    document.Add(Espacio);
        //    document.Add(Espacio);
        //    document.Add(Espacio);
        //    document.Add(Espacio);
        //    document.Add(Espacio);
        //    document.Add(header);
        //    document.Add(ls);
        //    document.Add(Espacio);
        //    document.Add(Weight);
        //    document.Add(cliente);
        //    document.Add(orden);
        //    document.Add(volumen);
        //    document.Add(origen);
        //    document.Add(destino);
        //    document.Add(cantidad);
        //    document.Add(Espacio);
        //    document.Add(Espacio);
        //    document.Add(itemsTable);
        //    //document.Add(itemsTable);
        //    //document.Add(img);
        //    document.Close();

        //    byte[] bytesStreams = stream.ToArray();
        //    stream = new MemoryStream();
        //    stream.Write(bytesStreams, 0, bytesStreams.Length);
        //    stream.Position = 0;

        //    //return new FileStreamResult(stream, "application/pdf");

        //    return Convert.ToBase64String(bytesStreams);
        //    //return new FilePathResult(bytesStreams, "application/pdf");

        //    //    byte[] byte1 = stream.ToArray();
        //    //    return File(byte1, "application/pdf", "pdf_file_name.pdf");
        //    //}



        //    //return new FileStreamResult(new MemoryStream(stream.ToArray()), "application/pdf");


        //}

//        public class HeaderHandleEvent : IEventHandler
//        {
//            iText.Layout.Element.Image imge;
//            iText.Layout.Element.Image imge2;

//            string PalletNumero;

//            public HeaderHandleEvent(iText.Layout.Element.Image img, string numeroPallet, iText.Layout.Element.Image img2)
//            {
//                imge = img;
//                PalletNumero = numeroPallet;
//                imge2 = img2;

//            }


//            public void HandleEvent(Event @event)
//            {
//                PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
//                PdfDocument pdfDoc = docEvent.GetDocument();
//                PdfPage page = docEvent.GetPage();

//                PdfCanvas canvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);
//#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
//                new Canvas(canvas, pdfDoc, new iText.Kernel.Geom.Rectangle(35, page.GetPageSize().GetTop() - 75, page.GetPageSize().GetRight() - 65, 65)).
//#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
//                    Add(getTable(docEvent));

//            }
//            private Table getTable(PdfDocumentEvent docEvent)
//            {


//                float[] cellWidth = { 30f, 10f };
//                Table tablaEvent = new Table(UnitValue.CreatePercentArray(cellWidth)).UseAllAvailableWidth();

//                Style styleCell = new Style().SetBorder(Border.NO_BORDER);
//                Style styletext = new Style().SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10f);

//                Cell cell = new Cell(2, 1).Add(imge.SetAutoScale(true).SetBorder(Border.NO_BORDER))/*.SetBackgroundColor(ColorConstants.BLUE)*/;

//                tablaEvent.AddCell(cell.AddStyle(styleCell).SetTextAlignment(TextAlignment.LEFT));
//                PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
//                cell = new Cell(2, 1).Add(new Paragraph("Pallet Number").SetFontSize(12).SetFont(bold).SetBorder(Border.NO_BORDER)).Add(new Paragraph("").SetFont(bold).SetBorder(Border.NO_BORDER))
//                    .Add(new Paragraph(PalletNumero).SetFontSize(15).SetVerticalAlignment(VerticalAlignment.MIDDLE)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.WHITE);


//                tablaEvent.AddCell(cell);
//                return tablaEvent;
//            }
//        }
//        public class BackgroundColorEvent : IEventHandler
//        {
//            iText.Kernel.Colors.Color SolidColor;
//            iText.Kernel.Colors.Color SolidColor2;

//            public BackgroundColorEvent() {
//                SolidColor = new DeviceRgb(44, 14, 250);
//                SolidColor2 = ColorConstants.BLUE;

//            }

//            public void HandleEvent(Event @event)
//            {

//                PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
//                PdfDocument pdf = docEvent.GetDocument();
//                PdfPage page = docEvent.GetPage();

//                iText.Kernel.Geom.Rectangle pageSize = page.GetPageSize();

//                PdfCanvas pdfCanvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdf);

//                pdfCanvas.SaveState().SetFillColor(SolidColor2).Rectangle(pageSize.GetLeft(), 0, pageSize.GetWidth(), pageSize.GetHeight())
//                    .Fill().RestoreState();
//                pdfCanvas.Release();
//            }
//        }

//        public class BackgroundCuerpoEvent : IEventHandler
//        {
//            iText.Kernel.Colors.Color SolidColor;

//            public BackgroundCuerpoEvent()
//            {
//                SolidColor = new DeviceRgb(248, 246, 250);
//            }

//            public void HandleEvent(Event @event)
//            {

//                PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
//                PdfDocument pdf = docEvent.GetDocument();
//                PdfPage page = docEvent.GetPage();

//                iText.Kernel.Geom.Rectangle pageSize = page.GetPageSize();

//                PdfCanvas pdfCanvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdf);

//                pdfCanvas.SaveState().SetFillColor(SolidColor)./*Rectangle((pageSize.GetLeft()-10), 100, (pageSize.GetWidth()-10), (pageSize.GetHeight()-180))*/
//                  Rectangle(20, 100, 382, 415)
//                    .Fill().RestoreState();
//                pdfCanvas.Release();
//            }
//        }

//        public class FooterHandleEvent : IEventHandler
//        {
//            iText.Layout.Element.Image imge;
//            iText.Layout.Element.Image imge2;


//            public FooterHandleEvent(iText.Layout.Element.Image img, iText.Layout.Element.Image img2)
//            {
//                imge = img;
//                imge2 = img2;

//            }

//            public void HandleEvent(Event @event)
//            {
//                PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
//                PdfDocument pdfDoc = docEvent.GetDocument();
//                PdfPage page = docEvent.GetPage();

//                PdfCanvas canvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);
//#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
//                new Canvas(canvas, pdfDoc, new iText.Kernel.Geom.Rectangle(36, 1, page.GetPageSize().GetRight() - 70, 90)).

//#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
//                    Add(getTable(docEvent));
//            }

//            private Table getTable(PdfDocumentEvent docEvent)
//            {

//                float[] cellWidth = { 30f, 10f };
//                Table tablaEvent = new Table(UnitValue.CreatePercentArray(cellWidth)).UseAllAvailableWidth();


//                Style styleCell = new Style().SetBorder(Border.NO_BORDER);
//                Style styletext = new Style().SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10f);

//                imge2.SetWidth(288).SetHeight(137);
//                imge.SetWidth(325).SetHeight(325);


//                Cell cell = new Cell().SetHeight(92f).Add(imge2.SetAutoScale(true).AddStyle(styleCell));
//                tablaEvent.AddCell(cell.SetTextAlignment(TextAlignment.RIGHT).SetBorder(Border.NO_BORDER));
//                cell = new Cell().SetHeight(92f).Add(imge.SetAutoScale(true).SetTextAlignment(TextAlignment.LEFT).AddStyle(styleCell)).SetBorder(Border.NO_BORDER);

//                tablaEvent.AddCell(cell);

//                return tablaEvent;
//            }
//        }
//        //Aqui comienza el nuevo metodo para imprimir sin imageness ni fondos
//        [HttpGet]
//        public ActionResult PalletPdf(int PackingId, int PalletId)
//        {
//            string Orden = null;
//            string Cliente = null;
//            decimal PesoBruto = 0;
//            decimal PesoNeto = 0;
//            string Origen = null;
//            string Destino = null;
//            string CodigoQr = null;

//            int palletNumber = 0;
//            daoPackilist = new DaoPackingList();
//            var Cabecera = daoPackilist.ConsultarInfoPallets(PackingId, PalletId);
//            var Detalle = daoPackilist.ConsultarPalletsDetalleIngreseados(PackingId, PalletId);

//            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
//            PdfFont No_Bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
//            List<ItemsPallet> lst = new List<ItemsPallet>();
//            foreach (var y in Cabecera)
//            {
//                Orden = y.NumeroOrden;
//                Cliente = y.NombreCliente;
//                PesoBruto = y.PesoBruto;
//                PesoNeto = y.PesoNeto;
//                Origen = y.Origen;
//                Destino = y.Destino;
//                palletNumber = y.PalletNumber;
//                CodigoQr = y.CodigoQr;
//            }

//            byte[] bytes = Convert.FromBase64String(CodigoQr);


//            foreach (var x in Detalle)
//            {
//                lst.Add(new ItemsPallet
//                {
//                    cantidad = Convert.ToString(x.CantidadItem),
//                    codigo = Convert.ToString(x.ItemCode),
//                    descripcion = Convert.ToString(x.DescriptionCode)
//                });
//            }


//            MemoryStream stream = new MemoryStream();

//            decimal suma = 0;
//            LineSeparator ls = new LineSeparator(new SolidLine());
//            var path = System.IO.Path.Combine(Server.MapPath("~/Images/LogoDacar.png"));

//            iText.Layout.Element.Image img1 = new iText.Layout.Element.Image(ImageDataFactory.Create(path));

//            var path2 = System.IO.Path.Combine(Server.MapPath("~/Images/ETPACKINGLIST-02.png"));

//            iText.Layout.Element.Image img2 = new iText.Layout.Element.Image(ImageDataFactory.Create(path2)).SetWidth(325).SetHeight(325);
//            PdfWriter writer = new PdfWriter(stream);

//            var path3 = System.IO.Path.Combine(Server.MapPath("~/Images/Imagen1.png"));

//            iText.Layout.Element.Image img3 = new iText.Layout.Element.Image(ImageDataFactory.Create(path3));

//            PdfDocument pdf = new PdfDocument(writer);
//            //Document document = new Document(pdf, iText.Kernel.Geom.PageSize.A5, true);


//            iText.Kernel.Geom.Rectangle rectangle13x21 = new iText.Kernel.Geom.Rectangle(368.50f, 595.28f);


//            //Document document = new Document(pdf, iText.Kernel.Geom.PageSize.A5, true);
//            Document document = new Document(pdf, new PageSize(rectangle13x21));


//            Table itemsTable = new Table(2);

//            itemsTable.SetHorizontalAlignment(HorizontalAlignment.CENTER);

//            Cell cell = new Cell().Add(new Paragraph("Product").SetFont(bold).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER)).SetBackgroundColor(ColorConstants.BLUE);
//            itemsTable.AddCell(cell);
//            Cell cell2 = new Cell().Add(new Paragraph("Unit").SetFont(bold).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER)).SetBackgroundColor(ColorConstants.BLUE);
//            itemsTable.AddCell(cell2);

//            foreach (var x in lst)
//            {
//                itemsTable.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(x.descripcion).SetFontSize(8)));
//                itemsTable.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph("  " + x.cantidad).SetFontSize(8)));
//            }

//            iText.Layout.Element.Image img = new iText.Layout.Element.Image(ImageDataFactory
//           .Create(bytes))
//           .SetTextAlignment(TextAlignment.CENTER).SetWidth(325).SetHeight(325);

//            pdf.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderHandleEvent2(img1, Convert.ToString(palletNumber)));
//            //pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new FooterHandleEvent2(img, img2));
//            pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new FooterHandleEvent2(img));

//            //pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new BackgroundCuerpoEvent2());
//            //pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new BackgroundColorEvent2());



//            Paragraph header = new Paragraph("PACKING LIST")
//               .SetTextAlignment(TextAlignment.CENTER)
//               .SetFontSize(20).SetFontColor(ColorConstants.BLACK).SetFont(bold);
//            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);

//            List list = new List().SetSymbolIndent(12)
//           .SetFont(font);

//            foreach (var x in lst)
//            {
//                suma = suma + Convert.ToDecimal(x.cantidad);
//            }

//            Paragraph Weight = new Paragraph("Gross Weight:" + PesoBruto + "                    Net Weight:" + PesoNeto).SetTextAlignment(TextAlignment.CENTER)
//               .SetFontSize(12).SetFontColor(ColorConstants.BLACK);
//            Paragraph cliente = new Paragraph("Customer:     ").SetTextAlignment(TextAlignment.LEFT)
//                     .SetFontSize(14).SetFont(bold).Add(new Paragraph(Cliente).SetFont(No_Bold)).SetFontColor(ColorConstants.BLACK);

//            Paragraph orden = new Paragraph("Order:      ").SetTextAlignment(TextAlignment.LEFT)
//                     .SetFontSize(14).SetFont(bold).Add(new Paragraph(Orden).SetFont(No_Bold)).SetFontColor(ColorConstants.BLACK);

//            Paragraph origen = new Paragraph("Origin:     ").SetTextAlignment(TextAlignment.LEFT)
//                     .SetFontSize(12).SetFont(bold).Add(new Paragraph(Origen).SetFont(No_Bold)).SetFontColor(ColorConstants.BLACK);

//            Paragraph destino = new Paragraph("Destination:    ").SetTextAlignment(TextAlignment.LEFT)
//                     .SetFontSize(12).SetFont(bold).Add(new Paragraph(Destino).SetFont(No_Bold)).SetFontColor(ColorConstants.BLACK);

//            Paragraph cantidad = new Paragraph("Quantity:  ").SetTextAlignment(TextAlignment.LEFT)
//                     .SetFontSize(12).SetFont(bold).Add(new Paragraph("" + suma).SetFont(No_Bold)).SetFontColor(ColorConstants.BLACK);

//            Paragraph Espacio = new Paragraph("  ").SetTextAlignment(TextAlignment.CENTER)
//               .SetFontSize(12).SetFontColor(ColorConstants.BLACK);

//            Paragraph PalletNumber = new Paragraph("Number Pallet:").SetTextAlignment(TextAlignment.RIGHT)
//               .SetFontSize(15).SetFontColor(ColorConstants.BLACK);


//            document.Add(Espacio);
//            document.Add(Espacio);
//            document.Add(Espacio);
//            document.Add(Espacio);
//            document.Add(Espacio);
//            document.Add(header);
//            document.Add(Espacio);
//            document.Add(Weight);
//            document.Add(cliente);
//            document.Add(orden);
//            document.Add(origen);
//            document.Add(destino);
//            document.Add(cantidad);
//            document.Add(itemsTable);
//            document.Close();

//            byte[] bytesStreams = stream.ToArray();
//            stream = new MemoryStream();
//            stream.Write(bytesStreams, 0, bytesStreams.Length);
//            stream.Position = 0;


//            return new FileStreamResult(stream, "application/pdf");


//        }

//        public class HeaderHandleEvent2 : IEventHandler
//        {
//            iText.Layout.Element.Image imge;

//            string PalletNumero;

//            public HeaderHandleEvent2(iText.Layout.Element.Image img, string numeroPallet)
//            {
//                imge = img;
//                PalletNumero = numeroPallet;
//            }


//            public void HandleEvent(Event @event)
//            {
//                PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
//                PdfDocument pdfDoc = docEvent.GetDocument();
//                PdfPage page = docEvent.GetPage();

//                PdfCanvas canvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);
//#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
//                new Canvas(canvas, pdfDoc, new iText.Kernel.Geom.Rectangle(35, page.GetPageSize().GetTop() - 75, page.GetPageSize().GetRight() - 65, 65)).
//#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
//                    Add(getTable(docEvent));

//            }
//            private Table getTable(PdfDocumentEvent docEvent)
//            {


//                float[] cellWidth = { 30f, 10f };
//                Table tablaEvent = new Table(UnitValue.CreatePercentArray(cellWidth)).UseAllAvailableWidth();

//                Style styleCell = new Style().SetBorder(Border.NO_BORDER);
//                Style styletext = new Style().SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10f);

//                //Cell cell = new Cell(2, 1).Add(imge.SetAutoScale(true).SetBorder(Border.NO_BORDER))/*.SetBackgroundColor(ColorConstants.BLUE)*/;

//                Cell cell = new Cell().SetHeight(92f).Add(new Paragraph("").SetBorder(Border.NO_BORDER));

//                tablaEvent.AddCell(cell.AddStyle(styleCell).SetTextAlignment(TextAlignment.LEFT));
//                PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
//                cell = new Cell().SetHeight(92f)
//                    //.Add(new Paragraph(PalletNumero).SetFontSize(15).SetVerticalAlignment(VerticalAlignment.MIDDLE)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.WHITE);
//                    .Add(new Paragraph(PalletNumero).SetFontSize(15)).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(ColorConstants.WHITE).SetVerticalAlignment(VerticalAlignment.BOTTOM);
//                tablaEvent.AddCell(cell);
//                return tablaEvent;
//            }
//        }
//        public class BackgroundColorEvent2 : IEventHandler
//        {
//            iText.Kernel.Colors.Color SolidColor;
//            iText.Kernel.Colors.Color SolidColor2;

//            public BackgroundColorEvent2()
//            {
//                SolidColor = new DeviceRgb(44, 14, 250);
//                SolidColor2 = ColorConstants.BLUE;

//            }

//            public void HandleEvent(Event @event)
//            {

//                PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
//                PdfDocument pdf = docEvent.GetDocument();
//                PdfPage page = docEvent.GetPage();

//                iText.Kernel.Geom.Rectangle pageSize = page.GetPageSize();

//                PdfCanvas pdfCanvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdf);

//                pdfCanvas.SaveState().SetFillColor(SolidColor2).Rectangle(pageSize.GetLeft(), 0, pageSize.GetWidth(), pageSize.GetHeight())
//                    .Fill().RestoreState();
//                pdfCanvas.Release();
//            }
//        }

//        public class BackgroundCuerpoEvent2 : IEventHandler
//        {
//            iText.Kernel.Colors.Color SolidColor;

//            public BackgroundCuerpoEvent2()
//            {
//                SolidColor = new DeviceRgb(248, 246, 250);
//            }

//            public void HandleEvent(Event @event)
//            {

//                PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
//                PdfDocument pdf = docEvent.GetDocument();
//                PdfPage page = docEvent.GetPage();

//                iText.Kernel.Geom.Rectangle pageSize = page.GetPageSize();

//                PdfCanvas pdfCanvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdf);

//                pdfCanvas.SaveState().SetFillColor(SolidColor)./*Rectangle((pageSize.GetLeft()-10), 100, (pageSize.GetWidth()-10), (pageSize.GetHeight()-180))*/
//                  Rectangle(20, 100, 382, 415)
//                    .Fill().RestoreState();
//                pdfCanvas.Release();
//            }
//        }

//        public class FooterHandleEvent2 : IEventHandler
//        {
//            iText.Layout.Element.Image imge;


//            public FooterHandleEvent2(iText.Layout.Element.Image img)
//            {
//                imge = img;
//            }

//            public void HandleEvent(Event @event)
//            {
//                PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
//                PdfDocument pdfDoc = docEvent.GetDocument();
//                PdfPage page = docEvent.GetPage();
//                PdfCanvas canvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);
//#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
//                new Canvas(canvas, pdfDoc, new iText.Kernel.Geom.Rectangle(36, 1, page.GetPageSize().GetRight() - 70, 90)).

//#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
//                    Add(getTable2(docEvent));
//            }
//            private Table getTable2(PdfDocumentEvent docEvent)
//            {
//                float[] cellWidth = { 30f, 10f };
//                Table tablaEvent = new Table(UnitValue.CreatePercentArray(cellWidth)).UseAllAvailableWidth();
//                Style styleCell = new Style().SetBorder(Border.NO_BORDER);
//                Style styletext = new Style().SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10f);

//                imge.SetWidth(325).SetHeight(325);


//                Cell cell = new Cell().SetHeight(92f).Add(new Paragraph("").SetBorder(Border.NO_BORDER));
//                //tablaEvent.AddCell(cell.SetTextAlignment(TextAlignment.RIGHT).SetBorder(Border.NO_BORDER));
//                tablaEvent.AddCell(cell.SetTextAlignment(TextAlignment.RIGHT));

//                //cell = new Cell().SetHeight(92f).Add(imge.SetAutoScale(true).SetTextAlignment(TextAlignment.LEFT).AddStyle(styleCell)).SetBorder(Border.NO_BORDER);
//                cell = new Cell().SetHeight(92f).Add(imge.SetAutoScale(true).SetTextAlignment(TextAlignment.LEFT).AddStyle(styleCell));

//                tablaEvent.AddCell(cell);

//                return tablaEvent;
//            }
//        }

//        public string Decrypt(string encryptedText)
//        {
//            if (encryptedText == null)
//            {
//                return null;
//            }
//            // Get the bytes of the string
//            var bytesToBeDecrypted = Convert.FromBase64String(encryptedText);
//            var passwordBytes = Encoding.UTF8.GetBytes(password);

//            passwordBytes = SHA512.Create().ComputeHash(passwordBytes);

//            var bytesDecrypted = Decrypt(bytesToBeDecrypted, passwordBytes);

//            return Encoding.UTF8.GetString(bytesDecrypted);
//        }

//        private static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
//        {
//            byte[] decryptedBytes = null;
//            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

//            using (MemoryStream ms = new MemoryStream())
//            {
//                using (RijndaelManaged AES = new RijndaelManaged())
//                {
//                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

//                    AES.KeySize = 256;
//                    AES.BlockSize = 128;
//                    AES.Key = key.GetBytes(AES.KeySize / 8);
//                    AES.IV = key.GetBytes(AES.BlockSize / 8);
//                    AES.Mode = CipherMode.CBC;

//                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
//                    {
//                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
//                        cs.Close();
//                    }

//                    decryptedBytes = ms.ToArray();
//                }
//            }

//            return decryptedBytes;
//        }
        //Ejemplo de disenio pallet 3
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
                PesoBruto = y.PesoBruto;
                PesoNeto = y.PesoNeto;
                Origen = y.Origen;
                Destino = y.Destino;
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

            //var path = System.IO.Path.Combine(Server.MapPath("~/Images/DisenoPackingNew.png"));
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
                //acumItem = acumItem + " "+ x.cantidad + " " + x.descripcion + " ,";
                CantidadTotal = CantidadTotal + x.CantidadItem.Value;
            }


            document.Add(new Paragraph("\n"));



            Paragraph palletNumb = new Paragraph("" + palletNumber+"/"+CantidadPallet).SetFont(bold).SetFontSize(14).SetBorder(Border.NO_BORDER).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER);
            PdfCanvas canvasNumbP = new PdfCanvas(pdf.GetFirstPage());
            iText.Kernel.Geom.Rectangle rectNumbP = new iText.Kernel.Geom.Rectangle(24, primerFila, 70, 24);
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            new Canvas(canvasNumbP, pdf, rectNumbP)
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                    .Add(palletNumb);
            //canvasNumbP.Rectangle(rectNumbP);
            canvasNumbP.Stroke();

            Paragraph ParaPesoBruto = new Paragraph("" + PesoBruto+"kg").SetFontSize(13).SetFont(bold).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER);
            PdfCanvas canvasPb = new PdfCanvas(pdf.GetFirstPage());
            iText.Kernel.Geom.Rectangle rectPb = new iText.Kernel.Geom.Rectangle(110, primerFila, 70, 24);
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            new Canvas(canvasPb, pdf, rectPb)
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                    .Add(ParaPesoBruto);
            //canvasPb.Rectangle(rectPb);
            canvasPb.Stroke();


            Paragraph ParaPesoNeto = new Paragraph("" + PesoNeto + "kg").SetFontSize(13).SetFont(bold).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER);
            PdfCanvas canvasPn = new PdfCanvas(pdf.GetFirstPage());
            iText.Kernel.Geom.Rectangle rectPn = new iText.Kernel.Geom.Rectangle(200, primerFila, 70, 24);
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            new Canvas(canvasPn, pdf, rectPn)
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                    .Add(ParaPesoNeto);
            //canvasPn.Rectangle(rectPn);
            canvasPn.Stroke();


            Paragraph p = new Paragraph("" + Orden).SetFont(bold).SetFontSize(14).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER);
            PdfCanvas canvas = new PdfCanvas(pdf.GetFirstPage());
            iText.Kernel.Geom.Rectangle rect = new iText.Kernel.Geom.Rectangle(170, segundaFila, 107, 35);
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            new Canvas(canvas, pdf, rect)
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                    .Add(p);
            //canvas.Rectangle(rect);
            canvas.Stroke();

            Paragraph p2 = new Paragraph("" + Cliente).SetFont(bold).SetFontSize(9).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER);
            PdfCanvas canvas2 = new PdfCanvas(pdf.GetFirstPage());
            iText.Kernel.Geom.Rectangle rect2 = new iText.Kernel.Geom.Rectangle(30, segundaFila, 110, 35);
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            new Canvas(canvas2, pdf, rect2)
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                    .Add(p2);
            //canvas.Rectangle(rect2);
            canvas2.Stroke();


            Paragraph p3 = new Paragraph("" + Destino).SetFont(bold).SetFontSize(12).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER);
            PdfCanvas canvas3 = new PdfCanvas(pdf.GetFirstPage());
            iText.Kernel.Geom.Rectangle rect3 = new iText.Kernel.Geom.Rectangle(170, tercerFila, 107, 42);
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            new Canvas(canvas3, pdf, rect3)
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                    .Add(p3);
            //canvas.Rectangle(rect3);
            canvas3.Stroke();

            Paragraph p4 = new Paragraph("" + Origen).SetFont(bold).SetFontSize(12).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER);
            PdfCanvas canvas4 = new PdfCanvas(pdf.GetFirstPage());
            iText.Kernel.Geom.Rectangle rect4 = new iText.Kernel.Geom.Rectangle(30, tercerFila, 110, 35);
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            new Canvas(canvas4, pdf, rect4)
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
                    .Add(p4);
            //canvas.Rectangle(rect4);
            canvas4.Stroke();

            Paragraph CantTotal = new Paragraph("" + CantidadTotal).SetFont(bold).SetHorizontalAlignment(HorizontalAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER);
            PdfCanvas canvasCantTotal = new PdfCanvas(pdf.GetFirstPage());
            iText.Kernel.Geom.Rectangle rectCantTotal = new iText.Kernel.Geom.Rectangle(145, cuartaFila, 130, 23);
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            new Canvas(canvasCantTotal, pdf, rectCantTotal)
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
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
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            new Canvas(canvasTabla1, pdf, rectTabla1)
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
            .Add(tabla);
            //canvasTabla1.Rectangle(rectTabla1);
            canvasTabla1.Stroke();

            PdfCanvas canvasTabla2 = new PdfCanvas(pdf.GetFirstPage());
            iText.Kernel.Geom.Rectangle rectTabla2 = new iText.Kernel.Geom.Rectangle(145, quintaFila, 135, 99);
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            new Canvas(canvasTabla2, pdf, rectTabla2)
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
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

#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                Canvas canvas = new Canvas(pdfCanvas, pdfDoc, page.GetPageSize());
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
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
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
                new Canvas(canvas, pdfDoc, new iText.Kernel.Geom.Rectangle(37, -3, page.GetPageSize().GetRight() - x, y)).

#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
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
        public ActionResult InformePackingListBackup(int PackingId)
        {
            int totalUnidades = 0;
            decimal totalVolumen = 0;
            decimal totalPBruto = 0;
            decimal totalPNeto = 0;
            daoPackilist = new DaoPackingList();

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

            var detalleGeneralPackingList = daoPackilist.ConsultarDetalleGeneralPackingList(PackingId);
            var pallet = daoPackilist.ConsultarPalletPackingCompleto(PackingId);

            foreach (var x in detalleGeneralPackingList) {
          
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

            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
            PdfFont No_Bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);


            MemoryStream stream = new MemoryStream();
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf, PageSize.A4, true);

            LineSeparator ls = new LineSeparator(new SolidLine());


            var path = System.IO.Path.Combine(Server.MapPath("~/Images/dacaritoo.png"));

            iText.Layout.Element.Image BackPack = new iText.Layout.Element.Image(ImageDataFactory.Create(path)).SetOpacity(0.1f);


                Paragraph header = new Paragraph("PACKING LIST")
               .SetTextAlignment(TextAlignment.CENTER)
               .SetFontSize(18).SetFontColor(ColorConstants.BLACK).SetFont(bold);
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);

            List list = new List().SetSymbolIndent(12)
           .SetFont(font);


            Paragraph Espacio = new Paragraph("  ").SetTextAlignment(TextAlignment.CENTER)
              .SetFontSize(12).SetFontColor(ColorConstants.BLACK);

            document.Add(header);
            document.Add(ls);

            float[] columnWidth = { 80f, 190f, 104f, 150f };
            Table tabla = new Table(columnWidth);

            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("TO:").SetFontSize(8)).SetBold().SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + nombreCliente).SetFontSize(8)).SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("DATE:").SetFontSize(8)).SetBold().SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + fecha).SetFontSize(8)).SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("INVOICE:").SetFontSize(8)).SetBold().SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + factura).SetFontSize(8)).SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("CONTAINER:").SetFontSize(8)).SetBold().SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + contenedor).SetFontSize(8)).SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("P.O:").SetFontSize(8)).SetBold().SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + pedido).SetFontSize(8)).SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("BOOKING:").SetFontSize(8)).SetBold().SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + reserva).SetFontSize(8)).SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("VESSEL:").SetFontSize(8)).SetBold().SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + embarcacion).SetFontSize(8)).SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("INTERCHANGE EIR:").SetFontSize(8)).SetBold().SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + intercambioEir).SetFontSize(8)).SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("REFERENCE:").SetFontSize(8)).SetBold().SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + referencia).SetFontSize(8)).SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("PRODUCT:").SetFontSize(8)).SetBold().SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + productos).SetFontSize(8)).SetHeight(16f).SetBorder(Border.NO_BORDER));

            Table table2 = new Table(UnitValue.CreatePercentArray(new float[] { 1, 2, 2, 2, 1, 2, 2 }));
         
            foreach (var x in pallet) {

                Cell cell = new Cell(2, 1).Add(new Paragraph("Pallet #")).SetBold().SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
                table2.AddCell(cell);

                cell = new Cell(1, 3).Add(new Paragraph("MEASUREMENT (m)")).SetBold().SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
                table2.AddCell(cell);

                cell = new Cell(2, 1).Add(new Paragraph("VOLUME(m³)")).SetBold().SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
                table2.AddCell(cell);

                cell = new Cell(1, 2).Add(new Paragraph("WEIGHT (Kg)")).SetBold().SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
                table2.AddCell(cell);

                table2.AddCell(new Paragraph("LONG").SetBold())/*.SetBold()*/.SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
                table2.AddCell(new Paragraph("WIDTH").SetBold())/*.SetBold()*/.SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
                table2.AddCell(new Paragraph("HIGH").SetBold())/*.SetBold()*/.SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
                table2.AddCell(new Paragraph("GROSS").SetBold())/*.SetBold()*/.SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
                table2.AddCell(new Paragraph("NET ").SetBold())/*.SetBold()*/.SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);

                table2.AddCell(new Paragraph(""+x.PalletNumber)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
                table2.AddCell(new Paragraph("" + x.LargoPallet)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
                table2.AddCell(new Paragraph("" + x.AnchoPallet)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
                table2.AddCell(new Paragraph("" + x.AltoPallet)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
                table2.AddCell(new Paragraph("" + x.VolumenPallet)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
                table2.AddCell(new Paragraph("" + x.PesoBruto)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
                table2.AddCell(new Paragraph("" + x.PesoNeto)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
       
                var palletDetalle = daoPackilist.ConsultarPalletPalletDetalleItemsCompleto(PackingId, x.PalletPacking1);

                Cell cell2 = new Cell(1, 4).Add(new Paragraph("DESCRIPTION")).SetBold().SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8)/*.SetBackgroundColor(ColorConstants.BLUE)*/;
                table2.AddCell(cell2);
                cell2 = new Cell(1, 3).Add(new Paragraph("QUANTITY")).SetBold().SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8)/*.SetBackgroundColor(ColorConstants.BLUE)*/;
                table2.AddCell(cell2);

                int totalUnidadesPallet = 0;
                decimal totalVolumenPallet = 0;
                decimal totalPBrutoPallet = 0;
                decimal totalPNetoPallet = 0;

                foreach (var y in palletDetalle)
                {
                    cell2 = new Cell(1, 4).Add(new Paragraph(""+y.DescriptionCode)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
                    table2.AddCell(cell2);
                    cell2 = new Cell(1, 3).Add(new Paragraph("" + y.CantidadItem)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
                    table2.AddCell(cell2);
                    totalUnidades = totalUnidades + Convert.ToInt32(y.CantidadItem);
                    totalUnidadesPallet=totalUnidadesPallet + Convert.ToInt32(y.CantidadItem);
                }
                totalVolumenPallet = totalVolumenPallet + Convert.ToDecimal(x.VolumenPallet);
                totalPBrutoPallet = totalPBrutoPallet + Convert.ToDecimal(x.PesoBruto);
                totalPNetoPallet = totalPNetoPallet + Convert.ToDecimal(x.PesoNeto);

                //Cell cellTotalsPallets = new Cell();

                cell2 = new Cell(1, 4).Add(new Paragraph("TOTAL QUANTITY PALLET")).SetBold().SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8)/*.SetBackgroundColor(ColorConstants.BLUE)*/;
                table2.AddCell(cell2);
                cell2 = new Cell(1, 3).Add(new Paragraph(""+ totalUnidadesPallet)).SetBold().SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8)/*.SetBackgroundColor(ColorConstants.BLUE)*/;
                table2.AddCell(cell2);
           
                totalVolumen = totalVolumen + Convert.ToDecimal(x.VolumenPallet);
                totalPBruto = totalPBruto+ Convert.ToDecimal(x.PesoBruto);
                totalPNeto = totalPNeto+ Convert.ToDecimal(x.PesoNeto);
            }
            float[] columnWidth3 = { 80f, 190f, 104f, 150f };

            Table tabla3 = new Table(columnWidth);

            Cell cell3 = new Cell(1, 7).Add(new Paragraph("TOTALS PACKING LIST")).SetBold().SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetKeepTogether(true).SetFontSize(8);
            tabla3.AddHeaderCell(cell3);

            cell3 = new Cell(1, 1).Add(new Paragraph("Units")).SetBold().SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetKeepTogether(true).SetFontSize(8)/*.SetBackgroundColor(ColorConstants.BLUE)*/;
            tabla3.AddHeaderCell(cell3);
            cell3 = new Cell(1, 1).Add(new Paragraph("Volume m³")).SetBold().SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetKeepTogether(true).SetFontSize(8)/*.SetBackgroundColor(ColorConstants.BLUE)*/;
            tabla3.AddHeaderCell(cell3);
            cell3 = new Cell(1, 1).Add(new Paragraph("WEIGHT GROSS kg")).SetBold().SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetKeepTogether(true).SetFontSize(8)/*.SetBackgroundColor(ColorConstants.BLUE)*/;
            tabla3.AddHeaderCell(cell3);
            cell3 = new Cell(1, 1).Add(new Paragraph("WEIGHT NET kg")).SetBold().SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetKeepTogether(true).SetFontSize(8)/*.SetBackgroundColor(ColorConstants.BLUE)*/;
            tabla3.AddHeaderCell(cell3);

            cell3 = new Cell(1, 1).Add(new Paragraph("" + totalUnidades + " Uni.")).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetKeepTogether(true).SetFontSize(8)/*.SetBackgroundColor(ColorConstants.BLUE)*/;
            tabla3.AddCell(cell3);
            cell3 = new Cell(1, 1).Add(new Paragraph("" + totalVolumen + "m³.")).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetKeepTogether(true).SetFontSize(8)/*.SetBackgroundColor(ColorConstants.BLUE)*/;
            tabla3.AddCell(cell3);
            cell3 = new Cell(1, 1).Add(new Paragraph("" + totalPBruto + "kg.")).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetKeepTogether(true).SetFontSize(8)/*.SetBackgroundColor(ColorConstants.BLUE)*/;
            tabla3.AddCell(cell3);
            cell3 = new Cell(1, 1).Add(new Paragraph("" + totalPNeto + "kg.")).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetKeepTogether(true).SetFontSize(8)/*.SetBackgroundColor(ColorConstants.BLUE)*/;
            tabla3.AddCell(cell3);

            document.Add(tabla).SetBorder(Border.NO_BORDER);
            document.Add(table2);
            document.Add(Espacio);
            document.Add(tabla3);

            document.Close();

            byte[] bytesStreams = stream.ToArray();
            stream = new MemoryStream();
            stream.Write(bytesStreams, 0, bytesStreams.Length);
            stream.Position = 0;
            return new FileStreamResult(stream, "application/pdf");
        }
        [HttpGet]
        public ActionResult InformePackingList(int PackingId)
        {
            int totalUnidades = 0;
            decimal totalVolumen = 0;
            decimal totalPBruto = 0;
            decimal totalPNeto = 0;
            daoPackilist = new DaoPackingList();

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

            var detalleGeneralPackingList = daoPackilist.ConsultarDetalleGeneralPackingList(PackingId);
            var pallet = daoPackilist.ConsultarPalletPackingCompleto(PackingId);

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

            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
            PdfFont No_Bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);


            MemoryStream stream = new MemoryStream();
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf, PageSize.A4, true);

            LineSeparator ls = new LineSeparator(new SolidLine());


            var path = System.IO.Path.Combine(Server.MapPath("~/Images/dacaritoo.png"));

            iText.Layout.Element.Image BackPack = new iText.Layout.Element.Image(ImageDataFactory.Create(path)).SetOpacity(0.1f);


            Paragraph header = new Paragraph("PACKING LIST")
           .SetTextAlignment(TextAlignment.CENTER)
           .SetFontSize(16).SetFontColor(ColorConstants.BLACK).SetFont(bold);
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);

            List list = new List().SetSymbolIndent(12)
           .SetFont(font);


            Paragraph Espacio = new Paragraph("  ").SetTextAlignment(TextAlignment.CENTER)
              .SetFontSize(12).SetFontColor(ColorConstants.BLACK);

            document.Add(header);
            document.Add(ls);

            float[] columnWidth = { 80f, 190f, 104f, 150f };
            Table tabla = new Table(columnWidth);

            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("TO:").SetFontSize(8)).SetBold().SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + nombreCliente).SetFontSize(8)).SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("DATE:").SetFontSize(8)).SetBold().SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + fecha).SetFontSize(8)).SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("INVOICE:").SetFontSize(8)).SetBold().SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + factura).SetFontSize(8)).SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("CONTAINER:").SetFontSize(8)).SetBold().SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + contenedor).SetFontSize(8)).SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("P.O:").SetFontSize(8)).SetBold().SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + pedido).SetFontSize(8)).SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("BOOKING:").SetFontSize(8)).SetBold().SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + reserva).SetFontSize(8)).SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("VESSEL:").SetFontSize(8)).SetBold().SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + embarcacion).SetFontSize(8)).SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("INTERCHANGE EIR:").SetFontSize(8)).SetBold().SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + intercambioEir).SetFontSize(8)).SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("REFERENCE:").SetFontSize(8)).SetBold().SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + referencia).SetFontSize(8)).SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("PRODUCT:").SetFontSize(8)).SetBold().SetHeight(16f).SetBorder(Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + productos).SetFontSize(8)).SetHeight(16f).SetBorder(Border.NO_BORDER));

            float[] columnWidth2 = { 52f, 52f, 52f, 52f, 52f, 52f, 52f, 52f, 52f, 52f };

            Table tabla2 = new Table(columnWidth2);
            Cell cell = new Cell(2, 1)
           .SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER)
           .Add(new Paragraph("Pallet #").SetFontSize(8)).SetBold().SetHeight(16f);
            tabla2.AddCell(cell);
            cell= new Cell(2,2)
            .SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER)
           .Add(new Paragraph("Description").SetFontSize(8)).SetBold().SetHeight(16f);
            tabla2.AddCell(cell);
            cell = new Cell(2, 1)
           .SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER)
          .Add(new Paragraph("Quantity").SetFontSize(8)).SetBold().SetHeight(16f);
            tabla2.AddCell(cell);
            cell = new Cell(1, 3)
           .SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER)
          .Add(new Paragraph("Measurement(m)").SetFontSize(8)).SetBold().SetHeight(16f);
            tabla2.AddCell(cell);
            cell = new Cell(2, 1)
          .SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER)
         .Add(new Paragraph("Volume(m³)").SetFontSize(8)).SetBold().SetHeight(16f);
            tabla2.AddCell(cell);
            cell = new Cell(1, 2)
          .SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER)
         .Add(new Paragraph("Weight(Kg)").SetFontSize(8)).SetBold().SetHeight(16f);
            tabla2.AddCell(cell);
            tabla2.AddCell(new Cell().Add(new Paragraph("Long").SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER));
            tabla2.AddCell(new Cell().Add(new Paragraph("Width").SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER));
            tabla2.AddCell(new Cell().Add(new Paragraph("High").SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER));
            tabla2.AddCell(new Cell().Add(new Paragraph("Gross").SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER));
            tabla2.AddCell(new Cell().Add(new Paragraph("Net").SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER));


            float[] columnWidth3 = { 52f, 52f, 52f, 52f, 52f, 52f, 52f, 52f, 52f, 52f };

            Table tabla3 = new Table(columnWidth3);

          
            foreach (var x in pallet){
                var palletDetalle = daoPackilist.ConsultarPalletPalletDetalleItemsCompleto(PackingId, x.PalletPacking1);
                totalVolumen = totalVolumen + x.VolumenPallet.Value;
                totalPBruto = totalPBruto + x.PesoBruto.Value;
                totalPNeto = totalPNeto + x.PesoNeto.Value;

                if (palletDetalle.Count == 1)
                {
                    Console.WriteLine("Entro x verdadero y El pallet contiene" + pallet.Count);
                    foreach (var y in palletDetalle){
                        tabla2.AddCell(new Cell().Add(new Paragraph("" + x.PalletNumber).SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER));
                        tabla2.AddCell(new Cell(1,2).Add(new Paragraph("" + y.DescriptionCode).SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER));
                        tabla2.AddCell(new Cell().Add(new Paragraph("" + y.CantidadItem).SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER));
                        tabla2.AddCell(new Cell().Add(new Paragraph("" + x.LargoPallet).SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER));
                        tabla2.AddCell(new Cell().Add(new Paragraph("" + x.AnchoPallet).SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER));
                        tabla2.AddCell(new Cell().Add(new Paragraph("" + x.AltoPallet).SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER));
                        tabla2.AddCell(new Cell().Add(new Paragraph("" + x.VolumenPallet).SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER));
                        tabla2.AddCell(new Cell().Add(new Paragraph("" + x.PesoBruto).SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER));
                        tabla2.AddCell(new Cell().Add(new Paragraph("" + x.PesoNeto).SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER));
                        totalUnidades = totalUnidades + y.CantidadItem.Value;
                     
                    }
                }
                else
                {
                    Console.WriteLine("entro x falso y El pallet contiene" + pallet.Count);

                    int i = 1;
                    foreach (var y in palletDetalle)
                    {
                        int contador = palletDetalle.Count;

                        if (i==1)
                        {
                            tabla2.AddCell(new Cell(contador,1).Add(new Paragraph("" + x.PalletNumber).SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE));
                            tabla2.AddCell(new Cell(1, 2).Add(new Paragraph("" + y.DescriptionCode).SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE));
                            tabla2.AddCell(new Cell(1, 1).Add(new Paragraph("" + y.CantidadItem).SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE));
                            tabla2.AddCell(new Cell(contador, 1).Add(new Paragraph("" + x.LargoPallet).SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE));
                            tabla2.AddCell(new Cell(contador, 1).Add(new Paragraph("" + x.AnchoPallet).SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE));
                            tabla2.AddCell(new Cell(contador, 1).Add(new Paragraph("" + x.AltoPallet).SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE));
                            tabla2.AddCell(new Cell(contador, 1).Add(new Paragraph("" + x.VolumenPallet).SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE));
                            tabla2.AddCell(new Cell(contador, 1).Add(new Paragraph("" + x.PesoBruto).SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE));
                            tabla2.AddCell(new Cell(contador, 1).Add(new Paragraph("" + x.PesoNeto).SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE));
                        }
                        if (i > 1) {
                            tabla2.AddCell(new Cell(1, 2).Add(new Paragraph("" + y.DescriptionCode).SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE));
                            tabla2.AddCell(new Cell(1, 1).Add(new Paragraph("" + y.CantidadItem).SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE));
                        }
                        i = i + 1;
                        totalUnidades = totalUnidades + y.CantidadItem.Value;
                      
                    }
                }
            }
            tabla2.AddCell(new Cell(1, 3).Add(new Paragraph("Totals").SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE));
            tabla2.AddCell(new Cell(1, 1).Add(new Paragraph(""+ totalUnidades).SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE));
            tabla2.AddCell(new Cell(1, 3).Add(new Paragraph("").SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE));
            tabla2.AddCell(new Cell(1, 1).Add(new Paragraph("" + totalVolumen+(" kg")).SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE));
            tabla2.AddCell(new Cell(1, 1).Add(new Paragraph("" + totalPBruto + (" kg")).SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE));
            tabla2.AddCell(new Cell(1, 1).Add(new Paragraph("" + totalPNeto + (" kg")).SetFontSize(8)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetVerticalAlignment(VerticalAlignment.MIDDLE));



            //Table table2 = new Table(UnitValue.CreatePercentArray(new float[] { 1, 2, 2, 2, 1, 2, 2 }));

            //foreach (var x in pallet)
            //{

            //    Cell cell = new Cell(2, 1).Add(new Paragraph("Pallet #")).SetBold().SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
            //    table2.AddCell(cell);

            //    cell = new Cell(1, 3).Add(new Paragraph("MEASUREMENT (m)")).SetBold().SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
            //    table2.AddCell(cell);

            //    cell = new Cell(2, 1).Add(new Paragraph("VOLUME(m³)")).SetBold().SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
            //    table2.AddCell(cell);

            //    cell = new Cell(1, 2).Add(new Paragraph("WEIGHT (Kg)")).SetBold().SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
            //    table2.AddCell(cell);

            //    table2.AddCell(new Paragraph("LONG").SetBold())/*.SetBold()*/.SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
            //    table2.AddCell(new Paragraph("WIDTH").SetBold())/*.SetBold()*/.SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
            //    table2.AddCell(new Paragraph("HIGH").SetBold())/*.SetBold()*/.SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
            //    table2.AddCell(new Paragraph("GROSS").SetBold())/*.SetBold()*/.SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
            //    table2.AddCell(new Paragraph("NET ").SetBold())/*.SetBold()*/.SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);

            //    table2.AddCell(new Paragraph("" + x.PalletNumber)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
            //    table2.AddCell(new Paragraph("" + x.LargoPallet)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
            //    table2.AddCell(new Paragraph("" + x.AnchoPallet)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
            //    table2.AddCell(new Paragraph("" + x.AltoPallet)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
            //    table2.AddCell(new Paragraph("" + x.VolumenPallet)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
            //    table2.AddCell(new Paragraph("" + x.PesoBruto)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
            //    table2.AddCell(new Paragraph("" + x.PesoNeto)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);

            //    var palletDetalle = daoPackilist.ConsultarPalletPalletDetalleItemsCompleto(PackingId, x.PalletPacking1);

            //    Cell cell2 = new Cell(1, 4).Add(new Paragraph("DESCRIPTION")).SetBold().SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8)/*.SetBackgroundColor(ColorConstants.BLUE)*/;
            //    table2.AddCell(cell2);
            //    cell2 = new Cell(1, 3).Add(new Paragraph("QUANTITY")).SetBold().SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8)/*.SetBackgroundColor(ColorConstants.BLUE)*/;
            //    table2.AddCell(cell2);

            //    int totalUnidadesPallet = 0;
            //    decimal totalVolumenPallet = 0;
            //    decimal totalPBrutoPallet = 0;
            //    decimal totalPNetoPallet = 0;

            //    foreach (var y in palletDetalle)
            //    {
            //        cell2 = new Cell(1, 4).Add(new Paragraph("" + y.DescriptionCode)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
            //        table2.AddCell(cell2);
            //        cell2 = new Cell(1, 3).Add(new Paragraph("" + y.CantidadItem)).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8);
            //        table2.AddCell(cell2);
            //        totalUnidades = totalUnidades + Convert.ToInt32(y.CantidadItem);
            //        totalUnidadesPallet = totalUnidadesPallet + Convert.ToInt32(y.CantidadItem);
            //    }
            //    totalVolumenPallet = totalVolumenPallet + Convert.ToDecimal(x.VolumenPallet);
            //    totalPBrutoPallet = totalPBrutoPallet + Convert.ToDecimal(x.PesoBruto);
            //    totalPNetoPallet = totalPNetoPallet + Convert.ToDecimal(x.PesoNeto);

            //    //Cell cellTotalsPallets = new Cell();

            //    cell2 = new Cell(1, 4).Add(new Paragraph("TOTAL QUANTITY PALLET")).SetBold().SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8)/*.SetBackgroundColor(ColorConstants.BLUE)*/;
            //    table2.AddCell(cell2);
            //    cell2 = new Cell(1, 3).Add(new Paragraph("" + totalUnidadesPallet)).SetBold().SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetFontSize(8)/*.SetBackgroundColor(ColorConstants.BLUE)*/;
            //    table2.AddCell(cell2);

            //    totalVolumen = totalVolumen + Convert.ToDecimal(x.VolumenPallet);
            //    totalPBruto = totalPBruto + Convert.ToDecimal(x.PesoBruto);
            //    totalPNeto = totalPNeto + Convert.ToDecimal(x.PesoNeto);
            //}
            //float[] columnWidth3 = { 80f, 190f, 104f, 150f };

            //Table tabla3 = new Table(columnWidth);

            //Cell cell3 = new Cell(1, 7).Add(new Paragraph("TOTALS PACKING LIST")).SetBold().SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetKeepTogether(true).SetFontSize(8);
            //tabla3.AddHeaderCell(cell3);

            //cell3 = new Cell(1, 1).Add(new Paragraph("Units")).SetBold().SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetKeepTogether(true).SetFontSize(8)/*.SetBackgroundColor(ColorConstants.BLUE)*/;
            //tabla3.AddHeaderCell(cell3);
            //cell3 = new Cell(1, 1).Add(new Paragraph("Volume m³")).SetBold().SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetKeepTogether(true).SetFontSize(8)/*.SetBackgroundColor(ColorConstants.BLUE)*/;
            //tabla3.AddHeaderCell(cell3);
            //cell3 = new Cell(1, 1).Add(new Paragraph("WEIGHT GROSS kg")).SetBold().SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetKeepTogether(true).SetFontSize(8)/*.SetBackgroundColor(ColorConstants.BLUE)*/;
            //tabla3.AddHeaderCell(cell3);
            //cell3 = new Cell(1, 1).Add(new Paragraph("WEIGHT NET kg")).SetBold().SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetKeepTogether(true).SetFontSize(8)/*.SetBackgroundColor(ColorConstants.BLUE)*/;
            //tabla3.AddHeaderCell(cell3);

            //cell3 = new Cell(1, 1).Add(new Paragraph("" + totalUnidades + " Uni.")).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetKeepTogether(true).SetFontSize(8)/*.SetBackgroundColor(ColorConstants.BLUE)*/;
            //tabla3.AddCell(cell3);
            //cell3 = new Cell(1, 1).Add(new Paragraph("" + totalVolumen + "m³.")).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetKeepTogether(true).SetFontSize(8)/*.SetBackgroundColor(ColorConstants.BLUE)*/;
            //tabla3.AddCell(cell3);
            //cell3 = new Cell(1, 1).Add(new Paragraph("" + totalPBruto + "kg.")).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetKeepTogether(true).SetFontSize(8)/*.SetBackgroundColor(ColorConstants.BLUE)*/;
            //tabla3.AddCell(cell3);
            //cell3 = new Cell(1, 1).Add(new Paragraph("" + totalPNeto + "kg.")).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetTextAlignment(TextAlignment.CENTER).SetKeepTogether(true).SetFontSize(8)/*.SetBackgroundColor(ColorConstants.BLUE)*/;
            //tabla3.AddCell(cell3);

            document.Add(tabla).SetBorder(Border.NO_BORDER);
            //document.Add(table2);
            document.Add(Espacio);
            document.Add(tabla2);
            document.Add(tabla3);

            document.Close();

            byte[] bytesStreams = stream.ToArray();
            stream = new MemoryStream();
            stream.Write(bytesStreams, 0, bytesStreams.Length);
            stream.Position = 0;
            return new FileStreamResult(stream, "application/pdf");
        }
    }
}