using DacarProsoft.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DacarDatos.Datos;
using iText.Kernel.Pdf;
using iText.Layout;
using System.IO;
using iText.Layout.Element;
using iText.Kernel.Geom;
using iText.Kernel.Colors;
using iText.Layout.Properties;
using DacarProsoft.Models;

namespace DacarProsoft.Controllers
{
    public class PruebaController : Controller
    {
        /*
          private DaoPruebaMVC daoPrueba { get; set; } = null;
          private DaoUtilitarios daoUtilitarios { get; set; } = null;
          // GET: Prueba
          /*
          public ActionResult PruebaVista()
          {

                         if (Session["usuario"] != null)
              {/*
                  ViewBag.JavaScript = "General/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
                  ViewBag.dxdevweb = "1";

                  ViewBag.MenuAcceso = Session["Menu"];

                  daoUtilitarios = new DaoUtilitarios();
                  daoPrueba = new DaoPruebaMVC();

                  ViewBag.prueba = "5+4664652";
                  var datMenu = daoUtilitarios.ConsultarMenuPrincipal();
                  ViewBag.MenuPrincipal = datMenu;
                  var datMenuOpciones = daoUtilitarios.ConsultarMenuOpciones();
                  ViewBag.MenuOpciones = datMenuOpciones;
                  var datSubMenuOpciones = daoUtilitarios.ConsultarSubMenuOpciones();
                  ViewBag.SubMenuOpciones = datSubMenuOpciones;

                  var datProvincia = daoPrueba.CargarProvincia();
                  ViewBag.datProvincia = datProvincia;


                  return View();
              }
              else
              {
                  return RedirectToAction("Login", "Account");
              }

          }

          public JsonResult Datos()
          {
              daoPrueba = new DaoPruebaMVC();
              var result = daoPrueba.Datos();
              return Json(result, JsonRequestBehavior.AllowGet);
          }

          public JsonResult Provincias()
          {
              daoPrueba = new DaoPruebaMVC();
              var result = daoPrueba.Provincias();
              return Json(result, JsonRequestBehavior.AllowGet);
          }
          public JsonResult Genero()
          {
              daoPrueba = new DaoPruebaMVC();
              var result = daoPrueba.GenerarGeneros();
              float total = 0;
              if (result.Count == 2)
              {
                  total = result[0].Cantidad + result[1].Cantidad;
              }
              else
              {
                  total = result[0].Cantidad;
              }

              foreach (var item in result)
              {
                  item.Cantidad = item.Cantidad / total;
              }
              return Json(result, JsonRequestBehavior.AllowGet);
          }

          public JsonResult ProvinciaPie()
          {
              daoPrueba = new DaoPruebaMVC();
              var result = daoPrueba.Datos();
              float total = 0;
             var res = result.GroupBy(n => n.ProvinciaDes)
                           .Select(n => new EPieChart
                           {
                               Descripcion = n.Key,
                               Cantidad = n.Count()
                           })
                           .OrderBy(n => n.Descripcion).ToList();
             total= result.Count();
              foreach (var item in res)
              {
                  item.Cantidad = item.Cantidad / total;
              }


              return Json(res, JsonRequestBehavior.AllowGet);
          }


          public bool InsertarEventoMes(PruebaMVC crono)
          {

              daoPrueba = new DaoPruebaMVC();

              var result = daoPrueba.Guardar(crono.Nombre, crono.Cedula, crono.Correo, crono.Genero, crono.Provincia);

              return result;
          }

          public bool ActualizarEvento(PruebaMVC crono, int Key)
          {

              daoPrueba = new DaoPruebaMVC();

              var result = daoPrueba.Actualizar(Key, crono.Nombre, crono.Cedula, crono.Correo, crono.Genero, crono.Provincia);

              return result;
          }
          public bool EliminarEvento(PruebaMVC crono)
          {

              daoPrueba = new DaoPruebaMVC();
              var result = daoPrueba.Eliminar(crono.idPersona);
              return result;
          }

          public ActionResult Pdf()
          {
              var daoPrueba = new DaoPruebaMVC();
              MemoryStream ms = new MemoryStream();

              PdfWriter writer = new PdfWriter(ms);
              PdfDocument pdf = new PdfDocument(writer);
              Document document = new Document(pdf, PageSize.LETTER);
              // document.Add(new Paragraph("Hello World!"));
              document.SetMargins(75, 35, 70, 35);

              Style StyleCell = new Style().SetBackgroundColor(ColorConstants.LIGHT_GRAY).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
              var Datos = daoPrueba.Datos();


              Table table = new Table(6).UseAllAvailableWidth();
              Cell cell = new Cell().Add(new Paragraph("#"));
              table.AddHeaderCell(cell.AddStyle(StyleCell));
               cell = new Cell().Add(new Paragraph("Nombre"));
              table.AddHeaderCell(cell.AddStyle(StyleCell));
               cell = new Cell().Add(new Paragraph("Cedula"));
              table.AddHeaderCell(cell.AddStyle(StyleCell));
               cell = new Cell().Add(new Paragraph("Correo"));
              table.AddHeaderCell(cell.AddStyle(StyleCell));
              cell = new Cell().Add(new Paragraph("Genero"));
              table.AddHeaderCell(cell.AddStyle(StyleCell));
              cell = new Cell().Add(new Paragraph("Provincia"));
              table.AddHeaderCell(cell.AddStyle(StyleCell));




              int x = 0;
              foreach (var item in Datos)
              {
                  x++;
                  cell = new Cell().Add(new Paragraph(x.ToString()));
                  table.AddCell(cell);
                  cell = new Cell().Add(new Paragraph(item.Nombre.ToString()));
                  table.AddCell(cell);
                  cell = new Cell().Add(new Paragraph(item.Cedula.ToString()));
                  table.AddCell(cell);
                  cell = new Cell().Add(new Paragraph(item.Correo.ToString()));
                  table.AddCell(cell);
                  cell = new Cell().Add(new Paragraph(item.Genero.ToString()));
                  table.AddCell(cell);
                  cell = new Cell().Add(new Paragraph(item.ProvinciaDes.ToString()));
                  table.AddCell(cell);
              }
              Paragraph header = new Paragraph("Pdf Prueba")
             .SetTextAlignment(TextAlignment.CENTER)
             .SetFontSize(20);

              document.Add(header);
              document.Add(table);
              document.Close();


              byte[] bytesStream = ms.ToArray();
              ms = new MemoryStream();
              ms.Write(bytesStream, 0, bytesStream.Length);
              ms.Position = 0;
              return new FileStreamResult(ms, "application/pdf");

          }
        */
    }
        

}