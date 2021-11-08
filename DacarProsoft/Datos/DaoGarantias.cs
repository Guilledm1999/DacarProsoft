using DacarDatos.Datos;
using DacarProsoft.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace DacarProsoft.Datos
{
    public class DaoGarantias
    {

        public List<GarantiaDetalle> ReporteGeneralPedidosExterior(/*DateTime FechaInicio, DateTime FechaFin*/)
        {
            

             // DateTime nuevaFechaFin = FechaFin;
           // nuevaFechaFin = nuevaFechaFin.AddDays(1);

            string fechaRegistro = null;

            List<GarantiaDetalle> lst = new List<GarantiaDetalle>();
                using (DacarProsoftEntities DB = new DacarProsoftEntities())
                {

                    var Listado = (from d in DB.IngresoGarantias
                                       // where d.RegistroGarantia >= FechaInicio && d.RegistroGarantia <= nuevaFechaFin
                                   orderby d.IngresoGarantiaId descending
                                   select new
                                   {
                                       d.IngresoGarantiaId,
                                       d.Cedula,
                                       d.Nombre,
                                       d.Apellido,
                                       d.Email,
                                       d.Distribuidor,
                                       d.Ciudad,
                                       d.ModeloBateria,
                                       //d.NumeroBateria,
                                       d.NumeroGarantia,
                                       d.RegistroGarantia,

                                   });
                    foreach (var x in Listado)
                {
                    DateTime fecha = Convert.ToDateTime(x.RegistroGarantia, CultureInfo.InvariantCulture);
                    //FechaCargaLista = fechaCargaLista.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    fechaRegistro = fecha.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    lst.Add(new GarantiaDetalle
                        {
                           IngresoGarantiaId=x.IngresoGarantiaId,
                           Cedula=x.Cedula,
                           Nombre=x.Nombre,
                           Apellido=x.Apellido,
                           Email=x.Email,
                           Distribuidor=x.Distribuidor,
                           Ciudad=x.Ciudad,
                           ModeloBateria=x.ModeloBateria,
                           //NumeroBateria=x.NumeroBateria,
                           NumeroGarantia=x.NumeroGarantia,
                           RegistroGarantia= fechaRegistro,

                    });
                    }
                    return lst;

                }

        }

        public int VerificarNumeroRevision(string numeroGarantia)
        {
            int result;
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var Listado = (from d in DB.IngresoRevisionGarantiaCabecera
                                   where d.NumeroGarantia == numeroGarantia
                                   orderby d.IngresoRevisionGarantiaId descending
                                   select new
                                   {
                                       d.NumeroRevision
                                   }).FirstOrDefault();

                    if (Listado != null)
                    {
                        result = Listado.NumeroRevision.Value + 1;
                    }
                    else {
                        result = 1;
                    }


                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public List<IngresoGarantiasModel> ConsultarNumeroGarantia(string numeroGarantia)
        {

            //string fechaRegistro = null;

            List<IngresoGarantiasModel> lst = new List<IngresoGarantiasModel>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {

                var Listado = (from d in DB.IngresoGarantias
                               where d.NumeroGarantia==numeroGarantia
                               select new
                               {
                                  d.IngresoGarantiaId,
                                  d.NumeroGarantia,
                                  d.Cedula,
                                  d.Nombre,
                                  d.Apellido,
                                  d.RegistroGarantia,
                                  d.Provincia,
                                  d.ModeloBateria
                               });
                foreach (var x in Listado)
                {
                    var verificacionRevision = VerificarNumeroRevision(x.NumeroGarantia);

                    //DateTime fecha = Convert.ToDateTime(x.RegistroGarantia, CultureInfo.InvariantCulture);
                    //fechaRegistro = fecha.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    lst.Add(new IngresoGarantiasModel
                    {
                        IngresoGarantiaId = x.IngresoGarantiaId,
                        Cedula = x.Cedula,
                        Nombre = x.Nombre,
                        Apellido = x.Apellido,                   
                        NumeroGarantia = x.NumeroGarantia,
                        RegistroGarantia = x.RegistroGarantia.Value,
                        Provincia=x.Provincia,
                        ModeloBateria=x.ModeloBateria,
                        NumeroRevision = verificacionRevision

                    });
                }
                return lst;

            }

        }
        public List<RevisionesTecnicasGarantias> ConsultarRevisionesTecnicas()
        {
            string fechaVenta = null;
            string fechaRegistro = null;

            List<RevisionesTecnicasGarantias> lst = new List<RevisionesTecnicasGarantias>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {

                var Listado = (from d in DB.IngresoRevisionGarantiaCabecera
                               select new
                               {
                                   d.IngresoRevisionGarantiaId,
                                   d.Cliente,
                                   d.Cedula,
                                   d.NumeroGarantia,
                                   d.NumeroComprobante,
                                   d.NumeroRevision,
                                   d.Provincia,
                                   d.Direccion,
                                   d.Vendedor,
                                   d.FacturaCliente,
                                   d.TestBateria,
                                   d.Marca,
                                   d.Modelo,
                                   d.Lote,
                                   d.FechaVenta,
                                   d.FechaIngreso,
                                   d.Prorrateo,
                                   d.Meses,
                                   d.PorcentajeVenta,
                                   d.Voltaje
                               });
                foreach (var x in Listado)
                {

                    DateTime fecha = Convert.ToDateTime(x.FechaIngreso, CultureInfo.InvariantCulture);
                    fechaRegistro = fecha.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                    DateTime fecha2 = Convert.ToDateTime(x.FechaVenta, CultureInfo.InvariantCulture);
                    fechaVenta = fecha2.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                    lst.Add(new RevisionesTecnicasGarantias
                    {
                        IngresoRevisionGarantiaId=x.IngresoRevisionGarantiaId,
                        Cliente=x.Cliente,
                        Cedula=x.Cedula,
                        NumeroGarantia=x.NumeroGarantia,
                        NumeroComprobante=x.NumeroComprobante,
                        NumeroRevision=x.NumeroRevision.Value,
                        Provincia=x.Provincia,
                        Direccion=x.Direccion,
                        Vendedor=x.Vendedor,
                        FacturaCliente=x.FacturaCliente,
                        TestBateria=x.TestBateria,
                        Marca=x.Marca,
                        Modelo=x.Modelo,
                        Lote=x.Lote,
                        FechaVenta= fechaVenta,
                        FechaIngreso= fechaRegistro,
                        Prorrateo=x.Prorrateo.Value,
                        Meses=x.Meses.Value,
                        PorcentajeVenta=x.PorcentajeVenta.Value,
                        Voltaje=x.Voltaje.Value

                    });
                }
                return lst;

            }

        }
        public int IngresarRevisionGarantiaCabecera(string cliente, string cedula, string numeroGarantia, string numeroComprobante, string numeroRevision, string provincia, string direccion, string vendedor, string ImgFac, string marca,
            string modelo, string lote, decimal prorrateo, int meses, DateTime fechaVenta, DateTime fechaIngreso, decimal porcentajeVenta, decimal voltaje, string ImgTest)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var result = new IngresoRevisionGarantiaCabecera();
                    result.Cliente = cliente;
                    result.Cedula = cedula;
                    result.NumeroGarantia = numeroGarantia;
                    result.NumeroComprobante = numeroComprobante;
                    result.NumeroRevision = Convert.ToInt32(numeroRevision);
                    result.Provincia = provincia;
                    result.Direccion = direccion;
                    result.Vendedor = vendedor;
                    result.FacturaCliente = ImgFac;
                    result.TestBateria = ImgTest;
                    result.Marca = marca;
                    result.Modelo = modelo;
                    result.Lote = lote;
                    result.Prorrateo = prorrateo;
                    result.Meses = meses;
                    result.FechaVenta = fechaVenta;
                    result.FechaIngreso = fechaIngreso;
                    result.PorcentajeVenta = porcentajeVenta;
                    result.Voltaje = voltaje;

                    DB.IngresoRevisionGarantiaCabecera.Add(result);
                    DB.SaveChanges();

                    int resultId = result.IngresoRevisionGarantiaId;
                    return resultId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public int IngresarRevisionGarantiaInspeccionInicial(int RevisionDeGarantia, string InGolpeadaoRota, string InHinchada, string InBornesFlojos, string InBornesFundidos, string IngElectrolito, string InFugaEnCubierta, string InFugaEnBornes, int InCCA)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var result = new IngresoRevisionGarantiaInspeccionInicial();
                    result.IngresoRevisionGarantiaId = RevisionDeGarantia;
                    result.GolpeadaORota = Convert.ToBoolean(InGolpeadaoRota);
                    result.Hinchada = Convert.ToBoolean(InHinchada);
                    result.BornesFlojosOHundidos = Convert.ToBoolean(InBornesFlojos);
                    result.BornesFundidos = Convert.ToBoolean(InBornesFundidos);
                    result.ElectrolitoErroneo = Convert.ToBoolean(IngElectrolito);
                    result.FugaEnCubierta = Convert.ToBoolean(InFugaEnCubierta);
                    result.FugaEnBornes = Convert.ToBoolean(InFugaEnBornes);
                    result.CCA = InCCA;

                    DB.IngresoRevisionGarantiaInspeccionInicial.Add(result);
                    DB.SaveChanges();

                    int resultId = result.IngresoRevisionGarantiaInspeccionInicialId;
                    return resultId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public bool IngresarRevisionGarantiaInspeccionInicialCeldas(int DetalleInspeccionInicialId, int InDcC1, int InDcC2, int InDcC3, int InDcC4, int InDcC5, int InDcC6)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var result = new IngresoInspeccionInicialDensidadCelda();
                    result.IngresoRevisionGarantiaInspeccionInicialId = DetalleInspeccionInicialId;
                    result.C1 = InDcC1;
                    result.C2 = InDcC2;
                    result.C3 = InDcC3;
                    result.C4 = InDcC4;
                    result.C5 = InDcC5;
                    result.C6 = InDcC6;

                    DB.IngresoInspeccionInicialDensidadCelda.Add(result);
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

        public bool IngresarRevisionGarantiaTrabajoRealizado(int RevisionDeGarantia, string TrPruebaAltaResistencia, string TrCambioAcido, string TrRecargaBateria, string TrInspeccionEstructuraExt)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var result = new IngresoRevisionGarantiaTrabajoRealizado();
                    result.IngresoRevisionGarantiaId = RevisionDeGarantia;
                    result.PruebaAltaResistencia = Convert.ToBoolean(TrPruebaAltaResistencia);
                    result.CambioAcido = Convert.ToBoolean(TrCambioAcido);
                    result.RecargaBateria = Convert.ToBoolean(TrRecargaBateria);
                    result.InspeccionEstructuraExterna = Convert.ToBoolean(TrInspeccionEstructuraExt);
                 

                    DB.IngresoRevisionGarantiaTrabajoRealizado.Add(result);
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

        public bool IngresarRevisionGarantiaDiagnostico(int RevisionDeGarantia, string DBateriaBuenEstado, string DPresentaFallosFabricacion, string DDentroPeriodo, string DUsoAdecuado, string DAplicaGarantia)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var result = new IngresoRevisionGarantiaDiagnostico();
                    result.IngresoRevisionGarantiaId = RevisionDeGarantia;
                    result.BateriaEnBuenEstado = Convert.ToBoolean(DBateriaBuenEstado);
                    result.PresentaFalloFabricacion = Convert.ToBoolean(DPresentaFallosFabricacion);
                    result.DentroPeriodoGarantia = Convert.ToBoolean(DDentroPeriodo);
                    result.AplicacionUsoAdecuado = Convert.ToBoolean(DUsoAdecuado);
                    result.AplicaGarantia = Convert.ToBoolean(DAplicaGarantia);


                    DB.IngresoRevisionGarantiaDiagnostico.Add(result);
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

        public List<ModelosMarcasPropias> ConsultarReferenciasModelosMarcasPropias()
        {
            List<ModelosMarcasPropias> lst = new List<ModelosMarcasPropias>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = (from d in DB.ModelosMarcasPropias
                               select new
                               {
                                   d.ModelosMarcasPropiasId,
                                   d.Referencia
                               }).ToList();

                foreach (var x in Listado)
                {
                    lst.Add(new ModelosMarcasPropias
                    {
                        ModelosMarcasPropiasId = x.ModelosMarcasPropiasId,
                        Referencia = x.Referencia
                    });
                }
                return lst;
            }
        }
    }
}