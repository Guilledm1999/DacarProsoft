using DacarDatos.Datos;
using DacarProsoft.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DacarProsoft.Datos
{
    public class DaoReportes
    {

        public List<EstadosPedidosSelect> StatusPedidos()
        {

            List<EstadosPedidosSelect> lst = new List<EstadosPedidosSelect>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {

                var Listado = (from d in DB.EstadosPedidosSelect
                               select new
                               {
                                   d.Descripcion,
                                   d.Codigo,
                               });
                foreach (var x in Listado)
                {
                    lst.Add(new EstadosPedidosSelect
                    {
                        Codigo = x.Codigo,
                        Descripcion = x.Descripcion
                    });
                }
                return lst;

            }

        }

        public List<ReporteGeneralPedidosExterior> ReporteGeneralPedidosExterior(int Tipo, DateTime FechaInicio, DateTime FechaFin)
        {
            string FechaEmision = null;
            string FechaRequerida = null;
            string FechaCargaLista = null;
            string DespachoPuerto = null;
            string Zarpe = null;
            string Arribo = null;
            string Entrega = null;
            string FechaIngresoSap = null;
            string FechaNuevoDespacho = null;
            string Observaciones = null;
            int CantidadTotalNueva = 0;

            List<ReporteGeneralPedidosExterior> lst = new List<ReporteGeneralPedidosExterior>();

            if (Tipo == 7)
            {
                using (DacarProsoftEntities DB = new DacarProsoftEntities())
                {

                    var Listado = (from d in DB.PedidoClienteCabecera
                                   join e in DB.PedidoClienteDetalleFinal on d.NumeroPedidoId equals e.PedidoClienteCabecera
                                   where  d.FechaEmision >= FechaInicio && d.FechaEmision <= FechaFin
                                   select new
                                   {
                                       d.NumeroPedidoId,
                                       d.CardCode,
                                       d.NombreCliente,
                                       d.FechaEmision,
                                       d.FechaRequerida,
                                       d.OrdenCompra,
                                       d.Direccion,
                                       d.TerminoImportacion,
                                       d.FechaCargaLista,
                                       d.FechaDespachoPuerto,
                                       d.FechaZarpe,
                                       d.FechaArribo,
                                       d.FechaEntrega,
                                       d.EstadoPedido,
                                       e.CantitadTotal,
                                       e.CantidadTotalNueva,
                                       e.PrecioFinalPedido,
                                       e.PesoNetoFinalPedido,
                                       e.Observaciones,
                                       e.FechaIngresadaSap,
                                       e.FechaNuevaDespacho
                                   });
                    foreach (var x in Listado)
                    {

                        if (x.FechaEmision != null)
                        {
                            DateTime fechaEmision = Convert.ToDateTime(x.FechaEmision, CultureInfo.InvariantCulture);
                            FechaEmision = fechaEmision.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            FechaEmision = "Sin Registro";
                        }

                        if (x.FechaRequerida != null)
                        {
                            DateTime fechaRequerida = Convert.ToDateTime(x.FechaRequerida, CultureInfo.InvariantCulture);
                            FechaRequerida = fechaRequerida.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            FechaRequerida = "Sin Registro";
                        }

                        if (x.FechaCargaLista != null)
                        {
                            DateTime fechacargaLista = Convert.ToDateTime(x.FechaCargaLista, CultureInfo.InvariantCulture);
                            FechaCargaLista = fechacargaLista.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            FechaCargaLista = "Sin Registro";
                        }


                        if (x.FechaDespachoPuerto != null)
                        {
                            DateTime despachoPuerto = Convert.ToDateTime(x.FechaDespachoPuerto, CultureInfo.InvariantCulture);
                            DespachoPuerto = despachoPuerto.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            DespachoPuerto = "Sin Registro";
                        }


                        if (x.FechaZarpe != null)
                        {
                            DateTime zarpe = Convert.ToDateTime(x.FechaZarpe, CultureInfo.InvariantCulture);
                            Zarpe = zarpe.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            Zarpe = "Sin Registro";
                        }


                        if (x.FechaArribo != null)
                        {
                            DateTime arribo = Convert.ToDateTime(x.FechaArribo, CultureInfo.InvariantCulture);
                            Arribo = arribo.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            Arribo = "Sin Registro";
                        }


                        if (x.FechaEntrega != null)
                        {
                            DateTime entrega = Convert.ToDateTime(x.FechaEntrega, CultureInfo.InvariantCulture);
                            Entrega = entrega.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            Entrega = "Sin Registro";
                        }


                        if (x.FechaIngresadaSap != null)
                        {
                            DateTime fechaIngresoSap = Convert.ToDateTime(x.FechaIngresadaSap, CultureInfo.InvariantCulture);
                            FechaIngresoSap = fechaIngresoSap.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            FechaIngresoSap = "Sin Registro";
                        }


                        if (x.FechaNuevaDespacho != null)
                        {
                            DateTime fechaNuevoDespacho = Convert.ToDateTime(x.FechaNuevaDespacho, CultureInfo.InvariantCulture);
                            FechaNuevoDespacho = fechaNuevoDespacho.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            FechaNuevoDespacho = "Sin Registro";
                        }

                        if (x.Observaciones != null && x.Observaciones != "")
                        {
                            Observaciones = x.Observaciones;
                        }
                        else
                        {
                            Observaciones = "Sin Observaciones";
                        }
                        if (x.CantidadTotalNueva != null)
                        {
                            CantidadTotalNueva = x.CantidadTotalNueva.Value;
                        }
                        else
                        {
                            CantidadTotalNueva = x.CantitadTotal.Value;
                        }
                        string estatus = ConsultarEstado(x.EstadoPedido.Value);


                        lst.Add(new ReporteGeneralPedidosExterior
                        {
                            NumeroPedidoId = x.NumeroPedidoId,
                            CardCode = x.CardCode,
                            NombreCliente = x.NombreCliente,
                            FechaEmision = FechaEmision,
                            FechaRequerida = FechaRequerida,
                            OrdenCompra = x.OrdenCompra,
                            Direccion = x.Direccion,
                            TerminoImportacion = x.TerminoImportacion,
                            Estado = estatus,
                            FechaCargaLista = FechaCargaLista,
                            FechaDespachoPuerto = DespachoPuerto,
                            FechaZarpe = Zarpe,
                            FechaArribo = Arribo,
                            FechaEntrega = Entrega,

                            Cantidad = x.CantitadTotal.Value,
                            CantidadNueva = CantidadTotalNueva,
                            PrecioTotal = x.PrecioFinalPedido.Value,
                            PesoNetoTotal = x.PesoNetoFinalPedido.Value,
                            Observaciones = Observaciones,
                            FechaIngresadaSap = FechaIngresoSap,
                            FechaNuevoDespacho = FechaNuevoDespacho

                        });
                    }
                    return lst;

                }
            }

            else {
                using (DacarProsoftEntities DB = new DacarProsoftEntities())
                {

                    var Listado = (from d in DB.PedidoClienteCabecera
                                   join e in DB.PedidoClienteDetalleFinal on d.NumeroPedidoId equals e.PedidoClienteCabecera
                                   where d.EstadoPedido == Tipo && d.FechaEmision >= FechaInicio && d.FechaEmision <= FechaFin
                                   select new
                                   {
                                       d.NumeroPedidoId,
                                       d.CardCode,
                                       d.NombreCliente,
                                       d.FechaEmision,
                                       d.FechaRequerida,
                                       d.OrdenCompra,
                                       d.Direccion,
                                       d.TerminoImportacion,
                                       d.FechaCargaLista,
                                       d.FechaDespachoPuerto,
                                       d.FechaZarpe,
                                       d.FechaArribo,
                                       d.FechaEntrega,
                                       d.EstadoPedido,
                                       e.CantitadTotal,
                                       e.CantidadTotalNueva,
                                       e.PrecioFinalPedido,
                                       e.PesoNetoFinalPedido,
                                       e.Observaciones,
                                       e.FechaIngresadaSap,
                                       e.FechaNuevaDespacho
                                   });
                    foreach (var x in Listado)
                    {

                        if (x.FechaEmision != null)
                        {
                            DateTime fechaEmision = Convert.ToDateTime(x.FechaEmision, CultureInfo.InvariantCulture);
                            FechaEmision = fechaEmision.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            FechaEmision = "Sin Registro";
                        }

                        if (x.FechaRequerida != null)
                        {
                            DateTime fechaRequerida = Convert.ToDateTime(x.FechaRequerida, CultureInfo.InvariantCulture);
                            FechaRequerida = fechaRequerida.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            FechaRequerida = "Sin Registro";
                        }

                        if (x.FechaCargaLista != null)
                        {
                            DateTime fechacargaLista = Convert.ToDateTime(x.FechaCargaLista, CultureInfo.InvariantCulture);
                            FechaCargaLista = fechacargaLista.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            FechaCargaLista = "Sin Registro";
                        }


                        if (x.FechaDespachoPuerto != null)
                        {
                            DateTime despachoPuerto = Convert.ToDateTime(x.FechaDespachoPuerto, CultureInfo.InvariantCulture);
                            DespachoPuerto = despachoPuerto.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            DespachoPuerto = "Sin Registro";
                        }


                        if (x.FechaZarpe != null)
                        {
                            DateTime zarpe = Convert.ToDateTime(x.FechaZarpe, CultureInfo.InvariantCulture);
                            Zarpe = zarpe.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            Zarpe = "Sin Registro";
                        }


                        if (x.FechaArribo != null)
                        {
                            DateTime arribo = Convert.ToDateTime(x.FechaArribo, CultureInfo.InvariantCulture);
                            Arribo = arribo.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            Arribo = "Sin Registro";
                        }


                        if (x.FechaEntrega != null)
                        {
                            DateTime entrega = Convert.ToDateTime(x.FechaEntrega, CultureInfo.InvariantCulture);
                            Entrega = entrega.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            Entrega = "Sin Registro";
                        }


                        if (x.FechaIngresadaSap != null)
                        {
                            DateTime fechaIngresoSap = Convert.ToDateTime(x.FechaIngresadaSap, CultureInfo.InvariantCulture);
                            FechaIngresoSap = fechaIngresoSap.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            FechaIngresoSap = "Sin Registro";
                        }


                        if (x.FechaNuevaDespacho != null)
                        {
                            DateTime fechaNuevoDespacho = Convert.ToDateTime(x.FechaNuevaDespacho, CultureInfo.InvariantCulture);
                            FechaNuevoDespacho = fechaNuevoDespacho.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            FechaNuevoDespacho = "Sin Registro";
                        }

                        if (x.Observaciones != null && x.Observaciones != "")
                        {
                            Observaciones = x.Observaciones;
                        }
                        else
                        {
                            Observaciones = "Sin Observaciones";
                        }
                        if (x.CantidadTotalNueva != null)
                        {
                            CantidadTotalNueva = x.CantidadTotalNueva.Value;
                        }
                        else
                        {
                            CantidadTotalNueva = x.CantitadTotal.Value;
                        }

                        string estatus=ConsultarEstado(x.EstadoPedido.Value);

                        lst.Add(new ReporteGeneralPedidosExterior
                        {
                            NumeroPedidoId = x.NumeroPedidoId,
                            CardCode = x.CardCode,
                            NombreCliente = x.NombreCliente,
                            FechaEmision = FechaEmision,
                            FechaRequerida = FechaRequerida,
                            OrdenCompra = x.OrdenCompra,
                            Direccion = x.Direccion,
                            TerminoImportacion = x.TerminoImportacion,
                            Estado = estatus,
                            FechaCargaLista = FechaCargaLista,
                            FechaDespachoPuerto = DespachoPuerto,
                            FechaZarpe = Zarpe,
                            FechaArribo = Arribo,
                            FechaEntrega = Entrega,

                            Cantidad = x.CantitadTotal.Value,
                            CantidadNueva = CantidadTotalNueva,
                            PrecioTotal = x.PrecioFinalPedido.Value,
                            PesoNetoTotal = x.PesoNetoFinalPedido.Value,
                            Observaciones = Observaciones,
                            FechaIngresadaSap = FechaIngresoSap,
                            FechaNuevoDespacho = FechaNuevoDespacho

                        });
                    }
                    return lst;

                }
            }        

        }

        public string ConsultarEstado(int EstadoId)
        {
            string result = "";
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var FechaPedido = (from d in DB.EstadosPedidos
                                   where d.Codigo == EstadoId
                                   select new
                                   {

                                       d.Descripcion
                                   }).FirstOrDefault(); ;
                if (FechaPedido.Descripcion != null)
                {

                    result = FechaPedido.Descripcion;
                    return result;

                }
                else
                {
                    result = "Sin especificar";
                    return result;

                }
            }
        }

        public List<ReporteGeneralDeControl> ReporteGeneralDeControl(int Tipo, DateTime FechaInicio, DateTime FechaFin)
        {
            string FechaEmision = null;
            string FechaIngresoSap = null;
            string TiempoAtencion = null;
            string FechaRequerida = null;
            string FechaNuevoDespacho = null;
            string FechaPlazoEntrega = null;
            string FechaCargaLista = null;
            string FechaPlazoCargaLista = null;

            List<ReporteGeneralDeControl> lst = new List<ReporteGeneralDeControl>();

            if (Tipo == 7)
            {
                using (DacarProsoftEntities DB = new DacarProsoftEntities())
                {

                    var Listado = (from d in DB.PedidoClienteCabecera
                                   join e in DB.PedidoClienteDetalleFinal on d.NumeroPedidoId equals e.PedidoClienteCabecera
                                   where d.FechaEmision >= FechaInicio && d.FechaEmision <= FechaFin
                                   orderby d.NumeroPedidoId descending

                                   select new
                                   {
                                       d.NumeroPedidoId,
                                       d.NombreCliente,
                                       d.OrdenCompra,
                                       d.EstadoPedido,
                                       d.FechaEmision,
                                       d.FechaRequerida,
                                       d.FechaCargaLista,
                                       e.FechaIngresadaSap,
                                       e.FechaNuevaDespacho
                                      
                                   });
                    foreach (var x in Listado)
                    {

                        if (x.FechaEmision != null)
                        {
                            DateTime fechaEmision = Convert.ToDateTime(x.FechaEmision, CultureInfo.InvariantCulture);
                            FechaEmision = fechaEmision.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            FechaEmision = "Sin Registro";
                        }
                        if (x.FechaIngresadaSap != null)
                        {
                            DateTime fechaIngresoSap = Convert.ToDateTime(x.FechaIngresadaSap, CultureInfo.InvariantCulture);
                            FechaIngresoSap = fechaIngresoSap.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            FechaIngresoSap = "Sin Registro";
                        }

                        if (x.FechaRequerida != null)
                        {
                            DateTime fechaRequerida = Convert.ToDateTime(x.FechaRequerida, CultureInfo.InvariantCulture);
                            FechaRequerida = fechaRequerida.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            FechaRequerida = "Sin Registro";
                        }

                        if (x.FechaCargaLista != null)
                        {
                            DateTime fechaCargaLista = Convert.ToDateTime(x.FechaCargaLista, CultureInfo.InvariantCulture);
                            FechaCargaLista = fechaCargaLista.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            FechaCargaLista = "Sin Registro";
                        }

                        if (x.FechaNuevaDespacho != null)
                        {
                            DateTime fechaNuevoDespcho = Convert.ToDateTime(x.FechaNuevaDespacho, CultureInfo.InvariantCulture);
                            FechaNuevoDespacho = fechaNuevoDespcho.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            FechaNuevoDespacho = "Sin Registro";
                        }

                        if (x.FechaIngresadaSap != null && x.FechaEmision!=null)
                        {
                            TimeSpan difFechas = Convert.ToDateTime(x.FechaIngresadaSap) - Convert.ToDateTime(x.FechaEmision);
                            int dias = difFechas.Days;

                            TiempoAtencion = ""+dias;
                        }
                        else
                        {
                            TiempoAtencion = "Sin Registro";
                        }

                        if (x.FechaNuevaDespacho != null && x.FechaEmision != null)
                        {
                            TimeSpan difFechas = Convert.ToDateTime(x.FechaNuevaDespacho) - Convert.ToDateTime(x.FechaEmision);
                            int dias = difFechas.Days;

                            FechaPlazoEntrega = "" + dias;
                        }
                        else
                        {
                            FechaPlazoEntrega = "Sin Registro";
                        }

                        if (x.FechaCargaLista != null && x.FechaEmision != null)
                        {
                            TimeSpan difFechas = Convert.ToDateTime(x.FechaCargaLista) - Convert.ToDateTime(x.FechaEmision);
                            int dias = difFechas.Days;

                            FechaPlazoCargaLista = "" + dias;
                        }
                        else
                        {
                            FechaPlazoCargaLista = "Sin Registro";
                        }

                        string estatus = ConsultarEstado(x.EstadoPedido.Value);


                        lst.Add(new ReporteGeneralDeControl
                        {
                            NumeroPedidoId = x.NumeroPedidoId,
                            NombreCliente = x.NombreCliente,
                            Estado = estatus,
                            OrdenCompra = x.OrdenCompra,
                            FechaEmision = FechaEmision,
                            FechaIngresoSap = FechaIngresoSap,
                            TiempoAtencion=TiempoAtencion,
                            FechaRequerida = FechaRequerida,
                            FechaDespacho= FechaNuevoDespacho,
                            FechaPlazoEntrega= FechaPlazoEntrega,
                            FechaCargaLista = FechaCargaLista,
                            FechaPlazoCargaLista= FechaPlazoCargaLista

                        });
                    }
                    return lst;

                }
            }

            else
            {
                using (DacarProsoftEntities DB = new DacarProsoftEntities())
                {

                    var Listado = (from d in DB.PedidoClienteCabecera
                                   join e in DB.PedidoClienteDetalleFinal on d.NumeroPedidoId equals e.PedidoClienteCabecera
                                   where d.EstadoPedido == Tipo && d.FechaEmision >= FechaInicio && d.FechaEmision <= FechaFin
                                   orderby d.NumeroPedidoId descending

                                   select new
                                   {
                                       d.NumeroPedidoId,
                                       d.NombreCliente,
                                       d.OrdenCompra,
                                       d.EstadoPedido,
                                       d.FechaEmision,
                                       d.FechaRequerida,
                                       d.FechaCargaLista,
                                       e.FechaIngresadaSap,
                                       e.FechaNuevaDespacho
                                   });
                    foreach (var x in Listado)
                    {

                        if (x.FechaEmision != null)
                        {
                            DateTime fechaEmision = Convert.ToDateTime(x.FechaEmision, CultureInfo.InvariantCulture);
                            FechaEmision = fechaEmision.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            FechaEmision = "Sin Registro";
                        }
                        if (x.FechaIngresadaSap != null)
                        {
                            DateTime fechaIngresoSap = Convert.ToDateTime(x.FechaIngresadaSap, CultureInfo.InvariantCulture);
                            FechaIngresoSap = fechaIngresoSap.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            FechaIngresoSap = "Sin Registro";
                        }

                        if (x.FechaRequerida != null)
                        {
                            DateTime fechaRequerida = Convert.ToDateTime(x.FechaRequerida, CultureInfo.InvariantCulture);
                            FechaRequerida = fechaRequerida.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            FechaRequerida = "Sin Registro";
                        }

                        if (x.FechaCargaLista != null)
                        {
                            DateTime fechaCargaLista = Convert.ToDateTime(x.FechaCargaLista, CultureInfo.InvariantCulture);
                            FechaCargaLista = fechaCargaLista.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            FechaCargaLista = "Sin Registro";
                        }

                        if (x.FechaNuevaDespacho != null)
                        {
                            DateTime fechaNuevoDespcho = Convert.ToDateTime(x.FechaNuevaDespacho, CultureInfo.InvariantCulture);
                            FechaNuevoDespacho = fechaNuevoDespcho.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            FechaNuevoDespacho = "Sin Registro";
                        }

                        if (x.FechaIngresadaSap != null && x.FechaEmision != null)
                        {
                            TimeSpan difFechas = Convert.ToDateTime(x.FechaIngresadaSap) - Convert.ToDateTime(x.FechaEmision);
                            int dias = difFechas.Days;

                            TiempoAtencion = "" + dias;
                        }
                        else
                        {
                            TiempoAtencion = "Sin Registro";
                        }

                        if (x.FechaNuevaDespacho != null && x.FechaEmision != null)
                        {
                            TimeSpan difFechas = Convert.ToDateTime(x.FechaNuevaDespacho) - Convert.ToDateTime(x.FechaEmision);
                            int dias = difFechas.Days;

                            FechaPlazoEntrega = "" + dias;
                        }
                        else
                        {
                            FechaPlazoEntrega = "Sin Registro";
                        }

                        if (x.FechaCargaLista != null && x.FechaEmision != null)
                        {
                            TimeSpan difFechas = Convert.ToDateTime(x.FechaCargaLista) - Convert.ToDateTime(x.FechaEmision);
                            int dias = difFechas.Days;

                            FechaPlazoCargaLista = "" + dias;
                        }
                        else
                        {
                            FechaPlazoCargaLista = "Sin Registro";
                        }

                        string estatus = ConsultarEstado(x.EstadoPedido.Value);

                        lst.Add(new ReporteGeneralDeControl
                        {
                            NumeroPedidoId = x.NumeroPedidoId,
                            NombreCliente = x.NombreCliente,
                            Estado = estatus,
                            OrdenCompra = x.OrdenCompra,
                            FechaEmision = FechaEmision,
                            FechaIngresoSap = FechaIngresoSap,
                            TiempoAtencion = TiempoAtencion,
                            FechaRequerida = FechaRequerida,
                            FechaDespacho = FechaNuevoDespacho,
                            FechaPlazoEntrega = FechaPlazoEntrega,
                            FechaCargaLista = FechaCargaLista,
                            FechaPlazoCargaLista = FechaPlazoCargaLista

                        });
                    }
                    return lst;

                }
            }

        }
        public List<FiltrosCategoriaReporteGarantias> ConsultarFiltrosCategoriaGarantias()
        {

            List<FiltrosCategoriaReporteGarantias> lst = new List<FiltrosCategoriaReporteGarantias>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {

                var Listado = (from d in DB.FiltrosCategoriaReporteGarantias
                               where d.Estado!=false
                               select new
                               {
                                   d.Descripcion,
                                   d.Valor,
                               });
                foreach (var x in Listado)
                {
                    lst.Add(new FiltrosCategoriaReporteGarantias
                    {
                        Valor = x.Valor,
                        Descripcion = x.Descripcion,
                    });
                }
                return lst;

            }

        }

        public List<ModelChartGarantias> ReporteAnalisisGarantiaPorCausales(DateTime FechaInicio, DateTime FechaFin)
        {


            DateTime nuevaFechaFin = FechaFin;
            nuevaFechaFin = nuevaFechaFin.AddDays(1);
            decimal acum = 0;
            List<ModelChartGarantias> lst = new List<ModelChartGarantias>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado= from d in DB.AnalisisRegistrosGarantias
                             orderby d.AnalisisRegistrosGarantiasId
                             where d.FechaRegistroAnalisis >= FechaInicio && d.FechaRegistroAnalisis <= nuevaFechaFin
                             group d by d.ResumenAnalisis into grp
                             select new {
                                 Modelo = grp.Key, 
                                 Contador = grp.Count() 
                             };
                foreach (var x in Listado)
                {
                    acum = acum + x.Contador;
                }
                foreach (var x in Listado)
                {
                    lst.Add(new ModelChartGarantias
                    {
                        Descripcion = x.Modelo,
                        Valor = x.Contador,
                        Porcentaje= decimal.Round(((x.Contador * 100) / acum), 2)
                    });
                }
                return lst;

            }

        }

        public List<ModelChartGarantias> ReporteAnalisisGarantiaPorArea(DateTime FechaInicio, DateTime FechaFin)
        {


            DateTime nuevaFechaFin = FechaFin;
            nuevaFechaFin = nuevaFechaFin.AddDays(1);
            decimal acum = 0;
            List<ModelChartGarantias> lst = new List<ModelChartGarantias>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = from d in DB.AnalisisRegistrosGarantias
                              orderby d.AnalisisRegistrosGarantiasId
                              where d.FechaRegistroAnalisis >= FechaInicio && d.FechaRegistroAnalisis <= nuevaFechaFin
                              group d by d.AreaResponsable into grp
                              select new
                              {
                                  Modelo = grp.Key,
                                  Contador = grp.Count()
                              };
                foreach (var x in Listado)
                {
                    acum = acum + x.Contador;
                }
                foreach (var x in Listado)
                {
                    lst.Add(new ModelChartGarantias
                    {
                        Descripcion = x.Modelo,
                        Valor = x.Contador,
                        Porcentaje= decimal.Round(((x.Contador * 100) / acum), 2)
                    });
                }
                return lst;

            }

        }
        public List<ModelChartGarantias> ReporteAnalisisGarantiaPorModelo(DateTime FechaInicio, DateTime FechaFin)
        {


            DateTime nuevaFechaFin = FechaFin;
            nuevaFechaFin = nuevaFechaFin.AddDays(1);
            decimal acum = 0;
            List<ModelChartGarantias> lst = new List<ModelChartGarantias>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = from d in DB.AnalisisRegistrosGarantias
                              orderby d.AnalisisRegistrosGarantiasId
                              where d.FechaRegistroAnalisis >= FechaInicio && d.FechaRegistroAnalisis <= nuevaFechaFin
                              group d by d.ModeloBateria into grp
                              select new
                              {
                                  Modelo = grp.Key,
                                  Contador = grp.Count()
                              };
                foreach (var x in Listado)
                {
                    acum = acum + x.Contador;
                }
                foreach (var x in Listado)
                {
                    lst.Add(new ModelChartGarantias
                    {
                        Descripcion = x.Modelo,
                        Valor = x.Contador,
                        Porcentaje= decimal.Round(((x.Contador * 100) / acum), 2)
                    });
                }
                return lst;

            }

        }
        public List<ModelChartGarantias> ReporteAnalisisGarantiaPorAplicacion(DateTime FechaInicio, DateTime FechaFin)
        {

            DateTime nuevaFechaFin = FechaFin;
            nuevaFechaFin = nuevaFechaFin.AddDays(1);
            decimal acum = 0;
            List<ModelChartGarantias> lst = new List<ModelChartGarantias>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = from d in DB.AnalisisRegistrosGarantias join
                              e in DB.ModelosMarcasPropias on d.ModeloBateria equals e.Referencia join
                              f in DB.LineasMarcasPropias on e.Linea equals f.Identificador
                              orderby d.AnalisisRegistrosGarantiasId
                              where d.FechaRegistroAnalisis >= FechaInicio && d.FechaRegistroAnalisis <= nuevaFechaFin
                              group d by f.Referencia into grp
                              select new
                              {
                                  Modelo = grp.Key,
                                  Contador = grp.Count()
                              };
                foreach (var x in Listado)
                {
                    acum = acum + x.Contador;
                }
                foreach (var x in Listado)
                {
                    lst.Add(new ModelChartGarantias
                    {
                        Descripcion = x.Modelo,
                        Valor = x.Contador,
                        Porcentaje=decimal.Round(((x.Contador * 100) / acum), 2)
                    });
                }
                return lst;

            }

        }

        public List<ModelChartGarantias> ReporteAnalisisGarantiaPorMeses(DateTime FechaInicio, DateTime FechaFin)
        {
            string Mes;
            decimal acum = 0;
            DateTime nuevaFechaFin = FechaFin;
            nuevaFechaFin = nuevaFechaFin.AddDays(1);

            List<ModelChartGarantias> lst = new List<ModelChartGarantias>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {

                var Listado = from d in DB.AnalisisRegistrosGarantias
                              where d.FechaRegistroAnalisis >= FechaInicio && d.FechaRegistroAnalisis <= nuevaFechaFin

                              group d by new {d.FechaRegistroAnalisis.Month } into ut
                                select new
                                {
                                    Contador = ut.Count(),
                                    MonthNumber = ut.Key.Month,
                                };
                foreach (var x in Listado)
                {
                    acum = acum + x.Contador;
                }

                foreach (var x in Listado)
                {
                    Mes = BuscarNombreMes(x.MonthNumber);
                    lst.Add(new ModelChartGarantias
                    {
                        Descripcion = Mes,
                        Valor = x.Contador,
                        Porcentaje=decimal.Round(((x.Contador * 100) / acum), 2)
                    });
                }
                return lst;

            }

        }
        public string BuscarNombreMes(int num) {

            string result;
            using (DacarProsoftEntities DB = new DacarProsoftEntities()) {
                var valor = (from d in DB.Meses
                             where d.Orden == num
                             select new
                             {
                                 d.Nombre
                             }).FirstOrDefault();
                result = valor.Nombre;

                return result;
            
            }
        }
        public int BuscarNumeroMes(string mes)
        {

            int result;
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var valor = (from d in DB.Meses
                             where d.Nombre == mes
                             select new
                             {
                                 d.Orden
                             }).FirstOrDefault();
                result = valor.Orden.Value;

                return result;

            }
        }

        public List<ModelChartGarantias> ReporteAnalisisGarantiaPorAnio1(int Anio)
        {
            string Mes;
            decimal acum = 0;

            List<ModelChartGarantias> lst = new List<ModelChartGarantias>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {

                var Listado = from d in DB.AnalisisRegistrosGarantias
                              where d.FechaRegistroAnalisis.Year == Anio

                              group d by new { d.FechaRegistroAnalisis.Month } into ut
                              select new
                              {
                                  Contador = ut.Count(),
                                  MonthNumber = ut.Key.Month,
                              };
                foreach (var x in Listado)
                {
                    acum = acum + x.Contador;
                }
                foreach (var x in Listado)
                {
                    Mes = BuscarNombreMes(x.MonthNumber);
                    lst.Add(new ModelChartGarantias
                    {
                        Descripcion = Mes,
                        Valor = x.Contador,
                        Porcentaje=decimal.Round(((x.Contador * 100) / acum), 2)
                    });
                }
                return lst;

            }

        }
        public List<ModelChartGarantias> ReporteAnalisisGarantiaPorAnio2(int Anio)
        {
            string Mes;
            decimal acum = 0;

            List<ModelChartGarantias> lst = new List<ModelChartGarantias>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {

                var Listado = from d in DB.AnalisisRegistrosGarantias
                              where d.FechaRegistroAnalisis.Year == Anio

                              group d by new { d.FechaRegistroAnalisis.Month } into ut
                              select new
                              {
                                  Contador = ut.Count(),
                                  MonthNumber = ut.Key.Month,
                              };
                foreach (var x in Listado)
                {
                    acum = acum + x.Contador;
                }
                foreach (var x in Listado)
                {
                    Mes = BuscarNombreMes(x.MonthNumber);
                    lst.Add(new ModelChartGarantias
                    {
                        Descripcion = Mes,
                        Valor = x.Contador,
                        Porcentaje = decimal.Round(((x.Contador * 100) / acum), 2)
                    });
                }
                return lst;

            }

        }

        public List<ModelChartGarantias> ReporteDetalleAnalisisCausalesMeses(int anio, string mes)
        {
            decimal acum = 0;
            var numeromes = BuscarCodigoMes(mes);
            //DateTime nuevaFechaFin = FechaFin;
            //nuevaFechaFin = nuevaFechaFin.AddDays(1);

            List<ModelChartGarantias> lst = new List<ModelChartGarantias>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = from d in DB.AnalisisRegistrosGarantias
                              orderby d.AnalisisRegistrosGarantiasId
                              where d.FechaRegistroAnalisis.Year==anio && d.FechaRegistroAnalisis.Month==numeromes
                              group d by d.ResumenAnalisis into grp
                              select new
                              {
                                  Modelo = grp.Key,
                                  Contador = grp.Count()
                              };
                foreach (var x in Listado)
                {
                    acum = acum + x.Contador;
                }

                foreach (var x in Listado)
                {
                    lst.Add(new ModelChartGarantias
                    {
                        Descripcion = x.Modelo,
                        Valor = x.Contador,
                        Porcentaje=decimal.Round(((x.Contador * 100) / acum), 2)
                    });
                }
                return lst;

            }

        }

        public List<ModelChartGarantias> ReporteDetalleAnalisisModelosMeses(int anio, string mes)
        {
            decimal acum=0;
            var numeromes = BuscarCodigoMes(mes);
            //DateTime nuevaFechaFin = FechaFin;
            //nuevaFechaFin = nuevaFechaFin.AddDays(1);

            List<ModelChartGarantias> lst = new List<ModelChartGarantias>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = from d in DB.AnalisisRegistrosGarantias
                              orderby d.AnalisisRegistrosGarantiasId
                              where d.FechaRegistroAnalisis.Year == anio && d.FechaRegistroAnalisis.Month == numeromes
                              group d by d.ModeloBateria into grp
                              select new
                              {
                                  Modelo = grp.Key,
                                  Contador = grp.Count()
                              };

                foreach (var x in Listado)
                {
                    acum = acum + x.Contador;
                }

                foreach (var x in Listado)
                {
                    lst.Add(new ModelChartGarantias
                    {
                        Descripcion = x.Modelo,
                        Valor = x.Contador,
                        Porcentaje=decimal.Round(((x.Contador * 100) / acum), 2)
                    });
                }
                return lst;

            }

        }



        public int BuscarCodigoMes(string Referencia)
        {

            int result;
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var valor = (from d in DB.Meses
                             where d.Nombre == Referencia
                             select new
                             {
                                 d.Orden
                             }).FirstOrDefault();
                result = valor.Orden.Value;

                return result;

            }
        }

        public List<ModelChartGarantias> ReporteAnalisisGarantiaPorNombreClienteMeses(string NombreCliente, int AnioConsulta)
        {
            string cadena = NombreCliente;

            //cadena = Referencia.Replace(" ", "-");
            cadena = cadena.Replace("\"", "");


            string Mes;
            decimal acum = 0;
           

            List<ModelChartGarantias> lst = new List<ModelChartGarantias>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {

                var Listado = from d in DB.AnalisisRegistrosGarantias join
                              e in DB.IngresoRevisionGarantiaCabecera on d.IngresoRevisionGarantiaId equals e.IngresoRevisionGarantiaId
                              where d.FechaRegistroAnalisis.Year==AnioConsulta && e.Cliente== cadena
                              group d by new { d.FechaRegistroAnalisis.Month } into ut
                              select new
                              {
                                  Contador = ut.Count(),
                                  MonthNumber = ut.Key.Month,
                
                              };

                foreach (var x in Listado)
                {
                    acum = acum + x.Contador;
                }

                foreach (var x in Listado)
                {
                    Mes = BuscarNombreMes(x.MonthNumber);
                    lst.Add(new ModelChartGarantias
                    {
                        Descripcion = Mes,
                        Valor = x.Contador,
                        Porcentaje=decimal.Round(((x.Contador * 100) / acum), 2)
                    });
                }
                return lst;

            }

        }
        public List<ModelChartGarantias> ReporteDetalleAnalisisCausalesMesesPorCliente(int anio, string mes, string cliente)
        {
            decimal acum = 0;
            string cadena = cliente.Replace("\"", "");
            var numeromes = BuscarCodigoMes(mes);
            //DateTime nuevaFechaFin = FechaFin;
            //nuevaFechaFin = nuevaFechaFin.AddDays(1);

            List<ModelChartGarantias> lst = new List<ModelChartGarantias>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = from d in DB.AnalisisRegistrosGarantias join
                              e in DB.IngresoRevisionGarantiaCabecera on d.IngresoRevisionGarantiaId equals e.IngresoRevisionGarantiaId
                              orderby d.AnalisisRegistrosGarantiasId
                              where d.FechaRegistroAnalisis.Year == anio && d.FechaRegistroAnalisis.Month == numeromes && e.Cliente== cadena
                              group d by d.ResumenAnalisis into grp
                              select new
                              {
                                  Modelo = grp.Key,
                                  Contador = grp.Count()
                              };
                foreach (var x in Listado)
                {
                    acum = acum + x.Contador;
                }

                foreach (var x in Listado)
                {
                    lst.Add(new ModelChartGarantias
                    {
                        Descripcion = x.Modelo,
                        Valor = x.Contador,
                        Porcentaje= decimal.Round(((x.Contador * 100) / acum), 2)
                    });
                }
                return lst;

            }

        }

        public List<ModelChartGarantias> ReporteDetalleAnalisisModelosMesesPorCliente(int anio, string mes, string cliente)
        {
            string cadena = cliente.Replace("\"", "");
            var numeromes = BuscarCodigoMes(mes);
            decimal acum = 0;
            //DateTime nuevaFechaFin = FechaFin;
            //nuevaFechaFin = nuevaFechaFin.AddDays(1);

            List<ModelChartGarantias> lst = new List<ModelChartGarantias>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = from d in DB.AnalisisRegistrosGarantias
                              join
e in DB.IngresoRevisionGarantiaCabecera on d.IngresoRevisionGarantiaId equals e.IngresoRevisionGarantiaId
                              orderby d.AnalisisRegistrosGarantiasId
                              where d.FechaRegistroAnalisis.Year == anio && d.FechaRegistroAnalisis.Month == numeromes && e.Cliente == cadena
                              group d by d.ModeloBateria into grp
                              select new
                              {
                                  Modelo = grp.Key,
                                  Contador = grp.Count()
                              };
                foreach (var x in Listado)
                {
                    acum = acum + x.Contador;
                }

                foreach (var x in Listado)
                {
                    lst.Add(new ModelChartGarantias
                    {
                        Descripcion = x.Modelo,
                        Valor = x.Contador,
                        Porcentaje = decimal.Round(((x.Contador * 100) / acum), 2)

                    });
                }
                return lst;

            }

        }

       
        public List<GrupoClientes> ListadoGrupoDeCliente()
        {
            List<GrupoClientes> lst = new List<GrupoClientes>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = from d in DB.GrupoClientes
                              select new
                              {
                                  d.GroupCode,
                                  d.GroupName
                              };
                foreach (var x in Listado)
                {
                    lst.Add(new GrupoClientes
                    {
                        GroupCode = x.GroupCode,
                        GroupName = x.GroupName
                    });
                }
                return lst;
            }
        }
        public List<ClienteClase> ListadoClienteClase()
        {
            List<ClienteClase> lst = new List<ClienteClase>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = from d in DB.ClienteClase
                              select new
                                             {                 
                                                 d.Codigo,
                                                 d.Nombre
                                             };
                    foreach (var x in Listado)
                    {
                        lst.Add(new ClienteClase
                        {
                           Codigo=x.Codigo,
                           Nombre=x.Nombre
                        });
                    }      
                return lst;
            }
        }
        public List<ClienteLinea> ListadoClienteLinea()
        {
            List<ClienteLinea> lst = new List<ClienteLinea>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = from d in DB.ClienteLinea
                              select new
                              {
                                  d.Codigo,
                                  d.Nombre
                              };
                foreach (var x in Listado)
                {
                    lst.Add(new ClienteLinea
                    {
                        Codigo = x.Codigo,
                        Nombre = x.Nombre
                    });
                }
                return lst;
            }
        }

        public List<ModelChartGarantias> ReporteDetalleAnalisisModelosPorTipoCliente(string tipoCliente, string ClienteClase, string ClienteLinea, int Anio)
        {
            string Mes;
            decimal acum = 0;
            List<ModelChartGarantias> lst = new List<ModelChartGarantias>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                
                var Listado = from d in DB.AnalisisRegistrosGarantias
                              join
                              e in DB.IngresoRevisionGarantiaCabecera on d.IngresoRevisionGarantiaId equals e.IngresoRevisionGarantiaId
                               where d.FechaRegistroAnalisis.Year == Anio && e.TipoCliente == tipoCliente && e.ClienteClase == ClienteClase && e.ClienteLinea == ClienteLinea
                               group d by new { d.FechaRegistroAnalisis.Month } into ut
                              select new
                              {
                                  Contador = ut.Count(),
                                  MonthNumber = ut.Key.Month,
                              };


                foreach (var x in Listado)
                {
                    acum = acum + x.Contador;
                }

                    foreach (var x in Listado)
                {
                    Mes = BuscarNombreMes(x.MonthNumber);

                    lst.Add(new ModelChartGarantias
                    {
                        Descripcion = Mes,
                        Valor = x.Contador,
                        Porcentaje= decimal.Round(((x.Contador*100)/acum),2)

                    });
                }
                return lst;

            }
        }

        public List<ModelChartGarantias> ReporteDetalleAnalisisCausalesMesesPorTipoCliente(string tipoCliente, string ClienteClase, string ClienteLinea, int Anio, string Mes)
        {
            decimal acum = 0;
            var numeromes = BuscarCodigoMes(Mes);
            //DateTime nuevaFechaFin = FechaFin;
            //nuevaFechaFin = nuevaFechaFin.AddDays(1);

            List<ModelChartGarantias> lst = new List<ModelChartGarantias>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = from d in DB.AnalisisRegistrosGarantias
                              join
                              e in DB.IngresoRevisionGarantiaCabecera on d.IngresoRevisionGarantiaId equals e.IngresoRevisionGarantiaId
                              orderby d.AnalisisRegistrosGarantiasId
                              where d.FechaRegistroAnalisis.Year == Anio && d.FechaRegistroAnalisis.Month == numeromes && e.TipoCliente==tipoCliente && e.ClienteClase==ClienteClase && e.ClienteLinea==ClienteLinea 
                              group d by d.ResumenAnalisis into grp
                              select new
                              {
                                  Modelo = grp.Key,
                                  Contador = grp.Count()
                              };
                foreach (var x in Listado)
                {
                    acum = acum + x.Contador;
                }

                foreach (var x in Listado)
                {
                    lst.Add(new ModelChartGarantias
                    {
                        Descripcion = x.Modelo,
                        Valor = x.Contador,
                        Porcentaje = decimal.Round(((x.Contador * 100) / acum), 2)
                    });
                }
                return lst;

            }

        }

        public List<ModelChartGarantias> ReporteDetalleAnalisisCausalesMesesPorTipoClientePorModelo(string tipoCliente, string ClienteClase, string ClienteLinea, int Anio, string Mes)
        {
            decimal acum = 0;
            var numeromes = BuscarCodigoMes(Mes);
            //DateTime nuevaFechaFin = FechaFin;
            //nuevaFechaFin = nuevaFechaFin.AddDays(1);

            List<ModelChartGarantias> lst = new List<ModelChartGarantias>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = from d in DB.AnalisisRegistrosGarantias
                              join
                              e in DB.IngresoRevisionGarantiaCabecera on d.IngresoRevisionGarantiaId equals e.IngresoRevisionGarantiaId
                              orderby d.AnalisisRegistrosGarantiasId
                              where d.FechaRegistroAnalisis.Year == Anio && d.FechaRegistroAnalisis.Month == numeromes && e.TipoCliente == tipoCliente && e.ClienteClase == ClienteClase && e.ClienteLinea == ClienteLinea
                              group d by d.ModeloBateria into grp
                              select new
                              {
                                  Modelo = grp.Key,
                                  Contador = grp.Count()
                              };
                foreach (var x in Listado)
                {
                    acum = acum + x.Contador;
                }

                foreach (var x in Listado)
                {
                    lst.Add(new ModelChartGarantias
                    {
                        Descripcion = x.Modelo,
                        Valor = x.Contador,
                        Porcentaje = decimal.Round(((x.Contador * 100) / acum), 2)
                    });
                }
                return lst;

            }

        }
        public List<Meses> Meses()
        {

            List<Meses> lst = new List<Meses>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {

                var Listado = (from d in DB.Meses
                               select new
                               {
                                   d.Nombre,
                                   d.Orden,
                               });
                foreach (var x in Listado)
                {
                    lst.Add(new Meses
                    {
                        Nombre = x.Nombre,
                        Orden = x.Orden
                    });
                }
                return lst;

            }

        }
        public List<AreaResponsableGarantias> AreaResponsable()
        {

            List<AreaResponsableGarantias> lst = new List<AreaResponsableGarantias>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {

                var Listado = (from d in DB.AreaResponsableGarantias
                               select new
                               {
                                   d.DescripcionAreaResponsable,
                                   d.CodigoArea,
                               });
                foreach (var x in Listado)
                {
                    lst.Add(new AreaResponsableGarantias
                    {
                        DescripcionAreaResponsable = x.DescripcionAreaResponsable,
                        CodigoArea = x.CodigoArea
                    });
                }
                return lst;

            }

        }
        public List<LineasMarcasPropias> TipoBateria()
        {

            List<LineasMarcasPropias> lst = new List<LineasMarcasPropias>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {

                var Listado = (from d in DB.LineasMarcasPropias
                               select new
                               {
                                   d.Referencia,
                                   d.Identificador,
                               });
                foreach (var x in Listado)
                {
                    lst.Add(new LineasMarcasPropias
                    {
                        Referencia = x.Referencia,
                        Identificador = x.Identificador
                    });
                }
                return lst;
            }

        }
        public List<CausalesGarantias> Causales()
        {

            List<CausalesGarantias> lst = new List<CausalesGarantias>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {

                var Listado = (from d in DB.CausalesGarantias
                               select new
                               {
                                   d.CausalesGarantiasId,
                                   d.DescripcionCausales,
                               });
                foreach (var x in Listado)
                {
                    lst.Add(new CausalesGarantias
                    {
                        CausalesGarantiasId = x.CausalesGarantiasId,
                        DescripcionCausales = x.DescripcionCausales
                    });
                }
                return lst;
            }

        }
        public List<ModelosMarcasPropias> GrupoBateria()
        {

            List<ModelosMarcasPropias> lst = new List<ModelosMarcasPropias>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {


                var Listado = (from d in DB.ModelosMarcasPropias
                               select new
                               {
                                   //d.ModelosMarcasPropiasId,
                                   d.Grupo,
                               }).ToList().Distinct();

                foreach (var x in Listado)
                {
                    lst.Add(new ModelosMarcasPropias
                    {
                        //ModelosMarcasPropiasId = x.ModelosMarcasPropiasId,
                        Grupo = x.Grupo
                    });
                }
                return lst;
            }
        }


        public List<ReporteGarandiasDetalleFinal> ReporteAnalisisGarantiaDetalleGeneral(int Anio1, int Anio2, string MesAnalisis, string AreaResponsable, string Causales, string GrupoBateria )
        {
            int Mes;
            decimal acum = 0;
            decimal acum2 = 0;

            Mes = BuscarNumeroMes(MesAnalisis);


            List<ReporteGarandiasDetalleFinal> lst = new List<ReporteGarandiasDetalleFinal>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {

                var Listado = from d in DB.AnalisisRegistrosGarantias
                              join e in DB.ModelosMarcasPropias on d.ModeloBateria equals e.Referencia
                              where d.FechaRegistroAnalisis.Year == Anio1 && d.FechaRegistroAnalisis.Month== Mes && d.AreaResponsable==AreaResponsable && d.ResumenAnalisis == Causales && e.Grupo==GrupoBateria

                              group d by new { d.FechaRegistroAnalisis.Year, d.ResumenAnalisis } into ut
                              select new
                              {
                                  Contador = ut.Count(),
                                  Analisis = ut.Key.ResumenAnalisis.Replace("\n", "").Replace("\r", ""),
                                  anio = ut.Key.Year
                              };

                var Listado2 = from d in DB.AnalisisRegistrosGarantias
                               join e in DB.ModelosMarcasPropias on d.ModeloBateria equals e.Referencia
                               where d.FechaRegistroAnalisis.Year == Anio2 && d.FechaRegistroAnalisis.Month == Mes && d.AreaResponsable == AreaResponsable && d.ResumenAnalisis == Causales && e.Grupo == GrupoBateria

                               group d by new { d.FechaRegistroAnalisis.Year, d.ResumenAnalisis } into ut

                               select new
                               {
                                   Contador = ut.Count(),
                                   Analisis = ut.Key.ResumenAnalisis.Replace("\n", "").Replace("\r", ""),
                                   anio = ut.Key.Year
                               };

                foreach (var x in Listado)
                {
                    acum = acum + x.Contador;
                }
                foreach (var y in Listado2)
                {
                    acum2 = acum2 + y.Contador;
                }

                foreach (var x in Listado)
                {
                    int temp = 0;

                    foreach (var y in Listado2)
                    {
                        if (y.Analisis.Replace("\n","").Replace("\r","") == x.Analisis.Replace("\n", "").Replace("\r", "") && y.anio != x.anio && y.Contador!=0) {
                            temp = y.Contador;
                        }
                  
                    }

                    lst.Add(new ReporteGarandiasDetalleFinal
                    {
                        Descripcion= Convert.ToString(x.Analisis),
                        anio1=x.Contador,
                        anio2=temp

                    });
                }
                return lst;

            }

        }

        public List<ModeloPivotAnalisisGarantiasAnio> ReportePivotDeAnalisisGarantiasAnios(int anio, int anio2)
        {
            string fechaRegistro = null;
            List<ModeloPivotAnalisisGarantiasAnio> lst = new List<ModeloPivotAnalisisGarantiasAnio>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {


                var Listado = (from d in DB.AnalisisRegistrosGarantias join
                               e in DB.ModelosMarcasPropias on d.ModeloBateria equals e.Referencia
                               where d.FechaRegistroAnalisis.Year==anio || d.FechaRegistroAnalisis.Year == anio2
                               select new
                               {
                                   d.AnalisisRegistrosGarantiasId,
                                   d.AreaResponsable,
                                   d.ResumenAnalisis,
                                   e.Grupo,
                                   d.ModeloBateria,
                                   d.FechaRegistroAnalisis
                                   
                               }).ToList().Distinct();

                foreach (var x in Listado)
                {
                    DateTime fecha = Convert.ToDateTime(x.FechaRegistroAnalisis, CultureInfo.InvariantCulture);
                    //FechaCargaLista = fechaCargaLista.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    fechaRegistro = fecha.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                    lst.Add(new ModeloPivotAnalisisGarantiasAnio
                    {
                       AnalisisRegistrosGarantiasId=x.AnalisisRegistrosGarantiasId,
                       AreaResponsable=x.AreaResponsable,
                       ResumenAnalisis=x.ResumenAnalisis,
                       Cantidad=1,
                       GrupoBateria=x.Grupo,
                       ModeloBateria=x.ModeloBateria,
                       FechaRegistro=fechaRegistro
                    });
                }
                return lst;
            }
        }
    }
}