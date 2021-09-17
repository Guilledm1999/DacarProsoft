using DacarDatos.Datos;
using DacarProsoft.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace DacarProsoft.Datos
{
    public class DaoPedidos
    {

        public List<PedidoCabecera> ConsultarPackingIngreseados()
        {
            List<PedidoCabecera> lst = new List<PedidoCabecera>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoPedidos = from d in DB.PedidoClienteCabecera
                                     orderby d.NumeroPedidoId descending
                                     where d.EstadoPedido==1
                                     select new
                                     {
                                         d.NumeroPedidoId,
                                         d.CardCode,
                                         d.NombreCliente,
                                         d.FechaEmision,
                                         d.OrdenCompra,
                                         d.FechaRequerida,
                                         d.Pais,
                                         d.Ciudad,
                                         d.Direccion,
                                         d.TerminoImportacion,
                                     };

                foreach (var x in ListadoPedidos)
                {

                    DateTime fechaemi = Convert.ToDateTime(x.FechaEmision, CultureInfo.InvariantCulture);
                    string fechaEmision= fechaemi.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                    DateTime fechareq = Convert.ToDateTime(x.FechaRequerida, CultureInfo.InvariantCulture);
                    string fechaRequerida = fechareq.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);


                    lst.Add(new PedidoCabecera
                    {
                        NumeroPedidoId = x.NumeroPedidoId,
                        CardCode = x.CardCode,
                        NombreCliente = x.NombreCliente,
                        FechaEmision = fechaEmision,
                        FechaRequerida = fechaRequerida,
                        OrdenCompra = x.OrdenCompra,
                        Pais = x.Pais,
                        Ciudad = x.Ciudad,
                        Direccion = x.Direccion,
                        TerminoImportacion = x.TerminoImportacion
                    });
                }
                return lst;
            }
        }

        public List<PedidosRegistradosSap> ConsultarPedidosGeneral(int estado)
        {
            List<PedidosRegistradosSap> lst = new List<PedidosRegistradosSap>();
            string FechaCargaLista=null;
            string FechaDespachoPuerto = null;
            string FechaZarpe = null;
            string FechaArribo = null;
            string FechaEntrega = null;

            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var DetallePedido = from d in DB.PedidoClienteCabecera join
                                    e in DB.PedidoClienteDetalleFinal on d.NumeroPedidoId equals e.PedidoClienteCabecera
                                    orderby d.NumeroPedidoId descending

                                    where d.EstadoPedido == estado
                                    select new
                                    {
                                        d.NumeroPedidoId,
                                        d.CardCode,
                                        d.NombreCliente,
                                        d.OrdenCompra,
                                        d.Pais,
                                        d.Ciudad,
                                        d.Direccion,
                                        d.TerminoImportacion,
                                        d.EstadoPedido,
                                        d.FechaEmision,
                                        e.FechaIngresadaSap,
                                        e.FechaNuevaDespacho,
                                        d.FechaCargaLista,
                                        d.FechaDespachoPuerto,
                                        d.FechaZarpe,
                                        d.FechaArribo,
                                        d.FechaEntrega
                                    };

                foreach (var x in DetallePedido)
                {
                    
                    DateTime fechaIngresadaSap = Convert.ToDateTime(x.FechaIngresadaSap, CultureInfo.InvariantCulture);
                    string FechaIngSap = fechaIngresadaSap.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                    DateTime fechaNuevoDespacho = Convert.ToDateTime(x.FechaNuevaDespacho, CultureInfo.InvariantCulture);
                    string FechaNuevDespacho = fechaIngresadaSap.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                    DateTime fechaEmision = Convert.ToDateTime(x.FechaEmision, CultureInfo.InvariantCulture);
                    string FechaEmision = fechaIngresadaSap.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                    if (x.FechaCargaLista!=null) {
                        DateTime fechaCargaLista = Convert.ToDateTime(x.FechaCargaLista, CultureInfo.InvariantCulture);
                        //FechaCargaLista = fechaCargaLista.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        FechaCargaLista = fechaCargaLista.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                    }
                    else
                    {
                        FechaCargaLista = "No Ingresada";
                    }
                    if (x.FechaDespachoPuerto != null)
                    {
                        DateTime fechaDespachoPuerto = Convert.ToDateTime(x.FechaDespachoPuerto, CultureInfo.InvariantCulture);
                        FechaDespachoPuerto = fechaDespachoPuerto.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                    }
                    else
                    {
                        FechaDespachoPuerto = "No Ingresada";
                    }
                    if (x.FechaZarpe != null)
                    {
                        DateTime fechaZarpe = Convert.ToDateTime(x.FechaZarpe, CultureInfo.InvariantCulture);
                        FechaZarpe = fechaZarpe.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        FechaZarpe = "No Ingresada";
                    }
                    if (x.FechaArribo != null)
                    {
                        DateTime fechaArribo = Convert.ToDateTime(x.FechaArribo, CultureInfo.InvariantCulture);
                        FechaArribo = fechaArribo.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        FechaArribo = "No Ingresada";
                    }
                    if (x.FechaEntrega != null)
                    {
                        DateTime fechaEntrega = Convert.ToDateTime(x.FechaEntrega, CultureInfo.InvariantCulture);
                        FechaEntrega = fechaEntrega.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        FechaEntrega = "No Ingresada";
                    }

                    lst.Add(new PedidosRegistradosSap
                    {
                        NumeroPedidoId=x.NumeroPedidoId,
                        CardCode = x.CardCode,
                        NombreCliente = x.NombreCliente,
                        OrdenCompra = x.OrdenCompra,
                        Pais = x.Pais,
                        Ciudad = x.Ciudad,
                        Direccion = x.Direccion,
                        TerminoImportacion = x.TerminoImportacion,
                        Estado = x.EstadoPedido.Value,
                        FechaEmision=FechaEmision,
                        FechaIngresadaSap = FechaIngSap,
                        FechaNuevaDespacho = FechaNuevDespacho,
                        FechaCargaLista=FechaCargaLista,
                        FechaDespachoPuerto=FechaDespachoPuerto,
                        FechaZarpe=FechaZarpe,
                        FechaArribo=FechaArribo,
                        FechaEntrega=FechaEntrega
                    });
                }
                return lst;
            }
        }
        public List<PedidoCabecera> ConsultarPedidosCanceladosGeneral(int estado)
        {
            List<PedidoCabecera> lst = new List<PedidoCabecera>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var DetallePedido = from d in DB.PedidoClienteCabecera
                                    where d.EstadoPedido == estado
                                    select new
                                    {
                                        d.NumeroPedidoId,
                                        d.CardCode,
                                        d.NombreCliente,
                                        d.OrdenCompra,
                                        d.Pais,
                                        d.Ciudad,
                                        d.Direccion,
                                        d.TerminoImportacion,
                                        d.EstadoPedido,
                                        d.FechaEmision,
                                        d.FechaRequerida
                                      
                                    };

                foreach (var x in DetallePedido)
                {
                    DateTime fechaemi = Convert.ToDateTime(x.FechaEmision, CultureInfo.InvariantCulture);
                    string fechaEmision = fechaemi.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                    DateTime fechareq = Convert.ToDateTime(x.FechaRequerida, CultureInfo.InvariantCulture);
                    string fechaRequerida = fechareq.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                    lst.Add(new PedidoCabecera
                    {
                        NumeroPedidoId = x.NumeroPedidoId,
                        CardCode = x.CardCode,
                        NombreCliente = x.NombreCliente,
                        OrdenCompra = x.OrdenCompra,
                        Pais = x.Pais,
                        Ciudad = x.Ciudad,
                        Direccion = x.Direccion,
                        TerminoImportacion = x.TerminoImportacion,
                        Estado=x.EstadoPedido.Value,
                        FechaEmision= fechaEmision,
                        FechaRequerida= fechaRequerida

                    });
                }
                return lst;
            }
        }
        public List<PedidoClienteCabecera> ConsultarPedidoCabecera(int idPedido)
        {
            List<PedidoClienteCabecera> lst = new List<PedidoClienteCabecera>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var DetallePedido = from d in DB.PedidoClienteCabecera
                                    where d.NumeroPedidoId == idPedido
                                    select new
                                    {
                                     d.CardCode,
                                     d.NombreCliente,
                                     d.FechaEmision,
                                     d.OrdenCompra,
                                     d.Pais,
                                     d.Ciudad,
                                     d.Direccion,
                                     d.TerminoImportacion,
                                     d.FechaRequerida,
                                     d.EstadoPedido
                                    };

                foreach (var x in DetallePedido)
                {
                    lst.Add(new PedidoClienteCabecera
                    {
                     
                        CardCode=x.CardCode,
                        NombreCliente=x.NombreCliente,
                        FechaEmision=x.FechaEmision,
                        OrdenCompra=x.OrdenCompra,
                        Pais=x.Pais,
                        Ciudad=x.Ciudad,
                        Direccion=x.Direccion,
                        TerminoImportacion=x.TerminoImportacion,
                        FechaRequerida=x.FechaRequerida,
                        EstadoPedido=x.EstadoPedido
                    });
                }
                return lst;
            }
        }

        public List<PedidoClienteDetalleFinal> ConsultarPedidoDetalleFinal(int idPedido)
        {
            List<PedidoClienteDetalleFinal> lst = new List<PedidoClienteDetalleFinal>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var DetallePedidoFinal = from d in DB.PedidoClienteDetalleFinal
                                         where d.PedidoClienteDetalleFinal1==idPedido
                                     select new
                                     {
                                         d.PedidoClienteDetalleFinal1,
                                         d.CantitadTotal,
                                         d.PrecioFinalPedido,
                                         d.PesoNetoFinalPedido,
                                         d.PesoBrutoFinalPedido,
                                         d.Observaciones
                                     };

                foreach (var x in DetallePedidoFinal)
                {
                    lst.Add(new PedidoClienteDetalleFinal
                    {
                       PedidoClienteDetalleFinal1=x.PedidoClienteDetalleFinal1,
                       CantitadTotal=x.CantitadTotal,
                       PrecioFinalPedido=x.PrecioFinalPedido,
                       PesoNetoFinalPedido=x.PesoNetoFinalPedido,
                       PesoBrutoFinalPedido=x.PesoBrutoFinalPedido,
                       Observaciones=x.Observaciones
                    });
                }
                return lst;
            }
        }
        public List<PedidoClienteDetalleFinal> ConsultarPedidoDetalleFinalAprobada(int idPedido)
        {
            List<PedidoClienteDetalleFinal> lst = new List<PedidoClienteDetalleFinal>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var DetallePedidoFinal = from d in DB.PedidoClienteDetalleFinal
                                         where d.PedidoClienteCabecera == idPedido
                                         select new
                                         {
                                             d.PedidoClienteDetalleFinal1,
                                             d.CantitadTotal,
                                             d.CantidadTotalNueva,
                                             d.PrecioFinalPedido,
                                             d.PesoNetoFinalPedido,
                                             d.PesoBrutoFinalPedido,
                                             d.Observaciones,
                                             d.FechaIngresadaSap,
                                             d.FechaNuevaDespacho
                                         };

                foreach (var x in DetallePedidoFinal)
                {
                    lst.Add(new PedidoClienteDetalleFinal
                    {
                        PedidoClienteDetalleFinal1 = x.PedidoClienteDetalleFinal1,
                        CantitadTotal = x.CantitadTotal,
                        CantidadTotalNueva=x.CantidadTotalNueva.Value,
                        PrecioFinalPedido = x.PrecioFinalPedido,
                        PesoNetoFinalPedido = x.PesoNetoFinalPedido,
                        PesoBrutoFinalPedido = x.PesoBrutoFinalPedido,
                        Observaciones = x.Observaciones,
                        FechaIngresadaSap=x.FechaIngresadaSap,
                        FechaNuevaDespacho=x.FechaNuevaDespacho
                    });
                }
                return lst;
            }
        }

        public List<PedidoClienteDetalle> ConsultarPedidoDetalle(int idPedido)
        {
            List<PedidoClienteDetalle> lst = new List<PedidoClienteDetalle>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var DetallePedido = from d in DB.PedidoClienteDetalle
                                    where d.NumeroPedidoId==idPedido
                                         select new
                                         {
                                             d.PedidoClienteDetalleId,
                                             d.ItemCode,
                                             d.ModeloBateria,
                                             d.Marca,
                                             d.NumeroParteCliente,
                                             d.EtiquetaDatosTecnicos,
                                             d.Polaridad,
                                             d.TipoTerminal,
                                             d.Cantidad,
                                             d.CantidadConfirmada,
                                             d.PrecioUnitario,
                                             d.PrecioTotal,
                                             d.PesoBateria,
                                             d.PesoNeto
                                         };

                foreach (var x in DetallePedido)
                {
                    lst.Add(new PedidoClienteDetalle
                    {
                       ItemCode=x.ItemCode,
                       PedidoClienteDetalleId=x.PedidoClienteDetalleId,
                       ModeloBateria=x.ModeloBateria,
                       Marca=x.Marca,
                       NumeroParteCliente=x.NumeroParteCliente,
                       EtiquetaDatosTecnicos=x.EtiquetaDatosTecnicos,
                       Polaridad=x.Polaridad,
                       TipoTerminal=x.TipoTerminal,
                       Cantidad=x.Cantidad,
                       CantidadConfirmada= x.Cantidad,
                       PrecioUnitario=x.PrecioUnitario,
                       PrecioTotal=x.PrecioTotal,
                       PesoBateria=x.PesoBateria,
                       PesoNeto=x.PesoNeto
                    });
                }
                return lst;
            }
        }
        public List<PedidoClienteDetalle> ConsultarPedidoDetalleConfirmado(int idPedido)
        {
            List<PedidoClienteDetalle> lst = new List<PedidoClienteDetalle>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var DetallePedido = from d in DB.PedidoClienteDetalle
                                    where d.NumeroPedidoId == idPedido
                                    select new
                                    {
                                        d.PedidoClienteDetalleId,
                                        d.ItemCode,
                                        d.ModeloBateria,
                                        d.Marca,
                                        d.NumeroParteCliente,
                                        d.EtiquetaDatosTecnicos,
                                        d.Polaridad,
                                        d.TipoTerminal,
                                        d.Cantidad,
                                        d.CantidadConfirmada,
                                        d.PrecioUnitario,
                                        d.PrecioTotal,
                                        d.PesoBateria,
                                        d.PesoNeto
                                    };

                foreach (var x in DetallePedido)
                {
                    lst.Add(new PedidoClienteDetalle
                    {
                        ItemCode = x.ItemCode,
                        PedidoClienteDetalleId = x.PedidoClienteDetalleId,
                        ModeloBateria = x.ModeloBateria,
                        Marca = x.Marca,
                        NumeroParteCliente = x.NumeroParteCliente,
                        EtiquetaDatosTecnicos = x.EtiquetaDatosTecnicos,
                        Polaridad = x.Polaridad,
                        TipoTerminal = x.TipoTerminal,
                        Cantidad = x.Cantidad,
                        CantidadConfirmada = x.CantidadConfirmada,
                        PrecioUnitario = x.PrecioUnitario,
                        PrecioTotal = x.PrecioTotal,
                        PesoBateria = x.PesoBateria,
                        PesoNeto = x.PesoNeto
                    });
                }
                return lst;
            }
        }

        public bool ComprobarExistenciaOrdenEnSap(string Orden)
        {
            bool comprobar = false;
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                try
                {
                    var Listado = (from d in DB.ORDR
                                   where d.U_SYP_NUMOCCL== Orden 
                                   select new
                                   {
                                       d.CardCode,
                                   });
                    foreach (var x in Listado)
                    {
                        if (x.CardCode != null)
                        {
                            comprobar = true;
                        }
                    }

                    return comprobar;
                }
                catch
                {
                    return false;

                }

            }
        }
        public bool GuardarActualizacionObservaciones(int PedidoId, string observaciones,DateTime FechaRegistroSap, DateTime FechaNuevoDespacho,int CantidadTotal, decimal PrecioTotal, decimal PesoTotal)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var regi = (from d in DB.PedidoClienteDetalleFinal
                            where d.PedidoClienteCabecera == PedidoId
                            select d).FirstOrDefault();
                try
                {
                    regi.Observaciones = observaciones;
                    regi.FechaIngresadaSap = FechaRegistroSap;
                    regi.FechaNuevaDespacho = FechaNuevoDespacho;
                    regi.CantidadTotalNueva = CantidadTotal;
                    regi.PrecioFinalPedido = PrecioTotal;
                    regi.PesoNetoFinalPedido = PesoTotal;

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
        public bool GuardarActualizacionDetalleFinales(int PedidoId,int CantidadTotal ,decimal PrecioTotal, decimal PesoTotal)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var regi = (from d in DB.PedidoClienteDetalleFinal
                            where d.PedidoClienteCabecera == PedidoId
                            select d).FirstOrDefault();
                try
                {
                    regi.CantitadTotal = CantidadTotal;
                    regi.PrecioFinalPedido = PrecioTotal;
                    regi.PesoNetoFinalPedido = PesoTotal;


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
        public bool GuardarActualizacionEstado(int PedidoId, int Estado)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var regi = (from d in DB.PedidoClienteCabecera
                            where d.NumeroPedidoId == PedidoId
                            select d).FirstOrDefault();
                try
                {
                    regi.EstadoPedido = Estado;
                  

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

        public bool GuardarActualizacionDetallePedido(int PedidoId, string ItemCode, int CantidadNueva)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var regi = (from d in DB.PedidoClienteDetalle
                            where d.NumeroPedidoId == PedidoId && d.ItemCode==ItemCode 
                            select d).FirstOrDefault();
                try
                {
                    regi.CantidadConfirmada = CantidadNueva;


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
        public List<FechasPedidos> ListaFechasPedidos() {

            List<FechasPedidos> lst = new List<FechasPedidos>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
             
                    var Listado = (from d in DB.FechasPedidos
                                   where d.EstadoFecha==true
                                   select new
                                   {
                                       d.FechasPedidosId,
                                       d.DescripcionFecha,
                                       d.OrdenFecha
                                   });
                    foreach (var x in Listado)
                    {
                        lst.Add(new FechasPedidos{ 
                        FechasPedidosId=x.FechasPedidosId,
                        DescripcionFecha=x.DescripcionFecha,
                        OrdenFecha=x.OrdenFecha
                        });
                    }
                    return lst;          

            }

        }

        public List<EstadosPedidos> StatusPedidos()
        {

            List<EstadosPedidos> lst = new List<EstadosPedidos>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {

                var Listado = (from d in DB.EstadosPedidos
                               where d.Codigo!=1 && d.Codigo!=6
                               select new
                               {
                                   d.Descripcion,
                                   d.Codigo,
                               });
                foreach (var x in Listado)
                {
                    lst.Add(new EstadosPedidos
                    {
                      Codigo=x.Codigo,
                      Descripcion=x.Descripcion
                    });
                }
                return lst;

            }

        }

        public string  ConsultaFechaCargaLista(int  PedidoId)
        {
            string result = "";
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                    var FechaPedido = (from d in DB.PedidoClienteCabecera
                                   where d.NumeroPedidoId == PedidoId
                                   select new
                                   {
                                       d.FechaCargaLista,
                                   }).FirstOrDefault(); ;
                    if (FechaPedido.FechaCargaLista != null)
                    {
                        DateTime fecha = Convert.ToDateTime(FechaPedido.FechaCargaLista, CultureInfo.InvariantCulture);
                        result = fecha.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        return result;

                    }
                    else {
                        result = "No";
                        return result;

                    }
            }
        }
        public string ConsultaFechaDespachoPuerto(int PedidoId)
        {
            string result = "";
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var FechaPedido = (from d in DB.PedidoClienteCabecera
                                   where d.NumeroPedidoId == PedidoId
                                   select new
                                   {
                                       d.FechaDespachoPuerto,
                                   }).FirstOrDefault(); ;
                if (FechaPedido.FechaDespachoPuerto != null)
                {
                    DateTime fecha = Convert.ToDateTime(FechaPedido.FechaDespachoPuerto, CultureInfo.InvariantCulture);
                    result = fecha.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    return result;

                }
                else
                {
                    result = "No";
                    return result;

                }
            }
        }
        public string ConsultaFechaZarpe(int PedidoId)
        {
            string result = "";
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var FechaPedido = (from d in DB.PedidoClienteCabecera
                                   where d.NumeroPedidoId == PedidoId
                                   select new
                                   {
                                       d.FechaZarpe,
                                      
                                   }).FirstOrDefault(); ;
                if (FechaPedido.FechaZarpe != null)
                {
                    DateTime fecha = Convert.ToDateTime(FechaPedido.FechaZarpe, CultureInfo.InvariantCulture);
                    result = fecha.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    return result;

                }
                else
                {
                    result = "No";
                    return result;

                }
            }
        }
        public string ConsultaFechaArribo(int PedidoId)
        {
            string result = "";
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var FechaPedido = (from d in DB.PedidoClienteCabecera
                                   where d.NumeroPedidoId == PedidoId
                                   select new
                                   {
                                    
                                       d.FechaArribo,
                                   }).FirstOrDefault(); ;
                if (FechaPedido.FechaArribo != null)
                {
                    DateTime fecha = Convert.ToDateTime(FechaPedido.FechaArribo, CultureInfo.InvariantCulture);
                    result = fecha.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    return result;

                }
                else
                {
                    result = "No";
                    return result;

                }
            }
        }
        public string ConsultaFechaEntrega(int PedidoId)
        {
            string result = "";
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var FechaPedido = (from d in DB.PedidoClienteCabecera
                                   where d.NumeroPedidoId == PedidoId
                                   select new
                                   {
                                      
                                       d.FechaEntrega
                                   }).FirstOrDefault(); ;
                if (FechaPedido.FechaEntrega != null)
                {
                    DateTime fecha = Convert.ToDateTime(FechaPedido.FechaEntrega, CultureInfo.InvariantCulture);
                    result = fecha.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    return result;

                }
                else
                {
                    result = "No";
                    return result;

                }
            }
        }

        public bool GuardarActualizacionFechaCargaLista(int PedidoId, DateTime Fecha)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var regi = (from d in DB.PedidoClienteCabecera
                            where d.NumeroPedidoId== PedidoId
                            select d).FirstOrDefault();
                try
                {
                    regi.FechaCargaLista = Fecha;
                   


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
        public bool GuardarActualizacionDespachoPuerto(int PedidoId, DateTime Fecha)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var regi = (from d in DB.PedidoClienteCabecera
                            where d.NumeroPedidoId == PedidoId
                            select d).FirstOrDefault();
                try
                {
                    regi.FechaDespachoPuerto = Fecha;



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
        public bool GuardarActualizacionZarpe(int PedidoId, DateTime Fecha)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var regi = (from d in DB.PedidoClienteCabecera
                            where d.NumeroPedidoId == PedidoId
                            select d).FirstOrDefault();
                try
                {
                    regi.FechaZarpe = Fecha;



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
        public bool GuardarActualizacionArribo(int PedidoId, DateTime Fecha)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var regi = (from d in DB.PedidoClienteCabecera
                            where d.NumeroPedidoId == PedidoId
                            select d).FirstOrDefault();
                try
                {
                    regi.FechaArribo = Fecha;



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
        public bool GuardarActualizacionEntrega(int PedidoId, DateTime Fecha)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var regi = (from d in DB.PedidoClienteCabecera
                            where d.NumeroPedidoId == PedidoId
                            select d).FirstOrDefault();
                try
                {
                    regi.FechaEntrega = Fecha;



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
        public bool GuardarActualizacionEstadoPedidoConfirmado(int PedidoId, int Estado)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var regi = (from d in DB.PedidoClienteCabecera
                            where d.NumeroPedidoId == PedidoId
                            select d).FirstOrDefault();
                try
                {
                    regi.EstadoPedido = Estado;



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
    }
}