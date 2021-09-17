using DacarDatos.Datos;
using DacarProsoft.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using System.Web;

namespace DacarProsoft.Datos
{
    public class DaoPackingList
    {

        public List<PackingListCabecera> ConsultarPackingListCabecera()
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoCabecera = DB.PackingListCabecera.ToList();
                return ListadoCabecera;
            }
        }
        public List<PackingIngresados> ConsultarPackingIngreseados(string tipo)
        {
            string estado;
            List<PackingIngresados> lst = new List<PackingIngresados>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoCabecera = from d in DB.Packing orderby d.PackingId descending
                                      where d.Tipo==tipo
                                      select new {
                                          d.PackingId,
                                          d.NumeroDocumento,
                                          d.NumeroOrden,
                                          d.NombreCliente,
                                          d.Origen,
                                          d.Destino,
                                          d.CantidadPallet,
                                          d.DetalleIngresado
                                      };

                foreach (var x in ListadoCabecera) {
                    var PalletFaltante=PalletFantantes(x.PackingId,x.CantidadPallet.Value);
                    if (PalletFaltante == 0)
                    {
                        estado = "Completo";
                    }
                    else {
                        estado = "Incompleto";
                    }

                    lst.Add(new PackingIngresados
                    {
                        PackingId = x.PackingId,
                        NumeroDocumento = x.NumeroDocumento.Value,
                        NumeroOrden = x.NumeroOrden,
                        NombreCliente = x.NombreCliente,
                        Origen = x.Origen,
                        Destino = x.Destino,
                        CantidadPallet = x.CantidadPallet.Value,
                        PalletFaltantes = PalletFaltante,
                        DetalleIngresado=x.DetalleIngresado,
                        Estado = estado
                    });
                }
             


                return lst;
            }
        }

        public int PalletFantantes(int PackingId, int TotalPallet) {
            int Contador = 0;
            int Total = 0;
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoCabecera = from d in DB.PalletPacking
                                      where d.PackingId==PackingId
                                      select new
                                      {
                                          d.PalletNumber
                                      };
                foreach (var x in ListadoCabecera) {
                    Contador = Contador + 1;
                }

                Total = TotalPallet - Contador;

                return Total;
            }
        }

        public List<PalletPacking> ConsultarPalletsIngreseados(int PackingId, int PalletId)
        {
            List<PalletPacking> lst = new List<PalletPacking>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoCabecera = from d in DB.PalletPacking
                                      where d.PackingId==PackingId && d.PalletPacking1==PalletId
                                      select new
                                      {
                                          d.PalletPacking1,
                                          d.PackingId,
                                          d.PalletNumber,
                                          d.LargoPallet,
                                          d.AnchoPallet,
                                          d.AltoPallet,
                                          d.VolumenPallet,
                                          d.PesoNeto,
                                          d.PesoBruto
                                      };

                foreach (var x in ListadoCabecera)
                {
              

                    lst.Add(new PalletPacking
                    {
                        PalletPacking1=x.PalletPacking1,
                        PackingId = x.PackingId,
                        PalletNumber=x.PalletNumber,
                        LargoPallet=x.LargoPallet,
                        AnchoPallet=x.AnchoPallet,
                        AltoPallet=x.AltoPallet,
                        VolumenPallet=x.VolumenPallet,
                        PesoNeto=x.PesoNeto,
                        PesoBruto=x.PesoBruto        

                    });
                }
                return lst;
            }
        }

        public List<InfoPallets> ConsultarInfoPallets(int PackingId, int PalletId)
        {
            List<InfoPallets> lst = new List<InfoPallets>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoCabecera = from d in DB.Packing 
                                      join e in DB.PalletPacking on d.PackingId equals e.PackingId
                                      where d.PackingId == PackingId && e.PalletPacking1 == PalletId
                                      select new
                                      {
                                          d.PackingId,
                                          d.NumeroOrden,
                                          d.NombreCliente,
                                          d.Destino,
                                          d.Origen,
                                          d.CantidadPallet,
                                          e.PalletNumber,
                                          e.PesoBruto,
                                          e.PesoNeto,
                                          e.CodigoQr

                                      };

                foreach (var x in ListadoCabecera)
                {


                    lst.Add(new InfoPallets
                    {
                        NumeroOrden = x.NumeroOrden,
                        NombreCliente = x.NombreCliente,
                        Destino = x.Destino,
                        Origen = x.Origen,
                        CantidadPallet=x.CantidadPallet.Value,
                        PalletNumber = x.PalletNumber.Value,
                        PesoBruto = x.PesoBruto.Value,
                        PesoNeto = x.PesoNeto.Value,
                        CodigoQr=x.CodigoQr

                    });
                }
                return lst;
            }
        }
        public List<PalletPackingDetalle> ConsultarPalletsDetalleIngreseados(int PackingId, int PalletPackingId)
        {
            string nombreForaneo = null;
            List<PalletPackingDetalle> lst = new List<PalletPackingDetalle>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoCabecera = from d in DB.PalletPackingDetalle
                                      where d.PackingId==PackingId && d.PalletPacking== PalletPackingId
                                      select new
                                      {
                                          d.PalletPackingDetalleId,
                                          d.PalletPacking,
                                          d.PackingId,
                                          d.ItemCode,
                                          d.DescriptionCode,
                                          d.CantidadItem
                                     
                                      };

                foreach (var x in ListadoCabecera)
                {
                    nombreForaneo = ConsultarNombreForaneo(x.ItemCode);

                    lst.Add(new PalletPackingDetalle
                    {
                        PalletPackingDetalleId=x.PalletPackingDetalleId,
                        PalletPacking = x.PalletPacking,
                        PackingId = x.PackingId,
                        ItemCode=x.ItemCode,
                        DescriptionCode= nombreForaneo,
                        CantidadItem=x.CantidadItem

                    });
                }
                return lst;
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
                    if (NombreForaneo.FrgnName != "" && NombreForaneo.FrgnName != null)
                    {
                        return NombreForaneo.FrgnName;

                    }
                    else {
                        return "SIN ESPECIFICAR";

                    }
                }
                catch (Exception ex) {
                    Console.WriteLine(ex);
                    return "Sin Especificar";
                }
            }
        }
        public List<Packing> ConsultarPacking()
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoPacking = DB.Packing.ToList();
                return ListadoPacking;
            }
        }
        public int ConsultarCantidadPalletPacking(int PackingId)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoPacking = (from d in DB.Packing
                                     where d.PackingId == PackingId
                                     select new
                                     {
                                         d.CantidadPallet
                                     }).FirstOrDefault();
                
                return ListadoPacking.CantidadPallet.Value;
            }
        }
        public List<Packing> ConsultarNumeroOrden(int PackingId)
        {
            List<Packing> lst = new List<Packing>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoPacking = (from d in DB.Packing
                                      where d.PackingId == PackingId
                                      select new
                                      {
                                          d.NumeroOrden,
                                          d.NombreCliente,
                                          d.Origen,
                                          d.Destino
                                      });
                foreach (var x in ListadoPacking)
                {
                    lst.Add(new Packing
                    {
                        NumeroOrden = x.NumeroOrden,
                        NombreCliente = x.NombreCliente,
                        Origen = x.Origen,
                        Destino = x.Destino
                    });

                }

                return lst;
            }
        }
        public bool ConsultarEstadoPacking(int PackingId)
        {
            int contador=0;
            int result = 0;
            var CantidadPallet = ConsultarCantidadPalletPacking(PackingId);

            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoPacking = from d in DB.PalletPacking
                                      where d.PackingId == PackingId
                                      select new
                                      {
                                          d.PalletNumber
                                      };
                foreach (var x in ListadoPacking) {
                    contador = contador + 1;
                }
                result = CantidadPallet - contador;
                if (result == 0)
                {
                    return true;

                }
                else {
                    return false;
                }

            }
        }
        public List<PalletPackingDetalle> ConsultarPalletPacking(int IdentificadorPacking)
        {
            List<PalletPackingDetalle> lst = new List<PalletPackingDetalle>();

            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoPalletDetalle = (from d in DB.PalletPackingDetalle
                                       where d.PackingId == IdentificadorPacking
                                       select new
                                       {
                                          d.PalletPackingDetalleId,
                                          d.ItemCode,
                                          d.DescriptionCode,
                                          d.CantidadItem
                                       });

                foreach (var x in ListadoPalletDetalle) {
                    lst.Add(new PalletPackingDetalle
                    {
                        PalletPackingDetalleId=x.PalletPackingDetalleId,
                        ItemCode=x.ItemCode,
                        DescriptionCode=x.DescriptionCode,
                        CantidadItem=x.CantidadItem
                    });
                        
                }
                return lst;
            }
        }
        public int ConsultarPalletPackingPorItem(int IdentificadorPacking, string ItemCode)
        {
            List<PalletPackingDetalle> lst = new List<PalletPackingDetalle>();
            int valor=0;
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoPalletDetalle = (from d in DB.PalletPackingDetalle
                                            where d.PackingId == IdentificadorPacking && d.ItemCode==ItemCode
                                            select new
                                            {
                                                d.CantidadItem
                                            });

                foreach (var x in ListadoPalletDetalle)
                {
                    if (x.CantidadItem != null)
                    {
                        valor = valor + x.CantidadItem.Value;
                    }
                    else {
                        valor = 0;
                    }

                }
                return valor;
            }
        }
   
        public int ConsultarAcumItem(int IdentificadorPacking, string itemCode)
        {
            List<PalletPackingDetalle> lst = new List<PalletPackingDetalle>();
            int valor = 0;
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoPalletDetalle = (from d in DB.PalletPackingDetalle
                                            where d.PackingId == IdentificadorPacking && d.ItemCode==itemCode
                                            select new
                                            {
                                                d.CantidadItem
                                            });

                foreach (var x in ListadoPalletDetalle)
                {
                    if (x.CantidadItem != null)
                    {
                        valor = valor + x.CantidadItem.Value;
                    }
                    else
                    {
                        valor = 0;
                    }

                }
                return valor;
            }
        }
        public int ConsultarTotalItem(int IdentificadorPacking, string itemCode)
        {
            List<PalletPackingDetalle> lst = new List<PalletPackingDetalle>();
            int valor = 0;
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoPalletDetalle = (from d in DB.PackingDtl
                                            where d.PackingId == IdentificadorPacking && d.CodigoItem == itemCode
                                            select new
                                            {
                                                d.CantidadItem
                                            }).FirstOrDefault();

                valor = ListadoPalletDetalle.CantidadItem.Value;
                //foreach (var x in ListadoPalletDetalle)
                //{
                //    if (x.CantidadItem != null)
                //    {
                //        valor = valor + x.CantidadItem.Value;
                //    }
                //    else
                //    {
                //        valor = 0;
                //    }

                //}
                return valor;
            }
        }
        public int ConsultarNumeroPallet(int IdentificadorPacking)
        {
            List<PalletPackingDetalle> lst = new List<PalletPackingDetalle>();
            int valor = 1;
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoPalletDetalle = (from d in DB.PalletPacking
                                            where d.PackingId == IdentificadorPacking
                                            select new
                                            {
                                                d.PalletNumber
                                            });

                foreach (var x in ListadoPalletDetalle)
                {
                    if (x.PalletNumber != null)
                    {
                        valor = valor + 1;
                    }
                    else
                    {
                        valor = 1;
                    }

                }
                return valor;
            }
        }
        public List<PackingDtl> ConsultarPackingDtl()
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoPackingDtl = DB.PackingDtl.ToList();
                return ListadoPackingDtl;
            }
        }
        public List<PalletPacking> ConsultarPallet(int PackingId)
        {
            List<PalletPacking> lst = new List<PalletPacking>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoPallet = from d in DB.PalletPacking
                                        where d.PackingId == PackingId
                                        select new {
                                            d.PalletPacking1,
                                            d.PackingId,
                                            d.PalletNumber,
                                            d.AnchoPallet,
                                            d.LargoPallet,
                                            d.AltoPallet,
                                            d.VolumenPallet,
                                            d.PesoNeto,
                                            d.PesoBruto
                                        };
                foreach (var x in ListadoPallet) {
                    lst.Add(new PalletPacking {
                        PalletPacking1=x.PalletPacking1,
                        PackingId=x.PackingId,
                        PalletNumber=x.PalletNumber,
                        AnchoPallet=x.AnchoPallet,
                        LargoPallet=x.LargoPallet,
                        AltoPallet=x.AltoPallet,
                        VolumenPallet=x.VolumenPallet,
                        PesoNeto=x.PesoNeto,
                        PesoBruto=x.PesoBruto
                    });
                }

                return lst;
            }
        }
        public int obtenerCantidadItemPallet(int PalletPackingId) {
            int acum = 0;
            List<PalletPacking> lst = new List<PalletPacking>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoPallet = from d in DB.PalletPackingDetalle
                                    where d.PalletPacking == PalletPackingId
                                    select new
                                    {
                                      d.CantidadItem
                                    };
                foreach (var x in ListadoPallet)
                {
                    acum = acum + x.CantidadItem.Value;
                }

            }
            return acum;
        }
        public List<PalletPackingCant> ConsultarPalletCant(int PackingId)
        {
            List<PalletPackingCant> lst = new List<PalletPackingCant>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoPallet = from d in DB.PalletPacking
                                    where d.PackingId == PackingId
                                    select new
                                    {
                                        d.PalletPacking1,
                                        d.PackingId,
                                        d.PalletNumber,
                                        d.AnchoPallet,
                                        d.LargoPallet,
                                        d.AltoPallet,
                                        d.VolumenPallet,
                                        d.PesoNeto,
                                        d.PesoBruto
                                    };
                foreach (var x in ListadoPallet)
                {
                    var cant = obtenerCantidadItemPallet(x.PalletPacking1);
                    lst.Add(new PalletPackingCant
                    {
                        PalletPacking1 = x.PalletPacking1,
                        PackingId = x.PackingId.Value,
                        PalletNumber = x.PalletNumber.Value,
                        AnchoPallet = x.AnchoPallet.Value,
                        LargoPallet = x.LargoPallet.Value,
                        AltoPallet = x.AltoPallet.Value,
                        VolumenPallet = x.VolumenPallet.Value,
                        PesoNeto = x.PesoNeto.Value,
                        PesoBruto = x.PesoBruto.Value,
                        Cantidad= cant

                    });
                }

                return lst;
            }
        }
        public List<PalletPackingDetalle> ConsultarPalletDetalle(int PackingId, int PalletPackingId)
        {
            List<PalletPackingDetalle> lst = new List<PalletPackingDetalle>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoPalletDetalle = from d in DB.PalletPackingDetalle
                                        where d.PackingId == PackingId && d.PalletPacking == PalletPackingId
                                        select new
                                        {
                                        d.PalletPacking1,
                                        d.PalletPackingDetalleId,
                                        d.PalletPacking,
                                        d.PackingId,
                                        d.ItemCode,
                                        d.DescriptionCode,
                                        d.CantidadItem
                                        };
                foreach (var x in ListadoPalletDetalle) {
                    lst.Add(new PalletPackingDetalle{
                        PalletPacking1=x.PalletPacking1,
                        PalletPackingDetalleId=x.PalletPackingDetalleId,
                        PalletPacking=x.PalletPacking,
                        PackingId=x.PackingId,
                        ItemCode=x.ItemCode,
                        DescriptionCode=x.DescriptionCode,
                        CantidadItem=x.CantidadItem
                    });
                }

                return lst;
            }
        }
        public List<DetallePallet> ConsultarPackingDtl(int IdentificadorPacking)
        {
            string estado = "";
            List<DetallePallet> lst = new List<DetallePallet>();

            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoDetallePallet = (from d in DB.PackingDtl
                                       where d.PackingId == IdentificadorPacking
                                       select new
                                       {
                                           d.PackingDtlId,
                                           d.CodigoItem,
                                           d.DescripcionItem,
                                           d.CantidadItem
                                       });

                foreach (var x in ListadoDetallePallet) {
                    var detallePackingLocal = ConsultarPalletPackingPorItem(IdentificadorPacking,x.CodigoItem);
                    if ((x.CantidadItem - detallePackingLocal) == 0)
                    {
                        estado = "Completo";
                    }
                    else {
                        estado = "Incompleto";
                    }

                    lst.Add(new DetallePallet {
                        PackingDtlId=x.PackingDtlId,
                        ItemCode=x.CodigoItem,
                        DescriptionItem=x.DescripcionItem,
                        CantidadItem=x.CantidadItem.Value,
                        Pallet=0,
                        TotalItem=detallePackingLocal,
                        SaldoItem=(x.CantidadItem.Value-detallePackingLocal),
                        TotalItem2 = detallePackingLocal,
                        SaldoItem2 = (x.CantidadItem.Value - detallePackingLocal),
                        Status =estado
                    });


                }

                return lst;
            }
        }

        public int IngresarPacking(int NumeroDocumento, string NumeroOrden, string NombreCliente, string Origen, string Destino,int CantidadPallet, string tipo)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var packing = new Packing();

                    packing.NumeroDocumento = NumeroDocumento;
                    packing.NumeroOrden = NumeroOrden;
                    packing.NombreCliente = NombreCliente;
                    packing.Origen = Origen;
                    packing.Destino = Destino;
                    packing.CantidadPallet = CantidadPallet;
                    packing.Tipo = tipo;
                    packing.DetalleIngresado = "NO";


                    DB.Packing.Add(packing);
                    DB.SaveChanges();
                    int prodId = packing.PackingId;
                    return prodId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }
        public bool IngresarPackingDtl(int PackingId, string CodigoItem, string DescripcionItem, int CantidadItem)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var packingDtl = new PackingDtl();

                    packingDtl.PackingId = PackingId;
                    packingDtl.CodigoItem = CodigoItem;
                    packingDtl.DescripcionItem = DescripcionItem;
                    packingDtl.CantidadItem = CantidadItem;

                    DB.PackingDtl.Add(packingDtl);
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
        public int IngresarPackingPallet(int PackingId, int palletNumber, decimal largo, decimal ancho, decimal alto, decimal volumen, decimal pesoNeto, decimal pesoBruto,string Qr)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var palletPacking = new PalletPacking();

                    palletPacking.PackingId = PackingId;
                    palletPacking.PalletNumber = palletNumber;
                    palletPacking.LargoPallet = largo;
                    palletPacking.AnchoPallet = ancho;
                    palletPacking.AltoPallet = alto;
                    palletPacking.VolumenPallet = volumen;
                    palletPacking.PesoNeto = pesoNeto;
                    palletPacking.PesoBruto = pesoBruto;
                    palletPacking.CodigoQr = Qr;

                    DB.PalletPacking.Add(palletPacking);
                    DB.SaveChanges();
                    int prodId = palletPacking.PalletPacking1;
                    return prodId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }
        public bool ActualizarPackingPallet(int PalletId,string Qr)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var query = (from a in DB.PalletPacking
                                 where a.PalletPacking1 == PalletId
                                 select a).FirstOrDefault();

                    query.CodigoQr = Qr;


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
        public bool IngresarPackingPalletDetalle(int PalletPakingId,int PackingId,string CodigoItem, string DescripcionItem, int CantidadItem)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var palletPackingDetalle = new PalletPackingDetalle();

                    palletPackingDetalle.PalletPacking = PalletPakingId;
                    palletPackingDetalle.PackingId = PackingId;
                    palletPackingDetalle.ItemCode = CodigoItem;
                    palletPackingDetalle.DescriptionCode = DescripcionItem;
                    palletPackingDetalle.CantidadItem = CantidadItem;

                    DB.PalletPackingDetalle.Add(palletPackingDetalle);
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


        public bool EliminarPallet(int PalletId)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {

                try
                {
                    DB.PalletPackingDetalle.RemoveRange(DB.PalletPackingDetalle.Where(x => x.PalletPacking == PalletId));
                    DB.SaveChanges();

                    DB.PalletPacking.RemoveRange(DB.PalletPacking.Where(x => x.PalletPacking1 == PalletId));
                    DB.SaveChanges();

                    return true;
                }
                catch (Exception ex) {
                    Console.WriteLine(ex);
                    return false;

                }
            }

        }
        public bool EliminarPacking(int PackingId)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {

                try
                {
                    DB.PalletPackingDetalle.RemoveRange(DB.PalletPackingDetalle.Where(x => x.PackingId == PackingId));
                    DB.SaveChanges();

                    DB.PalletPacking.RemoveRange(DB.PalletPacking.Where(x => x.PackingId == PackingId));
                    DB.SaveChanges();

                    DB.PackingDtl.RemoveRange(DB.PackingDtl.Where(x => x.PackingId == PackingId));
                    DB.SaveChanges();

                    DB.Packing.RemoveRange(DB.Packing.Where(x => x.PackingId == PackingId));
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
        public int ConsultarPackingListxPallet(string Orden)
        {
            List<NumeroPallet> lst = new List<NumeroPallet>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var ListadoCabecera = (from d in DB.PackingListCabecera
                                           where d.Orden == Orden
                                           orderby d.PackingListId descending
                                           select new
                                           {
                                               d.Orden,
                                               d.PalletNumber,
                                               d.FechaRegistro
                                           }).FirstOrDefault();


                    int pallet = (ListadoCabecera.PalletNumber.Value)+1;
                 
                    return pallet;

                }
                catch (Exception ex){
                    Console.WriteLine(ex);
                    return 1;
                }

            }
            
        }
   

        public List<PackingListDetalle> ConsultarPackingListDetalle(int PackingListId)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoDetalle = DB.PackingListDetalle.ToList();
                return ListadoDetalle;
            }
        }
        public List<Paises> ConsultarPaises()
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoDetalle = DB.Paises.ToList();
                return ListadoDetalle;
            }
        }
        public List<PaisOrigen> ConsultarPaisOrigen()
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoDetalle = DB.PaisOrigen.ToList();
                return ListadoDetalle;
            }
        }
        public List<DetalleGeneralDePackingListcs> ConsultarDetalleGeneralPackingList(int PackingId)
        {

            List<DetalleGeneralDePackingListcs> lst = new List<DetalleGeneralDePackingListcs>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoDetalle = from d in DB.DetalleGeneralPackingList
                                     where d.PackingId == PackingId
                                     select new {
                                         d.ClientePackingList,
                                         d.ContenedorPackingList,
                                         d.FechaPackingList,
                                         d.ReservaPackingList,
                                         d.FacturaPedido,
                                         d.PedidoPackingList,
                                         d.EmbarcacionPackingList,
                                         d.IntercambioEirPackingList,
                                         d.ReferenciasPackingList,
                                         d.ProductosPackingList
                                     };

                foreach (var x in ListadoDetalle) {
                    DateTime fechaDoc = Convert.ToDateTime(x.FechaPackingList, CultureInfo.InvariantCulture);
                    string fechaDocumento = fechaDoc.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                    lst.Add(new DetalleGeneralDePackingListcs
                    {
                        ClientePackingList=x.ClientePackingList,
                        ContenedorPackingList=x.ContenedorPackingList,
                        FechaDePackingList = fechaDocumento,
                        ReservaPackingList=x.ReservaPackingList,
                        FacturaPedido=x.FacturaPedido,
                        PedidoPackingList=x.PedidoPackingList,
                        EmbarcacionPackingList=x.EmbarcacionPackingList,
                        IntercambioEirPackingList=x.IntercambioEirPackingList,
                        ReferenciasPackingList=x.ReferenciasPackingList,
                        ProductosPackingList=x.ProductosPackingList
                    });
                }
                                    
                                    ;
                return lst;
            }
        }
        public int IngresarPackingListCabecera(int PalletNumber, decimal GrossWeight, decimal NetWeight, string Customer, string Orden, string Origen, string Destino, decimal Volumen,string codigoQr)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var packingList = new PackingListCabecera();
                    packingList.PalletNumber = PalletNumber;
                    packingList.GrossWeight = GrossWeight;
                    packingList.NetWeight = NetWeight;
                    packingList.Customer = Customer;
                    packingList.Orden = Orden;
                    packingList.Origen = Origen;
                    packingList.Destino = Destino;
                    packingList.Volumen = Volumen;
                    packingList.FechaRegistro = DateTime.Now;
                    packingList.CodigoQr = codigoQr;

                    DB.PackingListCabecera.Add(packingList);
                    DB.SaveChanges();

                    int plId = packingList.PackingListId;
                    return plId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public bool IngresarPackingListDetalle(int PackingListId, string Descripcion, int Cantidad)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var packingListDetalle = new PackingListDetalle();

                    packingListDetalle.PackingListId = PackingListId;
                    packingListDetalle.Descripcion = Descripcion;
                    packingListDetalle.Cantidad = Cantidad;

                    DB.PackingListDetalle.Add(packingListDetalle);
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


        public bool IngresarDetalleGeneralPackingList(int PackingId,string Cliente, string Contenedor, DateTime fecha, string Reserva,string Factura, string Pedido,string Embarcacion, string IntercambioEir, string Referencias, string Productos)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var packingListDetalle = new DetalleGeneralPackingList();

                    packingListDetalle.PackingId = PackingId;
                    packingListDetalle.ClientePackingList= Cliente;
                    packingListDetalle.ContenedorPackingList = Contenedor;
                    packingListDetalle.FechaPackingList = fecha;
                    packingListDetalle.ReservaPackingList = Reserva;
                    packingListDetalle.FacturaPedido = Factura;
                    packingListDetalle.PedidoPackingList = Pedido;
                    packingListDetalle.EmbarcacionPackingList = Embarcacion;
                    packingListDetalle.IntercambioEirPackingList = IntercambioEir;
                    packingListDetalle.ReferenciasPackingList = Referencias;
                    packingListDetalle.ProductosPackingList = Productos;

                    DB.DetalleGeneralPackingList.Add(packingListDetalle);

                    var query = (from d in DB.Packing
                                 where d.PackingId == PackingId
                                 select d).FirstOrDefault();

                    query.DetalleIngresado = "SI";
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

        public List<PalletPacking> ConsultarPalletPackingCompleto(int PackingId)
        {
            List<PalletPacking> lst = new List<PalletPacking>();

            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoDetallePallet = (from d in DB.PalletPacking
                                            where d.PackingId == PackingId
                                            select new
                                            {
                                               d.PalletPacking1,
                                               d.PalletNumber,
                                               d.LargoPallet,
                                               d.AltoPallet,
                                               d.AnchoPallet,
                                               d.VolumenPallet,
                                               d.PesoBruto,
                                               d.PesoNeto
                                            });

                foreach (var x in ListadoDetallePallet)
                {
                    lst.Add(new PalletPacking
                    {
                        PalletPacking1 = x.PalletPacking1,
                        PalletNumber = x.PalletNumber,
                        LargoPallet = x.LargoPallet,
                        AltoPallet = x.AltoPallet,
                        AnchoPallet = x.AnchoPallet,
                        VolumenPallet = x.VolumenPallet,
                        PesoNeto = x.PesoNeto,
                        PesoBruto = x.PesoBruto
                    });
                }

                return lst;
            }
        }

        public List<PalletPackingDetalle> ConsultarPalletPalletDetalleItemsCompleto(int PackingId, int PalletgDetalleId)
        {
            List<PalletPackingDetalle> lst = new List<PalletPackingDetalle>();

            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoDetallePallet = (from d in DB.PalletPackingDetalle
                                            where d.PackingId == PackingId && d.PalletPacking== PalletgDetalleId
                                            select new
                                            {
                                                d.DescriptionCode,
                                                d.CantidadItem,
                                                d.ItemCode                               
                                            });

                foreach (var x in ListadoDetallePallet)
                {
                    lst.Add(new PalletPackingDetalle
                    {
                       ItemCode=x.ItemCode,
                       DescriptionCode=x.DescriptionCode,
                       CantidadItem=x.CantidadItem
                    });
                }

                return lst;
            }
        }

        public string ConsultarUrlImprimirPalletPdf()
        {
            string result="";
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                
                    var NombreForaneo = (from d in DB.UrlImprimirPdfPallet
                                         select new
                                         {
                                             d.DireccionUrl
                                         }).FirstOrDefault();
                result = NombreForaneo.DireccionUrl;


                        return result;    
            }
        }
    }
}