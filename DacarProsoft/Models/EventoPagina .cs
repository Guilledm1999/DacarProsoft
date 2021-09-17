using iText.Kernel.Events;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using iText.IO.Image;
using iText.IO.Font.Constants;
using iText.Layout.Properties;

namespace DacarProsoft.Models
{
    public class EventoPagina : IEventHandler
    {
        private  Document documento;
        private Byte[] imagen;
          


        public EventoPagina(Document doc,Byte[] qr)
        {
            documento = doc;
            imagen = qr;
        }

        public EventoPagina()
        {
        }

        /**
         * Crea el rectangulo donde pondremos el encabezado
         * @param docEvent Evento de documento
         * @return Area donde colocaremos el encabezado
         */
        private Rectangle crearRectanguloEncabezado(PdfDocumentEvent docEvent)
        {
            PdfDocument pdfDoc = docEvent.GetDocument();
            PdfPage page = docEvent.GetPage();

            float xEncabezado = pdfDoc.GetDefaultPageSize().GetX() + documento.GetLeftMargin();
            float yEncabezado = pdfDoc.GetDefaultPageSize().GetTop() - documento.GetTopMargin();
            float anchoEncabezado = page.GetPageSize().GetWidth() - 72;
            float altoEncabezado = 50F;

            Rectangle rectanguloEncabezado = new Rectangle(xEncabezado, yEncabezado, anchoEncabezado, altoEncabezado);

            return rectanguloEncabezado;
        }

        /**
         * Crea el rectangulo donde pondremos el pie de pagina
         * @param docEvent Evento del documento
         * @return Area donde colocaremos el pie de pagina
         */
        private Rectangle crearRectanguloPie(PdfDocumentEvent docEvent)
        {
            PdfDocument pdfDoc = docEvent.GetDocument();
            PdfPage page = docEvent.GetPage();

            float xPie = pdfDoc.GetDefaultPageSize().GetX() + documento.GetLeftMargin();
            float yPie = pdfDoc.GetDefaultPageSize().GetBottom();
            float anchoPie = page.GetPageSize().GetWidth() - 72;
            float altoPie = 50F;

            Rectangle rectanguloPie = new Rectangle(xPie, yPie, anchoPie, altoPie);

            return rectanguloPie;
        }

        /**
         * Crea la tabla que contendra el mensaje del encabezado
         * @param mensaje Mensaje que desplegaremos
         * @return Tabla con el mensaje de encabezado
         */
        private Table crearTablaEncabezado(String mensaje)
        {
            
            float[] anchos = { 1F };
            Table tablaEncabezado = new Table(anchos);
            tablaEncabezado.SetWidth(527F);
            tablaEncabezado.AddCell(mensaje);
            return tablaEncabezado;
        }

        /**
         * Crea la tabla de pie de pagina, con el numero de pagina
         * @param docEvent Evento del documento
         * @return Pie de pagina con el numero de pagina
         */
        private Table crearTablaPie(PdfDocumentEvent docEvent)
        {
            PdfPage page = docEvent.GetPage();
            float[] anchos = { 1F };
            Table tablaPie = new Table(anchos);
            tablaPie.SetWidth(400F);
            int pageNum = docEvent.GetDocument().GetPageNumber(page);

            iText.Layout.Element.Image Imgdacar = new iText.Layout.Element.Image(ImageDataFactory
         .Create(imagen))
         .SetTextAlignment(TextAlignment.LEFT).SetWidth(155).SetHeight(80);

            tablaPie.AddCell(Imgdacar);
            tablaPie.AddCell("Pagina " + pageNum);

            return tablaPie;
        }

        /**
         * Manejador del evento de cambio de pagina, agrega el encabezado y pie de pagina
         * @param event Evento de pagina
         */
         
        public void HandleEvent(Event @event) {

        PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
        PdfDocument pdfDoc = docEvent.GetDocument();
        PdfPage page = docEvent.GetPage();
        PdfCanvas canvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);        
        
        Table tablaEncabezado = this.crearTablaEncabezado("Departamento de Recursos Humanos");
        Rectangle rectanguloEncabezado = this.crearRectanguloEncabezado(docEvent);
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        Canvas canvasEncabezado = new Canvas(canvas, pdfDoc, rectanguloEncabezado);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        canvasEncabezado.Add(tablaEncabezado);      

        Table tablaNumeracion = this.crearTablaPie(docEvent);
        Rectangle rectanguloPie = this.crearRectanguloPie(docEvent);
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        Canvas canvasPie = new Canvas(canvas, pdfDoc, rectanguloPie);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        canvasPie.Add(tablaNumeracion);
    }

}
}