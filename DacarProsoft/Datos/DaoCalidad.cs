using DacarDatos.Datos;
using DacarProsoft.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DacarProsoft.Datos
{
    public class DaoCalidad
    {
        public List<SelectListItem> ConsultarBaseDeDatos()
        {
            List<SelectListItem> lst = new List<SelectListItem>();

            string ruta= ObtenerRutaAccess();

            DirectoryInfo Dir2 = new DirectoryInfo(ruta);

            foreach (var file in Dir2.GetFiles("*", SearchOption.AllDirectories))
            {
                lst.Add(new SelectListItem
                {
                    Value = file.Name,
                    Text= System.IO.Path.GetFileNameWithoutExtension(file.Name)
                });

            }
                return lst;
        }

        public string ObtenerRutaAccess()
        {
            string result = "";
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var Listado = (from d in DB.RutaBasesAccessCalidad
                                   select new
                                   {
                                       d.RutaFisica
                                   }).FirstOrDefault();


                    result = Listado.RutaFisica;

                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return result;
                }
            }
        }
        public List<LineasMarcasPropias> ConsultarModelosMarcasPropias()
        {
            List<LineasMarcasPropias> lst = new List<LineasMarcasPropias>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = (from d in DB.LineasMarcasPropias
                               where d.Estado != false
                               select new
                               {
                                   d.LineasMarcasPropiasId,
                                   d.Referencia,
                                   d.Identificador
                               }).ToList();

                foreach (var x in Listado.Distinct())
                {
                    lst.Add(new LineasMarcasPropias
                    {
                        LineasMarcasPropiasId = x.LineasMarcasPropiasId,
                        Referencia = x.Referencia,
                        Identificador = x.Identificador
                    });
                }
                return lst;
            }
        }

        public List<SelectListItem> ModelosBateriasPorTipoVehiculo(string LineaVehiculo)
        {

            List<SelectListItem> lst = new List<SelectListItem>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = (from d in DB.DatosTecnicosCalidadBaterias
                               where d.Linea == LineaVehiculo
                               select new
                               {
                                   d.DatosTecnicosCalidadBateriasId,
                                   d.Modelo
                               }).ToList();

                foreach (var x in Listado)
                {
                    lst.Add(new SelectListItem()
                    {
                        Text = x.Modelo,
                        Value = x.DatosTecnicosCalidadBateriasId.ToString()
                    });

                }
                return lst;
            }
        }

        public List<SelectListItem> MarcaBateria()
        {

            List<SelectListItem> lst = new List<SelectListItem>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = (from d in DB.MarcaBateriaPruebaLaboratorio
                               select new
                               {
                                   d.Descripcion,
                                   d.MarcaBateriaPruebaLaboratorioId,
                                   d.Estado
                               }).ToList();

                foreach (var x in Listado)
                {
                    if (x.Estado==true) {
                        lst.Add(new SelectListItem()
                        {
                            Text = x.Descripcion,
                            Value = x.MarcaBateriaPruebaLaboratorioId.ToString()
                        });
                    }
                  
                }
                return lst;
            }
        }
        public List<SelectListItem> TipoNorma()
        {

            List<SelectListItem> lst = new List<SelectListItem>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = (from d in DB.TipoNormaPruebaLaboratorio
                               select new
                               {
                                   d.Descripcion,
                                   d.TipoNormaPruebaLaboratorioId,
                                   d.Estado
                               }).ToList();

                foreach (var x in Listado)
                {
                    if (x.Estado == true)
                    {
                        lst.Add(new SelectListItem()
                        {
                            Text = x.Descripcion,
                            Value = x.TipoNormaPruebaLaboratorioId.ToString()
                        });
                    }

                }
                return lst;
            }
        }
        public List<SelectListItem> Normativa()
        {

            List<SelectListItem> lst = new List<SelectListItem>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = (from d in DB.NormativaPruebaLaboratorio
                               select new
                               {
                                   d.Descripcion,
                                   d.NormativaPruebaLaboratorioId,
                                   d.Estado
                               }).ToList();

                foreach (var x in Listado)
                {
                    if (x.Estado == true)
                    {
                        lst.Add(new SelectListItem()
                        {
                            Text = x.Descripcion,
                            Value = x.NormativaPruebaLaboratorioId.ToString()
                        });
                    }

                }
                return lst;
            }
        }
        public List<SelectListItem> Separador()
        {

            List<SelectListItem> lst = new List<SelectListItem>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = (from d in DB.SeparadorPruebaLaboratorio
                               select new
                               {
                                   d.Descripcion,
                                   d.SeparadorPruebaLaboratorioId,
                                   d.Estado
                               }).ToList();

                foreach (var x in Listado)
                {
                    if (x.Estado == true)
                    {
                        lst.Add(new SelectListItem()
                        {
                            Text = x.Descripcion,
                            Value = x.SeparadorPruebaLaboratorioId.ToString()
                        });
                    }

                }
                return lst;
            }
        }
        public List<SelectListItem> TipoEnsayo()
        {

            List<SelectListItem> lst = new List<SelectListItem>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = (from d in DB.TipoEnsayoPruebaLaboratorio
                               select new
                               {
                                   d.Descripcion,
                                   d.TipoEnsayoPruebaLaboratorioId,
                                   d.Estado
                               }).ToList();

                foreach (var x in Listado)
                {
                    if (x.Estado == true)
                    {
                        lst.Add(new SelectListItem()
                        {
                            Text = x.Descripcion,
                            Value = x.TipoEnsayoPruebaLaboratorioId.ToString()
                        });
                    }

                }
                return lst;
            }
        }
        public int IngresarPruebaLaboratorio(DateTime FechaIngreso, int CodigoIngreso, string Marca, string TipoNorma, string Normativa, string PreAcondicionamiento, string TipoBateria, string Modelo, string Separador, string TipoEnsayo, string LoteEnsamble,
            string LoteCarga, int CCA, decimal Peso, decimal Voltaje, decimal DensidadIngreso, decimal DensidadPreAcondicionamiento, decimal TemperaturaIngreso, decimal TemperaturaPrueba, string DatoTeoricoPrueba, decimal ValorObjetivo, decimal ResultadoFinal,
            string Observaciones, decimal Calificacion, int CodigoBateria)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var result = new PruebaLaboratorioCalidad();
                    result.FechaIngreso = FechaIngreso;
                    result.CodigoIngreso = CodigoIngreso;
                    result.Marca = Marca;
                    result.TipoNorma = TipoNorma;
                    result.Normativa = Normativa;
                    result.PreAcondicionamiento = PreAcondicionamiento;
                    result.TipoBateria = TipoBateria;
                    result.Modelo = Modelo;
                    result.Separador = Separador;
                    result.TipoEnsayo = TipoEnsayo;
                    result.LoteEnsamble = LoteEnsamble;
                    result.LoteCarga = LoteCarga;
                    result.CCA = CCA;
                    result.Peso = Peso;
                    result.Voltaje = Voltaje;
                    result.DensidadIngreso = DensidadIngreso;
                    result.DensidadPreAcondicionamiento = DensidadPreAcondicionamiento;
                    result.TemperaturaIngreso = TemperaturaIngreso;
                    result.TemperaturaPrueba = TemperaturaPrueba;
                    result.DatoTeoricoPrueba = DatoTeoricoPrueba;
                    result.ValorObjetivo = ValorObjetivo;
                    result.ResultadoFinal = ResultadoFinal;
                    result.Observaciones = Observaciones;
                    result.ResultadoFinal = ResultadoFinal;
                    result.Calificacion = Calificacion;
                    result.CodigoBateria = CodigoBateria;
                    result.FechaRegistro = DateTime.Now;

                    DB.PruebaLaboratorioCalidad.Add(result);
                    DB.SaveChanges();

                    int resultId = result.PruebaLaboratorioCalidadId;
                    return resultId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }
        public List<ModelPruebaLaboratorioCalidad> ConsultarPruebasLaboratorio()
        {
            string fechaIngreso;
            string fechaRegistro;
            List<ModelPruebaLaboratorioCalidad> lst = new List<ModelPruebaLaboratorioCalidad>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = (from d in DB.PruebaLaboratorioCalidad
                               orderby d.PruebaLaboratorioCalidadId ascending
                               select new
                               {
                                   d.PruebaLaboratorioCalidadId,
                                   d.FechaIngreso,
                                   d.CodigoIngreso,
                                   d.Marca,
                                   d.TipoNorma,
                                   d.Normativa,
                                   d.PreAcondicionamiento,
                                   d.TipoBateria,
                                   d.Modelo,
                                   d.Separador,
                                   d.TipoEnsayo,
                                   d.LoteEnsamble,
                                   d.LoteCarga,
                                   d.CCA,
                                   d.Peso,
                                   d.Voltaje,
                                   d.DensidadIngreso,
                                   d.DensidadPreAcondicionamiento,
                                   d.TemperaturaIngreso,
                                   d.TemperaturaPrueba,
                                   d.DatoTeoricoPrueba,
                                   d.ValorObjetivo,
                                   d.ResultadoFinal,
                                   d.Observaciones,
                                   d.Calificacion,
                                   d.FechaRegistro,
                                   d.CodigoBateria
                               }).ToList();

                foreach (var x in Listado.Distinct())
                {
                    DateTime fecha = Convert.ToDateTime(x.FechaIngreso, CultureInfo.InvariantCulture);
                    fechaIngreso = fecha.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    DateTime fecha2 = Convert.ToDateTime(x.FechaRegistro, CultureInfo.InvariantCulture);
                    fechaRegistro = fecha2.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    lst.Add(new ModelPruebaLaboratorioCalidad
                    {
                        PruebaLaboratorioCalidadId=x.PruebaLaboratorioCalidadId,
                        FechaIngreso= fechaIngreso,
                        CodigoIngreso=x.CodigoIngreso,
                        Marca=x.Marca,
                        TipoNorma=x.TipoNorma,
                        Normativa=x.Normativa,
                        PreAcondicionamiento=x.PreAcondicionamiento,
                        TipoBateria=x.TipoBateria,
                        Modelo=x.Modelo,
                        Separador=x.Separador,
                        TipoEnsayo=x.TipoEnsayo,
                        LoteEnsamble=x.LoteEnsamble,
                        LoteCarga=x.LoteCarga,
                        CCA=x.CCA,
                        Peso=x.Peso,
                        Voltaje=x.Voltaje,
                        DensidadIngreso=x.DensidadIngreso,
                        DensidadPreAcondicionamiento=x.DensidadPreAcondicionamiento,
                        TemperaturaIngreso=x.TemperaturaIngreso,
                        TemperaturaPrueba=x.TemperaturaPrueba,
                        DatoTeoricoPrueba=Convert.ToString(Decimal.Round(Convert.ToDecimal(x.DatoTeoricoPrueba),2)),
                        ValorObjetivo=x.ValorObjetivo,
                        ResultadoFinal=x.ResultadoFinal,
                        Observaciones=x.Observaciones,
                        Calificacion=x.Calificacion,
                        FechaRegistro= fechaRegistro,
                        CodigoBateria=x.CodigoBateria.ToString()
                    });
                }
                return lst;
            }
        }

        public string ObtenerRutaSoftware()
        {
            string result = "";
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var Listado = (from d in DB.RutaInstalacionSoftware
                                   select new
                                   {
                                       d.DescripcionRuta
                                   }).FirstOrDefault();


                    result = Listado.DescripcionRuta;

                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return result;
                }
            }
        }
        public int ObtenerCodigoIngresoPrueba()
        {
            int result = 0;
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var Listado = (from d in DB.PruebaLaboratorioCalidad
                                   orderby d.PruebaLaboratorioCalidadId descending
                                   select new 
                                   {
                                       d.CodigoIngreso
                                   }).FirstOrDefault();


                    result = Listado.CodigoIngreso.Value+1;

                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return result;
                }
            }
        }
        public int ObtenerCodigoIngresoPruebaCCA()
        {
            int result = 0;
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var Listado = (from d in DB.PruebasLaboratorioCCA
                                   orderby d.PruebasLaboratorioCCAId descending
                                   select new
                                   {
                                       d.CodigoIngreso
                                   }).FirstOrDefault();

                    if (Listado == null) {
                        result = 5000;
                    }
                    else{
                        result = Listado.CodigoIngreso.Value + 1;
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return result;
                }
            }
        }

        public string ObtenerCCABateria(string modeloBateria)
        {
            string result = "";
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var Listado = (from d in DB.DatosTecnicosCalidadBaterias
                                   where d.Modelo==modeloBateria

                                   select new
                                   {
                                       d.CCA
                                   }).FirstOrDefault();


                    result = Listado.CCA;

                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return result;
                }
            }
        }
        public string ObtenerRcBateria(string modeloBateria)
        {
            string result = "";
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var Listado = (from d in DB.DatosTecnicosCalidadBaterias
                                   where d.Modelo == modeloBateria

                                   select new
                                   {
                                       d.RC
                                   }).FirstOrDefault();


                    result = Listado.RC;

                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return result;
                }
            }
        }
        public string ObtenerCapBateria(string modeloBateria)
        {
            string result = "";
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var Listado = (from d in DB.DatosTecnicosCalidadBaterias
                                   where d.Modelo == modeloBateria

                                   select new
                                   {
                                       d.CAP
                                   }).FirstOrDefault();


                    result = Listado.CAP;

                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return result;
                }
            }
        }
        public int ObtenerCodigoIngresoMedicionCarga()
        {
            int result = 0;
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var Listado = (from d in DB.IngresosMedicionCarga
                                   orderby d.IngresosMedicionCargaId descending
                                   select new
                                   {
                                       d.CodigoIngreso
                                   }).FirstOrDefault();

                    if (Listado != null)
                    {
                        result = Listado.CodigoIngreso.Value + 1;
                    }
                    else {
                        result = 1;
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return result;
                }
            }
        }

        public List<ModelPruebaLaboratorioCalidad> ConsultarIngresosMedicionesVoltaje()
        {
            string fechaIngreso;
            string fechaRegistro;
            List<ModelPruebaLaboratorioCalidad> lst = new List<ModelPruebaLaboratorioCalidad>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = (from d in DB.IngresosMedicionCarga
                               orderby d.IngresosMedicionCargaId descending
                               select new
                               {
                                   d.IngresosMedicionCargaId,
                                   d.FechaPrueba,
                                   d.CodigoIngreso,
                                   d.Marca,                              
                                   d.PreAcondicionamiento,
                                   d.TipoBateria,
                                   d.Modelo,
                                   d.Separador,
                                   d.LoteEnsamble,
                                   d.LoteCarga,
                                   d.Peso,
                                   //d.Voltaje,
                                   d.FechaRegistro
                               }).ToList();

                foreach (var x in Listado.Distinct())
                {
                    DateTime fecha = Convert.ToDateTime(x.FechaPrueba, CultureInfo.InvariantCulture);
                    fechaIngreso = fecha.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    DateTime fecha2 = Convert.ToDateTime(x.FechaRegistro, CultureInfo.InvariantCulture);
                    fechaRegistro = fecha2.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    lst.Add(new ModelPruebaLaboratorioCalidad
                    {
                        PruebaLaboratorioCalidadId = x.IngresosMedicionCargaId,
                        FechaIngreso = fechaIngreso,
                        CodigoIngreso = x.CodigoIngreso,
                        Marca = x.Marca,         
                        PreAcondicionamiento = x.PreAcondicionamiento,
                        TipoBateria = x.TipoBateria,
                        Modelo = x.Modelo,
                        Separador = x.Separador,
                        LoteEnsamble = x.LoteEnsamble,
                        LoteCarga = x.LoteCarga,
                        Peso = x.Peso,
                        //Voltaje = x.Voltaje,                  
                        FechaRegistro = fechaRegistro
                    });
                }
                return lst;
            }
        }
        public int IngresarPruebaMedicionBateria(DateTime FechaIngreso, int CodigoIngreso, string Marca, string PreAcondicionamiento, string TipoBateria, string Modelo, string Separador, string LoteEnsamble,
          string LoteCarga, decimal Peso, decimal Voltaje)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var result = new IngresosMedicionCarga();
                    result.FechaPrueba = FechaIngreso;
                    result.CodigoIngreso = CodigoIngreso;
                    result.Marca = Marca;
                    result.PreAcondicionamiento = PreAcondicionamiento;
                    result.TipoBateria = TipoBateria;
                    result.Modelo = Modelo;
                    result.Separador = Separador;
                    result.LoteEnsamble = LoteEnsamble;
                    result.LoteCarga = LoteCarga;
                    result.Peso = Peso;
                    //result.Voltaje = Voltaje;
                  
                    result.FechaRegistro = DateTime.Now;

                    DB.IngresosMedicionCarga.Add(result);
                    DB.SaveChanges();

                    int resultId = result.IngresosMedicionCargaId;
                    return resultId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public bool IngresarDetalleMedicionBateria(int IngresosMedicionCargaId, DateTime FechaPrueba, decimal Voltaje)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var result = new IngresosDetallesMedicionCarga();
                    result.IngresosMedicionCargaId = IngresosMedicionCargaId;
                    result.FechaPrueba = FechaPrueba;
                    result.voltaje = Voltaje;
                    result.FechaRegistro = DateTime.Now;

                    DB.IngresosDetallesMedicionCarga.Add(result);
                    DB.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
        public List<ModelPruebaLaboratorioCalidad> ConsultarMedicionesDeDescargas()
        {
            string fechaIngreso;
            string fechaRegistro;
            List<ModelPruebaLaboratorioCalidad> lst = new List<ModelPruebaLaboratorioCalidad>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = (from d in DB.IngresosMedicionCarga
                               orderby d.IngresosMedicionCargaId descending
                               select new
                               {   
                                   d.IngresosMedicionCargaId,
                                   d.FechaPrueba,
                                   d.CodigoIngreso,
                                   d.Marca,                                
                                   d.PreAcondicionamiento,
                                   d.TipoBateria,
                                   d.Modelo,
                                   d.Separador,
                                   d.LoteEnsamble,
                                   d.LoteCarga,
                                   d.Peso,                              
                                   d.FechaRegistro
                               }).ToList();

                foreach (var x in Listado.Distinct())
                {
                    int Contador=ContarRegistrosMedicionesDeDescargas(x.IngresosMedicionCargaId);
                    DateTime fecha = Convert.ToDateTime(x.FechaPrueba, CultureInfo.InvariantCulture);
                    fechaIngreso = fecha.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    DateTime fecha2 = Convert.ToDateTime(x.FechaRegistro, CultureInfo.InvariantCulture);
                    fechaRegistro = fecha2.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    lst.Add(new ModelPruebaLaboratorioCalidad
                    {
                        PruebaLaboratorioCalidadId = x.IngresosMedicionCargaId,
                        FechaIngreso = fechaIngreso,
                        CodigoIngreso = x.CodigoIngreso,
                        Marca = x.Marca,                      
                        PreAcondicionamiento = x.PreAcondicionamiento,
                        TipoBateria = x.TipoBateria,
                        Modelo = x.Modelo,
                        Separador = x.Separador,
                        LoteEnsamble = x.LoteEnsamble,
                        LoteCarga = x.LoteCarga,
                        Peso = x.Peso,                      
                        FechaRegistro = fechaRegistro,
                        ContadorRegistros=Contador
                    });
                }
                return lst;
            }
        }
        public List<ModelPruebaLaboratorioCalidad> ConsultarMedicionesDeDescargasPorId(int idMedicionDescarga)
        {
            string fechaIngreso;
            string fechaRegistro;
            List<ModelPruebaLaboratorioCalidad> lst = new List<ModelPruebaLaboratorioCalidad>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = (from d in DB.IngresosMedicionCarga
                               where d.IngresosMedicionCargaId==idMedicionDescarga
                               orderby d.IngresosMedicionCargaId descending
                               select new
                               {
                                   d.IngresosMedicionCargaId,
                                   d.FechaPrueba,
                                   d.CodigoIngreso,
                                   d.Marca,
                                   d.PreAcondicionamiento,
                                   d.TipoBateria,
                                   d.Modelo,
                                   d.Separador,
                                   d.LoteEnsamble,
                                   d.LoteCarga,
                                   d.Peso,
                                   d.FechaRegistro
                               }).ToList();

                foreach (var x in Listado.Distinct())
                {
                    int Contador = ContarRegistrosMedicionesDeDescargas(x.IngresosMedicionCargaId);
                    DateTime fecha = Convert.ToDateTime(x.FechaPrueba, CultureInfo.InvariantCulture);
                    fechaIngreso = fecha.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    DateTime fecha2 = Convert.ToDateTime(x.FechaRegistro, CultureInfo.InvariantCulture);
                    fechaRegistro = fecha2.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    lst.Add(new ModelPruebaLaboratorioCalidad
                    {
                        PruebaLaboratorioCalidadId = x.IngresosMedicionCargaId,
                        FechaIngreso = fechaIngreso,
                        CodigoIngreso = x.CodigoIngreso,
                        Marca = x.Marca,
                        PreAcondicionamiento = x.PreAcondicionamiento,
                        TipoBateria = x.TipoBateria,
                        Modelo = x.Modelo,
                        Separador = x.Separador,
                        LoteEnsamble = x.LoteEnsamble,
                        LoteCarga = x.LoteCarga,
                        Peso = x.Peso,
                        FechaRegistro = fechaRegistro,
                        ContadorRegistros = Contador
                    });
                }
                return lst;
            }
        }
        public int ContarRegistrosMedicionesDeDescargas(int IngresosMedicionCargaId)
        {
            int result = 0;
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var Listado = (from d in DB.IngresosDetallesMedicionCarga
                                   where d.IngresosMedicionCargaId==IngresosMedicionCargaId
                                   orderby d.IngresosMedicionCargaId descending
                                   select new
                                   {
                                       d.IngresosMedicionCargaId
                                   }).Count();

                    

                    return Listado;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return result;
                }
            }
        }
        public List<ModelPruebaLaboratorioCalidad> ConsultarDetalleMedicionDescarga(int idMedicionDescarga)
        {
            string fechaIngreso;
            List<ModelPruebaLaboratorioCalidad> lst = new List<ModelPruebaLaboratorioCalidad>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = (from d in DB.IngresosDetallesMedicionCarga
                               where d.IngresosMedicionCargaId== idMedicionDescarga
                               orderby d.IngresosDetallesMedicionCargaId 
                               ascending
                               select new
                               {
                                 d.IngresosDetallesMedicionCargaId,
                                 d.FechaPrueba,
                                 d.voltaje
                               }).ToList();

                foreach (var x in Listado.Distinct())
                {
                    DateTime fecha = Convert.ToDateTime(x.FechaPrueba, CultureInfo.InvariantCulture);
                    fechaIngreso = fecha.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    
                    lst.Add(new ModelPruebaLaboratorioCalidad
                    {
                        PruebaLaboratorioCalidadId = x.IngresosDetallesMedicionCargaId,
                        FechaIngreso = fechaIngreso,
                        Voltaje=x.voltaje
                    });
                }
                return lst;
            }
        }
    }
}