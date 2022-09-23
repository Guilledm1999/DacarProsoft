using DacarDatos.Datos;
using DacarProsoft.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace DacarProsoft.Datos
{
    public class DaoOrdenesVentas
    {
        public List<CabeceraOrdenVenta> ListadoCabeceraOrdenesVentasSap(string Exportacion)
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
                                             where d.U_BPP_MDMT==Exportacion&& d.DocDate >= fechaCorte && d.DocDate <= fechaActual
                                                   orderby d.DocDate descending
                                                   //d.U_SYP_EXPORTACION == Exportacion &&
                                                   //d.U_SYP_EXPORTACION == Exportacion &&

                                                   select new
                                             {
                                                 d.DocEntry,
                                                 d.DocNum,
                                                 d.DocDate,
                                                 d.TaxDate,
                                                 d.CardCode,
                                                 d.CardName,
                                                 d.DocTotal,
                                                 d.U_SYP_NUMOCCL
                                             };

                foreach (var x in ListadoCabeceraOrdenesVentas)
                {
                    var busqueda = BusquedaLocal(x.DocNum);
                    if (busqueda == false)
                    {
                        DateTime fechaCont = Convert.ToDateTime(x.DocDate, CultureInfo.InvariantCulture);
                        string fechaContabilizacion = fechaCont.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime fechaDoc = Convert.ToDateTime(x.TaxDate, CultureInfo.InvariantCulture);
                        string fechaDocumento = fechaDoc.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        string mesIngreso = MonthName(fechaDoc.Month);

                        lst.Add(new CabeceraOrdenVenta
                        {
                            DocEntry = x.DocEntry,
                            DocNum = x.DocNum,
                            DocDate = fechaContabilizacion,
                            TaxDate = fechaDocumento,
                            CardCode = x.CardCode,
                            CardName = x.CardName,
                            NumeroOrden=x.U_SYP_NUMOCCL,
                            DocTotal = x.DocTotal.Value,
                            SypExportacion = Exportacion,
                            Mes = textInfo.ToTitleCase(mesIngreso)
                        });
                    }
                }
                var ListadoCabeceraFactOrdenesVentas = from d in DB.OINV
                                                   where d.U_SYP_NUMOCCL == "PHE/6609" || d.U_SYP_NUMOCCL == "DAC-USA-01" || d.U_SYP_NUMOCCL == "45110" || d.U_SYP_NUMOCCL == "PHE/7062"
                                                       orderby d.DocDate descending
                                                   //d.U_SYP_EXPORTACION == Exportacion &&
                                                   //d.U_SYP_EXPORTACION == Exportacion &&

                                                   select new
                                                   {
                                                       d.DocEntry,
                                                       d.DocNum,
                                                       d.DocDate,
                                                       d.TaxDate,
                                                       d.CardCode,
                                                       d.CardName,
                                                       d.DocTotal,
                                                       d.U_SYP_NUMOCCL
                                                   };
                foreach (var x in ListadoCabeceraFactOrdenesVentas)
                {
                    var busqueda = BusquedaLocal(x.DocNum.Value);
                    if (busqueda == false)
                    {
                        DateTime fechaCont = Convert.ToDateTime(x.DocDate, CultureInfo.InvariantCulture);
                        string fechaContabilizacion = fechaCont.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime fechaDoc = Convert.ToDateTime(x.TaxDate, CultureInfo.InvariantCulture);
                        string fechaDocumento = fechaDoc.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        string mesIngreso = MonthName(fechaDoc.Month);

                        lst.Add(new CabeceraOrdenVenta
                        {
                            DocEntry = x.DocEntry,
                            DocNum = x.DocNum.Value,
                            DocDate = fechaContabilizacion,
                            TaxDate = fechaDocumento,
                            CardCode = x.CardCode,
                            CardName = x.CardName,
                            NumeroOrden = x.U_SYP_NUMOCCL,
                            DocTotal = x.DocTotal.Value,
                            SypExportacion = Exportacion,
                            Mes = textInfo.ToTitleCase(mesIngreso)

                        });
                    }
                }

                return lst;
            }
        }
        public string MonthName(int month)
        {
            DateTimeFormatInfo dtinfo = new CultureInfo("es-MX", false).DateTimeFormat;
            return dtinfo.GetMonthName(month);
        }

        public List<CabeceraOrdenVenta> ListadoCabeceraFacturasReservaSap(string FactRese)
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
                var ListadoCabeceraOrdenesVentas = from d in DB.OINV 
                                                   where d.isIns == FactRese && d.DocDate >= fechaCorte && d.DocDate <= fechaActual && d.DocStatus == "O" /*&& d.DocStatus == "C"*/
                                                   orderby d.DocDate descending
                                                   //d.U_SYP_EXPORTACION == Exportacion &&
                                                   //d.U_SYP_EXPORTACION == Exportacion &&

                                                   select new
                                                   {
                                                       d.DocEntry,
                                                       d.DocNum,
                                                       d.DocDate,
                                                       d.TaxDate,
                                                       d.CardCode,
                                                       d.CardName,
                                                       d.DocTotal,
                                                       d.U_SYP_NUMOCCL
                                                   };

                foreach (var x in ListadoCabeceraOrdenesVentas)
                {
                    var busqueda = BusquedaLocal(x.DocNum.Value);
                    if (busqueda == false)
                    {
                        DateTime fechaCont = Convert.ToDateTime(x.DocDate, CultureInfo.InvariantCulture);
                        string fechaContabilizacion = fechaCont.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime fechaDoc = Convert.ToDateTime(x.TaxDate, CultureInfo.InvariantCulture);
                        string fechaDocumento = fechaDoc.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        string mesIngreso = MonthName(fechaDoc.Month);

                        lst.Add(new CabeceraOrdenVenta
                        {
                            DocEntry = x.DocEntry,
                            DocNum = x.DocNum.Value,
                            DocDate = fechaContabilizacion,
                            TaxDate = fechaDocumento,
                            CardCode = x.CardCode,
                            CardName = x.CardName,
                            NumeroOrden = x.U_SYP_NUMOCCL,
                            DocTotal = x.DocTotal.Value,
                            Mes = textInfo.ToTitleCase(mesIngreso)
                        });
                    }
                }
                return lst;
            }
        }
        public List<CabeceraOrdenVenta> ListadoCabeceraFacturasReservaSapCanceladas(string FactRese)
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
                var ListadoCabeceraOrdenesVentas = from d in DB.OINV
                                                   where d.isIns == FactRese && d.DocDate >= fechaCorte && d.DocDate <= fechaActual  && d.DocStatus == "C"
                                                   orderby d.DocDate descending
                                                   //d.U_SYP_EXPORTACION == Exportacion &&
                                                   //d.U_SYP_EXPORTACION == Exportacion &&

                                                   select new
                                                   {
                                                       d.DocEntry,
                                                       d.DocNum,
                                                       d.DocDate,
                                                       d.TaxDate,
                                                       d.CardCode,
                                                       d.CardName,
                                                       d.DocTotal,
                                                       d.U_SYP_NUMOCCL
                                                   };

                foreach (var x in ListadoCabeceraOrdenesVentas)
                {
                    var busqueda = BusquedaLocal(x.DocNum.Value);
                    if (busqueda == false)
                    {
                        DateTime fechaCont = Convert.ToDateTime(x.DocDate, CultureInfo.InvariantCulture);
                        string fechaContabilizacion = fechaCont.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime fechaDoc = Convert.ToDateTime(x.TaxDate, CultureInfo.InvariantCulture);
                        string fechaDocumento = fechaDoc.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        string mesIngreso = MonthName(fechaDoc.Month);

                        lst.Add(new CabeceraOrdenVenta
                        {
                            DocEntry = x.DocEntry,
                            DocNum = x.DocNum.Value,
                            DocDate = fechaContabilizacion,
                            TaxDate = fechaDocumento,
                            CardCode = x.CardCode,
                            CardName = x.CardName,
                            NumeroOrden = x.U_SYP_NUMOCCL,
                            DocTotal = x.DocTotal.Value,
                            Mes = textInfo.ToTitleCase(mesIngreso)

                        });
                    }
                }
                return lst;
            }
        }
        public bool BusquedaLocal(int numero)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoPacking = from d in DB.Packing
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
        public List<DetalleOrdenVenta> ListadoDetalleOrdenesVentasSap(int DocEntry)
        {

            List<DetalleOrdenVenta> lst = new List<DetalleOrdenVenta>();
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                var ListadoDetalleOrdenesVentas = from d in DB.RDR1
                                                  where d.DocEntry == DocEntry
                                                  select new
                                                  {
                                                      d.DocEntry,
                                                      d.WhsCode,
                                                      d.Quantity,
                                                      d.ItemCode,
                                                      d.Dscription,
                                                      d.Text,
                                                      d.Price,
                                                      d.LineTotal,
                                                  };

                foreach (var x in ListadoDetalleOrdenesVentas)
                {
                  
                    lst.Add(new DetalleOrdenVenta
                    {
                        DocEntry = x.DocEntry,
                        WhsCode=x.WhsCode,
                        Cantidad=Convert.ToInt32(x.Quantity),
                        ItemCode=x.ItemCode,
                        Descripcion=x.Dscription,
                        Text=x.Text,
                        Precio=x.Price.Value,
                        PrecioTotal=x.LineTotal.Value
                    });
                }
                return lst;
            }

        }
        public List<DetalleOrdenVenta> ListadoDetalleFacturasReservaSap(int DocEntry)
        {

            List<DetalleOrdenVenta> lst = new List<DetalleOrdenVenta>();
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                var ListadoDetalleOrdenesVentas = from d in DB.INV1 join
                                                  e in DB.OITM on d.ItemCode equals e.ItemCode
                                                  where d.DocEntry == DocEntry
                                                  select new
                                                  {
                                                      d.DocEntry,
                                                      d.WhsCode,
                                                      d.Quantity,
                                                      d.ItemCode,
                                                      d.Dscription,
                                                      d.Text,
                                                      d.Price,
                                                      d.LineTotal,
                                                      e.FrgnName,
                                                      e.U_DAC_MARCA
                                                  };

                foreach (var x in ListadoDetalleOrdenesVentas)
                {

                    lst.Add(new DetalleOrdenVenta
                    {
                        DocEntry = x.DocEntry,
                        WhsCode = x.WhsCode,
                        Cantidad = Convert.ToInt32(x.Quantity),
                        ItemCode = x.ItemCode,
                        Descripcion = x.Dscription,
                        Text = x.Text,
                        Precio = x.Price.Value,
                        PrecioTotal = x.LineTotal.Value,
                        NombreForaneo=x.FrgnName,
                        NombreGenerico=x.U_DAC_MARCA
                    });
                }
                return lst;
            }

        }

        public int CantidadTotalPallet(int PackingId,int PalletPacking,int PalletNumber)
        {
            int cantTotal = 0;
            List<DiasBusqueda> lst = new List<DiasBusqueda>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoModelos = from d in DB.PalletPackingDetalle join e in DB.PalletPacking on d.PalletPacking equals e.PalletPacking1
                                     where e.PalletPacking1== PalletPacking && e.PalletNumber==PalletNumber
                                     select new
                                     {
                                         d.CantidadItem
                                     };
                foreach (var x in ListadoModelos)
                {
                    cantTotal = cantTotal + x.CantidadItem.Value;
                }

                return cantTotal;
            }
        }
        public int NumeroPalletPacking(int PalletPacking)
        {
            int cantTotal = 0;
            List<DiasBusqueda> lst = new List<DiasBusqueda>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoModelos = from d in DB.PalletPacking
                                     where d.PalletPacking1 == PalletPacking 
                                     select new
                                     {
                                         d.PalletNumber
                                     };
                foreach (var x in ListadoModelos)
                {
                    cantTotal = x.PalletNumber.Value;
                }

                return cantTotal;
            }
        }
        public string ConsultarNombreForaneo(string ItemCode)
        {
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                try
                {
                    var NombreForaneo = (from d in DB.OITM
                                         where d.ItemCode == ItemCode
                                         select new
                                         {
                                             d.FrgnName
                                         }).FirstOrDefault();

                    if (NombreForaneo != null) {
                        if (NombreForaneo.FrgnName != "" && NombreForaneo.FrgnName != null)
                        {
                            return NombreForaneo.FrgnName;

                        }
                        else
                        {
                            return "SIN ESPECIFICAR";

                        }
                    }
                    else
                    {
                        return "SIN ESPECIFICAR";

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return "Sin Especificar";
                }
            }
        }
        public string ConsultarGenerico(string ItemCode)
        {
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                try
                {
                    var Generico = (from d in DB.OITM
                                         where d.ItemCode == ItemCode
                                         select new
                                         {
                                             d.U_DAC_MARCA
                                         }).FirstOrDefault();
                    if (Generico != null) {
                        if (Generico.U_DAC_MARCA != "" && Generico.U_DAC_MARCA != null)
                        {
                            return Generico.U_DAC_MARCA;

                        }
                        else
                        {
                            return "SIN ESPECIFICAR";

                        }
                    }
                    else
                    {
                        return "SIN ESPECIFICAR";

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return "Sin Especificar";
                }
            }
        }
        public string ConsultarDatosTecnicos(string ItemCode)
        {
            string generico = ConsultarGenerico(ItemCode);
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var Generico = (from d in DB.GenericosItem
                                    where d.ModeloDacar == generico
                                    select new
                                    {
                                        d.EtiquetaDatosTecnicos
                                    }).FirstOrDefault();
                    if (Generico!=null) {
                        if (Generico.EtiquetaDatosTecnicos != "" && Generico.EtiquetaDatosTecnicos != null)
                        {
                            return Generico.EtiquetaDatosTecnicos;

                        }
                        else
                        {
                            return "SIN ESPECIFICAR";

                        }
                    }
                    else
                    {
                        return "SIN ESPECIFICAR";
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return "Sin Especificar";
                }
            }
        }
        public string ConsultarPolaridad(string ItemCode)
        {

            string generico = ConsultarGenerico(ItemCode);
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var Generico = (from d in DB.GenericosItem
                                    where d.ModeloDacar == generico
                                    select new
                                    {
                                        d.Polaridad
                                    }).FirstOrDefault();
                    if (Generico != null)
                    {
                        if (Generico.Polaridad != null && Generico.Polaridad != "")
                        {
                            return Generico.Polaridad;
                        }
                        else {
                            return "SIN ESPECIFICAR";
                        }
                    }
                    else
                    {
                        return "SIN ESPECIFICAR";

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return "SIN ESPECIFICAR";
                }
            }
        }

        public List<ItemsPackingList> ListadoItemsPackingList(int PackingId)
        {
            string nombreForaneo = null;
            List<ItemsPackingList> lst = new List<ItemsPackingList>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoDetalleOrdenesVentas = from  d in DB.PalletPackingDetalle /*join e in DB.PalletPacking on d.PackingId equals e.PackingId*/
                                                  where d.PackingId == PackingId
                                                  select new
                                                  {
                                                      d.PalletPacking,
                                                      d.PalletPackingDetalleId,
                                                      d.ItemCode,
                                                      d.DescriptionCode,
                                                      d.CantidadItem,
                                                      //e.PalletNumber                                                  
                                                  };

                foreach (var x in ListadoDetalleOrdenesVentas)
                {
                    nombreForaneo = ConsultarNombreForaneo(x.ItemCode);
                    int palletNumer =NumeroPalletPacking(x.PalletPacking.Value);
                    lst.Add(new ItemsPackingList
                    {
                        PalletPackingDetalleId=x.PalletPackingDetalleId,
                        NumeroPallet= palletNumer,
                        Descripcion= nombreForaneo,
                        ItemCode=x.ItemCode,
                        Cantidad=x.CantidadItem.Value,                      
                    });
                }
                return lst;
            }

        }
        public List<ItemsPackingList> ListadoDetallesPalletsPackingList(int PackingId)
        {
       
            List<ItemsPackingList> lst = new List<ItemsPackingList>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoDetalleOrdenesVentas = from d in DB.PalletPacking
                                                  where d.PackingId == PackingId
                                                  select new
                                                  {
                                                      d.PalletPacking1,
                                                      d.PalletNumber,
                                                      d.AltoPallet,
                                                      d.AnchoPallet,
                                                      d.VolumenPallet,
                                                      d.PesoBruto,
                                                      d.PesoNeto
                                                  };

                foreach (var x in ListadoDetalleOrdenesVentas)
                {
                    int cantTotal = 0;
                    cantTotal = CantidadTotalPallet(PackingId, x.PalletPacking1,x.PalletNumber.Value);
                    lst.Add(new ItemsPackingList
                    {
                        PalletPackinId = x.PalletPacking1,
                        NumeroPallet = x.PalletNumber.Value,
                        Cantidad = cantTotal,
                        Alto = x.AltoPallet.Value,
                        Ancho = x.AnchoPallet.Value,
                        Volumen = x.VolumenPallet.Value,
                        PesoBruto = x.PesoBruto.Value,
                        PesoNeto = x.PesoNeto.Value
                    });
                }
                return lst;
            }

        }

        public List<DetalleOrdenVenta> ListadoDetalleOrdenesVentasSapFaltantes(int DocEntry)
        {

            List<DetalleOrdenVenta> lst = new List<DetalleOrdenVenta>();
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                var ListadoDetalleOrdenesVentas = from d in DB.RDR1 join f in DB.ORDR on d.DocEntry equals f.DocEntry
                                                  where d.DocEntry == DocEntry
                                                  select new
                                                  {
                                                      f.DocNum,
                                                      d.DocEntry,
                                                      d.WhsCode,
                                                      d.Quantity,
                                                      d.ItemCode,
                                                      d.Dscription,
                                                      d.Price,
                                                      d.LineTotal,
                                                  };

                foreach (var x in ListadoDetalleOrdenesVentas)
                {

                    lst.Add(new DetalleOrdenVenta
                    {
                        DocEntry = x.DocEntry,
                        WhsCode = x.WhsCode,
                        Cantidad = Convert.ToInt32(x.Quantity),
                        ItemCode = x.ItemCode,
                        Descripcion = x.Dscription,
                        Precio = x.Price.Value,
                        PrecioTotal = x.LineTotal.Value

                    });
                }
                return lst;
            }

        }
        public bool ItemsFaltantes() {
            return true;
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

    }
}