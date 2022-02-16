using DacarProsoft.Datos;
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
                var conexionAcc= conexion.open(nombre);

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


        public bool RegistrarPruebasLaboratorio(DateTime FechaIngreso, int CodigoIngreso,string Marca, string TipoNorma, string Normativa, string PreAcondicionamiento, string TipoBateria, string Modelo, string Separador, string TipoEnsayo, string LoteEnsamble,
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
            catch(Exception ex) {
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

                string path = Path.Combine(Server.MapPath("~/Images/AnexosLaboratorio/" + modelo + "/"+FechaRegistro + "/"));

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
                            Value = rutaSoft + rutaAlt +file.Name,
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

    }
}