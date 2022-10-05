using DacarDatos.Datos;
using DacarProsoft.Datos;
using DacarProsoft.Models;
using iText.Forms.Fields;
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
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using iText.IO.Font;
using iText.Kernel.Pdf.Xobject;

namespace DacarProsoft.Controllers
{
    public class CalidadController : Controller
    {
        private DaoUtilitarios daoUtilitarios { get; set; } = null;
        private DaoCalidad daoCalidad { get; set; } = null;
        private DaoOrdenesVentas daoOrdenesVentas { get; set; } = null;
        private DaoPackingList daoPackingList { get; set; } = null;


        // GET: Calidad
        public ActionResult RegistrosVisuaLCN()
        {
            if (Session["usuario"] != null)
            {
                ViewBag.JavaScript = "General/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
                ViewBag.dxdevweb = "1";

                ViewBag.MenuAcceso = Session["Menu"];

                //ConexionAccess conexion = new ConexionAccess();

                daoCalidad = new DaoCalidad();
                daoUtilitarios = new DaoUtilitarios();

                var datMenu = daoUtilitarios.ConsultarMenuPrincipal();
                ViewBag.MenuPrincipal = datMenu;
                var datMenuOpciones = daoUtilitarios.ConsultarMenuOpciones();
                ViewBag.MenuOpciones = datMenuOpciones;
                var datSubMenuOpciones = daoUtilitarios.ConsultarSubMenuOpciones();
                ViewBag.SubMenuOpciones = datSubMenuOpciones;
                var dat = daoUtilitarios.ConsultarEstadosDeOrdenesProduccion();
                ViewBag.EstadoOrden = dat;

                var baseDatos = daoCalidad.ConsultarBaseDeDatos();
                ViewBag.BaseDatos = baseDatos;

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public JsonResult ConsultarRegistrosLcn(string nombre)
        {
            try
            {
                ConexionAccess conexion = new ConexionAccess();
                var conexionAcc = conexion.open(nombre);

                if (conexionAcc == true)
                {

                    var result = conexion.ConsultarFechasBaseDeDatos();
                    conexion.close();
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ConsultarDetalleRegistrosLcn(string nombreBase, int testUnique)
        {
            try
            {
                ConexionAccess conexion = new ConexionAccess();
                var conexionAcc = conexion.open(nombreBase);

                if (conexionAcc == true)
                {

                    var result = conexion.ConsultarDetalleRegistrosLcnAccess(testUnique);
                    conexion.close();
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public ActionResult IngresosPruebasLaboratorio()
        {
            if (Session["usuario"] != null)
            {
                ViewBag.JavaScript = "General/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
                ViewBag.dxdevweb = "1";

                ViewBag.MenuAcceso = Session["Menu"];

                //ConexionAccess conexion = new ConexionAccess();

                daoUtilitarios = new DaoUtilitarios();
                daoCalidad = new DaoCalidad();

                var datModelosMarcasPropias = daoCalidad.ConsultarModelosMarcasPropias();

                var datMarcas = daoCalidad.MarcaBateria();
                var datTipoNorma = daoCalidad.TipoNorma();
                var Normativa = daoCalidad.Normativa();
                var datSeparador = daoCalidad.Separador();
                var datTipoEnsayo = daoCalidad.TipoEnsayo();

                var datMenu = daoUtilitarios.ConsultarMenuPrincipal();
                ViewBag.MenuPrincipal = datMenu;
                var datMenuOpciones = daoUtilitarios.ConsultarMenuOpciones();
                ViewBag.MenuOpciones = datMenuOpciones;
                var datSubMenuOpciones = daoUtilitarios.ConsultarSubMenuOpciones();
                ViewBag.SubMenuOpciones = datSubMenuOpciones;

                ViewBag.MarcasPropias = datModelosMarcasPropias;

                ViewBag.datMarcas = datMarcas;
                ViewBag.datTipoNorma = datTipoNorma;
                ViewBag.Normativa = Normativa;
                ViewBag.datSeparador = datSeparador;
                ViewBag.datTipoEnsayo = datTipoEnsayo;

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public JsonResult ConsultarModelosBateriasPorTipoVehiculo(int id)
        {
            daoCalidad = new DaoCalidad();

            var cantones = daoCalidad.ModelosBateriasPorTipoVehiculo(id.ToString());
            return Json(new SelectList(cantones, "Value", "Text"));
        }
        public bool RegistrarPruebasLaboratorio(DateTime FechaIngreso, int CodigoIngreso, string Marca, string TipoNorma, string Normativa, string PreAcondicionamiento, string TipoBateria, string Modelo, string Separador, string TipoEnsayo, string LoteEnsamble,
            string LoteCarga, int CCA, decimal Peso, decimal Voltaje, decimal DensidadIngreso,/* decimal DensidadPreAcondicionamiento,*/ decimal TemperaturaIngreso, decimal TemperaturaPrueba, string DatoTeoricoPrueba/*, decimal ValorObjetivo*/, decimal ResultadoFinal,
            string Observaciones, decimal Calificacion, HttpPostedFileBase[] archivos, int CodigoBateria)
        {
            try
            {
                daoCalidad = new DaoCalidad();
                var result = daoCalidad.IngresarPruebaLaboratorio(FechaIngreso, CodigoIngreso, Marca, TipoNorma, Normativa, PreAcondicionamiento, TipoBateria, Modelo, Separador, TipoEnsayo, LoteEnsamble,
                LoteCarga, CCA, Peso, Voltaje, DensidadIngreso, 0, TemperaturaIngreso, TemperaturaPrueba, DatoTeoricoPrueba, 0, ResultadoFinal,
                Observaciones, Calificacion, CodigoBateria);

                string path = Path.Combine(Server.MapPath("~/Images/AnexosLaboratorio/" + Modelo + "/"));

                if (archivos != null) {
                    if (Directory.Exists(path))
                    {
                        Console.WriteLine("El directorio existe");

                        string path2 = path + "/" + DateTime.Now.Year + "-" + DateTime.Now.Month.ToString("d2") + "-" + DateTime.Now.Day + "/";
                        DirectoryInfo di = Directory.CreateDirectory(path2);

                        if (Directory.Exists(path2))
                        {
                            int i = 0;
                            foreach (var x in archivos)
                            {
                                string filename = Path.GetExtension(archivos[i].FileName);
                                string nombreArchivo = Path.GetFileNameWithoutExtension(archivos[i].FileName);
                                archivos[i].SaveAs(path2 + result + "-" + nombreArchivo + filename);
                                i = i + 1;
                            }
                        }
                        else
                        {
                            Directory.CreateDirectory(path2);
                            int i = 0;
                            foreach (var x in archivos)
                            {
                                string filename = Path.GetExtension(archivos[i].FileName);
                                string nombreArchivo = Path.GetFileNameWithoutExtension(archivos[i].FileName);
                                archivos[i].SaveAs(path2 + result + "-" + nombreArchivo + filename);
                                i = i + 1;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No existe el directorio");
                        string path2 = path + "/" + DateTime.Now.Year + "-" + DateTime.Now.Month.ToString("d2") + "-" + DateTime.Now.Day + "/";
                        Directory.CreateDirectory(path2);
                        int i = 0;
                        foreach (var x in archivos)
                        {
                            string filename = Path.GetExtension(archivos[i].FileName);
                            string nombreArchivo = Path.GetFileNameWithoutExtension(archivos[i].FileName);
                            archivos[i].SaveAs(path2 + result + "-" + nombreArchivo + filename);
                            i = i + 1;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
                return false;

            }
        }
        public ActionResult ConsultaPruebasLaboratorio()
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

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public JsonResult ConsultarRegistrosPruebasLaboratorio()
        {
            try
            {
                daoCalidad = new DaoCalidad();
                var result = daoCalidad.ConsultarPruebasLaboratorio();
                var json = Json(result, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = 50000000;
                return json;
                //return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public int ConsultarCodigoRegistroPrueba()
        {
            try
            {
                daoCalidad = new DaoCalidad();
                int result = daoCalidad.ObtenerCodigoIngresoPrueba();

                return result;
                //return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ConsultarAnexosRegistrados(int IdRegistro, string FechaRegistro, string modelo)
        {
            try
            {
                daoCalidad = new DaoCalidad();
                string fechaRegistro;
                string rutaAlt;
                string rutaSoft;
                DateTime fecha = Convert.ToDateTime(FechaRegistro, CultureInfo.InvariantCulture);
                fechaRegistro = fecha.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                List<SelectListItem> lst = new List<SelectListItem>();

                string path = Path.Combine(Server.MapPath("~/Images/AnexosLaboratorio/" + modelo + "/" + FechaRegistro + "/"));

                if (Directory.Exists(path))
                {
                    DirectoryInfo Dir2 = new DirectoryInfo(path);
                    rutaSoft = daoCalidad.ObtenerRutaSoftware();
                    rutaAlt = "/Images/AnexosLaboratorio/" + modelo + "/" + FechaRegistro + "/";

                    foreach (var file in Dir2.GetFiles("*", SearchOption.AllDirectories))
                    {
                        lst.Add(new SelectListItem
                        {
                            //Value = file.FullName,
                            Value = rutaSoft + rutaAlt + file.Name,
                            Text = System.IO.Path.GetFileName(file.Name)
                        });

                    }
                }
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public bool RegistrarNuevosAnexos(int PruebaLaboratorioCalidadId, string FechaRegistro, string modelo, HttpPostedFileBase[] archivos)
        {
            try
            {
                daoCalidad = new DaoCalidad();

                string path = Path.Combine(Server.MapPath("~/Images/AnexosLaboratorio/" + modelo + "/"));

                if (Directory.Exists(path))
                {
                    Console.WriteLine("El directorio existe");

                    string path2 = path + "/" + FechaRegistro + "/";
                    DirectoryInfo di = Directory.CreateDirectory(path2);

                    if (Directory.Exists(path2))
                    {
                        int i = 0;
                        foreach (var x in archivos)
                        {
                            string filename = Path.GetExtension(archivos[i].FileName);
                            string nombreArchivo = Path.GetFileNameWithoutExtension(archivos[i].FileName);
                            archivos[i].SaveAs(path2 + PruebaLaboratorioCalidadId + "-" + nombreArchivo + filename);
                            i = i + 1;
                        }
                    }
                    else
                    {
                        Directory.CreateDirectory(path2);
                        int i = 0;
                        foreach (var x in archivos)
                        {
                            string filename = Path.GetExtension(archivos[i].FileName);
                            string nombreArchivo = Path.GetFileNameWithoutExtension(archivos[i].FileName);
                            archivos[i].SaveAs(path2 + PruebaLaboratorioCalidadId + "-" + nombreArchivo + filename);
                            i = i + 1;
                        }

                    }
                }
                else
                {
                    Console.WriteLine("No existe el directorio");
                    string path2 = path + "/" + FechaRegistro + "/";
                    Directory.CreateDirectory(path2);
                    int i = 0;
                    foreach (var x in archivos)
                    {
                        string filename = Path.GetExtension(archivos[i].FileName);
                        string nombreArchivo = Path.GetFileNameWithoutExtension(archivos[i].FileName);
                        archivos[i].SaveAs(path2 + PruebaLaboratorioCalidadId + "-" + nombreArchivo + filename);
                        i = i + 1;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public String ConsultarValorTipoDePrueba(string modelo, int valor)
        {
            try
            {
                daoCalidad = new DaoCalidad();
                string result = "";
                if (valor == 1) {
                    result = daoCalidad.ObtenerCapBateria(modelo);
                }
                if (valor == 2)
                {
                    result = daoCalidad.ObtenerCCABateria(modelo);
                }
                if (valor == 3)
                {
                    result = daoCalidad.ObtenerRcBateria(modelo);
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public ActionResult GenerarPdfReporte(decimal Nominal, decimal NominalReal)
        {
            string valoview = Session["Grafico"].ToString();
            List<ModelPruebaLaboratorioCalidad> lst = new List<ModelPruebaLaboratorioCalidad>();
            var valor = (List<ModelPruebaLaboratorioCalidad>) Session["Registros"];
            decimal CCA = 0;
            decimal Peso = 0;
            decimal Voltaje = 0;
            decimal ValorObjetivo = 0;
            decimal ResultadoFinal = 0;
            decimal Calificacion = 0;
            decimal DensidadPromedio = 0;
            int contador = 0;
            iText.Kernel.Colors.Color lineColor = new DeviceRgb(164, 164, 164);

            //Session["Registros"]
            //try
            //{
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
         
            Paragraph header = new Paragraph("ANÁLISIS PRUEBAS LABORATORIO ")
         .SetTextAlignment(TextAlignment.CENTER)
         .SetFontSize(16).SetBold();
            Paragraph TipoEnsayo = new Paragraph("Tipo de ensayo: " + valor[0].TipoEnsayo+" | "+ "Modelo de bateria: " + valor[0].Modelo)
    .SetTextAlignment(TextAlignment.CENTER)
    .SetFontSize(14);

            string tipEnsayo="";

            if (valor[0].TipoEnsayo=="C20") {
                tipEnsayo = "(AH)";
            }
            if (valor[0].TipoEnsayo == "CCA")
            {
                tipEnsayo = "(Amp.)";
            }
            if (valor[0].TipoEnsayo == "CICLOS")
            {
                tipEnsayo = "(Ciclos)";
            }
            if (valor[0].TipoEnsayo == "RC")
            {
                tipEnsayo = "(Minutos)";
            }

            var path = System.IO.Path.Combine(Server.MapPath("~/Images/HojaMembretada.jpg"));
            iText.Layout.Element.Image BackPack = new iText.Layout.Element.Image(ImageDataFactory.Create(path))/*.SetOpacity(0.1f)*/;
            pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new BackgroundPageEvent(BackPack));


            float[] columnWidth2 = { 60f ,55f, 55f, 55f, 65f, 55f, 105f, 55f, 55f };
            Table tabla2 = new Table(columnWidth2);

            tabla2.AddCell(new Cell(1, 9).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("REGISTROS DE PRUEBAS DE LABORATORIO").SetFontSize(10)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Fecha").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Código").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Norma").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Separador").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("L. Ensamble").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("L. Carga").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Pre-Acondicionamiento").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Voltaje").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Resultado"+ tipEnsayo).SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));

            foreach (var x in valor)
            {
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(""+x.FechaIngreso).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + x.CodigoIngreso).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + x.TipoNorma).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + x.Separador).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + x.LoteEnsamble).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + x.LoteCarga).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + x.PreAcondicionamiento).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + x.Voltaje).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + x.ResultadoFinal).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));

                if (x.CCA!=null) {
                    CCA = CCA + x.CCA.Value;
                }
                if (x.Peso!=null) {
                    Peso = Peso + x.Peso.Value;
                }
                if (x.Voltaje != null)
                {
                    Voltaje = Voltaje + x.Voltaje.Value;
                }
                if (x.ValorObjetivo != null)
                {
                    ValorObjetivo = ValorObjetivo + x.ValorObjetivo.Value;
                }
                if (x.ResultadoFinal != null)
                {
                    ResultadoFinal = ResultadoFinal + x.ResultadoFinal.Value;
                }
                if (x.Calificacion != null)
                {
                    Calificacion = Calificacion + x.Calificacion.Value;
                }
                if (x.DensidadIngreso != null)
                {
                    DensidadPromedio = DensidadPromedio + x.DensidadIngreso.Value;
                }

                contador = contador + 1;
            }
       
            if ((Calificacion / contador) > 90)
            {
                var pathVistoBueno = System.IO.Path.Combine(Server.MapPath("~/Images/Visto_Bueno.png"));
                iText.Layout.Element.Image VistoBueno = new iText.Layout.Element.Image(ImageDataFactory.Create(pathVistoBueno));
                //PdfFont font = PdfFontFactory.CreateFont(FONT, iText.IO.Font.PdfEncodings.IDENTITY_H);
                VistoBueno.SetFixedPosition(435, 585);
                ////img.SetFixedPosition(25, 100);
                VistoBueno.SetHeight(10);
                VistoBueno.SetWidth(10);
                document.Add(VistoBueno);

            }
            else {
                var pathVistoBueno = System.IO.Path.Combine(Server.MapPath("~/Images/xmal.png"));
                iText.Layout.Element.Image VistoBueno = new iText.Layout.Element.Image(ImageDataFactory.Create(pathVistoBueno));
                //PdfFont font = PdfFontFactory.CreateFont(FONT, iText.IO.Font.PdfEncodings.IDENTITY_H);
                VistoBueno.SetFixedPosition(435, 585);
                ////img.SetFixedPosition(25, 100);
                VistoBueno.SetHeight(10);
                VistoBueno.SetWidth(10);
                document.Add(VistoBueno);

            }

            float[] columnWidth = { 100f, 102f, 106f, 100f };
            Table tabla = new Table(columnWidth);

            tabla.AddCell(new Cell(1, 4).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("PROMEDIOS GENERALES").SetFontSize(10)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Peso(kg)").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Voltaje(OCV)").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("CCA(Electronic Tester)").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Densidad (gr/cm3)").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));


            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(""+Decimal.Round(Peso/contador,2)).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(""+Decimal.Round(Voltaje/contador,2)).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            if (CCA == 0)
            {
                tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("No Aplica").SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            }
            else
            {
                tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + Decimal.Round(CCA / contador, 2)).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            }
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(""+ Decimal.Round(DensidadPromedio /contador,3)).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));

            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Valor Nominal" + tipEnsayo).SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Valor Mínimo Aceptable").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));

            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Resultado Final" + tipEnsayo).SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Calificación").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(""+ NominalReal).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(""+ Nominal).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(""+Decimal.Round(ResultadoFinal / contador, 2)).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(""+Decimal.Round(Calificacion / contador, 1)+"%").SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));

            document.Add(header);
            document.Add(TipoEnsayo);
            document.Add(tabla.SetHorizontalAlignment(HorizontalAlignment.CENTER));
            document.Add(Espacio);
            document.Add(img);
            document.Add(Espacio);
            document.Add(tabla2.SetHorizontalAlignment(HorizontalAlignment.CENTER));

            document.Close();


            byte[] bytesStreams = stream.ToArray();
            stream = new MemoryStream();
            stream.Write(bytesStreams, 0, bytesStreams.Length);
            stream.Position = 0;

            return new FileStreamResult(stream, "application/pdf");
        }
        public string EnviarPdfReporte(decimal Nominal, decimal NominalReal, string Correo, string CorreoCopia)
        {
            try
            {
                daoUtilitarios = new DaoUtilitarios();
                string valoview = Session["Grafico"].ToString();
            List<ModelPruebaLaboratorioCalidad> lst = new List<ModelPruebaLaboratorioCalidad>();
            var valor = (List<ModelPruebaLaboratorioCalidad>)Session["Registros"];
            decimal CCA = 0;
            decimal Peso = 0;
            decimal Voltaje = 0;
            decimal ValorObjetivo = 0;
            decimal ResultadoFinal = 0;
            decimal Calificacion = 0;
            decimal DensidadPromedio = 0;

                int contador = 0;
                string DirCorreo="";
                string ClavCorreo="";
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

                Paragraph header = new Paragraph("ANÁLISIS PRUEBAS LABORATORIO ")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(16).SetBold();
                Paragraph TipoEnsayo = new Paragraph("Tipo de ensayo: " + valor[0].TipoEnsayo + " | " + "Modelo de bateria: " + valor[0].Modelo)
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(14);
                string tipEnsayo = "";

                if (valor[0].TipoEnsayo == "C20")
                {
                    tipEnsayo = "(AH)";
                }
                if (valor[0].TipoEnsayo == "CCA")
                {
                    tipEnsayo = "(Amp.)";
                }
                if (valor[0].TipoEnsayo == "CICLOS")
                {
                    tipEnsayo = "(Ciclos)";
                }
                if (valor[0].TipoEnsayo == "RC")
                {
                    tipEnsayo = "(Minutos)";
                }

                var path = System.IO.Path.Combine(Server.MapPath("~/Images/HojaMembretada.jpg"));
            iText.Layout.Element.Image BackPack = new iText.Layout.Element.Image(ImageDataFactory.Create(path))/*.SetOpacity(0.1f)*/;
            pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new BackgroundPageEvent(BackPack));

            float[] columnWidth2 = { 60f, 55f, 55f, 55f, 65f, 55f, 55f, 55f, 55f };
            Table tabla2 = new Table(columnWidth2);

            tabla2.AddCell(new Cell(1, 9).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("REGISTROS DE PRUEBAS DE LABORATORIO").SetFontSize(10)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Fecha").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Código").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Norma").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Separador").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("L. Ensamble").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("L. Carga").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("CCA").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Voltaje").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Resultado"+ tipEnsayo).SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));

            foreach (var x in valor)
            {
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + x.FechaIngreso).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + x.CodigoIngreso).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + x.TipoNorma).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + x.Separador).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + x.LoteEnsamble).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + x.LoteCarga).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + x.CCA).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + x.Voltaje).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + x.ResultadoFinal).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));

                if (x.CCA != null)
                {
                    CCA = CCA + x.CCA.Value;
                }
                if (x.Peso != null)
                {
                    Peso = Peso + x.Peso.Value;
                }
                if (x.Voltaje != null)
                {
                    Voltaje = Voltaje + x.Voltaje.Value;
                }
                if (x.ValorObjetivo != null)
                {
                    ValorObjetivo = ValorObjetivo + x.ValorObjetivo.Value;
                }
                if (x.ResultadoFinal != null)
                {
                    ResultadoFinal = ResultadoFinal + x.ResultadoFinal.Value;
                }
                if (x.Calificacion != null)
                {
                    Calificacion = Calificacion + x.Calificacion.Value;
                }
                    if (x.DensidadIngreso != null)
                    {
                        DensidadPromedio = DensidadPromedio + x.DensidadIngreso.Value;
                    }
                    contador = contador + 1;
            }
                if ((Calificacion / contador) > 90)
                {
                    var pathVistoBueno = System.IO.Path.Combine(Server.MapPath("~/Images/Visto_Bueno.png"));
                    iText.Layout.Element.Image VistoBueno = new iText.Layout.Element.Image(ImageDataFactory.Create(pathVistoBueno));
                    //PdfFont font = PdfFontFactory.CreateFont(FONT, iText.IO.Font.PdfEncodings.IDENTITY_H);
                    VistoBueno.SetFixedPosition(435, 585);
                    ////img.SetFixedPosition(25, 100);
                    VistoBueno.SetHeight(10);
                    VistoBueno.SetWidth(10);
                    document.Add(VistoBueno);
                }
                else
                {
                    var pathVistoBueno = System.IO.Path.Combine(Server.MapPath("~/Images/xmal.png"));
                    iText.Layout.Element.Image VistoBueno = new iText.Layout.Element.Image(ImageDataFactory.Create(pathVistoBueno));
                    //PdfFont font = PdfFontFactory.CreateFont(FONT, iText.IO.Font.PdfEncodings.IDENTITY_H);
                    VistoBueno.SetFixedPosition(435, 585);
                    ////img.SetFixedPosition(25, 100);
                    VistoBueno.SetHeight(10);
                    VistoBueno.SetWidth(10);
                    document.Add(VistoBueno);
                }
                float[] columnWidth = { 100f, 102f, 106f, 100f };
                Table tabla = new Table(columnWidth);

                tabla.AddCell(new Cell(1, 4).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("PROMEDIOS GENERALES").SetFontSize(10)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
                tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Peso(kg)").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Voltaje(OCV)").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("CCA(Electronic Tester)").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Densidad (gr/cm3)").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));


                tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + Decimal.Round(Peso / contador, 2)).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + Decimal.Round(Voltaje / contador, 2)).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                if (CCA == 0)
                {
                    tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("No Aplica").SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                }
                else
                {
                    tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + Decimal.Round(CCA / contador, 2)).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                }
                tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + Decimal.Round(DensidadPromedio / contador, 3)).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));

                tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Valor Nominal"+tipEnsayo).SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Valor Mínimo Aceptable").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));

                tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Resultado Final"+tipEnsayo).SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Calificación").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + NominalReal).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + Nominal).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + Decimal.Round(ResultadoFinal / contador, 2)).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + Decimal.Round(Calificacion / contador, 1) + "%").SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));



                document.Add(header);
                document.Add(TipoEnsayo);
            document.Add(tabla.SetHorizontalAlignment(HorizontalAlignment.CENTER));
            document.Add(Espacio);
            document.Add(img);
            document.Add(Espacio);
            document.Add(tabla2.SetHorizontalAlignment(HorizontalAlignment.CENTER));
            document.Close();

            byte[] bytesStreams = stream.ToArray();
            stream = new MemoryStream();
            stream.Write(bytesStreams, 0, bytesStreams.Length);
            stream.Position = 0;

            var CorreoBase = daoUtilitarios.ConsultarCorreoElectronico();
             

                foreach (var x in CorreoBase) {
                    DirCorreo = x.DireccionCorreo;
                    ClavCorreo = x.ClaveCorreo;
                }

                //foreach (var address in addresses.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                //{
                //    mailMessage.To.Add(address);
                //}

                MailMessage mm = new MailMessage("bateriasdacar1975@gmail.com", Correo);
            mm.Subject = "ANALISIS PRUEBAS LABORATORIO " + valor[0].TipoEnsayo;

                //MailAddress copy = new MailAddress("Notification_List@contoso.com");
            mm.CC.Add(CorreoCopia);

            mm.Body = "Resultado de ensayo de baterias "+ valor[0].Modelo+ ", con fecha: "+DateTime.Now;
            mm.Attachments.Add(new Attachment(new MemoryStream(bytesStreams), "PruebaLaboratorio"+ valor[0].Modelo+".pdf"));
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
                //throw new Exception("No se ha podido enviar el email", ex.InnerException);
                return ex.Message;
            }
        }

        public string GuardarViewBag(string chart, List<ModelPruebaLaboratorioCalidad> registros)
        {
            Session["Grafico"] = chart;
            Session["Registros"] = registros;

            return null;
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
        public ActionResult IngresosMedicionDeDescarga()
        {
            if (Session["usuario"] != null)
            {
                ViewBag.JavaScript = "General/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
                ViewBag.dxdevweb = "1";

                ViewBag.MenuAcceso = Session["Menu"];

                //ConexionAccess conexion = new ConexionAccess();

                daoUtilitarios = new DaoUtilitarios();
                daoCalidad = new DaoCalidad();

                var datModelosMarcasPropias = daoCalidad.ConsultarModelosMarcasPropias();

                var datMarcas = daoCalidad.MarcaBateria();
                var datTipoNorma = daoCalidad.TipoNorma();
                var Normativa = daoCalidad.Normativa();
                var datSeparador = daoCalidad.Separador();
                var datTipoEnsayo = daoCalidad.TipoEnsayo();

                var datMenu = daoUtilitarios.ConsultarMenuPrincipal();
                ViewBag.MenuPrincipal = datMenu;
                var datMenuOpciones = daoUtilitarios.ConsultarMenuOpciones();
                ViewBag.MenuOpciones = datMenuOpciones;
                var datSubMenuOpciones = daoUtilitarios.ConsultarSubMenuOpciones();
                ViewBag.SubMenuOpciones = datSubMenuOpciones;

                ViewBag.MarcasPropias = datModelosMarcasPropias;

                ViewBag.datMarcas = datMarcas;
                ViewBag.datTipoNorma = datTipoNorma;
                ViewBag.Normativa = Normativa;
                ViewBag.datSeparador = datSeparador;
                ViewBag.datTipoEnsayo = datTipoEnsayo;

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public int ConsultarCodigoRegistroMedicionDeDescarga()
        {
            try
            {
                daoCalidad = new DaoCalidad();
                int result = daoCalidad.ObtenerCodigoIngresoMedicionCarga();

                return result;
                //return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ConsultarRegistrosMedicionesDescarga()
        {
            try
            {
                daoCalidad = new DaoCalidad();
                var result = daoCalidad.ConsultarIngresosMedicionesVoltaje();
                var json = Json(result, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = 50000000;
                return json;
                //return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public bool RegistrarPruebasMedicionDescarga(DateTime FechaIngreso, int CodigoIngreso, string Marca, string PreAcondicionamiento, string TipoBateria, string Modelo, string Separador, string LoteEnsamble,
        string LoteCarga, decimal Peso, decimal Voltaje)
        {       
                daoCalidad = new DaoCalidad();
                var result = daoCalidad.IngresarPruebaMedicionBateria(FechaIngreso, CodigoIngreso, Marca, PreAcondicionamiento, TipoBateria, Modelo, Separador, LoteEnsamble,LoteCarga, Peso, Voltaje);
                var result2 = daoCalidad.IngresarDetalleMedicionBateria(result, FechaIngreso, Voltaje);
                return result2;
          
        }

        public bool RegistrarNuevaMedicionDescarga(int IdPruebaMedicionDescarga, DateTime FechaMedicion, decimal Voltaje)
        {
            daoCalidad = new DaoCalidad();
            var result = daoCalidad.IngresarDetalleMedicionBateria(IdPruebaMedicionDescarga, FechaMedicion, Voltaje);
            return result;

        }
        public ActionResult ConsultaMedicionDeDescarga()
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

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public JsonResult ConsultarRegistrosMedicionesDescargas()
        {
            try
            {
                daoCalidad = new DaoCalidad();
                var result = daoCalidad.ConsultarMedicionesDeDescargas();
                var json = Json(result, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = 50000000;
                return json;
                //return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ConsultarDetallesMedicionDescarga(int idMedicion)
        {
            try
            {
                daoCalidad = new DaoCalidad();
                var result = daoCalidad.ConsultarDetalleMedicionDescarga(idMedicion);
                var json = Json(result, JsonRequestBehavior.AllowGet);
                json.MaxJsonLength = 50000000;
                return json;
                //return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public ActionResult GenerarPdfReporteAutodescarga(int idMeidicionDescarga, string variable)
        {
            daoCalidad = new DaoCalidad();

            string valoview = Session["GraficoAutodescarga"].ToString();

            var MedicionDescarga = daoCalidad.ConsultarMedicionesDeDescargasPorId(idMeidicionDescarga);

            var MedicionDescargaDetalle = daoCalidad.ConsultarDetalleMedicionDescarga(idMeidicionDescarga);

            int ContadorMedicionDescargaDetalle = daoCalidad.ContarRegistrosMedicionesDeDescargas(idMeidicionDescarga);

            string modeloBateria = "";
            int codigo = 0;
            string lEnsamble = "";
            string lCarga = "";
            string preAcondicionamiento = "";
            decimal peso = 0;


            foreach (var x in MedicionDescarga) {
                modeloBateria = x.Modelo;
                codigo = x.CodigoIngreso.Value;
                lEnsamble = x.LoteEnsamble;
                lCarga = x.LoteCarga;
                preAcondicionamiento = x.PreAcondicionamiento;
                peso = x.Peso.Value;
            }


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

            Paragraph header = new Paragraph("PRUEBAS AUTODESCARGA")
         .SetTextAlignment(TextAlignment.CENTER)
         .SetFontSize(16).SetBold();
            Paragraph Modelo = new Paragraph("Modelo de bateria: " + modeloBateria)
        .SetTextAlignment(TextAlignment.CENTER)
        .SetFontSize(14);

            var path = System.IO.Path.Combine(Server.MapPath("~/Images/HojaMembretada.jpg"));
            iText.Layout.Element.Image BackPack = new iText.Layout.Element.Image(ImageDataFactory.Create(path))/*.SetOpacity(0.1f)*/;
            pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new BackgroundPageEvent(BackPack));


            float[] columnWidth2 = { 80f, 80f, 80f, 80f, 80f};
            Table tabla2 = new Table(columnWidth2);
            tabla2.AddCell(new Cell(1, 7).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("REGISTROS DE AUTODESCARGAS").SetFontSize(10)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Fecha").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Voltaje").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));

            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(Border.NO_BORDER));

            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Fecha").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Voltaje").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            int contador = 1;
            decimal voltajeMayor = 0;
            decimal voltajeMenor = 0;

            var ValorAlto = MedicionDescargaDetalle.OrderBy((v) => v.PruebaLaboratorioCalidadId).FirstOrDefault();
            var ValorBajo = MedicionDescargaDetalle.OrderByDescending((v) => v.PruebaLaboratorioCalidadId).FirstOrDefault();


            voltajeMenor = ValorBajo.Voltaje.Value;
            voltajeMayor = ValorAlto.Voltaje.Value;

            //var numeroMayor = MedicionDescargaDetalle.Min();
            foreach (var y in MedicionDescargaDetalle)
            {
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + y.FechaIngreso).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + y.Voltaje).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                if (contador % 2 == 1) {
                    tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("").SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(Border.NO_BORDER));
                }

                contador = contador + 1; ;
            }


            float[] columnWidth = {80f, 80f, 80f,80f };
            Table tabla = new Table(columnWidth);

            tabla.AddCell(new Cell(1, 4).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("TABLA RESUMEN").SetFontSize(10)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Codigo").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("L. Ensamble").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("L. Carga").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Pre-Acondicionamiento").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));


            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(""+codigo).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(""+lEnsamble).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(""+lCarga).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(""+preAcondicionamiento).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));


            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("# Mediciones").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Voltaje Inicial").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Voltaje Final").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Peso").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));


            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(""+ ContadorMedicionDescargaDetalle).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(""+ voltajeMayor).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(""+voltajeMenor).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(""+peso).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));

            document.Add(header);
            document.Add(Espacio);

            document.Add(Modelo);
            document.Add(tabla.SetHorizontalAlignment(HorizontalAlignment.CENTER));
            document.Add(Espacio);
            document.Add(img);
            document.Add(Espacio);
            if (variable== "Si") {
                document.Add(tabla2.SetHorizontalAlignment(HorizontalAlignment.CENTER));
            }

            document.Close();


            byte[] bytesStreams = stream.ToArray();
            stream = new MemoryStream();
            stream.Write(bytesStreams, 0, bytesStreams.Length);
            stream.Position = 0;

            return new FileStreamResult(stream, "application/pdf");
        }
        public string GuardarViewBagDetalleAutodescarga(string chart)
        {
            Session["GraficoAutodescarga"] = chart;

            return null;
        }

        public string EnviarPdfReporteAutodescarga(int idMeidicionDescarga, string Correo, string CorreoCopia, string variable)
        {
            try
            {
                daoUtilitarios = new DaoUtilitarios();
            daoCalidad = new DaoCalidad();
                string DirCorreo = "";
                string ClavCorreo = "";
                string valoview = Session["GraficoAutodescarga"].ToString();

            var MedicionDescarga = daoCalidad.ConsultarMedicionesDeDescargasPorId(idMeidicionDescarga);

            var MedicionDescargaDetalle = daoCalidad.ConsultarDetalleMedicionDescarga(idMeidicionDescarga);

            int ContadorMedicionDescargaDetalle = daoCalidad.ContarRegistrosMedicionesDeDescargas(idMeidicionDescarga);

            string modeloBateria = "";
            int codigo = 0;
            string lEnsamble = "";
            string lCarga = "";
            string preAcondicionamiento = "";
            decimal peso = 0;

            foreach (var x in MedicionDescarga)
            {
                modeloBateria = x.Modelo;
                codigo = x.CodigoIngreso.Value;
                lEnsamble = x.LoteEnsamble;
                lCarga = x.LoteCarga;
                preAcondicionamiento = x.PreAcondicionamiento;
                peso = x.Peso.Value;
            }


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

            Paragraph header = new Paragraph("PRUEBAS AUTODESCARGA")
         .SetTextAlignment(TextAlignment.CENTER)
         .SetFontSize(16).SetBold();
            Paragraph Modelo = new Paragraph("Modelo de bateria: " + modeloBateria)
        .SetTextAlignment(TextAlignment.CENTER)
        .SetFontSize(14);

            var path = System.IO.Path.Combine(Server.MapPath("~/Images/HojaMembretada.jpg"));
            iText.Layout.Element.Image BackPack = new iText.Layout.Element.Image(ImageDataFactory.Create(path))/*.SetOpacity(0.1f)*/;
            pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new BackgroundPageEvent(BackPack));

                float[] columnWidth2 = { 80f, 80f, 80f, 80f, 80f };
                Table tabla2 = new Table(columnWidth2);
                tabla2.AddCell(new Cell(1, 7).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("REGISTROS DE AUTODESCARGAS").SetFontSize(10)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Fecha").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Voltaje").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));

                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(Border.NO_BORDER));

                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Fecha").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Voltaje").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                int contador = 1;
                decimal voltajeMayor = 0;
                decimal voltajeMenor = 0;

                var ValorAlto = MedicionDescargaDetalle.OrderBy((v) => v.PruebaLaboratorioCalidadId).FirstOrDefault();
                var ValorBajo = MedicionDescargaDetalle.OrderByDescending((v) => v.PruebaLaboratorioCalidadId).FirstOrDefault();


                voltajeMenor = ValorBajo.Voltaje.Value;
                voltajeMayor = ValorAlto.Voltaje.Value;

                //var numeroMayor = MedicionDescargaDetalle.Min();
                foreach (var y in MedicionDescargaDetalle)
                {
                    tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + y.FechaIngreso).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                    tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + y.Voltaje).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                    if (contador % 2 == 1)
                    {
                        tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("").SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(Border.NO_BORDER));
                    }

                    contador = contador + 1; ;
                }

                float[] columnWidth = { 80f, 80f, 80f, 80f };
            Table tabla = new Table(columnWidth);

            tabla.AddCell(new Cell(1, 4).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("TABLA RESUMEN").SetFontSize(10)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Codigo").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("L. Ensamble").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("L. Carga").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Pre-Acondicionamiento").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));


            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + codigo).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + lEnsamble).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + lCarga).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + preAcondicionamiento).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));


            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("# Mediciones").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Voltaje Inicial").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Voltaje Final").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Peso").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));


            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + ContadorMedicionDescargaDetalle).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + voltajeMayor).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + voltajeMenor).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + peso).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));

            document.Add(header);
            document.Add(Espacio);

            document.Add(Modelo);
            document.Add(tabla.SetHorizontalAlignment(HorizontalAlignment.CENTER));
            document.Add(Espacio);
            document.Add(img);
            document.Add(Espacio);
                if (variable == "Si") {
                    document.Add(tabla2.SetHorizontalAlignment(HorizontalAlignment.CENTER));
                }

                document.Close();

            byte[] bytesStreams = stream.ToArray();
            stream = new MemoryStream();
            stream.Write(bytesStreams, 0, bytesStreams.Length);
            stream.Position = 0;


            var CorreoBase = daoUtilitarios.ConsultarCorreoElectronico();


            foreach (var x in CorreoBase)
            {
                DirCorreo = x.DireccionCorreo;
                ClavCorreo = x.ClaveCorreo;
            }

            //foreach (var address in addresses.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            //{
            //    mailMessage.To.Add(address);
            //}
            MailMessage mm = new MailMessage("bateriasdacar1975@gmail.com", Correo);
            mm.Subject = "PRUEBAS AUTODESCARGAS " + modeloBateria;

            //MailAddress copy = new MailAddress("Notification_List@contoso.com");
            mm.CC.Add(CorreoCopia);

            mm.Body = "Resultados de pruebas de autodescargas , con fecha: " + DateTime.Now;
            mm.Attachments.Add(new Attachment(new MemoryStream(bytesStreams), "PruebasAutodescarga" + modeloBateria + ".pdf"));
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
                //throw new Exception("No se ha podido enviar el email", ex.InnerException);
                return ex.Message;
            }
        }
        public ActionResult IngresosPruebasCCALocal()
        {
            if (Session["usuario"] != null)
            {
                ViewBag.JavaScript = "General/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
                ViewBag.dxdevweb = "1";

                ViewBag.MenuAcceso = Session["Menu"];

                //ConexionAccess conexion = new ConexionAccess();

                daoUtilitarios = new DaoUtilitarios();
                daoCalidad = new DaoCalidad();

                var datModelosMarcasPropias = daoCalidad.ConsultarModelosMarcasPropias();

                var datMarcas = daoCalidad.MarcaBateria();
                var datTipoNorma = daoCalidad.TipoNorma();
                var Normativa = daoCalidad.Normativa();
                var datSeparador = daoCalidad.Separador();
                var datTipoEnsayo = daoCalidad.TipoEnsayo();

                var datMenu = daoUtilitarios.ConsultarMenuPrincipal();
                ViewBag.MenuPrincipal = datMenu;
                var datMenuOpciones = daoUtilitarios.ConsultarMenuOpciones();
                ViewBag.MenuOpciones = datMenuOpciones;
                var datSubMenuOpciones = daoUtilitarios.ConsultarSubMenuOpciones();
                ViewBag.SubMenuOpciones = datSubMenuOpciones;

                ViewBag.MarcasPropias = datModelosMarcasPropias;

                ViewBag.datMarcas = datMarcas;
                ViewBag.datTipoNorma = datTipoNorma;
                ViewBag.Normativa = Normativa;
                ViewBag.datSeparador = datSeparador;
                ViewBag.datTipoEnsayo = datTipoEnsayo;

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public int ConsultarCodigoRegistroPruebaCCALocal()
        {
            try
            {
                daoCalidad = new DaoCalidad();
                int result = daoCalidad.ObtenerCodigoIngresoPruebaCCA();

                return result;
                //return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public ActionResult LiberacionProductoExportacion()
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

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public JsonResult ObtenerPalletIngresadosLiberacion()
        {
            try
            {
                daoCalidad = new DaoCalidad();

                var Result = daoCalidad.ConsultarPackingIngreseadosLiberacionProducto();
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
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
                daoCalidad = new DaoCalidad();

                var Result = daoCalidad.ConsultarPalletCant(PackingId);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ObtenerPalletListMediciones(int PackinkId, int PalletId)
        {
            try
            {
                daoCalidad = new DaoCalidad();

                var Result = daoCalidad.ConsultarMedicionPallet(PackinkId, PalletId);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public bool InsertarMedicionPallet(int packingId, int palletId, MedicionPallet medicionModal)
        {
            daoCalidad = new DaoCalidad();
            var result = daoCalidad.InsertarMedicionPalletPaking(packingId, palletId, medicionModal.NumeroLote, medicionModal.Modelo, 
                medicionModal.Voltaje, medicionModal.nivel, medicionModal.Acabado, medicionModal.Limpieza, medicionModal.CCA);

            return result;
        }
        public JsonResult ObtenerModelosBatPallet(int PackinkId, int PalletId)
        {
            try
            {
                daoCalidad = new DaoCalidad();

                var Result = daoCalidad.ConsultarModelosProPallet(PackinkId, PalletId);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public bool EliminarMedicionPallet(MedicionPalletPacking medicionModal)
        {
            daoCalidad = new DaoCalidad();
            var result = daoCalidad.EliminarMedicionPallet(medicionModal.MedicionPalletPackingId);
            return result;
        }
        public bool ActualizarMedicionPallet(MedicionPalletPacking medicionModal, int Key)
        {

            daoCalidad = new DaoCalidad();
            var result = daoCalidad.ActualizarMedicionPallet(medicionModal, Key);

            return result;
        }
        public int NumeroMaximoMedicion()
        {
            daoCalidad = new DaoCalidad();
            var result = daoCalidad.NumeroMedicionesPackingList();
            return result;
        }
        public bool ActualizarEstadoPacking(int packingId)
        {
            daoCalidad = new DaoCalidad();
            var result = daoCalidad.ActualizarEstadoPackingList(packingId);

            return result;
        }
        public ActionResult PackingListLiberados()
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

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public JsonResult ObtenerPackingLiberados()
        {
            try
            {
                daoCalidad = new DaoCalidad();

                var Result = daoCalidad.ConsultarPackingLiberados();
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        [HttpGet]
        public ActionResult ImprimirLiberacionProducto(int packingId)
        {
            daoCalidad = new DaoCalidad();
            var resultadoPacking = daoCalidad.ConsultarPackingLiberado(packingId);
            var resultadoMedicionesPacking = daoCalidad.ConsultarMedicionPackingGeneral(packingId);

            string fechaLiberacion="";
            string nombreCliente="";
            string orden="";
            int pallet = 0;
            string nivel = "";
            string acabado = "";
            string limpieza = "";

            foreach (var x in resultadoPacking) {
                fechaLiberacion = x.FechaRegistro;
                nombreCliente = x.NombreCliente;
                orden = x.NumeroOrden;
            }

            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
            PdfFont No_Bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            iText.Kernel.Colors.Color fondoCelda = new DeviceRgb(199, 224, 255);
            iText.Kernel.Colors.Color lineColor = new DeviceRgb(39, 110, 198);

            MemoryStream stream = new MemoryStream();
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf, iText.Kernel.Geom.PageSize.A4, true);
            LineSeparator ls = new LineSeparator(new SolidLine());

            float[] cellWidth = {20f};
            Table tablaEvent = new Table(UnitValue.CreatePercentArray(1)).UseAllAvailableWidth();

            Cell cell5 = new Cell().Add(new Paragraph("_________________________"));
            tablaEvent.AddCell(cell5.SetTextAlignment(TextAlignment.CENTER).SetBorder(Border.NO_BORDER));                  
            Cell cell10 = new Cell().Add(new Paragraph("Control de Calidad").SetFontSize(10).SetHeight(14f));
            tablaEvent.AddCell(cell10.SetTextAlignment(TextAlignment.CENTER).SetBorder(Border.NO_BORDER));
            //Cell cell11 = new Cell().Add(new Paragraph("Industria Dacar Cia. Ltda.").SetFontSize(10));
            //tablaEvent.AddCell(cell11.SetTextAlignment(TextAlignment.CENTER).SetBorder(Border.NO_BORDER));

          
            pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new TableFooterEventHandler(tablaEvent));

            document.SetMargins(92, 36, 135, 36);
            var path = System.IO.Path.Combine(Server.MapPath("~/Images/HojaMembretada.jpg"));
            iText.Layout.Element.Image BackPack = new iText.Layout.Element.Image(ImageDataFactory.Create(path))/*.SetOpacity(0.1f)*/;


            pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new BackgroundPageEvent(BackPack));

            HeaderHandler headerHandler = new HeaderHandler();
            pdf.AddEventHandler(PdfDocumentEvent.START_PAGE, headerHandler);

            Paragraph header = new Paragraph("REPORTE DE LIBERACION DE PRODUCTO DE EXPORTACION")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(14).SetBold();
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            List list = new List().SetSymbolIndent(12)
            .SetFont(font);

            Paragraph Espacio = new Paragraph(" ").SetTextAlignment(TextAlignment.CENTER)
              .SetFontSize(12).SetFontColor(ColorConstants.BLACK);
            float[] columnWidth = {30f, 250f, 50f, 70f};

            Table tablaEncabezado = new Table(columnWidth);
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("PEDIDO:").SetFontSize(10).SetFixedLeading(7)).SetBold().SetHeight(18f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + orden).SetFontSize(10).SetFixedLeading(7)).SetHeight(18f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("CODIGO:").SetFontSize(10).SetFixedLeading(7)).SetBold().SetHeight(18f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + packingId).SetFontSize(10).SetFixedLeading(7)).SetHeight(18f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("CLIENTE:").SetFontSize(10).SetFixedLeading(7)).SetBold().SetHeight(18f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + nombreCliente).SetFontSize(10).SetFixedLeading(7)).SetHeight(18f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("FECHA DESPACHO:").SetFontSize(10).SetFixedLeading(7)).SetBold().SetHeight(18f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + fechaLiberacion).SetFontSize(10).SetFixedLeading(7)).SetHeight(18f).SetBorder(Border.NO_BORDER));

            float[] columnWidth3 = { 40f, 60f, 115f, 60f, 60f, 64f, 58f, 58f };
            Table tablaDetalleFact = new Table(columnWidth3);

            tablaDetalleFact.SetHorizontalAlignment(HorizontalAlignment.CENTER);
            tablaDetalleFact.AddHeaderCell(new Cell(1, 8).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBold().SetHeight(20f).SetTextAlignment(TextAlignment.CENTER).SetBorder(Border.NO_BORDER));
            tablaDetalleFact.AddHeaderCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("# Pallet").SetFontSize(8).SetFixedLeading(7)).SetBold().SetHeight(12f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(fondoCelda).SetBorder(new SolidBorder(lineColor, 1)));
            tablaDetalleFact.AddHeaderCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Lote Carga").SetFontSize(8).SetFixedLeading(7)).SetBold().SetHeight(12f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(fondoCelda).SetBorder(new SolidBorder(lineColor, 1)));
            tablaDetalleFact.AddHeaderCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Modelo").SetFontSize(8).SetFixedLeading(7)).SetBold().SetHeight(12f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(fondoCelda).SetBorder(new SolidBorder(lineColor, 1)));
            tablaDetalleFact.AddHeaderCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Voltaje").SetFontSize(8).SetFixedLeading(7)).SetBold().SetHeight(12f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(fondoCelda).SetBorder(new SolidBorder(lineColor, 1)));
            tablaDetalleFact.AddHeaderCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("CCA").SetFontSize(8).SetFixedLeading(7)).SetBold().SetHeight(12f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(fondoCelda).SetBorder(new SolidBorder(lineColor, 1)));
            tablaDetalleFact.AddHeaderCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Nivel Electrolito").SetFontSize(8).SetFixedLeading(1)).SetBold().SetHeight(12f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(fondoCelda).SetBorder(new SolidBorder(lineColor, 1)));
            tablaDetalleFact.AddHeaderCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Etiquetado").SetFontSize(8).SetFixedLeading(7)).SetBold().SetHeight(12f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(fondoCelda).SetBorder(new SolidBorder(lineColor, 1)));
            tablaDetalleFact.AddHeaderCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Limpieza").SetFontSize(8).SetFixedLeading(7)).SetBold().SetHeight(12f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(fondoCelda).SetBorder(new SolidBorder(lineColor, 1)));

            var FONT = System.IO.Path.Combine(Server.MapPath("~/Fuentes/FreeSans.ttf"));


            PdfFont simbol = PdfFontFactory.CreateFont(FONT, PdfEncodings.IDENTITY_H);


            foreach (var x in resultadoMedicionesPacking)
            {
                if (x.nivel==true) {
                    nivel = "\u221A";
                }
                if (x.Acabado == true)
                {
                    acabado = "\u221A";
                }
                if (x.Limpieza == true)
                {
                    limpieza = "\u221A";
                }

                pallet = daoCalidad.ObtenerNumeroPallet(x.PalletId.Value);
                tablaDetalleFact.AddCell(new Cell().Add(new Paragraph("" + pallet).SetFontSize(8).SetFixedLeading(8)).SetHeight(10f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                tablaDetalleFact.AddCell(new Cell().Add(new Paragraph("" + x.NumeroLote).SetFontSize(8).SetFixedLeading(8)).SetHeight(10f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                tablaDetalleFact.AddCell(new Cell().Add(new Paragraph("" + x.Modelo).SetFontSize(8).SetFixedLeading(8)).SetHeight(10f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                tablaDetalleFact.AddCell(new Cell().Add(new Paragraph("" + String.Format("{0:n}", x.Voltaje)).SetFontSize(8).SetFixedLeading(8)).SetHeight(10f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                tablaDetalleFact.AddCell(new Cell().Add(new Paragraph("" + x.CCA).SetFontSize(8).SetFixedLeading(8)).SetHeight(10f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                tablaDetalleFact.AddCell(new Cell().Add(new Paragraph("" + nivel).SetFont(simbol).SetFontSize(8).SetFixedLeading(8).SetBold()).SetHeight(10f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                tablaDetalleFact.AddCell(new Cell().Add(new Paragraph("" + acabado).SetFont(simbol).SetFontSize(8).SetFixedLeading(8).SetBold()).SetHeight(10f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                tablaDetalleFact.AddCell(new Cell().Add(new Paragraph(""+limpieza).SetFont(simbol).SetFontSize(8).SetFixedLeading(8).SetBold()).SetHeight(10f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));

                nivel = "";
                acabado = "";
                limpieza = "";
            }
            document.Add(header);
            document.Add(Espacio);
            document.Add(tablaEncabezado);
            document.Add(tablaDetalleFact);

            document.Close();

            byte[] bytesStreams = stream.ToArray();
            stream = new MemoryStream();
            stream.Write(bytesStreams, 0, bytesStreams.Length);
            stream.Position = 0;
            return new FileStreamResult(stream, "application/pdf");
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

                new Canvas(canvas, new iText.Kernel.Geom.Rectangle(35, 24, page.GetPageSize().GetRight() - 55, 81))
                    .Add(table)
                    .Close();
            }
        }
        private class HeaderHandler : IEventHandler
        {
            public void HandleEvent(Event currentEvent)
            {
                PdfFormXObject template = new iText.Kernel.Pdf.Xobject.PdfFormXObject(new iText.Kernel.Geom.Rectangle(450, 780, 30, 30));

                PdfDocumentEvent docEvent = (PdfDocumentEvent)currentEvent;
                PdfPage page = docEvent.GetPage();
                int pageNum = docEvent.GetDocument().GetPageNumber(page);

                PdfCanvas canvas = new PdfCanvas(page);
                canvas.BeginText();
                try
                {
                    canvas.SetFontAndSize(PdfFontFactory.CreateFont(StandardFonts.HELVETICA), 12);
                }
                catch (IOException e)
                {
                    Console.WriteLine(e);
                }

                canvas.MoveText(510, 20);
                canvas.ShowText("Página "+ pageNum);
                canvas.EndText();
                canvas.Stroke();
                canvas.AddXObject(template);
                canvas.Release();
            }
        }
        public ActionResult LiberacionProductoLocal()
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

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public JsonResult ObtenerPalletIngresadosLiberacionLocal()
        {
            try
            {
                daoCalidad = new DaoCalidad();
                var Result = daoCalidad.ListadoCabeceraOrdenesVentasSap();
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
                if (Result.Count == 0)
                {
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
        public bool RegistrarPedidoLocal(CabeceraOrdenVenta cabeceraOrdenVenta)
        {
            try
            {
                daoCalidad = new DaoCalidad();
                daoPackingList = new DaoPackingList();
                daoOrdenesVentas = new DaoOrdenesVentas();
                var result = daoCalidad.IngresarEncabezadoMedicionPalletLocal(cabeceraOrdenVenta);
                var detalleOrden = daoOrdenesVentas.ListadoDetalleOrdenesVentasSap(cabeceraOrdenVenta.DocEntry);
                if (detalleOrden.Count == 0)
                {
                    detalleOrden = daoOrdenesVentas.ListadoDetalleFacturasReservaSap(cabeceraOrdenVenta.DocEntry);
                }

                foreach (var x in detalleOrden)
                {
                    daoCalidad.IngresarPackingDtlLocal(result, x.ItemCode, x.Descripcion, x.Cantidad);
                }
                return true;
            }
            catch {
                return false;
            }
          
        }
        public ActionResult LiberacionProductoLocalRegistrados()
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

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public JsonResult ObtenerPalletIngresadosLiberacionLocalRegistrados()
        {
            try
            {
                daoCalidad = new DaoCalidad();

                var Result = daoCalidad.ConsultarPackingIngreseadosLiberacionLocalRegistrado();
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        //modificar para obtener el detalle del pedido local
        public JsonResult ObtenerModelosBatPalletLocal(int identificador)
        {
            try
            {
                daoCalidad = new DaoCalidad();

                var Result = daoCalidad.ConsultarModelosPedidoMedicionesLocal(identificador);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ObtenerListMedicionesPalletLocal(int identificador)
        {
            try
            {
                daoCalidad = new DaoCalidad();

                var Result = daoCalidad.ConsultarMedicionPalletLocal(identificador);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public bool InsertarMedicionPalletLocal(int packingId, string numeroLote, string Modelo, decimal Voltaje, string nivel, string acabado, string limpieza, 
            decimal cca, HttpPostedFileBase[] archivos)
        {
            daoCalidad = new DaoCalidad();
            bool valorNivel=false;
            bool valorAcabado = false;
            bool valorLempieza = false;
            if (nivel == "Si") {
                valorNivel = true;
            }
            if (acabado == "Si")
            {
                valorAcabado = true;
            }
            if (limpieza == "Si")
            {
                valorLempieza = true;
            }
            var result = daoCalidad.InsertarMedicionPalletLocal(packingId, 1, numeroLote, Modelo,
                Voltaje, valorNivel, valorAcabado, valorLempieza, cca);

            return result;
        }
        public bool ActualizarEstadoPackingLocal(int identificador)
        {
            daoCalidad = new DaoCalidad();
            var result = daoCalidad.RegistrarLiberacionLocal(identificador);

            return result;
        }
        public bool EliminarMedicionPalletLocal(MedicionPalletPacking medicionModal)
        {
            daoCalidad = new DaoCalidad();
            var result = daoCalidad.EliminarMedicionPalletLocal(medicionModal.MedicionPalletPackingId);
            return result;
        }
        public ActionResult PackingListLiberadosLocales()
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

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public JsonResult ObtenerPackingLocalesLiberados()
        {
            try
            {
                daoCalidad = new DaoCalidad();

                var Result = daoCalidad.ConsultarPackingLiberadosLocales();
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ObtenerPalletListMedicionesLocales(int identificador)
        {
            try
            {
                daoCalidad = new DaoCalidad();

                var Result = daoCalidad.ConsultarMedicionPalletLocal(identificador);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        [HttpGet]
        public ActionResult ImprimirLiberacionProductoLocal(int identificador)
        {
            daoCalidad = new DaoCalidad();
            var resultadoPacking = daoCalidad.ConsultarPackingLiberadoLocale(identificador);
            var resultadoMedicionesPacking = daoCalidad.ConsultarMedicionPalletLocal(identificador);

            string fechaLiberacion = "";
            string nombreCliente = "";
            string orden = "";
            int pallet = 0;
            string nivel = "";
            string acabado = "";
            string limpieza = "";
            int packing = 0;
            foreach (var x in resultadoPacking)
            {
                fechaLiberacion = x.FechaRegistro;
                nombreCliente = x.NombreCliente;
                orden = x.NumeroOrden;
                packing=x.PackingId;
            }

            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
            PdfFont No_Bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            iText.Kernel.Colors.Color fondoCelda = new DeviceRgb(199, 224, 255);
            iText.Kernel.Colors.Color lineColor = new DeviceRgb(39, 110, 198);

            MemoryStream stream = new MemoryStream();
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf, iText.Kernel.Geom.PageSize.A4, true);
            LineSeparator ls = new LineSeparator(new SolidLine());

            float[] cellWidth = { 20f };
            Table tablaEvent = new Table(UnitValue.CreatePercentArray(1)).UseAllAvailableWidth();

            Cell cell5 = new Cell().Add(new Paragraph("_________________________"));
            tablaEvent.AddCell(cell5.SetTextAlignment(TextAlignment.CENTER).SetBorder(Border.NO_BORDER));
            Cell cell10 = new Cell().Add(new Paragraph("Control de Calidad").SetFontSize(10).SetHeight(14f));
            tablaEvent.AddCell(cell10.SetTextAlignment(TextAlignment.CENTER).SetBorder(Border.NO_BORDER));
            //Cell cell11 = new Cell().Add(new Paragraph("Industria Dacar Cia. Ltda.").SetFontSize(10));
            //tablaEvent.AddCell(cell11.SetTextAlignment(TextAlignment.CENTER).SetBorder(Border.NO_BORDER));


            pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new TableFooterEventHandler(tablaEvent));

            document.SetMargins(92, 36, 135, 36);
            var path = System.IO.Path.Combine(Server.MapPath("~/Images/HojaMembretada.jpg"));
            iText.Layout.Element.Image BackPack = new iText.Layout.Element.Image(ImageDataFactory.Create(path))/*.SetOpacity(0.1f)*/;


            pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new BackgroundPageEvent(BackPack));

            HeaderHandler headerHandler = new HeaderHandler();
            pdf.AddEventHandler(PdfDocumentEvent.START_PAGE, headerHandler);

            Paragraph header = new Paragraph("REPORTE DE LIBERACION DE PRODUCTO LOCAL")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(14).SetBold();
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            List list = new List().SetSymbolIndent(12)
            .SetFont(font);

            Paragraph Espacio = new Paragraph(" ").SetTextAlignment(TextAlignment.CENTER)
              .SetFontSize(12).SetFontColor(ColorConstants.BLACK);
            float[] columnWidth = { 30f, 250f, 50f, 70f };

            Table tablaEncabezado = new Table(columnWidth);
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("PEDIDO:").SetFontSize(10).SetFixedLeading(7)).SetBold().SetHeight(18f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + orden).SetFontSize(10).SetFixedLeading(7)).SetHeight(18f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("CODIGO:").SetFontSize(10).SetFixedLeading(7)).SetBold().SetHeight(18f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + packing).SetFontSize(10).SetFixedLeading(7)).SetHeight(18f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("CLIENTE:").SetFontSize(10).SetFixedLeading(7)).SetBold().SetHeight(18f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + nombreCliente).SetFontSize(10).SetFixedLeading(7)).SetHeight(18f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("FECHA DESPACHO:").SetFontSize(10).SetFixedLeading(7)).SetBold().SetHeight(18f).SetBorder(Border.NO_BORDER));
            tablaEncabezado.AddCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + fechaLiberacion).SetFontSize(10).SetFixedLeading(7)).SetHeight(18f).SetBorder(Border.NO_BORDER));

            float[] columnWidth3 = { 40f, 60f, 115f, 60f, 60f, 64f, 58f, 58f };
            Table tablaDetalleFact = new Table(columnWidth3);

            tablaDetalleFact.SetHorizontalAlignment(HorizontalAlignment.CENTER);
            tablaDetalleFact.AddHeaderCell(new Cell(1, 8).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).SetBold().SetHeight(20f).SetTextAlignment(TextAlignment.CENTER).SetBorder(Border.NO_BORDER));
            tablaDetalleFact.AddHeaderCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("# Pallet").SetFontSize(8).SetFixedLeading(7)).SetBold().SetHeight(12f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(fondoCelda).SetBorder(new SolidBorder(lineColor, 1)));
            tablaDetalleFact.AddHeaderCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Lote Carga").SetFontSize(8).SetFixedLeading(7)).SetBold().SetHeight(12f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(fondoCelda).SetBorder(new SolidBorder(lineColor, 1)));
            tablaDetalleFact.AddHeaderCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Modelo").SetFontSize(8).SetFixedLeading(7)).SetBold().SetHeight(12f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(fondoCelda).SetBorder(new SolidBorder(lineColor, 1)));
            tablaDetalleFact.AddHeaderCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Voltaje").SetFontSize(8).SetFixedLeading(7)).SetBold().SetHeight(12f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(fondoCelda).SetBorder(new SolidBorder(lineColor, 1)));
            tablaDetalleFact.AddHeaderCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("CCA").SetFontSize(8).SetFixedLeading(7)).SetBold().SetHeight(12f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(fondoCelda).SetBorder(new SolidBorder(lineColor, 1)));
            tablaDetalleFact.AddHeaderCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Nivel Electrolito").SetFontSize(8).SetFixedLeading(1)).SetBold().SetHeight(12f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(fondoCelda).SetBorder(new SolidBorder(lineColor, 1)));
            tablaDetalleFact.AddHeaderCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Etiquetado").SetFontSize(8).SetFixedLeading(7)).SetBold().SetHeight(12f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(fondoCelda).SetBorder(new SolidBorder(lineColor, 1)));
            tablaDetalleFact.AddHeaderCell(new Cell(1, 1).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Limpieza").SetFontSize(8).SetFixedLeading(7)).SetBold().SetHeight(12f).SetTextAlignment(TextAlignment.CENTER).SetBackgroundColor(fondoCelda).SetBorder(new SolidBorder(lineColor, 1)));

            var FONT = System.IO.Path.Combine(Server.MapPath("~/Fuentes/FreeSans.ttf"));


            PdfFont simbol = PdfFontFactory.CreateFont(FONT, PdfEncodings.IDENTITY_H);


            foreach (var x in resultadoMedicionesPacking)
            {
                if (x.nivel == true)
                {
                    nivel = "\u221A";
                }
                if (x.Acabado == true)
                {
                    acabado = "\u221A";
                }
                if (x.Limpieza == true)
                {
                    limpieza = "\u221A";
                }

                pallet = 1;
                tablaDetalleFact.AddCell(new Cell().Add(new Paragraph("" + pallet).SetFontSize(8).SetFixedLeading(8)).SetHeight(10f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                tablaDetalleFact.AddCell(new Cell().Add(new Paragraph("" + x.NumeroLote).SetFontSize(8).SetFixedLeading(8)).SetHeight(10f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                tablaDetalleFact.AddCell(new Cell().Add(new Paragraph("" + x.Modelo).SetFontSize(8).SetFixedLeading(8)).SetHeight(10f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                tablaDetalleFact.AddCell(new Cell().Add(new Paragraph("" + String.Format("{0:n}", x.Voltaje)).SetFontSize(8).SetFixedLeading(8)).SetHeight(10f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                tablaDetalleFact.AddCell(new Cell().Add(new Paragraph("" + x.CCA).SetFontSize(8).SetFixedLeading(8)).SetHeight(10f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                tablaDetalleFact.AddCell(new Cell().Add(new Paragraph("" + nivel).SetFont(simbol).SetFontSize(8).SetFixedLeading(8).SetBold()).SetHeight(10f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                tablaDetalleFact.AddCell(new Cell().Add(new Paragraph("" + acabado).SetFont(simbol).SetFontSize(8).SetFixedLeading(8).SetBold()).SetHeight(10f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));
                tablaDetalleFact.AddCell(new Cell().Add(new Paragraph("" + limpieza).SetFont(simbol).SetFontSize(8).SetFixedLeading(8).SetBold()).SetHeight(10f).SetTextAlignment(TextAlignment.CENTER).SetBorder(new SolidBorder(lineColor, 1)));

                nivel = "";
                acabado = "";
                limpieza = "";
            }
            document.Add(header);
            document.Add(Espacio);
            document.Add(tablaEncabezado);
            document.Add(tablaDetalleFact);

            document.Close();

            byte[] bytesStreams = stream.ToArray();
            stream = new MemoryStream();
            stream.Write(bytesStreams, 0, bytesStreams.Length);
            stream.Position = 0;
            return new FileStreamResult(stream, "application/pdf");
        }
    }
}