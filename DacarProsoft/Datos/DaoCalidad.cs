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
                               orderby d.FechaRegistro ascending
                               orderby d.CodigoIngreso descending
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
                                   orderby d.FechaRegistro descending
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
        public List<PackingIngresados> ConsultarPackingIngreseadosLiberacionProducto()
        {
            string estado;
            int cantMediciones;
            CultureInfo ci = new CultureInfo("es-MX");
            ci = new CultureInfo("es-MX");
            TextInfo textInfo = ci.TextInfo;
            List<PackingIngresados> lst = new List<PackingIngresados>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoCabecera = from d in DB.Packing
                                      orderby d.FechaRegistro descending
                                      where d.LiberacionPacking==false
                                      select new
                                      {
                                          d.PackingId,
                                          d.NumeroDocumento,
                                          d.NumeroOrden,
                                          d.NombreCliente,
                                          d.Destino,
                                          d.CantidadPallet,
                                          d.FechaRegistro,
                                          d.NumeroContenedor
                                      };

                foreach (var x in ListadoCabecera)
                {
                    cantMediciones = CantidadMedicionesPackingList(x.PackingId);
                    DateTime fechaDoc = Convert.ToDateTime(x.FechaRegistro, CultureInfo.InvariantCulture);
                    string fechaDocumento = fechaDoc.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    var PalletFaltante = PalletFantantes(x.PackingId, x.CantidadPallet.Value);
                    if (PalletFaltante == 0)
                    {
                        estado = "Completo";
                    }
                    else
                    {
                        estado = "Incompleto";
                    }
                   
                    lst.Add(new PackingIngresados
                    {
                        PackingId = x.PackingId,
                        NumeroDocumento = x.NumeroDocumento.Value,
                        NumeroOrden = x.NumeroOrden,
                        NombreCliente = x.NombreCliente,
                        Destino = x.Destino,
                        CantidadPallet = x.CantidadPallet.Value,
                        PalletFaltantes = PalletFaltante,
                        Estado = estado,
                        FechaRegistro = fechaDocumento,
                        cantidadMediciones=cantMediciones,
                        NumeroContenedor=x.NumeroContenedor.Value
                    });
                }
                return lst;
            }
        }
        public int PalletFantantes(int PackingId, int TotalPallet)
        {
            int Contador = 0;
            int Total = 0;
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoCabecera = from d in DB.PalletPacking
                                      where d.PackingId == PackingId
                                      select new
                                      {
                                          d.PalletNumber
                                      };
                foreach (var x in ListadoCabecera)
                {
                    Contador = Contador + 1;
                }

                Total = TotalPallet - Contador;

                return Total;
            }
        }
        public int CantidadMedicionesPackingList(int PackingId)
        {
            int Total = 0;
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try {
                    var ListadoCabecera = (from d in DB.MedicionPalletPacking
                                           where d.PackingId == PackingId
                                           orderby d.MedicionPalletPackingId descending
                                           select new
                                           {
                                               d.NumeroMedicion
                                           }).Count();
                    if (ListadoCabecera != 0)
                    {
                        Total = ListadoCabecera;

                    }
                    return Total;
                }
                catch (Exception ex) {
                    Console.WriteLine(ex);
                    return Total;
                }
               
            }
        }
        public List<PalletPackingCant> ConsultarPalletCant(int PackingId)
        {
            List<PalletPackingCant> lst = new List<PalletPackingCant>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                int cantMediciones;
                var ListadoPallet = from d in DB.PalletPacking
                                    where d.PackingId == PackingId
                                    select new
                                    {
                                        d.PalletPacking1,
                                        d.PackingId,
                                        d.PalletNumber,
                                        d.AnchoPallet,
                                        d.LargoPallet,
                                        d.AltoPallet,
                                        d.VolumenPallet,
                                        d.PesoNeto,
                                        d.PesoBruto
                                    };
                foreach (var x in ListadoPallet)
                {
                    cantMediciones = CantidadMedicionesPackingListPallet(PackingId, x.PalletPacking1);
                    var cant = obtenerCantidadItemPallet(x.PalletPacking1);
                    lst.Add(new PalletPackingCant
                    {
                        PalletPacking1 = x.PalletPacking1,
                        PackingId = x.PackingId.Value,
                        PalletNumber = x.PalletNumber.Value,
                        AnchoPallet = x.AnchoPallet.Value,
                        LargoPallet = x.LargoPallet.Value,
                        AltoPallet = x.AltoPallet.Value,
                        VolumenPallet = Decimal.Round(x.VolumenPallet.Value, 2),
                        PesoNeto = Decimal.Round(x.PesoNeto.Value, 2),
                        PesoBruto = Decimal.Round(x.PesoBruto.Value, 2),
                        Cantidad = cant,
                        CantidadMediciones=cantMediciones
                    });
                }

                return lst;
            }
        }
        public int CantidadMedicionesPackingListPallet(int PackingId, int PalletId)
        {
            int Total = 0;
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var ListadoCabecera = (from d in DB.MedicionPalletPacking
                                           where d.PackingId == PackingId && d.PalletId==PalletId
                                           orderby d.MedicionPalletPackingId descending
                                           select new
                                           {
                                               d.NumeroMedicion
                                           }).Count();
                    if (ListadoCabecera != 0)
                    {
                        Total = ListadoCabecera;

                    }
                    return Total;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return Total;
                }

            }
        }
        public int obtenerCantidadItemPallet(int PalletPackingId)
        {
            int acum = 0;
            List<PalletPacking> lst = new List<PalletPacking>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoPallet = from d in DB.PalletPackingDetalle
                                    where d.PalletPacking == PalletPackingId
                                    select new
                                    {
                                        d.CantidadItem
                                    };
                foreach (var x in ListadoPallet)
                {
                    acum = acum + x.CantidadItem.Value;
                }

            }
            return acum;
        }
        public int NumeroMedicionesPackingList()
        {
            using (DacarProsoftEntities DB= new DacarProsoftEntities()) {
                try
                {
                    int valor = 0;
                    var res = (from d in DB.NumeroMedicionesPackingList
                              select new
                              {
                                  d.NumeroMediciones
                              }).FirstOrDefault();
                    valor = res.NumeroMediciones.Value;

                    return valor;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return 0;
                }
            }
              
        }
        public List<MedicionPalletPacking> ConsultarMedicionPallet(int PackinkId, int PalletId) {

            List<MedicionPalletPacking> lst = new List<MedicionPalletPacking>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities()) {
                try
                {
                    var res = from d in DB.MedicionPalletPacking
                              where d.PackingId == PackinkId && d.PalletId==PalletId
                              select new
                              {        
                                  d.MedicionPalletPackingId,
                                  d.PackingId,
                                  d.PalletId,
                                  d.NumeroMedicion,
                                  d.NumeroLote,
                                  d.Modelo,
                                  d.Voltaje,
                                  d.nivel,
                                  d.Acabado,
                                  d.Limpieza,
                                  d.CCA,
                                  d.FechaRegistro
                              };
                    foreach (var x in res) {

                        lst.Add(new MedicionPalletPacking
                        {
                            MedicionPalletPackingId=x.MedicionPalletPackingId,
                            PackingId=x.PackingId,
                            PalletId=x.PalletId,
                            NumeroMedicion=x.NumeroMedicion,
                            NumeroLote=x.NumeroLote,
                            Modelo=x.Modelo,
                            Voltaje=x.Voltaje,
                            nivel=x.nivel.Value,
                            Acabado=x.Acabado.Value,
                            Limpieza=x.Limpieza.Value,
                            CCA=x.CCA,
                            FechaRegistro=x.FechaRegistro
                        });
                    }
                    return lst;
                }
                catch {
                    return null;
                }
            }
        }
        public bool InsertarMedicionPalletPaking(int packingId, int palletId, string numeroLote, string modelo, decimal voltaje, bool nivel, bool acabado, bool limpieza, decimal CCA)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var valorMedi = CantidadMedicionesPackingListPallet(packingId, palletId);
                    var result = new MedicionPalletPacking();
                    result.PackingId = packingId;
                    result.PalletId=palletId;
                    result.NumeroMedicion= valorMedi+1;
                    result.NumeroLote = numeroLote;
                    result.Modelo = modelo;
                    result.Voltaje = voltaje;
                    result.nivel = nivel;
                    result.Acabado = acabado;
                    result.Limpieza = limpieza;
                    result.CCA = CCA;
                    result.FechaRegistro = DateTime.Now;
               
                    DB.MedicionPalletPacking.Add(result);
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
        public List<PalletPackingDetalle> ConsultarModelosProPallet(int PackinkId, int PalletId)
        {

            List<PalletPackingDetalle> lst = new List<PalletPackingDetalle>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var res = from d in DB.PalletPackingDetalle
                              where d.PackingId == PackinkId && d.PalletPacking == PalletId
                              select new
                              {
                                  d.PalletPackingDetalleId,
                                  d.ItemCode,
                                  d.DescriptionCode,
                              };
                    foreach (var x in res)
                    {

                        lst.Add(new PalletPackingDetalle
                        {
                            PalletPackingDetalleId=x.PalletPackingDetalleId,
                            ItemCode=x.ItemCode,
                            DescriptionCode=x.DescriptionCode     
                        });
                    }
                    return lst;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                    return lst;
                }
            }
        }
        public bool EliminarMedicionPallet(int MedicionId)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    DB.MedicionPalletPacking.RemoveRange(DB.MedicionPalletPacking.Where(x => x.MedicionPalletPackingId == MedicionId));
                    DB.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;

                }
            }

        }
        public bool ActualizarMedicionPallet(MedicionPalletPacking medicion, int Key)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var result = (from a in DB.MedicionPalletPacking
                                  where a.MedicionPalletPackingId == Key
                                  select a).FirstOrDefault();


                    

                    result.NumeroLote = medicion.NumeroLote;
                    result.Modelo = medicion.Modelo;
                    result.Voltaje = medicion.Voltaje;
                    result.nivel = medicion.nivel;
                    result.Acabado = medicion.Acabado;
                    result.Limpieza = medicion.Limpieza;

                    result.FechaActualizacion = DateTime.Now;

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
        public bool ActualizarEstadoPackingList(int packingId)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var result = (from a in DB.Packing
                                  where a.PackingId==packingId
                                  select a).FirstOrDefault();

                    result.LiberacionPacking = true;
                    result.FechaLiberacion = DateTime.Now;
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
        public List<PackingIngresados> ConsultarPackingLiberados()
        {
            string estado;
            int cantMediciones;
            CultureInfo ci = new CultureInfo("es-MX");
            ci = new CultureInfo("es-MX");
            TextInfo textInfo = ci.TextInfo;
            List<PackingIngresados> lst = new List<PackingIngresados>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoCabecera = from d in DB.Packing
                                      orderby d.FechaRegistro descending
                                      where d.LiberacionPacking == true
                                      select new
                                      {
                                          d.PackingId,
                                          d.NumeroDocumento,
                                          d.NumeroOrden,
                                          d.NombreCliente,
                                          d.Destino,
                                          d.CantidadPallet,
                                          d.FechaLiberacion
                                      };

                foreach (var x in ListadoCabecera)
                {
                    cantMediciones = CantidadMedicionesPackingList(x.PackingId);
                    DateTime fechaDoc = Convert.ToDateTime(x.FechaLiberacion, CultureInfo.InvariantCulture);
                    string fechaDocumento = fechaDoc.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    var PalletFaltante = PalletFantantes(x.PackingId, x.CantidadPallet.Value);
                    if (PalletFaltante == 0)
                    {
                        estado = "Completo";
                    }
                    else
                    {
                        estado = "Incompleto";
                    }

                    lst.Add(new PackingIngresados
                    {
                        PackingId = x.PackingId,
                        NumeroDocumento = x.NumeroDocumento.Value,
                        NumeroOrden = x.NumeroOrden,
                        NombreCliente = x.NombreCliente,
                        Destino = x.Destino,
                        CantidadPallet = x.CantidadPallet.Value,
                        PalletFaltantes = PalletFaltante,
                        Estado = estado,
                        FechaRegistro = fechaDocumento,
                        cantidadMediciones = cantMediciones
                    });
                }
                return lst;
            }
        }
        public List<PackingIngresados> ConsultarPackingLiberado(int packingId)
        {
            string estado;
            int cantMediciones;
            CultureInfo ci = new CultureInfo("es-MX");
            ci = new CultureInfo("es-MX");
            TextInfo textInfo = ci.TextInfo;
            List<PackingIngresados> lst = new List<PackingIngresados>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoCabecera = from d in DB.Packing
                                      orderby d.FechaRegistro descending
                                      where d.LiberacionPacking == true && d.PackingId== packingId
                                      select new
                                      {
                                          d.PackingId,
                                          d.NumeroDocumento,
                                          d.NumeroOrden,
                                          d.NombreCliente,
                                          d.Destino,
                                          d.CantidadPallet,
                                          d.FechaLiberacion
                                      };

                foreach (var x in ListadoCabecera)
                {
                    cantMediciones = CantidadMedicionesPackingList(x.PackingId);
                    DateTime fechaDoc = Convert.ToDateTime(x.FechaLiberacion, CultureInfo.InvariantCulture);
                    string fechaDocumento = fechaDoc.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    var PalletFaltante = PalletFantantes(x.PackingId, x.CantidadPallet.Value);
                    if (PalletFaltante == 0)
                    {
                        estado = "Completo";
                    }
                    else
                    {
                        estado = "Incompleto";
                    }

                    lst.Add(new PackingIngresados
                    {
                        PackingId = x.PackingId,
                        NumeroDocumento = x.NumeroDocumento.Value,
                        NumeroOrden = x.NumeroOrden,
                        NombreCliente = x.NombreCliente,
                        Destino = x.Destino,
                        CantidadPallet = x.CantidadPallet.Value,
                        PalletFaltantes = PalletFaltante,
                        Estado = estado,
                        FechaRegistro = fechaDocumento,
                        cantidadMediciones = cantMediciones
                    });
                }
                return lst;
            }
        }
        public List<MedicionPalletPacking> ConsultarMedicionPackingGeneral(int PackinkId)
        {

            List<MedicionPalletPacking> lst = new List<MedicionPalletPacking>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var res = from d in DB.MedicionPalletPacking
                              where d.PackingId == PackinkId
                              orderby d.PalletId ascending
                              select new
                              {
                                  d.MedicionPalletPackingId,
                                  d.PackingId,
                                  d.PalletId,
                                  d.NumeroMedicion,
                                  d.NumeroLote,
                                  d.Modelo,
                                  d.Voltaje,
                                  d.nivel,
                                  d.Acabado,
                                  d.Limpieza,
                                  d.CCA,
                                  d.FechaRegistro
                              };
                    foreach (var x in res)
                    {

                        lst.Add(new MedicionPalletPacking
                        {
                            MedicionPalletPackingId = x.MedicionPalletPackingId,
                            PackingId = x.PackingId,
                            PalletId = x.PalletId,
                            NumeroMedicion = x.NumeroMedicion,
                            NumeroLote = x.NumeroLote,
                            Modelo = x.Modelo,
                            Voltaje = x.Voltaje,
                            nivel = x.nivel.Value,
                            Acabado = x.Acabado.Value,
                            Limpieza = x.Limpieza.Value,
                            CCA = x.CCA,
                            FechaRegistro = x.FechaRegistro
                        });
                    }
                    return lst;
                }
                catch
                {
                    return null;
                }
            }
        }
        public int ObtenerNumeroPallet(int palletId)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    int valor = 0;
                    var res = (from d in DB.PalletPacking
                               where d.PalletPacking1== palletId
                               select new
                               {
                                   d.PalletNumber
                               }).FirstOrDefault();
                    valor = res.PalletNumber.Value;

                    return valor;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return 0;
                }
            }

        }
        public int BusquedaxDias()
        {
            int dias = 0;
            List<DiasBusqueda> lst = new List<DiasBusqueda>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoModelos = from d in DB.DiasBusqueda
                                     select new
                                     {
                                         d.NumeroDias
                                     };
                foreach (var x in ListadoModelos)
                {
                    dias = x.NumeroDias.Value;
                }
                return dias;
            }
        }
        public List<CabeceraOrdenVenta> ListadoCabeceraOrdenesVentasSap()
        {
            var dias = BusquedaxDias();
            DateTime fechaActual = DateTime.Now;
            DateTime fechaCorte = fechaActual.AddDays(-(dias));
            CultureInfo ci = new CultureInfo("es-MX");
            ci = new CultureInfo("es-MX");
            TextInfo textInfo = ci.TextInfo;
            List<CabeceraOrdenVenta> lst = new List<CabeceraOrdenVenta>();
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                var ListadoCabeceraOrdenesVentas = from d in DB.ORDR
                                                   where d.U_BPP_MDMT == "OFICINA" && d.DocDate >= fechaCorte && d.DocDate <= fechaActual
                                                   orderby d.DocDate descending
                                                   select new
                                                   {
                                                       d.DocEntry,
                                                       d.DocNum,
                                                       d.DocDate,
                                                       d.TaxDate,
                                                       d.CardCode,
                                                       d.CardName,
                                                       d.DocTotal,
                                                       d.U_SYP_NUMOCCL,
                                                   };

                foreach (var x in ListadoCabeceraOrdenesVentas)
                {
                    var busqueda = BusquedaLocalPedidoLocal(x.DocNum);
                    if (busqueda == false)
                    {
                        DateTime fechaCont = Convert.ToDateTime(x.DocDate, CultureInfo.InvariantCulture);
                        string fechaContabilizacion = fechaCont.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        DateTime fechaDoc = Convert.ToDateTime(x.TaxDate, CultureInfo.InvariantCulture);
                        string fechaDocumento = fechaDoc.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        lst.Add(new CabeceraOrdenVenta
                        {
                            DocEntry = x.DocEntry,
                            DocNum = x.DocNum,
                            DocDate = fechaContabilizacion,
                            TaxDate = fechaDocumento,
                            CardCode = x.CardCode,
                            CardName = x.CardName,
                            NumeroOrden = x.U_SYP_NUMOCCL,
                            DocTotal = x.DocTotal.Value,
                        });
                    }
                }
                return lst;
            }
        }
        public int IngresarEncabezadoMedicionPalletLocal(CabeceraOrdenVenta cabeceraOrdenVenta)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var res = new EncabezadoMedicionPalletLocal();
                    res.DocEntry = cabeceraOrdenVenta.DocEntry;
                    res.NumeroDocumento = cabeceraOrdenVenta.DocNum;
                    res.NumeroOrden = cabeceraOrdenVenta.NumeroOrden;
                    res.NombreCliente = cabeceraOrdenVenta.CardName;
                    res.FechaDocumento = Convert.ToDateTime(cabeceraOrdenVenta.DocDate);
                    res.FechaRegistro = DateTime.Now;
                    res.Liberacion = false;
                    
                    DB.EncabezadoMedicionPalletLocal.Add(res);
                    DB.SaveChanges();
                    int prodId = res.EncabezadoMedicionPalletLocalId;
                    return prodId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }
        public bool IngresarPackingDtlLocal(int PackingId, string CodigoItem, string DescripcionItem, int CantidadItem)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var packingDtl = new PackingDtlLocal();

                    packingDtl.IdentificadorCabecera = PackingId;
                    packingDtl.CodigoItem = CodigoItem;
                    packingDtl.DescripcionItem = DescripcionItem;
                    packingDtl.CantidadItem = CantidadItem;

                    DB.PackingDtlLocal.Add(packingDtl);
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
        public bool BusquedaLocalPedidoLocal(int numero)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoPacking = from d in DB.EncabezadoMedicionPalletLocal
                                     where d.NumeroDocumento == numero

                                     select new
                                     {
                                         d.NumeroDocumento,
                                     };

                if (ListadoPacking.Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
        public List<PackingIngresados> ConsultarPackingIngreseadosLiberacionLocalRegistrado()
        {
            int cantMediciones;
            CultureInfo ci = new CultureInfo("es-MX");
            ci = new CultureInfo("es-MX");
            TextInfo textInfo = ci.TextInfo;
            List<PackingIngresados> lst = new List<PackingIngresados>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoCabecera = from d in DB.EncabezadoMedicionPalletLocal
                                      orderby d.FechaRegistro descending
                                      where d.Liberacion == false
                                      select new
                                      {
                                          d.EncabezadoMedicionPalletLocalId,
                                          d.DocEntry,
                                          d.NumeroDocumento,
                                          d.NumeroOrden,
                                          d.NombreCliente,
                                          d.FechaRegistro,
                                          
                                      };
                foreach (var x in ListadoCabecera)
                {
                    cantMediciones = CantidadMedicionesPedidoLocal(x.EncabezadoMedicionPalletLocalId);
                    DateTime fechaDoc = Convert.ToDateTime(x.FechaRegistro, CultureInfo.InvariantCulture);
                    string fechaDocumento = fechaDoc.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    lst.Add(new PackingIngresados
                    {
                        EncabezadoPedidoLocal=x.EncabezadoMedicionPalletLocalId,
                        PackingId = x.DocEntry.Value,
                        NumeroDocumento = x.NumeroDocumento.Value,
                        NumeroOrden = x.NumeroOrden,
                        NombreCliente = x.NombreCliente,
                        CantidadPallet = 1,
                        FechaRegistro = fechaDocumento,
                        cantidadMediciones = cantMediciones,
                        NumeroContenedor = 1
                    });
                }
                return lst;
            }
        }
        public int CantidadMedicionesPedidoLocal(int identificador)
        {
            int Total = 0;
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var ListadoCabecera = (from d in DB.MedicionPalletPackingLocal
                                           where d.PackingId == identificador
                                           orderby d.MedicionPalletPackingLocalId descending
                                           select new
                                           {
                                               d.NumeroMedicion
                                           }).Count();
                    if (ListadoCabecera != 0)
                    {
                        Total = ListadoCabecera;

                    }
                    return Total;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return Total;
                }

            }
        }
        //modificar para obtener el detalle del pedido local
        public List<PalletPackingDetalle> ConsultarModelosProPalletLocal(int PackinkId, int PalletId)
        {
            List<PalletPackingDetalle> lst = new List<PalletPackingDetalle>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var res = from d in DB.PalletPackingDetalle
                              where d.PackingId == PackinkId && d.PalletPacking == PalletId
                              select new
                              {
                                  d.PalletPackingDetalleId,
                                  d.ItemCode,
                                  d.DescriptionCode,
                              };
                    foreach (var x in res)
                    {

                        lst.Add(new PalletPackingDetalle
                        {
                            PalletPackingDetalleId = x.PalletPackingDetalleId,
                            ItemCode = x.ItemCode,
                            DescriptionCode = x.DescriptionCode
                        });
                    }
                    return lst;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return lst;
                }
            }
        }
        public List<PalletPackingDetalle> ConsultarModelosPedidoMedicionesLocal(int cabecera)
        {

            List<PalletPackingDetalle> lst = new List<PalletPackingDetalle>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var res = from d in DB.PackingDtlLocal
                              where d.IdentificadorCabecera == cabecera
                              select new
                              {
                                  d.PackingDtlLocalId,
                                  d.CodigoItem,
                                  d.DescripcionItem,
                              };
                    foreach (var x in res)
                    {

                        lst.Add(new PalletPackingDetalle
                        {
                            PalletPackingDetalleId = x.PackingDtlLocalId,
                            ItemCode = x.CodigoItem,
                            DescriptionCode = x.DescripcionItem
                        });
                    }
                    return lst;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return lst;
                }
            }
        }
        public List<MedicionPalletPacking> ConsultarMedicionPalletLocal(int identificador)
        {

            List<MedicionPalletPacking> lst = new List<MedicionPalletPacking>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var res = from d in DB.MedicionPalletPackingLocal
                              where d.PackingId == identificador
                              select new
                              {
                                  d.MedicionPalletPackingLocalId,
                                  d.PackingId,
                                  d.NumeroMedicion,
                                  d.NumeroLote,
                                  d.Modelo,
                                  d.Voltaje,
                                  d.nivel,
                                  d.Acabado,
                                  d.Limpieza,
                                  d.CCA,
                                  d.FechaRegistro
                              };
                    foreach (var x in res)
                    {

                        lst.Add(new MedicionPalletPacking
                        {
                            MedicionPalletPackingId = x.MedicionPalletPackingLocalId,
                            PackingId = x.PackingId,
                            PalletId = 1,
                            NumeroMedicion = x.NumeroMedicion,
                            NumeroLote = x.NumeroLote,
                            Modelo = x.Modelo,
                            Voltaje = x.Voltaje,
                            nivel = x.nivel.Value,
                            Acabado = x.Acabado.Value,
                            Limpieza = x.Limpieza.Value,
                            CCA = x.CCA,
                            FechaRegistro = x.FechaRegistro
                        });
                    }
                    return lst;
                }
                catch
                {
                    return null;
                }
            }
        }
        public int CantidadMedicionesPackingListPalletLocal(int PackingId, int PalletId)
        {
            int Total = 0;
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var ListadoCabecera = (from d in DB.MedicionPalletPackingLocal
                                           where d.PackingId == PackingId
                                           orderby d.MedicionPalletPackingLocalId descending
                                           select new
                                           {
                                               d.NumeroMedicion
                                           }).Count();
                    if (ListadoCabecera != 0)
                    {
                        Total = ListadoCabecera;
                    }
                    return Total;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return Total;
                }

            }
        }
        public bool InsertarMedicionPalletLocal(int packingId, int palletId, string numeroLote, string modelo, decimal voltaje, bool nivel, bool acabado, bool limpieza, decimal CCA)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var valorMedi = CantidadMedicionesPackingListPalletLocal(packingId, palletId);
                    var result = new MedicionPalletPackingLocal();
                    result.PackingId = packingId;
                    result.Pallet = palletId;
                    result.NumeroMedicion = valorMedi + 1;
                    result.NumeroLote = numeroLote;
                    result.Modelo = modelo;
                    result.Voltaje = voltaje;
                    result.nivel = nivel;
                    result.Acabado = acabado;
                    result.Limpieza = limpieza;
                    result.CCA = CCA;
                    result.FechaRegistro = DateTime.Now;

                    DB.MedicionPalletPackingLocal.Add(result);
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
        public bool RegistrarLiberacionLocal(int identificador)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var result = (from a in DB.EncabezadoMedicionPalletLocal
                                  where a.DocEntry == identificador
                                  select a).FirstOrDefault();

                    result.Liberacion = true;
                    result.FechaLiberacion = DateTime.Now;
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
        public bool EliminarMedicionPalletLocal(int MedicionId)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    DB.MedicionPalletPackingLocal.RemoveRange(DB.MedicionPalletPackingLocal.Where(x => x.MedicionPalletPackingLocalId == MedicionId));
                    DB.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;

                }
            }
        }
        public List<PackingIngresados> ConsultarPackingLiberadosLocales()
        {
            int cantMediciones;
            CultureInfo ci = new CultureInfo("es-MX");
            ci = new CultureInfo("es-MX");
            TextInfo textInfo = ci.TextInfo;
            List<PackingIngresados> lst = new List<PackingIngresados>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoCabecera = from d in DB.EncabezadoMedicionPalletLocal
                                      orderby d.FechaRegistro descending
                                      where d.Liberacion == true
                                      select new
                                      {
                                          d.EncabezadoMedicionPalletLocalId,
                                          d.DocEntry,
                                          d.NumeroDocumento,
                                          d.NumeroOrden,
                                          d.NombreCliente,                                         
                                          d.FechaLiberacion
                                      };

                foreach (var x in ListadoCabecera)
                {
                    cantMediciones = CantidadMedicionesPedidoLocal(x.EncabezadoMedicionPalletLocalId);
                    DateTime fechaDoc = Convert.ToDateTime(x.FechaLiberacion, CultureInfo.InvariantCulture);
                    string fechaDocumento = fechaDoc.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                  
                    lst.Add(new PackingIngresados
                    {
                        EncabezadoPedidoLocal=x.EncabezadoMedicionPalletLocalId,
                        PackingId = x.DocEntry.Value,
                        NumeroDocumento = x.NumeroDocumento.Value,
                        NumeroOrden = x.NumeroOrden,
                        NombreCliente = x.NombreCliente,
                        CantidadPallet = 1,
                        FechaRegistro = fechaDocumento,
                        cantidadMediciones = cantMediciones
                    });
                }
                return lst;
            }
        }
        public List<PackingIngresados> ConsultarPackingLiberadoLocale(int identificador)
        {
            int cantMediciones;
            CultureInfo ci = new CultureInfo("es-MX");
            ci = new CultureInfo("es-MX");
            TextInfo textInfo = ci.TextInfo;
            List<PackingIngresados> lst = new List<PackingIngresados>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoCabecera = from d in DB.EncabezadoMedicionPalletLocal
                                      orderby d.FechaRegistro descending
                                      where d.Liberacion == true && d.EncabezadoMedicionPalletLocalId==identificador
                                      select new
                                      {
                                          d.EncabezadoMedicionPalletLocalId,
                                          d.DocEntry,
                                          d.NumeroDocumento,
                                          d.NumeroOrden,
                                          d.NombreCliente,
                                          d.FechaLiberacion
                                      };

                foreach (var x in ListadoCabecera)
                {
                    cantMediciones = CantidadMedicionesPedidoLocal(x.EncabezadoMedicionPalletLocalId);
                    DateTime fechaDoc = Convert.ToDateTime(x.FechaLiberacion, CultureInfo.InvariantCulture);
                    string fechaDocumento = fechaDoc.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                    lst.Add(new PackingIngresados
                    {
                        EncabezadoPedidoLocal = x.EncabezadoMedicionPalletLocalId,
                        PackingId = x.DocEntry.Value,
                        NumeroDocumento = x.NumeroDocumento.Value,
                        NumeroOrden = x.NumeroOrden,
                        NombreCliente = x.NombreCliente,
                        CantidadPallet = 1,
                        FechaRegistro = fechaDocumento,
                        cantidadMediciones = cantMediciones
                    });
                }
                return lst;
            }
        }
    }
}