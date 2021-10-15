using DacarProsoft.Datos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DacarProsoft.Controllers
{
    public class GarantiasController : Controller
    {
        private DaoUtilitarios daoUtilitarios { get; set; } = null;
        private DaoGarantias daoGarantias { get; set; } = null;

        // GET: Garantias
        public ActionResult IngresoGarantias()
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

        public JsonResult ConsultarNumeroGarantia(string numero)
        {
            try
            {
                daoGarantias = new DaoGarantias();
                var Result = daoGarantias.ConsultarNumeroGarantia(numero);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public int RegistrarRevisionDeGarantiaCabecera(string cliente, string cedula, string numeroGarantia, string numeroComprobante, string numeroRevision, string provincia, string direccion, string vendedor, HttpPostedFileBase ImgFac, string marca,
            string modelo, string lote, decimal prorrateo, int meses, DateTime fechaVenta, DateTime fechaIngreso, decimal porcentajeVenta, decimal voltaje, HttpPostedFileBase ImgTest)
        {
            try
            {
                daoGarantias = new DaoGarantias();

                string filename = Path.GetExtension(ImgFac.FileName);

                var destinationPath = Path.Combine(Server.MapPath("~/Images/ImagenesGarantias/Facturas/"), numeroRevision+"-"+numeroGarantia+"-"+cedula);
                ImgFac.SaveAs(destinationPath + filename);

                string destinoImg1 = numeroRevision + "-" + numeroGarantia + "-" + cedula + filename;

                string filename2 = Path.GetExtension(ImgTest.FileName);

                var destinationPath2 = Path.Combine(Server.MapPath("~/Images/ImagenesGarantias/Test/"), numeroRevision + "-" + numeroGarantia + "-" + cedula);
                ImgTest.SaveAs(destinationPath2 + filename2);

                string destinoImg2 = numeroRevision + "-" + numeroGarantia + "-" + cedula + filename;


                var Result = daoGarantias.IngresarRevisionGarantiaCabecera(cliente,  cedula,  numeroGarantia,  numeroComprobante,  numeroRevision,  provincia,  direccion,  vendedor, destinoImg1,  marca,
                modelo,  lote,  prorrateo,  meses,  fechaVenta,  fechaIngreso,  porcentajeVenta,  voltaje, destinoImg2);
                return Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public bool RegistrarRevisionDeGarantiaDetalle(int RevisionId, string InGolpeadaoRota, string InHinchada, string InBornesFlojos, string InBornesFundidos, string IngElectrolito, string InFugaEnCubierta, string InFugaEnBornes,int InDcC1,
            int InDcC2, int InDcC3, int InDcC4, int InDcC5, int InDcC6, int InCCA, string TrPruebaAltaResistencia, string TrCambioAcido, string TrRecargaBateria, string TrInspeccionEstructuraExt, string DBateriaBuenEstado, string DPresentaFallosFabricacion,
       string DDentroPeriodo, string DUsoAdecuado, string DAplicaGarantia)
        {
            try
            {
                daoGarantias = new DaoGarantias();

                var Result = daoGarantias.IngresarRevisionGarantiaInspeccionInicial(RevisionId, InGolpeadaoRota, InHinchada, InBornesFlojos, InBornesFundidos, IngElectrolito, InFugaEnCubierta, InFugaEnBornes, InCCA);
                var Result2 = daoGarantias.IngresarRevisionGarantiaInspeccionInicialCeldas(Result, InDcC1, InDcC2, InDcC3, InDcC4, InDcC5, InDcC6);
                var Result3 = daoGarantias.IngresarRevisionGarantiaTrabajoRealizado(RevisionId, TrPruebaAltaResistencia, TrCambioAcido, TrRecargaBateria, TrInspeccionEstructuraExt);
                var Result4 = daoGarantias.IngresarRevisionGarantiaDiagnostico(RevisionId, DBateriaBuenEstado, DPresentaFallosFabricacion, DDentroPeriodo, DUsoAdecuado, DAplicaGarantia);


                if (Result2==true && Result!=0 && Result3==true && Result4==true) {
                    return true;
                }
                else {
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
                throw;
            }
        }

        //public bool RegistrarRevisionDeGarantiaDetalleInspeccionInicial(int RevisionDeGarantia, string InGolpeadaoRota, string InHinchada, string InBornesFlojos, string InBornesFundidos, string IngElectrolito, string InFugaEnCubierta, string InFugaEnBornes, int InDcC1,
        //    int InDcC2, int InDcC3, int InDcC4, int InDcC5, int InDcC6, int InCCA)
        //{
        //    try
        //    {
        //        daoGarantias = new DaoGarantias();
        //        var Result = daoGarantias.IngresarRevisionGarantiaInspeccionInicial(RevisionDeGarantia, InGolpeadaoRota, InHinchada, InBornesFlojos, InBornesFundidos, IngElectrolito, InFugaEnCubierta, InFugaEnBornes,InCCA);
        //        var Result2 = RegistrarRevisionDeGarantiaDetalleInspeccionInicialCeldas(Result, InDcC1, InDcC2,  InDcC3,  InDcC4,  InDcC5,  InDcC6);

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        throw;
        //    }
        //}
        //public bool RegistrarRevisionDeGarantiaDetalleInspeccionInicialCeldas(int DetalleInspeccionInicialId, int InDcC1,
        //int InDcC2, int InDcC3, int InDcC4, int InDcC5, int InDcC6)
        //{
        //    try
        //    {
        //        daoGarantias = new DaoGarantias();
        //        var Result = daoGarantias.IngresarRevisionGarantiaInspeccionInicialCeldas( DetalleInspeccionInicialId, InDcC1, InDcC2,  InDcC3,  InDcC4,  InDcC5,  InDcC6);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        throw;
        //    }
        //}
        //public bool RegistrarRevisionDeGarantiaDetalleTrabajoRealizado(int RevisionDeGarantia, string TrPruebaAltaResistencia, string TrCambioAcido, string TrRecargaBateria, string TrInspeccionEstructuraExt)
        //{
        //    try
        //    {
        //        daoGarantias = new DaoGarantias();
        //        var Result = daoGarantias.IngresarRevisionGarantiaTrabajoRealizado(RevisionDeGarantia,  TrPruebaAltaResistencia,  TrCambioAcido,  TrRecargaBateria,  TrInspeccionEstructuraExt);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        throw;
        //    }
        //}
        //public bool RegistrarRevisionDeGarantiaDetalleDiagnostico(int RevisionDeGarantia, string DBateriaBuenEstado, string DPresentaFallosFabricacion, string DDentroPeriodo, string DUsoAdecuado, string DAplicaGarantia)
        //{
        //    try
        //    {
        //        daoGarantias = new DaoGarantias();
        //        var Result = daoGarantias.IngresarRevisionGarantiaDiagnostico( RevisionDeGarantia,  DBateriaBuenEstado,  DPresentaFallosFabricacion,  DDentroPeriodo,  DUsoAdecuado,  DAplicaGarantia);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        throw;
        //    }
        //}
    }
}