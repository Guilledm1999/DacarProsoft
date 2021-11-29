using DacarDatos.Datos;
using DacarProsoft.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace DacarProsoft.Datos
{
    public class DaoProduccion
    {
        public List<RevisionesTecnicasGarantias> ConsultarRegistrosGarantias()
        {
            string fechaVenta = null;
            string fechaRegistro = null;
            List<RevisionesTecnicasGarantias> lst = new List<RevisionesTecnicasGarantias>();

            try
            {
                using (DacarProsoftEntities DB = new DacarProsoftEntities())
                {
                    var Listado = (from d in DB.IngresoRevisionGarantiaCabecera
                                   orderby d.IngresoRevisionGarantiaId descending
                                   select new

                                   {
                                       d.IngresoRevisionGarantiaId,
                                       d.Cliente,
                                       d.NumeroComprobante,
                                       d.Provincia,
                                       d.Direccion,
                                       d.Vendedor,
                                       d.Modelo,
                                       d.Lote,
                                       d.FechaVenta,
                                       d.FechaIngreso,
                                       d.Prorrateo,
                                       d.Meses,
                                       d.PorcentajeVenta,
                                       d.Voltaje,
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
                            IngresoRevisionGarantiaId = x.IngresoRevisionGarantiaId,
                            Cliente = x.Cliente,
                            NumeroComprobante = x.NumeroComprobante.Value,
                            Provincia = x.Provincia,
                            Direccion = x.Direccion,
                            Vendedor = x.Vendedor,
                            Modelo = x.Modelo,
                            Lote = x.Lote,
                            FechaVenta = fechaVenta,
                            FechaIngreso = fechaRegistro,
                            Prorrateo = x.Prorrateo.Value,
                            Meses = x.Meses.Value,
                            PorcentajeVenta = x.PorcentajeVenta.Value,
                            Voltaje = x.Voltaje.Value,
                            AplicaGarantia = x.AplicaGarantia

                        });
                    }
                    return lst;
                }
            }
            catch(Exception ex) {
                Console.WriteLine(ex);
                return lst;
            }
          
        }
        public List<CausalesGarantias> ConsultarCausalesGarantia()
        {
            List<CausalesGarantias> lst = new List<CausalesGarantias>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {

                var Listado = (from d in DB.CausalesGarantias
                               select new
                               {
                                   d.DescripcionCausales,
                                   d.CausalesGarantiasId
                               });
                foreach (var x in Listado)
                {

                    lst.Add(new CausalesGarantias
                    {
                        DescripcionCausales = x.DescripcionCausales,
                        CausalesGarantiasId = x.CausalesGarantiasId
                    });
                }
                return lst;

            }
        }

        public string ConsultarAreaResponsable(int CodigoArea)
        {
            var identificadorArea = ConsultarIdentificadorAreaResponsable(CodigoArea);
            string Descripcion;

            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {

                var result = (from d in DB.AreaResponsableGarantias
                               where d.CodigoArea== identificadorArea
                              select new
                               {
                                   d.DescripcionAreaResponsable,
                                   
                               }).FirstOrDefault();
                Descripcion = result.DescripcionAreaResponsable;
                
                return Descripcion;

            }
        }
        public int ConsultarIdentificadorAreaResponsable(int causalesGarantiasId)
        {
            int valor=0;
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {

                var result = (from d in DB.CausalesGarantias
                              where d.CausalesGarantiasId == causalesGarantiasId
                              select new
                              {
                                  d.IdentificadorAreaResponsable,

                              }).FirstOrDefault();
                valor = result.IdentificadorAreaResponsable.Value;

                return valor;

            }
        }
        public bool RegistrarAnalisisGarantia(int IngresoRevisionGarantiaId, string LoteFabricacion, string LoteEnsamble, string LoteCarga, string ModeloBateria, decimal Voltaje, decimal CCA, 
            decimal DencidadCelda1, decimal DencidadCelda2, decimal DencidadCelda3, decimal DencidadCelda4, decimal DencidadCelda5, decimal DencidadCelda6, string ResumenAnalisis, string AreaResponsable, 
            string Observaciones)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var result = new AnalisisRegistrosGarantias();
                    result.IngresoRevisionGarantiaId = IngresoRevisionGarantiaId;
                    result.LoteFabricacion = LoteFabricacion;
                    result.LoteEnsamble = LoteEnsamble;
                    result.LoteCarga = LoteCarga;
                    result.ModeloBateria = ModeloBateria;
                    result.Voltaje = Voltaje;
                    result.CCA = CCA;
                    result.DencidadCelda1 = DencidadCelda1;
                    result.DencidadCelda2 = DencidadCelda2;
                    result.DencidadCelda3 = DencidadCelda3;
                    result.DencidadCelda4 = DencidadCelda4;
                    result.DencidadCelda5 = DencidadCelda5;
                    result.DencidadCelda6 = DencidadCelda6;
                    result.ResumenAnalisis = ResumenAnalisis;
                    result.AreaResponsable = AreaResponsable; 
                    result.Observaciones = Observaciones;
                    result.FechaRegistroAnalisis = DateTime.Now;




                    DB.AnalisisRegistrosGarantias.Add(result);
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
    }
}