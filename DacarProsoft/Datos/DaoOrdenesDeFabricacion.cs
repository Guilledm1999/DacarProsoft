using DacarDatos.Datos;
using DacarProsoft.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace DacarProsoft.Datos
{
    public class DaoOrdenesDeFabricacion
    {
        private DaoUtilitarios daoUtilitarios { get; set; } = null;

        public List<OrdenesProduccion> ListadoOrdenesFabricacion()
        {
            daoUtilitarios = new DaoUtilitarios();
            var EstadosOrdenes = daoUtilitarios.ConsultarEstadosDeOrdenesProduccion();

            List<VU_REPORTEPRODUCCION> lst = new List<VU_REPORTEPRODUCCION>();
            DateTime fechaActual = DateTime.Now;
            DateTime fechaLimite = fechaActual.AddYears(-1);

            List<OrdenesProduccion> lst2 = new List<OrdenesProduccion>();


            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                var ListadoOrdenesFabricacion = from d in DB.VU_REPORTEPRODUCCION
                                                where d.CreateDate> fechaLimite orderby d.CreateDate 
                                                select new
                                                {
                                                    d.NumInterno,
                                                    d.SeriesName,
                                                    d.ItemCode,
                                                    d.ItemName,
                                                    d.CreateDate,
                                                    d.TipoProduccion,
                                                    d.Linea,
                                                    d.Status,
                                                    d.PlannedQty,
                                                    d.CmpltQty,
                                                    d.RjctQty,
                                                    d.Expr1,
                                                    d.BaseQty,//cantidad base
                                                    d.Expr2,
                                                    d.WhsName,
                                                    d.IssuedQty,//Cantidad emitida
                                                    d.Comments

                                                };



                foreach (var x in ListadoOrdenesFabricacion)
                {
                    DateTime fecha = Convert.ToDateTime(x.CreateDate, CultureInfo.InvariantCulture);
                    string fechaOrden = fecha.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string EstadoDeOrden= x.Status;

                    foreach (var dto in EstadosOrdenes)
                    {
                        if (x.Status==dto.EstadoSap) {
                            EstadoDeOrden = dto.DescripcionEstado;
                        }
                    }

                        lst2.Add(new OrdenesProduccion
                    {
                      
                        NumInterno = x.NumInterno,
                        SeriesName = x.SeriesName,
                        ItemCode = x.ItemCode,
                        ItemName = x.ItemName,
                        CreacionOrden = fechaOrden,
                        TipoProduccion = x.TipoProduccion,
                        Linea = x.Linea,
                        Status = EstadoDeOrden,
                        PlannedQty = x.PlannedQty,
                        CmpltQty = x.CmpltQty,
                        BaseQty = x.BaseQty,
                        WhsName = x.WhsName,
                        IssuedQty = x.IssuedQty,
                        Comments = x.Comments

                    });
                }
                return lst2;
            }

        }


        public List<OrdenesProduccion> ListadoOrdenesFabricacionPorEstado(String estado)
        {
            daoUtilitarios = new DaoUtilitarios();
            var EstadosOrdenes = daoUtilitarios.ConsultarEstadosDeOrdenesProduccion();

            List<VU_REPORTEPRODUCCION> lst = new List<VU_REPORTEPRODUCCION>();
            DateTime fechaActual = DateTime.Now;
            DateTime fechaLimite = fechaActual.AddYears(-1);

            List<OrdenesProduccion> lst2 = new List<OrdenesProduccion>();


            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                var ListadoOrdenesFabricacion = from d in DB.VU_REPORTEPRODUCCION
                                                where d.CreateDate > fechaLimite && d.Status==estado
                                                orderby d.CreateDate
                                                select new
                                                {
                                                    d.NumInterno,
                                                    d.SeriesName,
                                                    d.ItemCode,
                                                    d.ItemName,
                                                    d.CreateDate,
                                                    d.TipoProduccion,
                                                    d.Linea,
                                                    d.Status,
                                                    d.PlannedQty,
                                                    d.CmpltQty,
                                                    d.RjctQty,
                                                    d.Expr1,
                                                    d.BaseQty,//cantidad base
                                                    d.Expr2,
                                                    d.WhsName,
                                                    d.IssuedQty,//Cantidad emitida
                                                    d.Comments

                                                };



                foreach (var x in ListadoOrdenesFabricacion)
                {
                    DateTime fecha = Convert.ToDateTime(x.CreateDate, CultureInfo.InvariantCulture);
                    string fechaOrden = fecha.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string EstadoDeOrden = x.Status;

                    foreach (var dto in EstadosOrdenes)
                    {
                        if (x.Status == dto.EstadoSap)
                        {
                            EstadoDeOrden = dto.DescripcionEstado;
                        }
                    }

                    lst2.Add(new OrdenesProduccion
                    {

                        NumInterno = x.NumInterno,
                        SeriesName = x.SeriesName,
                        ItemCode = x.ItemCode,
                        ItemName = x.ItemName,
                        CreacionOrden = fechaOrden,
                        TipoProduccion = x.TipoProduccion,
                        Linea = x.Linea,
                        Status = EstadoDeOrden,
                        PlannedQty = x.PlannedQty,
                        CmpltQty = x.CmpltQty,
                        BaseQty = x.BaseQty,
                        WhsName = x.WhsName,
                        IssuedQty = x.IssuedQty,
                        Comments = x.Comments

                    });
                }
                return lst2;
            }

        }

    }
}