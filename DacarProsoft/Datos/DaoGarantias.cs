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

        //public int VerificarNumeroRevision(string numeroGarantia)
        //{
        //    int result;
        //    using (DacarProsoftEntities DB = new DacarProsoftEntities())
        //    {
        //        try
        //        {
        //            var Listado = (from d in DB.IngresoRevisionGarantiaCabecera
        //                           where d.NumeroGarantia == numeroGarantia
        //                           orderby d.IngresoRevisionGarantiaId descending
        //                           select new
        //                           {
        //                               d.NumeroRevision
        //                           }).FirstOrDefault();

        //            if (Listado != null)
        //            {
        //                result = Listado.NumeroRevision.Value + 1;
        //            }
        //            else {
        //                result = 1;
        //            }


        //            return result;
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //            return 0;
        //        }
        //    }
        //}
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
        public decimal ObtenerPrecioBateria(string  Referencia)
        {
            string cadena= Referencia;

            //cadena = Referencia.Replace(" ", "-");
            cadena = cadena.Replace("\n","");

            if (cadena.Substring(0, 1) == "D")
            {
                cadena = cadena + "-BS";
            }
            if (cadena== "31-DC-100 (S-2000)") {
                cadena = "MIL-31-DC-100";
            }
            decimal result;
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                try
                {
                    var Listado = (from d in DB.OITM join
                                   e in DB.ITM1 on d.ItemCode equals e.ItemCode
                                   where d.ItemName == cadena && e.PriceList==1 
                                   orderby d.ItemCode ascending
                                   select new
                                   {
                                       d.ItemCode,
                                       e.Price
                                   }).FirstOrDefault();

                    if (Listado != null)
                    {
                        result = Listado.Price.Value;
                    }
                    else
                    {
                        result = 0;
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
            decimal valorBateria=0;
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
                                  d.Ciudad,
                                  d.ModeloBateria,
                                  d.NumeroFactura
                               });
                foreach (var x in Listado)
                {
                    //var verificacionRevision = VerificarNumeroRevision(x.NumeroGarantia);
                    if (x.ModeloBateria!=null && x.ModeloBateria !="") {
                        valorBateria = ObtenerPrecioBateria(x.ModeloBateria);
                    }
                    
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
                        Ciudad=x.Ciudad,
                        ModeloBateria=x.ModeloBateria,
                        //NumeroRevision = verificacionRevision,
                        NumeroCombrobante= NumeroCombrobante,
                        NumeroFactura=x.NumeroFactura,
                        ValorBateria=valorBateria
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
                                   d.NumeroFactura,
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
                        NumeroRevision=x.NumeroFactura.Value,
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
        public int IngresarRevisionGarantiaCabecera(string cliente, string cedula, string numeroGarantia, string numeroComprobante, string numeroRevision, string provincia, string direccion, string vendedor, string ImgFac, string marca,
            string modelo, string lote, string prorrateo, string meses, string fechaVenta, string fechaIngreso, string porcentajeVenta, string voltaje, string loteEnsamble, string ImgTest)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var result = new IngresoRevisionGarantiaCabecera();
                    result.Cliente = cliente;
                    result.Cedula = cedula;
                    result.NumeroGarantia = numeroGarantia;
                    result.NumeroComprobante = Convert.ToInt32(numeroComprobante);
                    result.NumeroFactura = Convert.ToInt32(numeroRevision);
                    result.Provincia = provincia;
                    result.Direccion = direccion;
                    result.Vendedor = vendedor;
                    result.FacturaCliente = ImgFac;
                    result.TestBateria = ImgTest;
                    result.Marca = marca;
                    result.Modelo = modelo;
                    result.Lote = lote;
                    result.Prorrateo = Convert.ToDecimal(prorrateo);
                    result.Meses = Convert.ToInt32(meses);
                    result.FechaVenta = Convert.ToDateTime(fechaVenta);
                    result.FechaIngreso = Convert.ToDateTime(fechaIngreso);
                    result.PorcentajeVenta = Convert.ToDecimal(porcentajeVenta);
                    result.Voltaje = Convert.ToDecimal(voltaje);
                    result.LoteEnsamble = loteEnsamble;
                    result.AnalisisRealizado = false;

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

        public List<CantonesEcuador> ConsultarCantones(int idProvincia)
        {
            List<CantonesEcuador> lst = new List<CantonesEcuador>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = (from d in DB.CantonesEcuador
                               where d.id_provincia == idProvincia
                               select new
                               {
                                   d.id,
                                   d.canton
                               }).ToList();

                foreach (var x in Listado)
                {
                    lst.Add(new CantonesEcuador
                    {
                        id = x.id,
                        canton = x.canton
                    });
                }
                return lst;
            }
        }
        public List<ModelosMarcasPropias> ConsultarReferenciasModelosMarcasPropias()
        {
            List<ModelosMarcasPropias> lst = new List<ModelosMarcasPropias>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = (from d in DB.ModelosMarcasPropias
                               orderby d.Referencia ascending
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

        public int ConsultarLineaMarca(int MarcaPropiasId) {
            int val = 0;
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = (from d in DB.ModelosMarcasPropias
                               where d.ModelosMarcasPropiasId == MarcaPropiasId
                               select new
                               {

                                   d.Linea


                               }).FirstOrDefault();
                val = Listado.Linea.Value;

                return val;
            }
        }
        public List<DetalleProrrateo> ConsultaInfoProrrateo(int MarcaPropiasId, string MarcaPropiasTexto, decimal PvpVentas, DateTime FechaIngreso, DateTime FechaVenta)
        {
           decimal ValorBateria = ObtenerPrecioBateria(MarcaPropiasTexto);

           int Meses = (FechaIngreso.Month - FechaVenta.Month) + 12 * (FechaIngreso.Year - FechaVenta.Year);


            List<DetalleProrrateo> lst = new List<DetalleProrrateo>();
            int Linea = 0;
            decimal valorPorcentaje = 0;
            //decimal valorProrrateo =0;
            Linea = ConsultarLineaMarca(MarcaPropiasId);

            if (Linea==1) {
                if (MarcaPropiasTexto.Substring(0, 3) == "ECO")
                {
                    if (Meses <= 9)
                    {
                        valorPorcentaje = 100;
                    }
                    if (Meses == 10)
                    {
                        valorPorcentaje = 50;
                    }
                    if (Meses == 11)
                    {
                        valorPorcentaje = 45;
                    }
                    if (Meses == 12)
                    {
                        valorPorcentaje = 40;
                    }
                    if (Meses == 13)
                    {
                        valorPorcentaje = 35;
                    }
                    if (Meses == 14)
                    {
                        valorPorcentaje = 30;
                    }
                    if (Meses == 15)
                    {
                        valorPorcentaje = 25;
                    }
                    if (Meses >= 16)
                    {
                        valorPorcentaje = 0;
                    }
                    if (Meses <= 0)
                    {
                        valorPorcentaje = 0;
                    }
                }
                if (MarcaPropiasTexto.Substring(0, 2) == "TX")
                {
                    if (Meses <= 9)
                    {
                        valorPorcentaje = 100;
                    }
                    if (Meses == 10)
                    {
                        valorPorcentaje = 50;
                    }
                    if (Meses == 11)
                    {
                        valorPorcentaje = 45;
                    }
                    if (Meses == 12)
                    {
                        valorPorcentaje = 40;
                    }
                    if (Meses == 13)
                    {
                        valorPorcentaje = 35;
                    }
                    if (Meses == 14)
                    {
                        valorPorcentaje = 30;
                    }
                    if (Meses == 15)
                    {
                        valorPorcentaje = 25;
                    }
                    if (Meses >= 16)
                    {
                        valorPorcentaje = 0;
                    }
                    if (Meses <= 0)
                    {
                        valorPorcentaje = 0;
                    }
                }
                if (MarcaPropiasTexto.Substring(0, 2) == "BP")
                {
                    if (Meses <= 12)
                    {
                        valorPorcentaje = 100;
                    }
                    if (Meses == 13)
                    {
                        valorPorcentaje = 58;
                    }
                    if (Meses == 14)
                    {
                        valorPorcentaje = 55;
                    }
                    if (Meses == 15)
                    {
                        valorPorcentaje = 52;
                    }
                    if (Meses == 16)
                    {
                        valorPorcentaje = 49;
                    }
                    if (Meses == 17)
                    {
                        valorPorcentaje = 46;
                    }
                    if (Meses == 18)
                    {
                        valorPorcentaje = 42;
                    }
                    if (Meses == 19)
                    {
                        valorPorcentaje = 39;
                    }
                    if (Meses == 20)
                    {
                        valorPorcentaje = 36;
                    }
                    if (Meses == 21)
                    {
                        valorPorcentaje = 33;
                    }
                    if (Meses == 22)
                    {
                        valorPorcentaje = 30;
                    }
                    if (Meses == 23)
                    {
                        valorPorcentaje = 26;
                    }
                    if (Meses == 24)
                    {
                        valorPorcentaje = 23;
                    }
                    if (Meses >= 25)
                    {
                        valorPorcentaje = 0;
                    }
                    if (Meses <= 0)
                    {
                        valorPorcentaje = 0;
                    }
                }
                if(MarcaPropiasTexto.Substring(0, 3) != "ECO" && MarcaPropiasTexto.Substring(0, 2) != "TX" && MarcaPropiasTexto.Substring(0, 2) != "BP") {
                    if (Meses <= 10)
                    {
                        valorPorcentaje = 100;
                    }
                    if (Meses == 11)
                    {
                        valorPorcentaje = 54;
                    }
                    if (Meses == 12)
                    {
                        valorPorcentaje = 49;
                    }
                    if (Meses == 13)
                    {
                        valorPorcentaje = 45;
                    }
                    if (Meses == 14)
                    {
                        valorPorcentaje = 41;
                    }
                    if (Meses == 15)
                    {
                        valorPorcentaje = 37;
                    }
                    if (Meses == 16)
                    {
                        valorPorcentaje = 33;
                    }
                    if (Meses == 17)
                    {
                        valorPorcentaje = 28;
                    }
                    if (Meses == 18)
                    {
                        valorPorcentaje = 24;
                    }
                    if (Meses >= 19)
                    {
                        valorPorcentaje = 0;
                    }
                    if (Meses <= 0)
                    {
                        valorPorcentaje = 0;
                    }
                }          

            }
            if (Linea == 2)
            {
                if (Meses <= 12)
                {
                    valorPorcentaje = 100;
                }
                if (Meses == 13)
                {
                    valorPorcentaje = 48;
                }
                if (Meses == 14)
                {
                    valorPorcentaje = 44;
                }
                if (Meses == 15)
                {
                    valorPorcentaje = 40;
                }
                if (Meses == 16)
                {
                    valorPorcentaje = 36;
                }
                if (Meses == 17)
                {
                    valorPorcentaje = 32;
                }
                if (Meses == 18)
                {
                    valorPorcentaje = 28;
                }
                if (Meses == 19)
                {
                    valorPorcentaje = 24;
                }
                if (Meses == 20)
                {
                    valorPorcentaje = 20;
                }
                if (Meses == 21)
                {
                    valorPorcentaje = 16;
                }
                if (Meses == 22)
                {
                    valorPorcentaje = 12;
                }
                if (Meses == 23)
                {
                    valorPorcentaje = 8;
                }
                if (Meses == 24)
                {
                    valorPorcentaje = 4;
                }
                if (Meses >= 25)
                {
                    valorPorcentaje = 0;
                }
                if (Meses <= 0)
                {
                    valorPorcentaje = 0;
                }
            }
            if (Linea == 3)
            {
                if (MarcaPropiasTexto.Substring(0, 2) == "GC")
                {
                    if (Meses <= 6)
                    {
                        valorPorcentaje = 100;
                    }
                    if (Meses == 7)
                    {
                        valorPorcentaje = 52;
                    }
                    if (Meses == 8)
                    {
                        valorPorcentaje = 45;
                    }
                    if (Meses == 9)
                    {
                        valorPorcentaje = 38;
                    }
                    if (Meses == 10)
                    {
                        valorPorcentaje = 31;
                    }
                    if (Meses == 11)
                    {
                        valorPorcentaje = 24;
                    }
                    if (Meses == 12)
                    {
                        valorPorcentaje = 17;
                    }

                    if (Meses >= 13)
                    {
                        valorPorcentaje = 0;
                    }
                    if (Meses <= 0)
                    {
                        valorPorcentaje = 0;
                    }
                }
                else {
                    if (Meses <= 6)
                    {
                        valorPorcentaje = 100;
                    }
                    if (Meses == 7)
                    {
                        valorPorcentaje = 65;
                    }
                    if (Meses == 8)
                    {
                        valorPorcentaje = 60;
                    }
                    if (Meses == 9)
                    {
                        valorPorcentaje = 55;
                    }
                    if (Meses == 10)
                    {
                        valorPorcentaje = 50;
                    }
                    if (Meses == 11)
                    {
                        valorPorcentaje = 45;
                    }
                    if (Meses == 12)
                    {
                        valorPorcentaje = 40;
                    }
                    if (Meses == 13)
                    {
                        valorPorcentaje = 35;
                    }
                    if (Meses == 14)
                    {
                        valorPorcentaje = 30;
                    }
                    if (Meses == 15)
                    {
                        valorPorcentaje = 25;
                    }
                    if (Meses == 16)
                    {
                        valorPorcentaje = 20;
                    }
                    if (Meses == 17)
                    {
                        valorPorcentaje = 15;
                    }
                    if (Meses == 18)
                    {
                        valorPorcentaje = 10;
                    }
                    if (Meses >= 19)
                    {
                        valorPorcentaje = 0;
                    }
                    if (Meses <= 0)
                    {
                        valorPorcentaje = 0;
                    }

                }

            }
            if (Linea == 4)
            {
                if (Meses <= 6)
                {
                    valorPorcentaje = 100;
                }
                if (Meses == 7)
                {
                    valorPorcentaje = 65;
                }
                if (Meses == 8)
                {
                    valorPorcentaje = 60;
                }
                if (Meses == 9)
                {
                    valorPorcentaje = 55;
                }
                if (Meses == 10)
                {
                    valorPorcentaje = 50;
                }
                if (Meses == 11)
                {
                    valorPorcentaje = 45;
                }
                if (Meses == 12)
                {
                    valorPorcentaje = 40;
                }
                if (Meses == 13)
                {
                    valorPorcentaje = 35;
                }
                if (Meses == 14)
                {
                    valorPorcentaje = 30;
                }
                if (Meses == 15)
                {
                    valorPorcentaje = 25;
                }
                if (Meses == 16)
                {
                    valorPorcentaje = 20;
                }
                if (Meses == 17)
                {
                    valorPorcentaje = 15;
                }
                if (Meses == 18)
                {
                    valorPorcentaje = 10;
                }
                if (Meses >= 19)
                {
                    valorPorcentaje = 0;
                }
                if (Meses <= 0)
                {
                    valorPorcentaje = 0;
                }
            }
            if (Linea == 5)
            {
                if (MarcaPropiasTexto.Substring(0, 2) == "BP")
                {
                    if (Meses <= 6)
                    {
                        valorPorcentaje = 100;
                    }
                    if (Meses == 7)
                    {
                        valorPorcentaje = 57;
                    }
                    if (Meses == 8)
                    {
                        valorPorcentaje = 51;
                    }
                    if (Meses == 9)
                    {
                        valorPorcentaje = 45;
                    }
                    if (Meses == 10)
                    {
                        valorPorcentaje = 38;
                    }
                    if (Meses == 11)
                    {
                        valorPorcentaje = 32;
                    }
                    if (Meses == 12)
                    {
                        valorPorcentaje = 26;
                    }

                    if (Meses >= 13)
                    {
                        valorPorcentaje = 0;
                    }
                    if (Meses <= 0)
                    {
                        valorPorcentaje = 0;
                    }
                }
                else
                {
                    if (Meses <= 6)
                    {
                        valorPorcentaje = 100;
                    }
                    if (Meses == 7)
                    {
                        valorPorcentaje = 57;
                    }
                    if (Meses == 8)
                    {
                        valorPorcentaje = 51;
                    }
                    if (Meses == 9)
                    {
                        valorPorcentaje = 45;
                    }
                    if (Meses == 10)
                    {
                        valorPorcentaje = 38;
                    }
                    if (Meses == 11)
                    {
                        valorPorcentaje = 32;
                    }
                    if (Meses == 12)
                    {
                        valorPorcentaje = 26;
                    }

                    if (Meses >= 13)
                    {
                        valorPorcentaje = 0;
                    }
                    if (Meses <= 0)
                    {
                        valorPorcentaje = 0;
                    }

                }
            }
            if (Linea == 6)
            {
                if (Meses <= 6)
                {
                    valorPorcentaje = 100;
                }
                if (Meses == 7)
                {
                    valorPorcentaje = 52;
                }
                if (Meses == 8)
                {
                    valorPorcentaje = 45;
                }
                if (Meses == 9)
                {
                    valorPorcentaje = 38;
                }
                if (Meses == 10)
                {
                    valorPorcentaje = 31;
                }
                if (Meses == 11)
                {
                    valorPorcentaje = 24;
                }
                if (Meses == 12)
                {
                    valorPorcentaje = 17;
                }

                if (Meses >= 13)
                {
                    valorPorcentaje = 0;
                }
                if (Meses <= 0)
                {
                    valorPorcentaje = 0;
                }
            }

            lst.Add(new DetalleProrrateo
            {
              PorcentajeProrrateo=Convert.ToInt32(valorPorcentaje),
              MesesGarantia=Meses,
              ValorBateria= ValorBateria
            });

            return lst;
        }
    }
}