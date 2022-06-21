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
    public class DaoIngresoMercanciasSap
    {
        private DaoUtilitarios daoUtilitarios { get; set; } = null;
        public List<Moign> ListadoCabeceraChatarraSap(string tipoIngreso)
        {
            string clienteLinea=null;
            string clienteClase=null;
            string NumeroOrdenCliente = null;
            int codGroup=0;
            var dias = BusquedaxDias();
            DateTime fechaActual = DateTime.Now;
            DateTime fechaCorte = fechaActual.AddDays(-(dias));

            daoUtilitarios = new DaoUtilitarios();
            var Result = daoUtilitarios.ConsultarBusquedaIngresoMercanciasTipo();
            List<Moign> lst = new List<Moign>();
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {       
                var ListadoCabeceraChatarra = from d in DB.OIGN
                                              join e in DB.OCRD on d.U_SYP_CODCL equals e.CardCode
                                              join f in DB.OCRG on e.GroupCode equals f.GroupCode
                                              where d.DocDate >= fechaCorte && d.DocDate <= fechaActual && d.U_SYP_TmovIng == "COMP-CHAT" && (/*d.Comments.Contains("CHAT")|| */d.Comments.Contains("CHA"))
                                              orderby d.DocDate descending
                                              select new
                                              {
                                                  d.DocEntry,
                                                  d.DocNum,
                                                  d.DocDate,
                                                  d.U_SYP_CODCL,
                                                  d.U_SYP_NOMCL,
                                                  d.U_SYP_NUMOCCL,
                                                  f.GroupCode,
                                                  f.GroupName,
                                                  d.Comments,
                                                  d.U_SYP_TmovIng,
                                                  d.U_DC_KILOS
                                              };
                foreach (var x in ListadoCabeceraChatarra)
                {
                    var NombreGrupo = GrupoCliente(x.GroupCode);

                    var busqueda = BusquedaLocal(x.DocNum);
                    if (busqueda == false)
                    {
                        var DatosClientes=ConsultarDatosClientes(x.U_SYP_CODCL);

                        foreach (var y in DatosClientes)
                        {
                            clienteLinea = y.ClienteLinea;
                            clienteClase = y.ClienteClase;
                            codGroup = y.GroupCode;
                        }
                        DateTime fechaDoc = Convert.ToDateTime(x.DocDate, CultureInfo.InvariantCulture);
                        string fechaDocumento = fechaDoc.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        String Comentario = x.Comments;
                        decimal kilo = 0;
                        if (x.Comments == null)
                        {
                            Comentario = "Sin Comentarios";
                        }
                        if (x.U_DC_KILOS == null)
                        {
                            kilo = 0;
                        }
                        else
                        {
                            string result = string.Concat(x.U_DC_KILOS.Where(c => Char.IsDigit(c)));
                            kilo = Convert.ToDecimal(result);
                        }
                        if (x.U_SYP_NUMOCCL == null)
                        {
                            NumeroOrdenCliente = "No ingresada";
                        }
                        else
                        {
                            NumeroOrdenCliente = x.U_SYP_NUMOCCL;
                        }
                        lst.Add(new Moign
                        {
                            DocEntry = x.DocEntry,
                            DocNum = (x.DocNum).ToString(),
                            DocDate = fechaDocumento,
                            CedulaCliente = x.U_SYP_CODCL,
                            NombreCliente = x.U_SYP_NOMCL,
                            CardCode= codGroup,
                            NumeroPedido= NumeroOrdenCliente,  
                            GrupoName= NombreGrupo,
                            ClienteClase = clienteClase,
                            ClienteLinea= clienteLinea,
                            KilosReales = decimal.Round(kilo,2),
                            Comments = Comentario,
                            TipoIngreso = tipoIngreso
                        });
                    }
                }
                return lst;
            }
        }

        public List<Moign> ListadoCabeceraChatarraSapDescCliente(string tipoIngreso,int codigoCliente)
        {
            string clienteLinea = null;
            string clienteClase = null;
            int codGroup = 0;
            var dias = BusquedaxDias();
            DateTime fechaActual = DateTime.Now;
            DateTime fechaCorte = fechaActual.AddDays(-(dias));

            daoUtilitarios = new DaoUtilitarios();
            var Result = daoUtilitarios.ConsultarBusquedaIngresoMercanciasTipo();
            List<Moign> lst = new List<Moign>();
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                var ListadoCabeceraChatarra = from d in DB.OIGN
                                              join e in DB.OCRD on d.U_SYP_CODCL equals e.CardCode
                                              join f in DB.OCRG on e.GroupCode equals f.GroupCode
                                              where d.DocDate >= fechaCorte && d.DocDate <= fechaActual && d.U_SYP_TmovIng == "COMP-CHAT"&&f.GroupCode== codigoCliente
                                              orderby d.DocDate descending
                                              select new
                                              {
                                                  d.DocEntry,
                                                  d.DocNum,
                                                  d.DocDate,
                                                  d.U_SYP_CODCL,
                                                  d.U_SYP_NOMCL,
                                                  d.Comments,
                                                  d.U_SYP_TmovIng,
                                                  d.U_DC_KILOS
                                              };

                foreach (var x in ListadoCabeceraChatarra)
                {

                    var busqueda = BusquedaLocal(x.DocNum);
                    if (busqueda == false)
                    {
                        var DatosClientes = ConsultarDatosClientes(x.U_SYP_CODCL);

                        foreach (var y in DatosClientes)
                        {
                            clienteLinea = y.ClienteLinea;
                            clienteClase = y.ClienteClase;
                            codGroup = y.GroupCode;
                        }

                        DateTime fechaDoc = Convert.ToDateTime(x.DocDate, CultureInfo.InvariantCulture);
                        string fechaDocumento = fechaDoc.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        String Comentario = x.Comments;
                        decimal kilo = 0;
                        if (x.Comments == null)
                        {
                            Comentario = "Sin Comentarios";
                        }
                        if (x.U_DC_KILOS == null)
                        {
                            kilo = 0;
                        }
                        else
                        {
                            kilo = Convert.ToDecimal(x.U_DC_KILOS);
                        }
                        lst.Add(new Moign
                        {
                            DocEntry = x.DocEntry,
                            DocNum = (x.DocNum).ToString(),
                            DocDate = fechaDocumento,
                            CedulaCliente = x.U_SYP_CODCL,
                            NombreCliente = x.U_SYP_NOMCL,
                            CardCode = codGroup,
                            ClienteClase = clienteClase,
                            ClienteLinea = clienteLinea,
                            KilosReales = decimal.Round(kilo, 2),
                            Comments = Comentario,
                            TipoIngreso = tipoIngreso
                        });
                    }

                }
                return lst;
            }

        }
        public List<CaracteristicasCliente> ConsultarDatosClientes(string CardCode)
        {

            List<CaracteristicasCliente> lst = new List<CaracteristicasCliente>();
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                var DatosClientes = from d in DB.OCRD
                              join e in DB.OCRG on d.GroupCode equals e.GroupCode
                              join f in DB.C_SYP_CLASESN on d.U_SYP_CLASESN equals f.Code
                              join g in DB.C_SYP_LINEASN on d.U_SYP_LINEASN equals g.Code
                                    where d.CardCode == CardCode
                              select new
                              {
                                  NombreClase=f.Name,
                                  NombreLinea=g.Name ,
                                  CodGroup=e.GroupCode
                              };
                foreach (var x in DatosClientes)
                {
                    string[] cclase = (x.NombreClase).Split(' ');
                    string[] clinea = (x.NombreLinea).Split(' ');


                    lst.Add(new CaracteristicasCliente
                    {
                        ClienteClase= cclase[1],
                        ClienteLinea= clinea[1],
                        GroupCode= x.CodGroup
                    });
                    }
                return lst;
            }
        }

        public List<MdlOpdn> ListadoCompraCabeceraChatarraSap(string tipoIngreso)
        {
            string tipoCliente = null;
            string clienteLinea = null;
            string clienteClase = null;
            string NumeroOrdenCliente = null;

            int codGroup = 0;
            daoUtilitarios = new DaoUtilitarios();
            DateTime fechaActual = DateTime.Now;
            DateTime fechaCorte = fechaActual.AddDays(-200);
            List<MdlOpdn> lst = new List<MdlOpdn>();
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                var ListadoCabeceraChatarra = from d in DB.OPDN
                                              where d.DocDate >= fechaCorte && d.DocDate <= fechaActual && d.U_SYP_TmovIng == "COMP-CHAT"
                                              orderby d.DocDate descending

                                              select new
                                              {
                                                  d.DocEntry,
                                                  d.DocNum,
                                                  d.DocDate,
                                                  d.CardCode,
                                                  d.CardName,
                                                  d.Comments,
                                                  d.U_SYP_NUMOCCL,
                                                  d.U_SYP_TmovIng,
                                                  d.U_DC_KILOS
                                              };

                foreach (var x in ListadoCabeceraChatarra)
                {
                    decimal kilo = 0;
                    var busqueda = BusquedaLocal(x.DocNum.Value);
                    if (busqueda == false)
                    {
                        var DatosClientes = ConsultarDatosClientes(x.CardCode);

                        foreach (var y in DatosClientes)
                        {
                            clienteLinea = y.ClienteLinea;
                            clienteClase = y.ClienteClase;
                            codGroup = y.GroupCode;
                        }
                        if (clienteLinea==null) {
                            clienteLinea = "No Definido";
                            tipoCliente = "No Definido";
                        }
                        if (clienteClase==null) {
                            clienteClase = "No Definido";
                            tipoCliente = "No Definido";
                        }
                        if (codGroup==0) {
                            codGroup = 0;
                        }
                        DateTime fechaDoc = Convert.ToDateTime(x.DocDate, CultureInfo.InvariantCulture);
                        string fechaDocumento = fechaDoc.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        String Comentario = x.Comments;

                        if (x.Comments == null)
                        {
                            Comentario = "Sin Comentarios";
                        }
                        if (x.U_DC_KILOS == null)
                        {
                            kilo = 0;
                        }
                        else {
                            kilo = Convert.ToDecimal(x.U_DC_KILOS.Replace(".",""));
                        }
                        if (x.U_SYP_NUMOCCL == null)
                        {
                            NumeroOrdenCliente = "No ingresada";
                        }
                        else {
                            NumeroOrdenCliente = x.U_SYP_NUMOCCL;
                        }
                        lst.Add(new MdlOpdn
                        {
                            DocEntry = x.DocEntry,
                            DocNum = (x.DocNum).ToString(),
                            DocDate = fechaDocumento,
                            CedulaCliente = x.CardCode,
                            NombreCliente = x.CardName,
                            NumeroPedido= NumeroOrdenCliente,
                            GrupoName= tipoCliente,
                            CardCode = codGroup,
                            ClienteClase = clienteClase,
                            ClienteLinea = clienteLinea,
                            KilosReales = Decimal.Round(kilo,2),
                            Comments = Comentario,
                            TipoIngreso = tipoIngreso
                        });
                    }
                }
                return lst;
            }
        }

        public List<MdlOpdn> ListadoCompraCabeceraChatarraSapDescCliente(string tipoIngreso, int codigoCliente)
        {
            string clienteLinea = null;
            string clienteClase = null;
            int codGroup = 0;
            daoUtilitarios = new DaoUtilitarios();
            DateTime fechaActual = DateTime.Now;
            DateTime fechaCorte = fechaActual.AddDays(-200);
            List<MdlOpdn> lst = new List<MdlOpdn>();
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                var ListadoCabeceraChatarra = from d in DB.OPDN
                                              join e in DB.OCRD on d.U_SYP_CODCL equals e.CardCode
                                              join f in DB.OCRG on e.GroupCode equals f.GroupCode
                                              where d.DocDate >= fechaCorte && d.DocDate <= fechaActual && d.U_SYP_TmovIng == "COMP-CHAT" && f.GroupCode == codigoCliente
                                              orderby d.DocDate descending

                                             select new
                                              {
                                                  d.DocEntry,
                                                  d.DocNum,
                                                  d.DocDate,
                                                  d.CardCode,
                                                  d.CardName,
                                                  d.Comments,
                                                  d.U_SYP_TmovIng,
                                                  d.U_DC_KILOS
                                              };

                foreach (var x in ListadoCabeceraChatarra)
                {
                    decimal kilo = 0;
                    var busqueda = BusquedaLocal(x.DocNum.Value);
                    if (busqueda == false)
                    {
                        var DatosClientes = ConsultarDatosClientes(x.CardCode);

                        foreach (var y in DatosClientes)
                        {
                            clienteLinea = y.ClienteLinea;
                            clienteClase = y.ClienteClase;
                            codGroup = y.GroupCode;
                        }
                        if (clienteLinea == null)
                        {
                            clienteLinea = "No Definido";
                        }
                        if (clienteClase == null)
                        {
                            clienteClase = "No Definido";
                        }
                        if (codGroup == 0)
                        {
                            codGroup = 0;
                        }

                        DateTime fechaDoc = Convert.ToDateTime(x.DocDate, CultureInfo.InvariantCulture);
                        string fechaDocumento = fechaDoc.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        String Comentario = x.Comments;

                        if (x.Comments == null)
                        {
                            Comentario = "Sin Comentarios";
                        }
                        if (x.U_DC_KILOS == null)
                        {
                            kilo = 0;
                        }
                        else
                        {
                            kilo = Convert.ToDecimal(x.U_DC_KILOS);
                        }
                        lst.Add(new MdlOpdn
                        {
                            DocEntry = x.DocEntry,
                            DocNum = (x.DocNum).ToString(),
                            DocDate = fechaDocumento,
                            CedulaCliente = x.CardCode,
                            NombreCliente = x.CardName,
                            CardCode = codGroup,
                            ClienteClase = clienteClase,
                            ClienteLinea = clienteLinea,
                            KilosReales = Decimal.Round(kilo, 2),
                            Comments = Comentario,
                            TipoIngreso = tipoIngreso
                        });
                    }
                }
                return lst;
            }

        }
        public string Bodega(int DocEntry)
        {
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                var ListadoDetalleChatarra = (from d in DB.IGN1
                                             where d.DocEntry == DocEntry
                                             select new
                                             {
                                                 d.WhsCode
                                             }).FirstOrDefault();
                string Bodega = ListadoDetalleChatarra.WhsCode;
                return Bodega;
            }
        }
        public List<Mign1> ListadoDetalleChatarraSapSinFu(int DocEntry)
        {
            decimal pesoChatarra = 0;
            decimal precioChatarra = 0;
            List<Mign1> lst = new List<Mign1>();
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                var ListadoDetalleChatarra = from d in DB.IGN1
                                             where d.DocEntry == DocEntry
                                             select new
                                             {
                                                 d.ItemCode,
                                                 d.Dscription,
                                                 d.Quantity,
                                             };

                foreach (var x in ListadoDetalleChatarra)
                {

                    //var modeloDetalleChatarra=  ConsultarModelosPorDescripcion(x.Dscription);

                    var modeloChatarra = ConsultarModelosPorCodigoItemSap(x.ItemCode);
                    foreach (var y in modeloChatarra)
                    {
                        pesoChatarra = y.PesoArticulo;
                        precioChatarra = y.UltimoPrecioCompra;
                    }
                    lst.Add(new Mign1
                    {
                        DocEntry = DocEntry,
                        ItemCode = x.ItemCode,
                        Description = x.Dscription,
                        Cantidad = Convert.ToInt32(x.Quantity),
                        PesoTeoricoUnitario = pesoChatarra,
                        PesoTeoricoSubtotal = (x.Quantity.Value * pesoChatarra),
                        PesoTeoricoAjustado = 0,
                        PesoTeoricoAjustadoTotal = 0
                    });
                }
                return lst;
            }

        }
        public List<Mign1Individual> ListadoDetalleChatarraSapSinFuIndividual(int DocEntry)
        {
            decimal pesoChatarra = 0;
            decimal precioChatarra = 0;
            List<Mign1Individual> lst = new List<Mign1Individual>();
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                var ListadoDetalleChatarra = from d in DB.IGN1
                                             where d.DocEntry == DocEntry
                                             select new
                                             {
                                                 d.ItemCode,
                                                 d.Dscription,
                                                 d.Quantity,
                                                 d.U_DC_PESOREAL
                                                 
                                             };

                foreach (var x in ListadoDetalleChatarra)
                {

                    //var modeloDetalleChatarra=  ConsultarModelosPorDescripcion(x.Dscription);

                    var modeloChatarra = ConsultarModelosPorCodigoItemSap(x.ItemCode);
                    foreach (var y in modeloChatarra)
                    {
                        pesoChatarra = y.PesoArticulo;
                        precioChatarra = y.UltimoPrecioCompra;
                    }
                    lst.Add(new Mign1Individual
                    {
                        DocEntry = DocEntry,
                        ItemCode = x.ItemCode,
                        Description = x.Dscription,
                        Cantidad = Convert.ToInt32(x.Quantity),

                        //PesoTeoricoUnitario = decimal.Round(pesoChatarra, 2),
                        //PesoTeoricoSubtotal = decimal.Round((x.Quantity.Value * pesoChatarra), 2),
                        PesoTeoricoUnitario = pesoChatarra,
                        PesoTeoricoSubtotal = (x.Quantity.Value * pesoChatarra),
                        PesoNetoTipo=Convert.ToDecimal(x.U_DC_PESOREAL),
                        //PesoTeoricoAjustado = 0,
                        //PesoTeoricoAjustadoTotal = 0,
                        //DesviacionIndividual=0
                    });
                }
                return lst;
            }

        }

        public string CompraBodega(int DocEntry)
        {
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                var ListadoDetalleChatarra = (from d in DB.PDN1
                                              where d.DocEntry == DocEntry
                                              select new
                                              {
                                                  d.WhsCode
                                              }).FirstOrDefault();
                string Bodega = ListadoDetalleChatarra.WhsCode;
                return Bodega;
            }
        }
        //public int CodigoGrupo(string Nombre)
        //{
        //    using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
        //    {
        //        var ListadoDetalleChatarra = (from d in DB.OCRD
        //                                      where d.CardName == Nombre
        //                                      select new
        //                                      {
        //                                          d.GroupCode
        //                                      }).FirstOrDefault();
        //        //aqui el error
        //        int codigo = Convert.ToInt32(ListadoDetalleChatarra.GroupCode);
        //        return codigo;
        //    }
        //}
        public int CodigoGrupo(string Identificacion)
        {
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                var ListadoDetalleChatarra = (from d in DB.OCRD
                                              where d.CardCode == Identificacion
                                              select new
                                              {
                                                  d.GroupCode
                                              }).FirstOrDefault();
                //aqui el error
                int codigo = Convert.ToInt32(ListadoDetalleChatarra.GroupCode);
                return codigo;
            }
        }

        public List<MdlPdn1> ListadoCompraDetalleChatarraSapSinFu(int DocEntry)
        {
            decimal pesoChatarra = 0;
            decimal precioChatarra = 0;
            List<MdlPdn1> lst = new List<MdlPdn1>();
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                var ListadoDetalleChatarra = from d in DB.PDN1
                                             where d.DocEntry == DocEntry
                                             select new
                                             {
                                                 d.ItemCode,
                                                 d.Dscription,
                                                 d.Quantity,
                                             };

                foreach (var x in ListadoDetalleChatarra)
                {
                    //var modeloDetalleChatarra=  ConsultarModelosPorDescripcion(x.Dscription);
                    var modeloChatarra = ConsultarModelosPorCodigoItemSap(x.ItemCode);
                    foreach (var y in modeloChatarra)
                    {
                        pesoChatarra = y.PesoArticulo;
                        precioChatarra = y.UltimoPrecioCompra;
                    }
                    lst.Add(new MdlPdn1
                    {
                        DocEntry = DocEntry,
                        ItemCode = x.ItemCode,
                        Description = x.Dscription,
                        Cantidad = Convert.ToInt32(x.Quantity),
                        //PesoTeoricoUnitario = decimal.Round(pesoChatarra, 2),
                        //PesoTeoricoSubtotal = decimal.Round((x.Quantity.Value * pesoChatarra), 2),
                        PesoTeoricoUnitario = pesoChatarra,
                        PesoTeoricoSubtotal = (x.Quantity.Value * pesoChatarra),
                        //PesoTeoricoAjustado = 0,
                        //PesoTeoricoAjustadoTotal = 0
                    });
                }
                return lst;
            }

        }
        public List<MdlPdn1Individual> ListadoCompraDetalleChatarraSapSinFuIndividual(int DocEntry)
        {
            decimal pesoChatarra = 0;
            decimal precioChatarra = 0;
            List<MdlPdn1Individual> lst = new List<MdlPdn1Individual>();
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                var ListadoDetalleChatarra = from d in DB.PDN1
                                             where d.DocEntry == DocEntry
                                             select new
                                             {
                                                 d.ItemCode,
                                                 d.Dscription,
                                                 d.Quantity,
                                                 d.U_DC_PESOREAL
                                             };

                foreach (var x in ListadoDetalleChatarra)
                {
                    //var modeloDetalleChatarra=  ConsultarModelosPorDescripcion(x.Dscription);
                    var modeloChatarra = ConsultarModelosPorCodigoItemSap(x.ItemCode);
                    foreach (var y in modeloChatarra)
                    {
                        pesoChatarra = y.PesoArticulo;
                        precioChatarra = y.UltimoPrecioCompra;
                    }
                    lst.Add(new MdlPdn1Individual
                    {
                        DocEntry = DocEntry,
                        ItemCode = x.ItemCode,
                        Description = x.Dscription,
                        Cantidad = Convert.ToInt32(x.Quantity),
                        //PesoTeoricoUnitario = decimal.Round(pesoChatarra, 2),
                        //PesoTeoricoSubtotal = decimal.Round((x.Quantity.Value * pesoChatarra), 2),
                        PesoTeoricoUnitario = pesoChatarra,
                        PesoTeoricoSubtotal = (x.Quantity.Value * pesoChatarra),
                        PesoNetoTipo=Convert.ToDecimal(x.U_DC_PESOREAL),
                        //PesoTeoricoAjustado = 0,
                        //PesoTeoricoAjustadoTotal = 0,
                        //DesviacionIndividual=0
                    });
                }
                return lst;
            }

        }
        public List<MdlPdn1Individual> ListadoCalculoIndividual(List<MdlPdn1Individual> listadoCalculo)
        {
            decimal valorUnitarioCalculado;
            decimal factorUnitario;
            decimal subtotalDesviacion;
            decimal desviacion;
            List<MdlPdn1Individual> lst = new List<MdlPdn1Individual>();

            foreach (var y in listadoCalculo)
            {
                valorUnitarioCalculado = y.PesoNetoTipo / y.Cantidad;

                //factorUnitario = (y.PesoNetoTipo / y.PesoTeoricoUnitario);
                //subtotalDesviacion = (y.PesoNetoTipo / y.PesoTeoricoUnitario) * 100;

                factorUnitario = (valorUnitarioCalculado / y.PesoTeoricoUnitario);
                subtotalDesviacion = (valorUnitarioCalculado / y.PesoTeoricoUnitario) * 100;
                if (subtotalDesviacion > 100)
                {
                    desviacion = subtotalDesviacion - 100;
                }
                else
                {
                    desviacion = (100 - subtotalDesviacion) * -1;
                }

                lst.Add(new MdlPdn1Individual
                {
                    DocEntry = y.DocEntry,
                    ItemCode = y.ItemCode,
                    Description = y.Description,
                    Cantidad = Convert.ToInt32(y.Cantidad),
                    PesoTeoricoUnitario = y.PesoTeoricoUnitario,
                    PesoTeoricoSubtotal =decimal.Round ((y.Cantidad * y.PesoTeoricoUnitario),2),
                    PesoNetoTipo = decimal.Round(y.PesoNetoTipo,2),
                    PesoTeoricoAjustado = decimal.Round((y.PesoTeoricoUnitario*factorUnitario),2),
                    PesoTeoricoAjustadoTotal = decimal.Round(((y.PesoTeoricoUnitario * factorUnitario)*y.Cantidad),2),
                    DesviacionIndividual = decimal.Round(desviacion,2)
                });

                }

                return lst;
        }
        public List<DetalleChatarraGuardado> ListadoModificarCalculoIndividual(List<DetalleChatarraGuardado> listadoCalculo)
        {
            decimal valorUnitarioCalculado;
            decimal factorUnitario;
            decimal subtotalDesviacion;
            decimal desviacion;
            List<DetalleChatarraGuardado> lst = new List<DetalleChatarraGuardado>();

            foreach (var y in listadoCalculo)
            {
                valorUnitarioCalculado = y.PesoNetoTipo / y.Cantidad;

                //factorUnitario = (y.PesoNetoTipo / y.PesoTeoricoUnitario);
                //subtotalDesviacion = (y.PesoNetoTipo / y.PesoTeoricoUnitario) * 100;
                factorUnitario = (valorUnitarioCalculado / y.PesoTeoricoUnitario);
                subtotalDesviacion = (valorUnitarioCalculado / y.PesoTeoricoUnitario) * 100;

                if (subtotalDesviacion > 100)
                {
                    desviacion = subtotalDesviacion - 100;
                }
                else
                {
                    desviacion = (100 - subtotalDesviacion) * -1;
                }

                lst.Add(new DetalleChatarraGuardado
                {
                    ChatarraDetalleId=y.ChatarraDetalleId,
                    ChatarraId=y.ChatarraId,
                    DocEntry = y.DocEntry,
                    ItemCode = y.ItemCode,
                    Description = y.Description,
                    Cantidad = Convert.ToInt32(y.Cantidad),
                    PesoTeoricoUnitario = y.PesoTeoricoUnitario,
                    PesoTeoricoTotal = decimal.Round((y.Cantidad * y.PesoTeoricoUnitario), 2),
                    PesoNetoTipo = decimal.Round(y.PesoNetoTipo, 2),
                    PesoTeoricoAjustado = decimal.Round((y.PesoTeoricoUnitario * factorUnitario), 2),
                    PesoTeoricoAjustadoTotal = decimal.Round(((y.PesoTeoricoUnitario * factorUnitario) * y.Cantidad), 2),
                    DesviacionIndividual = decimal.Round(desviacion, 2)
                });

            }

            return lst;
        }
        public List<Mign1> ListadoDetalleChatarraSap(int DocEntry, decimal factorUnitario)
        {
            decimal pesoChatarra = 0;
            decimal precioChatarra = 0;
            List<Mign1> lst = new List<Mign1>();
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                var ListadoDetalleChatarra = from d in DB.IGN1
                                              where d.DocEntry == DocEntry
                                             select new
                                              {
                                                  d.ItemCode,
                                                  d.Dscription,
                                                  d.Quantity
                                              };

                foreach (var x in ListadoDetalleChatarra)
                {

                  //var modeloDetalleChatarra=  ConsultarModelosPorDescripcion(x.Dscription);

                    var modeloChatarra = ConsultarModelosPorCodigoItemSap(x.ItemCode);
                    foreach (var y in modeloChatarra)
                    {
                        pesoChatarra = y.PesoArticulo;
                        precioChatarra = y.UltimoPrecioCompra;
                    }
                    lst.Add(new Mign1
                    {
                        DocEntry = DocEntry,
                        ItemCode = x.ItemCode,
                        Description = x.Dscription,
                        Cantidad = Convert.ToInt32(x.Quantity),
                        //PesoTeoricoUnitario = decimal.Round(pesoChatarra,2),
                        //PesoTeoricoSubtotal = decimal.Round((x.Quantity.Value * pesoChatarra),2),
                        //PesoTeoricoAjustado = decimal.Round((pesoChatarra * factorUnitario), 2),
                        //PesoTeoricoAjustadoTotal = decimal.Round(((pesoChatarra * factorUnitario)* x.Quantity.Value), 2)
                        PesoTeoricoUnitario = pesoChatarra,
                        PesoTeoricoSubtotal = (x.Quantity.Value * pesoChatarra),
                        PesoTeoricoAjustado = (pesoChatarra * factorUnitario),
                        PesoTeoricoAjustadoTotal = ((pesoChatarra * factorUnitario) * x.Quantity.Value), 
                    });
                }
                return lst;
            }

        }
        public List<DetalleChatarraGuardado> ListadoModificacionDetalleChatarraSap(int DocEntry, decimal factorUnitario)
        {
            List<DetalleChatarraGuardado> lst = new List<DetalleChatarraGuardado>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoDetalleChatarra = from d in DB.ChatarraDetalle
                                             where d.DocEntry == DocEntry
                                             select new
                                             {   d.ChatarraDetalleId,
                                                 d.ChatarraId,
                                                 d.CodigoItem,
                                                 d.Descripcion,
                                                 d.Cantidad,
                                                 d.PesoTeoricoUnitario,
                                                 d.PesoTeoricoTotal
                                             };

                foreach (var x in ListadoDetalleChatarra)
                {
                    lst.Add(new DetalleChatarraGuardado
                    {
                        ChatarraDetalleId=x.ChatarraDetalleId,
                        ChatarraId=x.ChatarraId.Value,
                        DocEntry = DocEntry,
                        ItemCode = Convert.ToString(x.CodigoItem),
                        Description = x.Descripcion,
                        Cantidad = x.Cantidad.Value,
                        PesoTeoricoUnitario = x.PesoTeoricoUnitario.Value,
                        PesoTeoricoTotal = (x.Cantidad.Value * x.PesoTeoricoUnitario.Value),
                        PesoTeoricoAjustado = (x.PesoTeoricoUnitario.Value * factorUnitario),
                        PesoTeoricoAjustadoTotal = ((x.PesoTeoricoUnitario.Value * factorUnitario) * x.Cantidad.Value),
                    });
                }
                return lst;
            }

        }
        public List<MdlPdn1> ListadoCompraDetalleChatarraSap(int DocEntry, decimal factorUnitario)
        {
            decimal pesoChatarra = 0;
            decimal precioChatarra = 0;
            List<MdlPdn1> lst = new List<MdlPdn1>();
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                var ListadoDetalleChatarra = from d in DB.PDN1
                                             where d.DocEntry == DocEntry
                                             select new
                                             {
                                                 d.ItemCode,
                                                 d.Dscription,
                                                 d.Quantity
                                             };

                foreach (var x in ListadoDetalleChatarra)
                {

                    //var modeloDetalleChatarra=  ConsultarModelosPorDescripcion(x.Dscription);

                    var modeloChatarra = ConsultarModelosPorCodigoItemSap(x.ItemCode);
                    foreach (var y in modeloChatarra)
                    {
                        pesoChatarra = y.PesoArticulo;
                        precioChatarra = y.UltimoPrecioCompra;
                    }
                    lst.Add(new MdlPdn1
                    {
                        DocEntry = DocEntry,
                        ItemCode = x.ItemCode,
                        Description = x.Dscription,
                        Cantidad = Convert.ToInt32(x.Quantity),
                        //PesoTeoricoUnitario = decimal.Round(pesoChatarra, 2),
                        //PesoTeoricoSubtotal = decimal.Round((x.Quantity.Value * pesoChatarra), 2),
                        //PesoTeoricoAjustado = decimal.Round((pesoChatarra * factorUnitario), 2),
                        //PesoTeoricoAjustadoTotal = decimal.Round(((pesoChatarra * factorUnitario) * x.Quantity.Value), 2)
                        PesoTeoricoUnitario = pesoChatarra,
                        PesoTeoricoSubtotal = (x.Quantity.Value * pesoChatarra),
                        PesoTeoricoAjustado = (pesoChatarra * factorUnitario),
                        PesoTeoricoAjustadoTotal = ((pesoChatarra * factorUnitario) * x.Quantity.Value),
                    });
                }
                return lst;
            }

        }



        public decimal ConsultarModelosPorDescripcion(String nombre)
        {
            List<Modelos> lst = new List<Modelos>();
            Decimal peso=0;
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoModelos = from d in DB.Modelos
                                     where d.Nombre==nombre
                                  select new {
                                      d.ModeloID,
                                      d.Nombre,
                                      d.PesoTeorico
                                  } ;

                foreach (var x in ListadoModelos)
                {
                    peso = decimal.Round(x.PesoTeorico.Value, 2);
                }
                return peso;
            }
        }

        public List<PesoPrecioArticulo> ConsultarModelosPorCodigoItemSap(string Codigo)
        {
            List<Modelos> lst = new List<Modelos>();

            List<PesoPrecioArticulo> lst2 = new List<PesoPrecioArticulo>();

            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                var ListadoModelos = from d in DB.OITM
                                     where d.ItemCode == Codigo
                                     select new
                                     {
                                         d.ItemCode,
                                         d.ItemName,
                                         d.U_DAC_PESOPRODUCTO,
                                         d.LastPurPrc
                                     };

                foreach (var x in ListadoModelos)
                {
                    decimal val = 0;
                    if (x.U_DAC_PESOPRODUCTO == null)
                    {
                        val = 1;
                    }
                    else {
                        val= x.U_DAC_PESOPRODUCTO.Value;
                    }
                    lst2.Add(new PesoPrecioArticulo
                    {
                        CodigoItem = x.ItemCode,
                        ItemName=x.ItemName,
                        PesoArticulo=decimal.Round(val,2),
                        UltimoPrecioCompra=decimal.Round(x.LastPurPrc.Value,2)
                    });
                }
                return lst2;
            }
        }
        public List<Modelos> ConsultarModelos()
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoModelos = DB.Modelos.ToList();
                return ListadoModelos;
            }
        }
        public bool GuardarModelos(String descripcion, decimal PesoTeorico)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try {
                    var modelo = new Modelos();
                    modelo.Nombre = descripcion;
                    modelo.PesoTeorico = PesoTeorico;

                    DB.Modelos.Add(modelo);
                    DB.SaveChanges();
                    return true;
                }
                catch (Exception ex) {
                    Console.WriteLine(ex);
                    return false;
                }
                
            }
        }

        public int IngresarChatarra(int DocEntry,string NumeroDocumento,string NumeroPedido, string  Fecha, string Identificacion, string Cliente,string clienteLinea,string clienteClase, string TipoIngreso, string Comentarios, string Bodega)
        {
            //var codigo = CodigoGrupo(Cliente);
            var codigo = CodigoGrupo(Identificacion);

            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var chatarra = new Chatarra();
                    chatarra.DocEntry = DocEntry;
                    chatarra.NumeroDocumento = Convert.ToInt32(NumeroDocumento);
                    chatarra.NumeroPedido = NumeroPedido;
                    chatarra.Fecha = Fecha;
                    chatarra.Identificacion = Identificacion;
                    chatarra.CardCode = codigo;
                    chatarra.Cliente = Cliente;
                    chatarra.ClienteClase = clienteClase;
                    chatarra.ClienteLinea = clienteLinea;
                    chatarra.TipoIngreso = TipoIngreso;
                    chatarra.Comentarios = Comentarios;
                    chatarra.BodegaId = Bodega;
                    chatarra.FechaRegistro = DateTime.Now;

                    DB.Chatarra.Add(chatarra);
                    DB.SaveChanges();

                    int prodId = chatarra.ChatarraId;        
                    return prodId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public bool IngresarDetalleChatarra(int ChatarraId, int DocEntry, string CodigoItem, string Descripcion, int Cantidad, decimal PesoTeoricoUnitario, decimal PesoTeoritcoTotal, decimal PesoUnitarioAjustado, decimal PesoTotalAjustado)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var chatarraDetalle = new ChatarraDetalle();
                    chatarraDetalle.ChatarraId = ChatarraId;
                    chatarraDetalle.DocEntry = DocEntry;
                    chatarraDetalle.CodigoItem = CodigoItem;
                    chatarraDetalle.Descripcion = Descripcion;
                    chatarraDetalle.Cantidad = Cantidad;
                    chatarraDetalle.PesoTeoricoUnitario = PesoTeoricoUnitario;
                    chatarraDetalle.PesoTeoricoTotal = PesoTeoritcoTotal;
                    chatarraDetalle.PesoUnitarioAjustado = PesoUnitarioAjustado;
                    chatarraDetalle.PesoTotalAjustado = PesoTotalAjustado;

                    DB.ChatarraDetalle.Add(chatarraDetalle);
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
        public bool IngresarDetalleChatarraIndividual(int ChatarraId, int DocEntry, string CodigoItem, string Descripcion, int Cantidad, decimal PesoTeoricoUnitario, decimal PesoTeoritcoTotal, decimal PesoNetoTipo, decimal PesoUnitarioAjustado, decimal PesoTotalAjustado, decimal desviacionIndividual)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var chatarraDetalle = new ChatarraDetalleIndividual();
                    chatarraDetalle.ChatarraId = ChatarraId;
                    chatarraDetalle.DocEntry = DocEntry;
                    chatarraDetalle.CodigoItem = CodigoItem;
                    chatarraDetalle.Descripcion = Descripcion;
                    chatarraDetalle.Cantidad = Cantidad;
                    chatarraDetalle.PesoTeoricoUnitario = PesoTeoricoUnitario;
                    chatarraDetalle.PesoTeoricoTotal = PesoTeoritcoTotal;
                    chatarraDetalle.PesoNetoIndividual = PesoNetoTipo;
                    chatarraDetalle.PesoUnitarioAjustado = PesoUnitarioAjustado;
                    chatarraDetalle.PesoTotalAjustado = PesoTotalAjustado;
                    chatarraDetalle.DesviacionIndividual = desviacionIndividual;

                    DB.ChatarraDetalleIndividual.Add(chatarraDetalle);
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
        public bool IngresarPesosTotalesChatarras(int DocEntry, String PesoTeoricoTotalCalculado, String PesoBultoIngresado, String PesoTotalAjustado, String Desviacion,int CantidadTotal, int TipoIngreso) {
            using (DacarProsoftEntities DB = new DacarProsoftEntities()) {
                try {
                    var chatarraPesos = new ChatarraPesos();
                    chatarraPesos.DocEntry = DocEntry;
                    chatarraPesos.PesoTeoricoTotalCalculado = Convert.ToDecimal(PesoTeoricoTotalCalculado);
                    chatarraPesos.PesoBultoIngresado = Convert.ToDecimal(PesoBultoIngresado);
                    chatarraPesos.PesoAjustadoTotal = Convert.ToDecimal(PesoTotalAjustado);
                    chatarraPesos.Desviacion = Desviacion;
                    chatarraPesos.CantidadTotal = CantidadTotal;
                    chatarraPesos.ModoIngreso = TipoIngreso;

                    DB.ChatarraPesos.Add(chatarraPesos);
                    DB.SaveChanges();
                    return true;
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public bool BusquedaLocal(int numero)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoChatarrasLocales = from d in DB.Chatarra where d.NumeroDocumento == numero

                                              select new
                                              {
                                                  d.NumeroDocumento,
                                                  d.Identificacion,
                                                  d.Cliente
                                              };

                if (ListadoChatarrasLocales.Count() >0)
                {
                    return true;
                }
                else {
                    return false;

                }

            }
        }

        public List<IngresosChatarras> ConsultarIngresosChatarraLocal(int anio/*, int codigoCliente,string codigos*/)
        {
            List<IngresosChatarras> lst = new List<IngresosChatarras>();

            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                //if (codigos == "--Todos--") {
                    var ListadoModelos = from d in DB.Chatarra
                                         join e in DB.ChatarraPesos on d.DocEntry equals e.DocEntry
                                         where (d.FechaRegistro != null) && ((d.FechaRegistro).Year == anio)
                                         orderby d.FechaRegistro descending

                                         select new
                                         {
                                             d.DocEntry,
                                             d.NumeroDocumento,
                                             d.Identificacion,
                                             d.Cliente,
                                             d.TipoIngreso,
                                             d.ClienteClase,
                                             d.ClienteLinea,
                                             d.CardCode,
                                             d.Comentarios,
                                             d.BodegaId,
                                             d.FechaRegistro,
                                             e.PesoTeoricoTotalCalculado,
                                             e.PesoBultoIngresado,
                                             e.CantidadTotal,
                                             e.PesoAjustadoTotal,
                                             e.Desviacion
                                         };

                    foreach (var x in ListadoModelos)
                    {
                    var groupName = GrupoCliente((x.CardCode).Value);
                    if (groupName == null && groupName == "") {
                        groupName = "No definido";
                    }
                        DateTime fechaDoc = Convert.ToDateTime(x.FechaRegistro, CultureInfo.InvariantCulture);
                        int mes = fechaDoc.Month;
                        DateTime fecha = Convert.ToDateTime(fechaDoc.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                        String mesIngreso = MonthName(fecha.Month);
                        string fechaDocumento = fechaDoc.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        lst.Add(new IngresosChatarras
                        {
                            DocEntry = x.DocEntry.Value,
                            NumeroDocumento = x.NumeroDocumento.Value,
                            CedulaCliente = x.Identificacion,
                            NombreCliente = x.Cliente,
                            CardCode=x.CardCode.Value,
                            GroupCode= groupName,
                            ClienteClase =x.ClienteClase,
                            ClienteLinea=x.ClienteLinea,
                            TipoIngreso = x.TipoIngreso,
                            Comments = x.Comentarios,
                            CantidadTotal = x.CantidadTotal.Value,
                            PesoTeoricoTotalCalculado = x.PesoTeoricoTotalCalculado.Value,
                            PesoBultoIngresado = x.PesoBultoIngresado.Value,
                            PesoAjustadoTotal = x.PesoAjustadoTotal.Value,
                            Desviacion = x.Desviacion,
                            Bodega = x.BodegaId,
                            FechaIngreso = fechaDocumento,
                            MesIngreso = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(mesIngreso)
                        });
                    }
                //}
                //else{
                //    var ListadoModelos = from d in DB.Chatarra
                //                         join e in DB.ChatarraPesos on d.DocEntry equals e.DocEntry
                //                         where (d.FechaRegistro != null)&& d.CardCode== codigoCliente && ((d.FechaRegistro).Value.Year == anio)
                //                         orderby d.FechaRegistro descending

                //                         select new
                //                         {
                //                             d.DocEntry,
                //                             d.NumeroDocumento,
                //                             d.Identificacion,
                //                             d.Cliente,
                //                             d.TipoIngreso,
                //                             d.ClienteClase,
                //                             d.ClienteLinea,
                //                             d.CardCode,
                //                             d.Comentarios,
                //                             d.BodegaId,
                //                             d.FechaRegistro,
                //                             e.PesoTeoricoTotalCalculado,
                //                             e.PesoBultoIngresado,
                //                             e.CantidadTotal,
                //                             e.PesoAjustadoTotal,
                //                             e.Desviacion
                //                         };

                //    foreach (var x in ListadoModelos)
                //    {
                //        DateTime fechaDoc = Convert.ToDateTime(x.FechaRegistro, CultureInfo.InvariantCulture);
                //        int mes = fechaDoc.Month;
                //        DateTime fecha = Convert.ToDateTime(fechaDoc.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //        String mesIngreso = MonthName(fecha.Month);
                //        string fechaDocumento = fechaDoc.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                //        lst.Add(new IngresosChatarras
                //        {
                //            DocEntry = x.DocEntry.Value,
                //            NumeroDocumento = x.NumeroDocumento.Value,
                //            CedulaCliente = x.Identificacion,
                //            NombreCliente = x.Cliente,
                //            CardCode = x.CardCode.Value,
                //            ClienteClase = x.ClienteClase,
                //            ClienteLinea = x.ClienteLinea,
                //            TipoIngreso = x.TipoIngreso,
                //            Comments = x.Comentarios,
                //            CantidadTotal = x.CantidadTotal.Value,
                //            PesoTeoricoTotalCalculado = x.PesoTeoricoTotalCalculado.Value,
                //            PesoBultoIngresado = x.PesoBultoIngresado.Value,
                //            PesoAjustadoTotal = x.PesoAjustadoTotal.Value,
                //            Desviacion = x.Desviacion,
                //            Bodega = x.BodegaId,
                //            FechaIngreso = fechaDocumento,
                //            MesIngreso = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(mesIngreso)
                //        });
                //    }
                //}
                return lst;
            }
        }
        public List<IngresosChatarras> ConsultarModificarIngresosChatarraLocal(int anio/*, int codigoCliente, string codigos*/)
        {
            List<IngresosChatarras> lst = new List<IngresosChatarras>();

            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                //if (codigos == "--Todos--")
                //{
                    var ListadoModelos = from d in DB.Chatarra
                                         join e in DB.ChatarraPesos on d.DocEntry equals e.DocEntry
                                         where (d.FechaRegistro != null) && ((d.FechaRegistro).Year == anio)
                                         orderby d.FechaRegistro.Month

                                         select new
                                         {
                                             d.DocEntry,
                                             d.NumeroDocumento,
                                             d.NumeroPedido,
                                             d.Identificacion,
                                             d.Cliente,
                                             d.TipoIngreso,
                                             d.CardCode,
                                             d.ClienteClase,
                                             d.ClienteLinea,
                                             d.Comentarios,
                                             d.BodegaId,
                                             d.FechaRegistro,
                                             e.PesoTeoricoTotalCalculado,
                                             e.PesoBultoIngresado,
                                             e.CantidadTotal,
                                             e.PesoAjustadoTotal,
                                             e.Desviacion,
                                             e.ModoIngreso
                                         };

                    foreach (var x in ListadoModelos)
                    {
                        var groupCode = GrupoCliente((x.CardCode).Value);
                        if (groupCode==null&& groupCode=="") {
                            groupCode = "No definido";
                        }
                        DateTime fechaDoc = Convert.ToDateTime(x.FechaRegistro, CultureInfo.InvariantCulture);
                        int mes = fechaDoc.Month;
                        DateTime fecha = Convert.ToDateTime(fechaDoc.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
                        String mesIngreso = MonthName(fecha.Month);
                        string fechaDocumento = fechaDoc.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        lst.Add(new IngresosChatarras
                        {
                            DocEntry = x.DocEntry.Value,
                            NumeroDocumento = x.NumeroDocumento.Value,
                            NumeroPedido=x.NumeroPedido,
                            CedulaCliente = x.Identificacion,
                            NombreCliente = x.Cliente,
                            CardCode = x.CardCode.Value,
                            GroupCode= groupCode,
                            ClienteLinea = x.ClienteLinea,
                            ClienteClase = x.ClienteClase,
                            TipoIngreso = x.TipoIngreso,
                            Comments = x.Comentarios,
                            CantidadTotal = x.CantidadTotal.Value,
                            PesoTeoricoTotalCalculado = x.PesoTeoricoTotalCalculado.Value,
                            PesoBultoIngresado = x.PesoBultoIngresado.Value,
                            PesoAjustadoTotal = x.PesoAjustadoTotal.Value,
                            Desviacion = x.Desviacion,
                            Bodega = x.BodegaId,
                            FechaIngreso = fechaDocumento,
                            MesIngreso = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(mesIngreso),
                            ModoIngreso = (x.ModoIngreso).Value
                        });
                    }
                    return lst;
                       
            }
        }
        public List<IngresosChatarras> ConsultarModificarIngresosChatarraImprimir(int DocEntry)
        {
            List<IngresosChatarras> lst = new List<IngresosChatarras>();

            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                //if (codigos == "--Todos--")
                //{
                var ListadoModelos = from d in DB.Chatarra
                                     join e in DB.ChatarraPesos on d.DocEntry equals e.DocEntry
                                     where d.DocEntry==DocEntry
                                     select new
                                     {
                                         d.DocEntry,
                                         d.NumeroDocumento,
                                         d.NumeroPedido,
                                         d.Identificacion,
                                         d.Cliente,
                                         d.TipoIngreso,
                                         d.CardCode,
                                         d.ClienteClase,
                                         d.ClienteLinea,
                                         d.Comentarios,
                                         d.BodegaId,
                                         d.FechaRegistro,
                                         e.PesoTeoricoTotalCalculado,
                                         e.PesoBultoIngresado,
                                         e.CantidadTotal,
                                         e.PesoAjustadoTotal,
                                         e.Desviacion,
                                         e.ModoIngreso
                                     };

                foreach (var x in ListadoModelos)
                {
                    var groupCode = GrupoCliente((x.CardCode).Value);
                    if (groupCode == null && groupCode == "")
                    {
                        groupCode = "No definido";
                    }
                    DateTime fechaDoc = Convert.ToDateTime(x.FechaRegistro, CultureInfo.InvariantCulture);
                    int mes = fechaDoc.Month;
                    DateTime fecha = Convert.ToDateTime(fechaDoc.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
                    String mesIngreso = MonthName(fecha.Month);
                    string fechaDocumento = fechaDoc.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    lst.Add(new IngresosChatarras
                    {
                        DocEntry = x.DocEntry.Value,
                        NumeroDocumento = x.NumeroDocumento.Value,
                        NumeroPedido = x.NumeroPedido,
                        CedulaCliente = x.Identificacion,
                        NombreCliente = x.Cliente,
                        CardCode = x.CardCode.Value,
                        GroupCode = groupCode,
                        ClienteLinea = x.ClienteLinea,
                        ClienteClase = x.ClienteClase,
                        TipoIngreso = x.TipoIngreso,
                        Comments = x.Comentarios,
                        CantidadTotal = x.CantidadTotal.Value,
                        PesoTeoricoTotalCalculado = x.PesoTeoricoTotalCalculado.Value,
                        PesoBultoIngresado = x.PesoBultoIngresado.Value,
                        PesoAjustadoTotal = x.PesoAjustadoTotal.Value,
                        Desviacion = x.Desviacion,
                        Bodega = x.BodegaId,
                        FechaIngreso = fechaDocumento,
                        MesIngreso = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(mesIngreso),
                        ModoIngreso = (x.ModoIngreso).Value
                    });
                }
                return lst;
            }
        }

        public List<ChatarraDetalle> ConsultarIngresosChatarraDetalleLocal(int docEntry)
        {
            List<ChatarraDetalle> lst = new List<ChatarraDetalle>();

            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoModelos = from d in DB.ChatarraDetalle
                                     where d.DocEntry == docEntry
                                     select new
                                     {
                                         d.DocEntry,
                                         d.CodigoItem,
                                         d.Descripcion,
                                         d.Cantidad,
                                         d.PesoTeoricoUnitario,
                                         d.PesoTeoricoTotal,
                                         d.PesoUnitarioAjustado,
                                         d.PesoTotalAjustado
                                     };

                foreach (var x in ListadoModelos) {
                    lst.Add(new ChatarraDetalle{  
                        DocEntry=x.DocEntry,
                        CodigoItem=x.CodigoItem,
                        Descripcion=x.Descripcion,
                        Cantidad=x.Cantidad,
                        PesoTeoricoUnitario= decimal.Round(x.PesoTeoricoUnitario.Value,2),
                        PesoTeoricoTotal=decimal.Round(x.PesoTeoricoTotal.Value,2),
                        PesoUnitarioAjustado= decimal.Round(x.PesoUnitarioAjustado.Value,2),
                        PesoTotalAjustado= decimal.Round(x.PesoTotalAjustado.Value,2)

                    });

                }
                return lst;
            }
        }
        public List<ChatarraDetalle> ConsultarIngresosIndividualesChatarraDetalleLocal(int docEntry)
        {
            List<ChatarraDetalle> lst = new List<ChatarraDetalle>();

            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoModelos = from d in DB.ChatarraDetalleIndividual
                                     where d.DocEntry == docEntry
                                     select new
                                     {
                                         d.DocEntry,
                                         d.CodigoItem,
                                         d.Descripcion,
                                         d.Cantidad,
                                         d.PesoTeoricoUnitario,
                                         d.PesoTeoricoTotal,
                                         d.PesoUnitarioAjustado,
                                         d.PesoTotalAjustado
                                     };

                foreach (var x in ListadoModelos)
                {
                    lst.Add(new ChatarraDetalle
                    {
                        DocEntry = x.DocEntry,
                        CodigoItem = x.CodigoItem,
                        Descripcion = x.Descripcion,
                        Cantidad = x.Cantidad,
                        PesoTeoricoUnitario = decimal.Round(x.PesoTeoricoUnitario.Value, 2),
                        PesoTeoricoTotal = decimal.Round(x.PesoTeoricoTotal.Value, 2),
                        PesoUnitarioAjustado = decimal.Round(x.PesoUnitarioAjustado.Value, 2),
                        PesoTotalAjustado = decimal.Round(x.PesoTotalAjustado.Value, 2)

                    });

                }
                return lst;
            }
        }

        public int BusquedaxDias()
        {
            int dias = 0;
            List <DiasBusqueda> lst= new List<DiasBusqueda>();
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

        public string MonthName(int month)
        {
            DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
            return dtinfo.GetMonthName(month);
        }

        public List<SelectListItem> ConsultarAniosVentas()
        {
            DateTime fechaActual = DateTime.Now;
            List<SelectListItem> anios = new List<SelectListItem>();
            int ano = Convert.ToInt32(fechaActual.Year);
            int i = 0;
            for (i = 0; i <= 3; i++)
            {
                anios.Add(new SelectListItem() { Text = Convert.ToString(ano - i), Value = Convert.ToString(ano - i) });
            }
            return anios;
        }

        public List<DetalleChatarraGuardado> DetallaModificacionChatarra(int DocEntry,int ModoIngreso)
        {
            List<DetalleChatarraGuardado> lst = new List<DetalleChatarraGuardado>();
            var modoIngr = ConsultarModoIngreso(DocEntry);
            if (modoIngr == 1)
            {
                using (DacarProsoftEntities DB = new DacarProsoftEntities())
                {
                    var ListadoDetalle = from d in DB.ChatarraDetalle
                                  where d.DocEntry == DocEntry
                                  select new
                                  {
                                      d.ChatarraDetalleId,
                                      d.ChatarraId,
                                      d.DocEntry,
                                      d.CodigoItem,
                                      d.Descripcion,
                                      d.Cantidad,
                                      d.PesoTeoricoUnitario,
                                      d.PesoTeoricoTotal,
                                      d.PesoUnitarioAjustado,
                                      d.PesoTotalAjustado
                                  };
                    foreach (var x in ListadoDetalle)
                    {
                        lst.Add(new DetalleChatarraGuardado
                        {
                   ChatarraDetalleId=x.ChatarraDetalleId,
                   ChatarraId=x.ChatarraId.Value,
                   DocEntry=x.DocEntry.Value,
                   ItemCode=Convert.ToString(x.CodigoItem),
                   Description=x.Descripcion,
                   Cantidad=x.Cantidad.Value,
                   PesoTeoricoUnitario=x.PesoTeoricoUnitario.Value,
                   PesoTeoricoTotal=x.PesoTeoricoTotal.Value,
                   PesoTeoricoAjustado=x.PesoUnitarioAjustado.Value,
                   PesoTeoricoAjustadoTotal=x.PesoTotalAjustado.Value

                        });
                    }
                    return lst;
                }
            }
            else {
                using (DacarProsoftEntities DB = new DacarProsoftEntities())
                {
                    var ListadoDetalle = from d in DB.ChatarraDetalleIndividual
                                         where d.DocEntry == DocEntry
                                         select new
                                         {
                                             d.ChatarraDetalleIndividual1,
                                             d.ChatarraId,
                                             d.DocEntry,
                                             d.CodigoItem,
                                             d.Descripcion,
                                             d.Cantidad,
                                             d.PesoTeoricoUnitario,
                                             d.PesoTeoricoTotal,
                                             d.PesoNetoIndividual,
                                             d.PesoUnitarioAjustado,
                                             d.PesoTotalAjustado,
                                             d.DesviacionIndividual
                                         };
                    foreach (var x in ListadoDetalle)
                    {
                        lst.Add(new DetalleChatarraGuardado
                        {
                            ChatarraDetalleId = x.ChatarraDetalleIndividual1,
                            ChatarraId = x.ChatarraId.Value,
                            DocEntry = x.DocEntry.Value,
                            ItemCode = Convert.ToString(x.CodigoItem),
                            Description = x.Descripcion,
                            Cantidad = x.Cantidad.Value,
                            PesoTeoricoUnitario = x.PesoTeoricoUnitario.Value,
                            PesoTeoricoTotal = x.PesoTeoricoTotal.Value,
                            PesoNetoTipo=x.PesoNetoIndividual.Value,
                            PesoTeoricoAjustado = x.PesoUnitarioAjustado.Value,
                            PesoTeoricoAjustadoTotal = x.PesoTotalAjustado.Value,
                            DesviacionIndividual=x.DesviacionIndividual.Value                          
                        });
                    }
                    return lst;
                }
            }
        }
        public int ConsultarModoIngreso(int DocEntry)
        {
            int val = 0;
            List<DiasBusqueda> lst = new List<DiasBusqueda>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var modoIng = from d in DB.ChatarraPesos
                                     where d.DocEntry == DocEntry
                                     select new
                                     {
                                         d.ModoIngreso
                                     };
                foreach (var x in modoIng)
                {
                    val = x.ModoIngreso.Value;
                }
                return val;
            }
        }

        public bool GuardarActualizacionDetalle(int ChatarraDetalleId, decimal PesoUnitarioAjustado, decimal PesoTotalAjustado)
        {
            List<IngresosChatarras> lst = new List<IngresosChatarras>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var regi = (from d in DB.ChatarraDetalle                               
                                     where d.ChatarraDetalleId== ChatarraDetalleId
                                     select d).FirstOrDefault();
                try
                {
                    regi.PesoUnitarioAjustado = PesoUnitarioAjustado;
                    regi.PesoTotalAjustado = PesoTotalAjustado;
                    DB.SaveChanges();
                    return true;
                }
                catch (Exception ex) {
                    Console.WriteLine(ex);
                    return false;
                }
            }           
        }
        public bool GuardarActualizacionDetalleIndividual(int ChatarraDetalleId, decimal PesoIngresadoIndividual, decimal PesoUnitarioAjustado, decimal PesoTotalAjustado, decimal DesviacionIndividual)
        {
            List<IngresosChatarras> lst = new List<IngresosChatarras>();

            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {        
                var regi = (from d in DB.ChatarraDetalleIndividual
                            where d.ChatarraDetalleIndividual1 == ChatarraDetalleId
                            select d).FirstOrDefault();               
                try
                {
                    regi.PesoNetoIndividual = PesoIngresadoIndividual;
                    regi.PesoUnitarioAjustado = PesoUnitarioAjustado;
                    regi.PesoTotalAjustado = PesoTotalAjustado;
                    regi.DesviacionIndividual = DesviacionIndividual;
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
        public bool GuardarActualizacionPesosTotales(int DocEntry, decimal PesoTeoricoTotalCal, decimal PesoBultoIng,decimal PesoAjustadoTot,string desviacionTot)
        {
            List<IngresosChatarras> lst = new List<IngresosChatarras>();

            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var regi = (from d in DB.ChatarraPesos
                            where d.DocEntry == DocEntry
                            select d).FirstOrDefault();
                try
                {
                    regi.PesoTeoricoTotalCalculado = PesoTeoricoTotalCal;
                    regi.PesoBultoIngresado = PesoBultoIng;
                    regi.PesoAjustadoTotal = PesoAjustadoTot;
                    regi.Desviacion = desviacionTot;

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
        public string GrupoCliente(int GroupCode)
        {
            string NombreGrupo="";
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoModelos = from d in DB.GrupoClientes where d.GroupCode==GroupCode
                                     select new
                                     {
                                         d.GroupName
                                     };
                foreach (var x in ListadoModelos)
                {
                    if (x.GroupName != null && x.GroupName != "")
                    {
                        NombreGrupo = x.GroupName;
                    }
                    else {
                        NombreGrupo = "No definido";
                    }
                }
                return NombreGrupo;
            }
        }

        public List<IngresosGenralesChatarra> ConsultaDeRegistrosChatarrasGeneralPorFechas(DateTime fechaInicio, DateTime fechaFin, int OpcionFiltrado)
        {
            string fechaRegistro;
            List<IngresosGenralesChatarra> lst = new List<IngresosGenralesChatarra>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                if (OpcionFiltrado == 1)
                {
                    var ListadoDetalleChatarra = from d in DB.ChatarraDetalle
                                                 join e in DB.Chatarra on d.ChatarraId equals e.ChatarraId 
                                                 join f in DB.GrupoClientes on e.CardCode equals f.GroupCode
                                                 //join f in DB.GrupoClientes on e.CardCode equals f.GroupName
                                                 where e.FechaRegistro >= fechaInicio && e.FechaRegistro <= fechaFin
                                                 select new
                                                 {
                                                 e.NumeroDocumento,
                                                 e.Fecha,
                                                 e.Identificacion,
                                                 e.Cliente,                                              
                                                 f.GroupName,
                                                 e.ClienteLinea,
                                                 e.ClienteClase,
                                                 e.TipoIngreso,
                                                 e.Comentarios,
                                                 e.BodegaId,
                                                 e.FechaRegistro,
                                                 d.Descripcion,
                                                 d.Cantidad,
                                                 d.PesoTeoricoTotal,
                                                 d.PesoTotalAjustado
                                                 };

                    foreach (var x in ListadoDetalleChatarra)
                    {
                        DateTime FechaRegistro = Convert.ToDateTime(x.FechaRegistro, CultureInfo.InvariantCulture);
                        fechaRegistro = FechaRegistro.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        lst.Add(new IngresosGenralesChatarra
                        {
                            NumeroDocumento = x.NumeroDocumento.Value,
                            Fecha = x.Fecha,
                            Identificacion = x.Identificacion,
                            Cliente = x.Cliente,
                            GroupName = x.GroupName,
                            ClienteLinea = x.ClienteLinea,
                            ClienteClase = x.ClienteClase,
                            TipoIngreso = x.TipoIngreso,
                            Comentarios = x.Comentarios,
                            BodegaId = x.BodegaId,
                            FechaRegistro = fechaRegistro,
                            Descripcion = x.Descripcion,
                            Cantidad = x.Cantidad.Value,
                            PesoTotalAjustado = x.PesoTotalAjustado.Value,
                            PesoTeoricoTotalAjustado = x.PesoTeoricoTotal.Value,
                            DiferenciaPesos = Decimal.Round((x.PesoTeoricoTotal.Value - x.PesoTotalAjustado.Value), 2),
                            MesRegistro = FechaRegistro.ToString("MMMM")
                        }); ;
                    }
                    return lst;
                }
                else {
                    var ListadoDetalleChatarra = from d in DB.ChatarraDetalleIndividual
                                                 join e in DB.Chatarra on d.ChatarraId equals e.ChatarraId
                                                 join f in DB.GrupoClientes on e.CardCode equals f.GroupCode
                                                 //join f in DB.GrupoClientes on e.CardCode equals f.GroupName

                                                 where e.FechaRegistro >= fechaInicio && e.FechaRegistro <= fechaFin
                                                 select new
                                                 {
                                                     e.NumeroDocumento,
                                                     e.Fecha,
                                                     e.Identificacion,
                                                     e.Cliente,
                                                     f.GroupName,
                                                     e.ClienteLinea,
                                                     e.ClienteClase,
                                                     e.TipoIngreso,
                                                     e.Comentarios,
                                                     e.BodegaId,
                                                     e.FechaRegistro,
                                                     d.Descripcion,
                                                     d.Cantidad,
                                                     d.PesoTeoricoTotal,
                                                     d.PesoTotalAjustado
                                                 };

                    foreach (var x in ListadoDetalleChatarra)
                    {
                        DateTime FechaRegistro = Convert.ToDateTime(x.FechaRegistro, CultureInfo.InvariantCulture);
                        fechaRegistro = FechaRegistro.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        lst.Add(new IngresosGenralesChatarra
                        {
                            NumeroDocumento = x.NumeroDocumento.Value,
                            Fecha = x.Fecha,
                            Identificacion = x.Identificacion,
                            Cliente = x.Cliente,
                            GroupName = x.GroupName,
                            ClienteLinea = x.ClienteLinea,
                            ClienteClase = x.ClienteClase,
                            TipoIngreso = x.TipoIngreso,
                            Comentarios = x.Comentarios,
                            BodegaId = x.BodegaId,
                            FechaRegistro = fechaRegistro,
                            Descripcion = x.Descripcion,
                            Cantidad = x.Cantidad.Value,
                            PesoTotalAjustado = x.PesoTotalAjustado.Value,
                            PesoTeoricoTotalAjustado = x.PesoTeoricoTotal.Value,
                            DiferenciaPesos = Decimal.Round((x.PesoTeoricoTotal.Value - x.PesoTotalAjustado.Value), 2),
                            MesRegistro = FechaRegistro.ToString("MMMM")
                        });
                    }
                    return lst;
                }             
            }
        }

        public List<GenericoParaGroupBy> ReporteClienteChatarrasPorMeses(int anioBusqueda, string NombreCliente)
        {
            string Mes;

            List<GenericoParaGroupBy> lst = new List<GenericoParaGroupBy>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = from d in DB.Chatarra join
                              e in DB.ChatarraPesos on d.DocEntry equals e.DocEntry 
                              where 
                              d.Cliente==NombreCliente &&
                              d.FechaRegistro.Year == anioBusqueda
                              group new { d, e } by new { d.FechaRegistro.Month} into ut
                              orderby ut.Key.Month
                              select new
                              {
                                  contador= ut.Sum(val=>val.e.CantidadTotal),
                                  MonthNumber = ut.Key.Month
                              };
                foreach (var x in Listado)
                {
                    Mes = BuscarNombreMes(x.MonthNumber);
                    lst.Add(new GenericoParaGroupBy
                    {
                        Descripcion = Mes,
                        Valor = x.contador.Value
                    });
                }
                return lst;
            }
        }

        public List<GenericoParaGroupBy> ReporteGeneralChatarrasPorMeses(int anioBusqueda)
        {
            string Mes;

            List<GenericoParaGroupBy> lst = new List<GenericoParaGroupBy>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = from d in DB.Chatarra
                              join
                              e in DB.ChatarraPesos on d.DocEntry equals e.DocEntry
                              where                           
                              d.FechaRegistro.Year == anioBusqueda
                              group new { d, e } by new { d.FechaRegistro.Month } into ut
                              orderby ut.Key.Month
                              select new
                              {
                                  contador = ut.Sum(val => val.e.CantidadTotal),
                                  MonthNumber = ut.Key.Month
                              };
                foreach (var x in Listado)
                {
                    Mes = BuscarNombreMes(x.MonthNumber);
                    lst.Add(new GenericoParaGroupBy
                    {
                        Descripcion = Mes,
                        Valor = x.contador.Value
                    });
                }
                return lst;
            }
        }

        public List<GenericoParaGroupBy> ReporteGeneralChatarrasPorMesesPorTipo(int anioBusqueda, string tipo)
        {
            string Mes;
            List<GenericoParaGroupBy> lst = new List<GenericoParaGroupBy>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = from d in DB.Chatarra
                              join
                              e in DB.ChatarraPesos on d.DocEntry equals e.DocEntry
                              where
                              d.FechaRegistro.Year == anioBusqueda && d.TipoIngreso == tipo
                              group new { d, e } by new { d.FechaRegistro.Month } into ut
                              orderby ut.Key.Month
                              select new
                              {
                                  contador = ut.Sum(val => val.e.CantidadTotal),
                                  MonthNumber = ut.Key.Month
                              };
                foreach (var x in Listado)
                {
                    Mes = BuscarNombreMes(x.MonthNumber);
                    lst.Add(new GenericoParaGroupBy
                    {
                        Descripcion = Mes,
                        Valor = x.contador.Value
                    });
                }
                return lst;
            }
        }

        public List<GenericoParaGroupBy> ReporteGeneralChatarrasPorMesesPorTipoCliente(int anioBusqueda, int cardCode)
        {
            string Mes;
            var groupCode = GrupoCliente((cardCode));

            List<GenericoParaGroupBy> lst = new List<GenericoParaGroupBy>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = from d in DB.Chatarra
                              join
                              e in DB.ChatarraPesos on d.DocEntry equals e.DocEntry
                              join 
                              f in DB.GrupoClientes on d.CardCode equals f.GroupCode
                              where
                              d.FechaRegistro.Year == anioBusqueda && f.GroupCode == cardCode
                              group new { d, e } by new { d.FechaRegistro.Month } into ut
                              orderby ut.Key.Month
                              select new
                              {
                                  contador = ut.Sum(val => val.e.CantidadTotal),
                                  MonthNumber = ut.Key.Month
                              };

                foreach (var x in Listado)
                {
                    Mes = BuscarNombreMes(x.MonthNumber);
                    lst.Add(new GenericoParaGroupBy
                    {
                        Descripcion = Mes,
                        Valor = x.contador.Value
                    });
                }
                return lst;
            }
        }

        public string BuscarNombreMes(int num)
        {
            string result;
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
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

        public List<ReporteChatarraConDesviacion> ReporteGeneralChatarrasPorDesviacionesSap(int anioBusqueda)
        {
            int contador = 1;          
            string fechaRegistro;
            List<ReporteChatarraConDesviacion> lst = new List<ReporteChatarraConDesviacion>();
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1()) { 

                var Listado = from d in DB.ReporteIngresoChatarraConDesviacionRN
                              where d.Fecha.Value.Year== anioBusqueda
                              orderby d.Fecha.Value.Month ascending
                              select new
                              {                                
                                  d.N_Documento,
                                  d.Pedido,
                                  d.Identificador,
                                  d.Cliente,
                                  d.Tipo_Cliente,
                                  d.Cliente_Linea,
                                  d.Cliente_Clase,
                                  d.Tipo_Ingreso,
                                  d.Cantidad,
                                  d.Peso_Teorico,
                                  d.Peso_Real,
                                  d.Desviacion,
                                  d.Bodega,
                                  d.Vendedor,
                                  d.Comentarios,
                                  d.Fecha,
                                  d.DocEntry
                              };
 
                foreach (var x in Listado)
                {
                    if (x.Peso_Teorico!=null) {
                        DateTime FechaRegistro = Convert.ToDateTime(x.Fecha, CultureInfo.InvariantCulture);
                        fechaRegistro = FechaRegistro.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                        lst.Add(new ReporteChatarraConDesviacion
                        {
                            Id = contador,
                            N_Documento = x.N_Documento,
                            Pedido = x.Pedido,
                            Identificador = x.Identificador,
                            Cliente = x.Cliente,
                            Tipo_Cliente = x.Tipo_Cliente.Trim(new Char[] { ' ', '.', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' }),
                            Cliente_Linea = x.Cliente_Linea.Trim(new Char[] { ' ', '.', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' }),
                            Cliente_Clase = x.Cliente_Clase.Trim(new Char[] { ' ', '.', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' }),
                            Tipo_Ingreso = x.Tipo_Ingreso,
                            Cantidad = x.Cantidad,
                            Peso_Teorico = Decimal.Round(x.Peso_Teorico.Value, 2),
                            Peso_Real = Decimal.Round(x.Peso_Real.Value, 2),
                            Desviacion = Decimal.Round(x.Desviacion.Value, 2),
                            Bodega = x.Bodega,
                            Vendedor = x.Vendedor,
                            Comentarios = x.Comentarios,
                            FechaRegistro = fechaRegistro,
                            DocEntry=x.DocEntry,
                            //FechaRegistro2=CultureInfo.InvariantCulture.TextInfo.ToTitleCase((x.Fecha.Value).ToString("MMMM"))
                            FechaRegistro2 = x.Fecha.Value.Month
                        });
                        contador++;
                    }                  
                }
                return lst;
            }
        }

        public List<Mign1> ListadoNotasCreditoDetalleChatarraSap(int DocEntry)
        {
            decimal pesoChatarra = 0;
            decimal pesoReal = 0;
            decimal desviacion= 0;
            decimal tempDesviacion = 0;
            List<Mign1> lst = new List<Mign1>();
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                var ListadoDetalleChatarra = from d in DB.IGN1
                                             where d.DocEntry == DocEntry
                                             select new
                                             {
                                                 d.ItemCode,
                                                 d.Dscription,
                                                 d.Quantity,
                                                 d.U_DC_PESOBRUTO,
                                                 d.U_DC_PESOREAL,                                     
                                             };
                foreach (var x in ListadoDetalleChatarra)
                {
                    var modeloChatarra = ConsultarModelosPorCodigoItemSap(x.ItemCode);
                    foreach (var y in modeloChatarra)
                    {
                        pesoChatarra = y.PesoArticulo;
                    }
                    if (x.U_DC_PESOREAL == null)
                    {
                        pesoReal = (x.Quantity.Value * pesoChatarra);
                    }
                    else {
                        pesoReal = x.U_DC_PESOREAL.Value;
                    }
                    tempDesviacion = (pesoReal / (x.Quantity.Value * pesoChatarra))*100;
                    if (tempDesviacion > 100) {
                        desviacion = 100 - tempDesviacion;
                    }
                    else {
                        desviacion = tempDesviacion - 100;
                    }
                    lst.Add(new Mign1
                    {
                        DocEntry = DocEntry,
                        ItemCode = x.ItemCode,
                        Description = x.Dscription,
                        Cantidad = Convert.ToInt32(x.Quantity),
                        PesoTeoricoUnitario = pesoChatarra,
                        PesoTeoricoSubtotal = (x.Quantity.Value * pesoChatarra),
                        PesoIngresado = decimal.Round(pesoReal, 2),
                        Desviacion = decimal.Round(desviacion, 2),
                    });
                }
                return lst;
            }
        }

        public List<MdlPdn1> ListadoIngresoCompraDetalleChatarraSap(int DocEntry)
        {
            decimal pesoChatarra = 0;
            decimal pesoReal = 0;
            decimal desviacion = 0;
            decimal tempDesviacion = 0;

            List<MdlPdn1> lst = new List<MdlPdn1>();
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                var ListadoDetalleChatarra = from d in DB.PDN1
                                             where d.DocEntry == DocEntry
                                             select new
                                             {
                                                 d.ItemCode,
                                                 d.Dscription,
                                                 d.Quantity,
                                                 d.U_DC_PESOBRUTO,
                                                 d.U_DC_PESOREAL,
                                             };

                foreach (var x in ListadoDetalleChatarra)
                {
                    var modeloChatarra = ConsultarModelosPorCodigoItemSap(x.ItemCode);
                    foreach (var y in modeloChatarra)
                    {
                        pesoChatarra = y.PesoArticulo;
                    }
                    if (x.U_DC_PESOREAL == null)
                    {
                        pesoReal = (x.Quantity.Value * pesoChatarra);
                    }
                    else
                    {
                        pesoReal = x.U_DC_PESOREAL.Value;
                    }
                    tempDesviacion = (pesoReal / (x.Quantity.Value * pesoChatarra)) * 100;

                    if (tempDesviacion > 100)
                    {
                        desviacion = 100 - tempDesviacion;
                    }
                    else
                    {
                        desviacion = tempDesviacion - 100;
                    }
                    lst.Add(new MdlPdn1
                    {
                        DocEntry = DocEntry,
                        ItemCode = x.ItemCode,
                        Description = x.Dscription,
                        Cantidad = Convert.ToInt32(x.Quantity),
                        PesoTeoricoUnitario = decimal.Round(pesoChatarra, 2),
                        PesoTeoricoSubtotal = decimal.Round((x.Quantity.Value * pesoChatarra), 2),
                        PesoIngresado = decimal.Round(pesoReal, 2),
                        Desviacion = decimal.Round(desviacion, 2),
                    });
                }
                return lst;
            }
        }
    }
}
