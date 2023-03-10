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
        private DaoUsuarios daoUsuarios { get; set; } = null;


        // GET: Garantias
        public ActionResult IngresoGarantias()
        {
            if (Session["usuario"] != null)
            {

                ViewBag.JavaScript = "General/" + RouteData.Values["controller"] + "/" + RouteData.Values["action"];
                ViewBag.dxdevweb = "1";

                ViewBag.MenuAcceso = Session["Menu"];

                daoUtilitarios = new DaoUtilitarios();
                daoGarantias = new DaoGarantias();
                daoUsuarios = new DaoUsuarios();

                var datClientesSap = daoUsuarios.ConsutaClientesSap();
                ViewBag.ClientesSap = datClientesSap;

                //ConsutaClientesSap
                var datvendedor = daoGarantias.ConsultarVendedores();
                ViewBag.Vendedores = datvendedor;

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

        public JsonResult ConsultarModelosBaterias()
        {
            daoGarantias = new DaoGarantias();
            var modelos = daoGarantias.ConsultarReferenciasModelosMarcasPropias();
            return Json(new SelectList(modelos, "ModelosMarcasPropiasId", "Referencia"));
        }

        public JsonResult ConsultarClientesSap()
        {
            daoUsuarios = new DaoUsuarios();
            var datClientesSap = daoUsuarios.ConsutaClientesSap();
            return Json(new SelectList(datClientesSap, "CardCode", "NombreCliente"));
        }

        public JsonResult ConsultarProvincias()
        {
            daoGarantias = new DaoGarantias();
            var provincia = daoGarantias.ConsultarProvincias();
            return Json(new SelectList(provincia, "id", "provincia"));
        }

        public int RegistrarRevisionDeGarantiaCabecera(string cliente, string cedula, string numeroGarantia, string numeroComprobante, string numeroFactura, string provincia, string direccion, string vendedor, HttpPostedFileBase ImgFac, string marca,
            string modelo, string lote, string prorrateo, string meses, string fechaVenta, string fechaIngreso, string porcentajeVenta, string voltaje, string loteEnsamble, HttpPostedFileBase ImgTest)
        {
            var clienteCorregido = cliente.Replace("\"", "");
            string filename2 = null;
            string filename = null;
            string destinoImg2 = null;
            string destinoImg1 = null;
            try
            {
                daoGarantias = new DaoGarantias();

                if (ImgFac != null)
                {
                    filename = Path.GetExtension(ImgFac.FileName);
                    var destinationPath = Path.Combine(Server.MapPath("~/Images/ImagenesGarantias/Facturas/"), numeroFactura + "-" + numeroGarantia + "-" + cedula);
                    ImgFac.SaveAs(destinationPath + filename);
                    destinoImg1 = numeroFactura + "-" + numeroGarantia + "-" + cedula + filename;
                }
                    

                if (ImgTest!=null) {
                    filename2 = Path.GetExtension(ImgTest.FileName);
                    var destinationPath2 = Path.Combine(Server.MapPath("~/Images/ImagenesGarantias/Test/"), numeroFactura + "-" + numeroGarantia + "-" + cedula);
                    ImgTest.SaveAs(destinationPath2 + filename2);
                    destinoImg2 = numeroFactura + "-" + numeroGarantia + "-" + cedula + filename2;
                }

                var Result = daoGarantias.IngresarRevisionGarantiaCabecera(clienteCorregido,  cedula,  numeroGarantia,  numeroComprobante, numeroFactura,  provincia,  direccion,  vendedor, destinoImg1,  marca,
                modelo,  lote,  prorrateo,  meses,  fechaVenta,  fechaIngreso,  porcentajeVenta,  voltaje, loteEnsamble,destinoImg2);
                return Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public bool RegistrarRevisionDeGarantiaDetalle(int RevisionId, string InGolpeadaoRota, string InHinchada, string InBornesFlojos, string InBornesFundidos, string IngElectrolito, string InFugaEnCubierta, string InFugaEnBornes,string InRevisionesPeriodicas, int InDcC1,
            int InDcC2, int InDcC3, int InDcC4, int InDcC5, int InDcC6, int InCCA, string TrPruebaAltaResistencia, string TrCambioAcido, string TrRecargaBateria, string TrInspeccionEstructuraExt, string DBateriaBuenEstado, string DPresentaFallosFabricacion,
       string DDentroPeriodo, string DUsoAdecuado/*, string DAplicaGarantia*/, bool AplicaGarantia, bool IngresoManual, string Cliente)
        {
            string valor;
            string valor2;
            string valor3;

            if (AplicaGarantia == true)
            {
                valor = "No Aplica";
                valor3 = "false";
            }
            else {
                valor = "Si Aplica";
                valor3 = "true";
            }
            if (IngresoManual == true)
            {
                valor2 = "Manual";
            }
            else
            {
                valor2 = "Automatico";

            }
            try
            {
                daoGarantias = new DaoGarantias();

                var Result = daoGarantias.IngresarRevisionGarantiaInspeccionInicial(RevisionId, InGolpeadaoRota, InHinchada, InBornesFlojos, InBornesFundidos, IngElectrolito, InFugaEnCubierta, InFugaEnBornes, InCCA, InRevisionesPeriodicas);
                var Result2 = daoGarantias.IngresarRevisionGarantiaInspeccionInicialCeldas(Result, InDcC1, InDcC2, InDcC3, InDcC4, InDcC5, InDcC6);
                var Result3 = daoGarantias.IngresarRevisionGarantiaTrabajoRealizado(RevisionId, TrPruebaAltaResistencia, TrCambioAcido, TrRecargaBateria, TrInspeccionEstructuraExt);
                var Result4 = daoGarantias.IngresarRevisionGarantiaDiagnostico(RevisionId, DBateriaBuenEstado, DPresentaFallosFabricacion, DDentroPeriodo, DUsoAdecuado, valor3);
                var Result5 = daoGarantias.ActualizarRegistroCabecera(RevisionId, valor, valor2, Cliente);



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

        public JsonResult CantonesEcuador(int id)
        {
            daoGarantias = new DaoGarantias();

            var cantones = daoGarantias.ConsultarCantones(id);
            return Json(new SelectList(cantones, "Id", "canton"));
        }

        public ActionResult ConsultaRevisionesTecnicas()
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

        public JsonResult ConsultarRevisionesTecnica()
        {
            try
            {
                daoGarantias = new DaoGarantias();
                var Result = daoGarantias.ConsultarRevisionesTecnicas();
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public int ObtenerNumeroComprobante()
        {
            try
            {
                daoGarantias = new DaoGarantias();
                int secuencial = daoGarantias.ObtenerNumeroSecuencial();
                int NumeroComprobante = daoGarantias.ObtenerNumeroCombrobante(secuencial);
                return NumeroComprobante;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ConsultarInspeccionInicial(int IdCabeceraInspeccion)
        {
            try
            {
                daoGarantias = new DaoGarantias();
                var Result = daoGarantias.ConsultaInspeccionInicial(IdCabeceraInspeccion);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ConsultarInspeccionInicialDensidadCelda(int IdIngresoGarantiaInspeccionInicial)
        {
            try
            {
                daoGarantias = new DaoGarantias();
                var Result = daoGarantias.ConsultaInspeccionInicialDensidadCelda(IdIngresoGarantiaInspeccionInicial);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ConsultarDiagnostico(int IdCabeceraInspeccion)
        {
            try
            {
                daoGarantias = new DaoGarantias();
                var Result = daoGarantias.ConsultaDiagnostico(IdCabeceraInspeccion);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public JsonResult ConsultarTrabajoRealizado(int IdCabeceraInspeccion)
        {
            try
            {
                daoGarantias = new DaoGarantias();
                var Result = daoGarantias.ConsultaTrabajoRealizado(IdCabeceraInspeccion);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public JsonResult ConsultarProrrateo(int MarcaPropiasId, string MarcaPropiasTexto, decimal PvpVentas, DateTime FechaIngreso, DateTime FechaVenta)
        {
            try
            {
                daoGarantias = new DaoGarantias();
                var Result = daoGarantias.ConsultaInfoProrrateo(MarcaPropiasId, MarcaPropiasTexto,PvpVentas, FechaIngreso, FechaVenta);
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public decimal ConsultarValorBateria(string MarcaPropiasTexto)
        {
            try
            {
                daoGarantias = new DaoGarantias();
                var Result = daoGarantias.ObtenerPrecioBateria(MarcaPropiasTexto);
                return Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}