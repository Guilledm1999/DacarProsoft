using DacarDatos.Datos;
using DacarProsoft.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

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
                                       d.Pais,
                                       d.Ciudad,
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
                            Pais = x.Pais,
                            Ciudad = x.Ciudad,
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
                                       d.Pais,
                                       d.Ciudad,
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
                            Pais = x.Pais,
                            Ciudad = x.Ciudad,
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

    }
}