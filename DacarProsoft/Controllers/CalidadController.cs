using DacarProsoft.Datos;
using DacarProsoft.Models;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DacarProsoft.Controllers
{
    public class CalidadController : Controller
    {
        private DaoUtilitarios daoUtilitarios { get; set; } = null;
        private DaoCalidad daoCalidad { get; set; } = null;

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
            string Observaciones, decimal Calificacion, HttpPostedFileBase[] archivos)
        {
            try
            {
                daoCalidad = new DaoCalidad();
                var result = daoCalidad.IngresarPruebaLaboratorio(FechaIngreso, CodigoIngreso, Marca, TipoNorma, Normativa, PreAcondicionamiento, TipoBateria, Modelo, Separador, TipoEnsayo, LoteEnsamble,
                LoteCarga, CCA, Peso, Voltaje, DensidadIngreso, 0, TemperaturaIngreso, TemperaturaPrueba, DatoTeoricoPrueba, 0, ResultadoFinal,
                Observaciones, Calificacion);

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
        public ActionResult GenerarPdfReporte()
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
            //Document document = new Document(pdf, iText.Kernel.Geom.PageSize.A5, true);
            //Obtener valores de cm a pulgadas y de ahi multiplicar x 72
            //iText.Kernel.Geom.Rectangle rectangle13x21 = new iText.Kernel.Geom.Rectangle(368.50f, 595.28f);

            //iText.Kernel.Geom.Rectangle rectangle11x16 = new iText.Kernel.Geom.Rectangle(325.98f, 453.54f);
            //iText.Kernel.Geom.Rectangle rectangle10x15 = new iText.Kernel.Geom.Rectangle(283.46f, 425.20f);
            Document document = new Document(pdf, iText.Kernel.Geom.PageSize.A4, true);
            //Document document = new Document(pdf, new iText.Kernel.Geom.PageSize(rectangle10x15));
            iText.Layout.Element.Image img = new iText.Layout.Element.Image(ImageDataFactory
             .Create(bytes))
             .SetTextAlignment(TextAlignment.CENTER).SetWidth(480).SetHeight(240).SetHorizontalAlignment(HorizontalAlignment.CENTER);


            Paragraph header = new Paragraph("ANALISIS PRUEBAS LABORATORIO POR "+valor[0].TipoEnsayo)
         .SetTextAlignment(TextAlignment.CENTER)
         .SetFontSize(16).SetBold();
            Paragraph Modelo = new Paragraph("Modelo de bateria: "+valor[0].Modelo)
        .SetTextAlignment(TextAlignment.CENTER)
        .SetFontSize(14);

            var path = System.IO.Path.Combine(Server.MapPath("~/Images/HojaMembretada.jpg"));
            iText.Layout.Element.Image BackPack = new iText.Layout.Element.Image(ImageDataFactory.Create(path))/*.SetOpacity(0.1f)*/;
            pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new BackgroundPageEvent(BackPack));


            float[] columnWidth2 = { 60f ,55f, 55f, 55f, 65f, 55f, 55f, 55f, 55f };
            Table tabla2 = new Table(columnWidth2);

            tabla2.AddCell(new Cell(1, 9).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("REGISTROS DE PRUEBAS DE LABORATORIO").SetFontSize(10)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Fecha").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Codigo").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Norma").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Separador").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("L. Ensamble").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("L. Carga").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("CCA").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Voltaje").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Resultado").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));


            foreach (var x in valor)
            {
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(""+x.FechaIngreso).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + x.CodigoIngreso).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + x.TipoNorma).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + x.Separador).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + x.LoteEnsamble).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + x.LoteCarga).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + x.CCA).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + x.Voltaje).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
                tabla2.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("" + x.ResultadoFinal).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));


                CCA = CCA + x.CCA.Value;
                Peso = Peso + x.Peso.Value;
                Voltaje = Voltaje + x.Voltaje.Value;
                ValorObjetivo = ValorObjetivo + x.ValorObjetivo.Value;
                ResultadoFinal = ResultadoFinal + x.ResultadoFinal.Value;
                Calificacion = Calificacion + x.Calificacion.Value;
                contador = contador + 1;
            }

            float[] columnWidth = { 80f, 80f, 80f };
            Table tabla = new Table(columnWidth);

            tabla.AddCell(new Cell(1, 3).SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("PROMEDIOS GENERALES").SetFontSize(10)).SetBold().SetHeight(16f).SetTextAlignment(TextAlignment.CENTER).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("CCA").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Peso").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Voltaje").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(""+Decimal.Round(CCA/contador,2)).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(""+Decimal.Round(Peso/contador,2)).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(""+Decimal.Round(Voltaje/contador,2)).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Valor Objetivo").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Resultado Final").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph("Calificacion").SetFontSize(9)).SetBold().SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(""+Decimal.Round(ValorObjetivo / contador, 2)).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(""+Decimal.Round(ResultadoFinal / contador, 2)).SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));
            tabla.AddCell(new Cell().SetTextAlignment(TextAlignment.LEFT).SetVerticalAlignment(VerticalAlignment.MIDDLE).Add(new Paragraph(""+Decimal.Round(Calificacion / contador, 1)+"%").SetFontSize(9)).SetHeight(16f).SetHeight(12f).SetBorder(new SolidBorder(lineColor, 1)));

            document.Add(Espacio);
            document.Add(Espacio);
            document.Add(Espacio);
            document.Add(Espacio);
            document.Add(Espacio);
            document.Add(Espacio);

            document.Add(header);
            document.Add(Espacio);

            document.Add(Modelo);
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

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //    throw;
            //}
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
    }
}