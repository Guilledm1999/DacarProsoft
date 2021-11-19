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
        public int ObtenerNumeroSecuencial()
        {
            int result;
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var Listado = (from d in DB.SecuencialRevisionTecnica
                                   select new
                                   {
                                       d.InicioSecuencia
                                   }).FirstOrDefault();

                    
                        result = Listado.InicioSecuencia.Value;
                 
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }
        public int ObtenerNumeroCombrobante(int valor)
        {
            int result;
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var Listado = (from d in DB.IngresoRevisionGarantiaCabecera
                                   orderby d.IngresoRevisionGarantiaId descending
                                   select new
                                   {
                                       d.NumeroComprobante
                                   }).FirstOrDefault();

                    if (Listado != null)
                    {
                        result = Listado.NumeroComprobante.Value + 1;
                    }
                    else
                    {
                        result =valor + 1;
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
            int ValorSecuencial = ObtenerNumeroSecuencial();
            int NumeroCombrobante = ObtenerNumeroCombrobante(ValorSecuencial);
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
                                  d.ModeloBateria,
                                  d.NumeroFactura
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
                        NumeroRevision = verificacionRevision,
                        NumeroCombrobante= NumeroCombrobante,
                        NumeroFactura=x.NumeroFactura
                    });
                }
                return lst;

            }

        }
        public List<OSLP> ConsultarVendedores()
        {     
            List<OSLP> lst = new List<OSLP>();
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {

                var Listado = (from d in DB.OSLP
                               where d.Memo!= null && d.Memo != "NULL"
                               select new
                               {
                                 d.SlpCode,
                                 d.Memo
                               });
                foreach (var x in Listado)
                {
                  
                    lst.Add(new OSLP
                    {
                     SlpCode=x.SlpCode,
                     Memo=x.Memo
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
                               orderby d.IngresoRevisionGarantiaId descending
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
                                   d.Voltaje,
                                   d.ModoIngreso,
                                   d.AplicaGarantia
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
                        NumeroComprobante=x.NumeroComprobante.Value,
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
                        Voltaje=x.Voltaje.Value,
                        ModoIngreso=x.ModoIngreso,
                        AplicaGarantia=x.AplicaGarantia

                    });
                }
                return lst;

            }

        }
        public int IngresarRevisionGarantiaCabecera(string cliente, string cedula, string numeroGarantia, int numeroComprobante, string numeroRevision, string provincia, string direccion, string vendedor, string ImgFac, string marca,
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

        public bool ActualizarRegistroCabecera(int IngresoRevisionGarantiaId, string AplicaGarantia, string RegistroManual)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var query = (from a in DB.IngresoRevisionGarantiaCabecera
                                 where a.IngresoRevisionGarantiaId == IngresoRevisionGarantiaId
                                 select a).FirstOrDefault();

                    query.AplicaGarantia = AplicaGarantia;
                    query.ModoIngreso = RegistroManual;


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

        public int IngresarRevisionGarantiaInspeccionInicial(int RevisionDeGarantia, string InGolpeadaoRota, string InHinchada, string InBornesFlojos, string InBornesFundidos, string IngElectrolito, string InFugaEnCubierta, string InFugaEnBornes, int InCCA, string InRevisionesPeriodicas)
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
                    result.RevisionesPeriodicas = Convert.ToBoolean(InRevisionesPeriodicas);

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
        public List<ProvinciasEcuador> ConsultarProvincias()
        {
            List<ProvinciasEcuador> lst = new List<ProvinciasEcuador>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = (from d in DB.ProvinciasEcuador
                               select new
                               {
                                   d.id,
                                   d.provincia
                               }).ToList();

                foreach (var x in Listado)
                {
                    lst.Add(new ProvinciasEcuador
                    {
                        id = x.id,
                        provincia = x.provincia
                    });
                }
                return lst;
            }
        }
        public List<IngresoRevisionGarantiaInspeccionInicial> ConsultaInspeccionInicial(int IdCabeceraInspeccion)
        {
            List<IngresoRevisionGarantiaInspeccionInicial> lst = new List<IngresoRevisionGarantiaInspeccionInicial>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = (from d in DB.IngresoRevisionGarantiaInspeccionInicial
                               where d.IngresoRevisionGarantiaId==IdCabeceraInspeccion
                               select new
                               {
                                   d.IngresoRevisionGarantiaInspeccionInicialId,
                                   d.GolpeadaORota,
                                   d.Hinchada,
                                   d.BornesFlojosOHundidos,
                                   d.BornesFundidos,
                                   d.ElectrolitoErroneo,
                                   d.FugaEnCubierta,
                                   d.FugaEnBornes,
                                   d.CCA

                               }).ToList();

                foreach (var x in Listado)
                {
                    lst.Add(new IngresoRevisionGarantiaInspeccionInicial
                    {
                                   IngresoRevisionGarantiaInspeccionInicialId=x.IngresoRevisionGarantiaInspeccionInicialId,
                                   GolpeadaORota=x.GolpeadaORota,
                                   Hinchada=x.Hinchada,
                                   BornesFlojosOHundidos=x.BornesFlojosOHundidos,
                                   BornesFundidos=x.BornesFundidos,
                                   ElectrolitoErroneo=x.ElectrolitoErroneo,
                                   FugaEnCubierta=x.FugaEnCubierta,
                                   FugaEnBornes=x.FugaEnBornes,
                                   CCA=x.CCA
                    });
                }
                return lst;
            }
        }
        public List<IngresoRevisionGarantiaDiagnostico> ConsultaDiagnostico(int IdCabeceraInspeccion)
        {
            List<IngresoRevisionGarantiaDiagnostico> lst = new List<IngresoRevisionGarantiaDiagnostico>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = (from d in DB.IngresoRevisionGarantiaDiagnostico
                               where d.IngresoRevisionGarantiaId == IdCabeceraInspeccion
                               select new
                               {
                                  d.IngresoRevisionGarantiaDiagnosticoId,
                                  d.BateriaEnBuenEstado,
                                  d.PresentaFalloFabricacion,
                                  d.DentroPeriodoGarantia,
                                  d.AplicacionUsoAdecuado,
                                  d.AplicaGarantia

                               }).ToList();

                foreach (var x in Listado)
                {
                    lst.Add(new IngresoRevisionGarantiaDiagnostico
                    {
                      IngresoRevisionGarantiaDiagnosticoId=x.IngresoRevisionGarantiaDiagnosticoId,
                      BateriaEnBuenEstado=x.BateriaEnBuenEstado,
                      PresentaFalloFabricacion=x.PresentaFalloFabricacion,
                      DentroPeriodoGarantia=x.DentroPeriodoGarantia,
                      AplicacionUsoAdecuado=x.AplicacionUsoAdecuado,
                      AplicaGarantia=x.AplicaGarantia
                    });
                }
                return lst;
            }
        }
        public List<IngresoRevisionGarantiaTrabajoRealizado> ConsultaTrabajoRealizado(int IdCabeceraInspeccion)
        {
            List<IngresoRevisionGarantiaTrabajoRealizado> lst = new List<IngresoRevisionGarantiaTrabajoRealizado>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = (from d in DB.IngresoRevisionGarantiaTrabajoRealizado
                               where d.IngresoRevisionGarantiaId == IdCabeceraInspeccion
                               select new
                               {
                                   d.IngresoRevisionGarantiaTrabajoRealizadoId,
                                   d.PruebaAltaResistencia,
                                   d.CambioAcido,
                                   d.RecargaBateria,
                                   d.InspeccionEstructuraExterna


                               }).ToList();

                foreach (var x in Listado)
                {
                    lst.Add(new IngresoRevisionGarantiaTrabajoRealizado
                    {
                        IngresoRevisionGarantiaTrabajoRealizadoId=x.IngresoRevisionGarantiaTrabajoRealizadoId,
                        PruebaAltaResistencia=x.PruebaAltaResistencia,
                        CambioAcido=x.CambioAcido,
                        RecargaBateria=x.RecargaBateria,
                        InspeccionEstructuraExterna=x.InspeccionEstructuraExterna
                    });
                }
                return lst;
            }
        }
        public List<IngresoInspeccionInicialDensidadCelda> ConsultaInspeccionInicialDensidadCelda(int IdIngresoGarantiaInspeccionInicial)
        {
            List<IngresoInspeccionInicialDensidadCelda> lst = new List<IngresoInspeccionInicialDensidadCelda>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = (from d in DB.IngresoInspeccionInicialDensidadCelda
                               where d.IngresoRevisionGarantiaInspeccionInicialId == IdIngresoGarantiaInspeccionInicial
                               select new
                               {
                               
                                   d.IngresoInspeccionInicialDensidadCeldaId,
                                   d.C1,
                                   d.C2,
                                   d.C3,
                                   d.C4,
                                   d.C5,
                                   d.C6


                               }).ToList();

                foreach (var x in Listado)
                {
                    lst.Add(new IngresoInspeccionInicialDensidadCelda
                    {
                        IngresoInspeccionInicialDensidadCeldaId=x.IngresoInspeccionInicialDensidadCeldaId,
                        C1=x.C1,
                        C2=x.C2,
                        C3=x.C3,
                        C4=x.C4,
                        C5=x.C5,
                        C6=x.C6
                    });
                }
                return lst;
            }
        }
    }
}