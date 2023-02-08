using DacarDatos.Datos;
using DacarProsoft.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace DacarProsoft.Datos
{
    public class DaoAdministrar
    {
        public List<GenericosItem> ConsultarGenericosItem()
        {
            List<GenericosItem> lst = new List<GenericosItem>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = (from d in DB.GenericosItem
                               orderby d.FechaRegistro descending
                               select new
                               {
                                 d.GenericoItemId,
                                 d.GrupoGenericoItem,
                                 d.ModeloDacar,
                                 d.NumeroParteCliente,
                                 d.EtiquetaDatosTecnicos,
                                 d.Polaridad,
                                 d.TipoTerminal,
                                 d.CantidadPiso,
                                 d.PisoMaximo,
                                 d.BateriasPallet,
                                 d.PesoTara

                               }).ToList();

                foreach (var x in Listado.Distinct())
                {
                    lst.Add(new GenericosItem
                    {
                     GenericoItemId=x.GenericoItemId,
                     GrupoGenericoItem=x.GrupoGenericoItem,
                     ModeloDacar=x.ModeloDacar,
                     NumeroParteCliente=x.NumeroParteCliente,
                     EtiquetaDatosTecnicos=x.EtiquetaDatosTecnicos,
                     Polaridad=x.Polaridad,
                     TipoTerminal=x.TipoTerminal,
                     CantidadPiso=x.CantidadPiso,
                     PisoMaximo=x.PisoMaximo,
                     BateriasPallet=x.BateriasPallet,
                     PesoTara=x.PesoTara
                    });
                }
                return lst;
            }
        }

        public List<EDatosTecnicosCalidadBateria> ConsultarDatosTecnicosCalidadBateria()
        {
            List<EDatosTecnicosCalidadBateria> lst = new List<EDatosTecnicosCalidadBateria>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                 lst = (from d in DB.DatosTecnicosCalidadBaterias
                             select new EDatosTecnicosCalidadBateria
                             {
                               DatosTecnicosCalidadBateriasId=  d.DatosTecnicosCalidadBateriasId,
                               Modelo = d.Modelo,
                               CAP = d.CAP,
                               RC = d.RC,
                               CCA = d.CCA,
                               CACeroGrados= d.CACeroGrados,
                               CCAMenosDiescochoExpo= d.CCAMenosDiescochoExpo,
                               C20xDiseno = d.C20xDiseno,
                               CCAxDisenoSeparadorFibra = d.CCAxDisenoSeparadorFibra,
                               CAxDisenoSeparadorFibra = d.CAxDisenoSeparadorFibra,
                               HCAxDisenoSeparadorFibra =d.HCAxDisenoSeparadorFibra,
                               CCAxDisenoSeparadorPE = d.CCAxDisenoSeparadorPE,
                               CAxDisenoSeparadorPE = d.CAxDisenoSeparadorPE,
                               HCAxDisenoSeparadorPE = d.HCAxDisenoSeparadorPE,
                               CantPlacas = d.CantPlacas,
                                 CapResxDiseno = d.CapResxDiseno,
                               PesoSellada = d.PesoSellada,
                               PesoHumedaKg = d.PesoHumedaKg,
                               Linea = d.Linea,
                               C100 =d.C100,
                               C10 = d.C10,
                               C5 = d.C5
                                
                             }).ToList();

                return lst;
            }
        }



        public bool IngresarGenericoItem(string GrupoGenericoItem, string ModeloDacar, string NumeroParteCliente,string EtiquetaDatosTecnicos, string Polaridad, string TipoTerminal, int CantidadPiso,
            int PisoMaximo, int BateriasPallet, decimal PesoTara)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var result = new GenericosItem();
                    result.GrupoGenericoItem = GrupoGenericoItem;
                    result.ModeloDacar = ModeloDacar;
                    result.NumeroParteCliente = NumeroParteCliente;
                    result.EtiquetaDatosTecnicos = EtiquetaDatosTecnicos;
                    result.Polaridad = Polaridad;
                    result.TipoTerminal = TipoTerminal;
                    result.CantidadPiso = CantidadPiso;
                    result.PisoMaximo = PisoMaximo;
                    result.BateriasPallet = BateriasPallet;
                    result.PesoTara = PesoTara;
                    result.FechaRegistro = DateTime.Now;

                    DB.GenericosItem.Add(result);
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

        public bool IngresarDatosTecnicosCalidadBateria(EDatosTecnicosCalidadBateria PoObject)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var result = new DatosTecnicosCalidadBaterias();
                    result.Modelo = PoObject.Modelo;
                    result.CAP = PoObject.CAP;
                    result.RC = PoObject.RC;
                    result.CCA = PoObject.CCA;
                    result.CACeroGrados = PoObject.CACeroGrados;
                    result.C20xDiseno = PoObject.C20xDiseno;
                    result.CapResxDiseno = PoObject.CapResxDiseno;
                    result.CCAMenosDiescochoExpo = PoObject.CCAMenosDiescochoExpo;
                    result.CCAxDisenoSeparadorFibra = PoObject.CCAxDisenoSeparadorFibra;
                    result.CAxDisenoSeparadorFibra = PoObject.CAxDisenoSeparadorFibra;
                    result.HCAxDisenoSeparadorFibra = PoObject.HCAxDisenoSeparadorFibra;
                    result.CAxDisenoSeparadorPE = PoObject.CAxDisenoSeparadorPE;
                    result.CCAxDisenoSeparadorPE = PoObject.CCAxDisenoSeparadorPE;
                    result.HCAxDisenoSeparadorPE = PoObject.HCAxDisenoSeparadorPE;
                    result.CantPlacas = PoObject.CantPlacas;
                    result.PesoSellada = PoObject.PesoSellada;
                    result.PesoHumedaKg = PoObject.PesoHumedaKg;
                    result.Linea = PoObject.Linea;
                    result.C100 = PoObject.C100;
                    result.C10 = PoObject.C10;
                    result.C5 = PoObject.C5;

                    DB.DatosTecnicosCalidadBaterias.Add(result);
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
        //public bool ActualizarGenericoItem(int GenericoItemId,string GrupoGenericoItem, string ModeloDacar, string NumeroParteCliente, string EtiquetaDatosTecnicos, string Polaridad, string TipoTerminal, int CantidadPiso,
        //    int PisoMaximo, int BateriasPallet, decimal PesoTara)
        //{
        public bool ActualizarGenericoItem(GenericosItem generico, int Key)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var result = (from a in DB.GenericosItem
                                                  where a.GenericoItemId == Key
                                  select a).FirstOrDefault();

                    //generico.GrupoGenericoItem, generico.ModeloDacar, generico.NumeroParteCliente, generico.EtiquetaDatosTecnicos, generico.Polaridad, generico.TipoTerminal, generico.CantidadPiso.Value,
                    //generico.PisoMaximo.Value, generico.BateriasPallet.Value, generico.PesoTara.Value
                    if (generico.GrupoGenericoItem!=null) {
                        result.GrupoGenericoItem = generico.GrupoGenericoItem;
                    }
                    if (generico.ModeloDacar != null)
                    {
                        result.ModeloDacar = generico.ModeloDacar;
                    }
                    if (generico.NumeroParteCliente != null)
                    {
                        result.NumeroParteCliente = generico.NumeroParteCliente;
                    }
                    if (generico.EtiquetaDatosTecnicos != null)
                    {
                        result.EtiquetaDatosTecnicos = generico.EtiquetaDatosTecnicos;
                    }
                    if (generico.Polaridad != null)
                    {
                        result.Polaridad = generico.Polaridad;
                    }
                    if (generico.TipoTerminal != null)
                    {
                        result.TipoTerminal = generico.TipoTerminal;
                    }
                    if (generico.CantidadPiso != null)
                    {
                        result.CantidadPiso = generico.CantidadPiso;
                    }
                    if (generico.PisoMaximo != null)
                    {
                        result.PisoMaximo = generico.PisoMaximo;
                    }
                    if (generico.BateriasPallet != null)
                    {
                        result.BateriasPallet = generico.BateriasPallet;
                    }
                    if (generico.PesoTara != null)
                    {
                        result.PesoTara = generico.PesoTara;
                    }

                    result.FechaRegistro = DateTime.Now;

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
        public bool ActualizarDatosTecnicosCalidadBateria(EDatosTecnicosCalidadBateria generico, int Key)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var result = (from a in DB.DatosTecnicosCalidadBaterias
                                  where a.DatosTecnicosCalidadBateriasId == Key
                                  select a).FirstOrDefault();

                    if (generico.Modelo != null)
                    {
                        result.Modelo = generico.Modelo;
                    }
                    if (generico.CAP != null)
                    {
                        result.CAP = generico.CAP;
                    }
                    if (generico.C5 != null)
                    {
                        result.C5 = generico.C5;
                    }
                    if (generico.C10 != null)
                    {
                        result.C10 = generico.C10;
                    }
                    if (generico.C100 != null)
                    {
                        result.C100 = generico.C100;
                    }
                    if (generico.RC != null)
                    {
                        result.RC = generico.RC;
                    }
                    if (generico.CCA != null)
                    {
                        result.CCA = generico.CCA;
                    }
                    if (generico.CACeroGrados != null)
                    {
                        result.CACeroGrados = generico.CACeroGrados;
                    }
                    if (generico.CCAMenosDiescochoExpo != null)
                    {
                        result.CCAMenosDiescochoExpo = generico.CCAMenosDiescochoExpo;
                    }
                    if (generico.C20xDiseno != null)
                    {
                        result.C20xDiseno = generico.C20xDiseno;
                    }
                    if (generico.CapResxDiseno != null)
                    {
                        result.CapResxDiseno = generico.CapResxDiseno;
                    }
                    if (generico.CCAxDisenoSeparadorFibra != null)
                    {
                        result.CCAxDisenoSeparadorFibra = generico.CCAxDisenoSeparadorFibra;
                    }
                    if (generico.CAxDisenoSeparadorFibra != null)
                    {
                        result.CAxDisenoSeparadorFibra = generico.CAxDisenoSeparadorFibra;
                    }
                    if (generico.HCAxDisenoSeparadorFibra != null)
                    {
                        result.HCAxDisenoSeparadorFibra = generico.HCAxDisenoSeparadorFibra;
                    }
                    if (generico.CantPlacas != null)
                    {
                        result.CantPlacas = generico.CantPlacas;
                    }
                    if (generico.PesoSellada != null)
                    {
                        result.PesoSellada = generico.PesoSellada;
                    }
                    if (generico.PesoHumedaKg != null)
                    {
                        result.PesoHumedaKg = generico.PesoHumedaKg;
                    }
                    if (generico.Linea != null)
                    {
                        result.Linea = generico.Linea;
                    }


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

        public bool EliminarGenericoItem(int GenericoItemId)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    DB.GenericosItem.RemoveRange(DB.GenericosItem.Where(x => x.GenericoItemId == GenericoItemId));
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

        public bool EliminarDatosTecnicosCalidadBateria(int GenericoItemId)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    DB.DatosTecnicosCalidadBaterias.RemoveRange(DB.DatosTecnicosCalidadBaterias.Where(x => x.DatosTecnicosCalidadBateriasId == GenericoItemId));
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
        public List<MaestrosGenerales> ConsultarMaestrosGenerales()
        {
            string fechaCreacion;
            string fechaActuaizacion;
            List<MaestrosGenerales> lst = new List<MaestrosGenerales>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = (from d in DB.MaestrosUtilitarios
                               orderby d.MaestrosUtilitariosId descending
                               select new
                               {
                                 d.MaestrosUtilitariosId,
                                 d.Descripcion,
                                 d.Valor,
                                 d.fechaCreacion,
                                 d.fechaActualizacion,
                                 d.estado

                               }).ToList();

                foreach (var x in Listado.Distinct())
                {
                    DateTime creacion = Convert.ToDateTime(x.fechaCreacion, CultureInfo.InvariantCulture);
                    DateTime actualizacion = Convert.ToDateTime(x.fechaActualizacion, CultureInfo.InvariantCulture);

                    fechaCreacion = creacion.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    fechaActuaizacion = actualizacion.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

                    lst.Add(new MaestrosGenerales
                    {
                        MaestrosUtilitariosId=x.MaestrosUtilitariosId,
                        Descripcion=x.Descripcion,
                        Valor=x.Valor,
                        fechaCreacion=fechaCreacion,
                        fechaActualizacion=fechaActuaizacion,
                        estado=x.estado.Value
                    });
                }
                return lst;
            }
        }
        public bool IngresarMaestroGeneral(string descripcion,string valor, bool estado)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var result = new MaestrosUtilitarios();
                    result.Descripcion = descripcion;
                    result.Valor = valor;
                    result.estado = estado;
                    result.fechaCreacion = DateTime.Now;
                    result.fechaActualizacion = DateTime.Now;

                    DB.MaestrosUtilitarios.Add(result);
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
        public bool ActualizarMaestroGeneral(MaestrosGenerales generico, int Key)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var result = (from a in DB.MaestrosUtilitarios
                                  where a.MaestrosUtilitariosId == Key
                                  select a).FirstOrDefault();

                   
                    if (generico.Descripcion != null)
                    {
                        result.Descripcion = generico.Descripcion;
                    }
                    if (generico.Valor != null)
                    {
                        result.Valor = generico.Valor;
                    }

                    result.estado = generico.estado;

                    result.fechaActualizacion = DateTime.Now;

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
        public bool EliminarMaestroGeneral(int MaestroGeneralId)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    DB.MaestrosUtilitarios.RemoveRange(DB.MaestrosUtilitarios.Where(x => x.MaestrosUtilitariosId == MaestroGeneralId));
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

        public List<HistoricoChatarra> ConsultarHistoricoChatarra()
        {         
            List<HistoricoChatarra> lst = new List<HistoricoChatarra>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = (from d in DB.HistoricoChatarra
                               orderby d.Mes ascending
                               select new
                               {
                                   d.HistoricoChatarraId,
                                   d.Anio,
                                   d.Mes,
                                   d.Cantidad,
                                   d.Peso,
                                   d.Precio,
                                   d.TipoIngreso
                               }).ToList();

                foreach (var x in Listado)
                {              
                    lst.Add(new HistoricoChatarra
                    {
                      HistoricoChatarraId=x.HistoricoChatarraId,
                      Anio=x.Anio,
                      Mes=x.Mes,
                      Cantidad=x.Cantidad,
                      Precio=x.Precio,
                      Peso=x.Peso,
                      TipoIngreso=x.TipoIngreso
                    });
                }
                return lst;
            }
        }
        public List<HistoricoChatarra> ConsultarHistoricoChatarraAnioAnterior(int anio)
        {
            List<HistoricoChatarra> lst = new List<HistoricoChatarra>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var Listado = (from d in DB.HistoricoChatarra
                               where d.Anio==anio && d.Mes <= DateTime.Now.Month
                               orderby d.Mes ascending
                               select new
                               {
                                   d.HistoricoChatarraId,
                                   d.Anio,
                                   d.Mes,
                                   d.Cantidad,
                                   d.Peso,
                                   d.Precio,
                                   d.TipoIngreso
                               }).ToList();

                foreach (var x in Listado)
                {
                    lst.Add(new HistoricoChatarra
                    {
                        HistoricoChatarraId = x.HistoricoChatarraId,
                        Anio = x.Anio,
                        Mes = x.Mes,
                        Cantidad = x.Cantidad,
                        Precio = x.Precio,
                        Peso = x.Peso,
                        TipoIngreso = x.TipoIngreso
                    });
                }
                return lst;
            }
        }
        public bool IngresarHistoricoChatarra(int mes, int cantidad, decimal peso, decimal precio, string tipoIngreso)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var result = new HistoricoChatarra();

                    result.Anio = (DateTime.Now.Year - 1);
                    result.Mes = mes;
                    result.Cantidad = cantidad;
                    result.Peso = peso;
                    result.Precio = precio;
                    result.TipoIngreso = tipoIngreso;

                    DB.HistoricoChatarra.Add(result);
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
        public bool ActualizarHistoricoChatarra(int identificador, int anio, int mes, int cantidad, decimal peso, decimal precio)
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var result = (from a in DB.HistoricoChatarra
                                  where a.HistoricoChatarraId == identificador && a.Mes==mes
                                  select a).FirstOrDefault();

                    result.Anio = anio;
                    result.Mes = mes;
                    result.Cantidad = cantidad;
                    result.Peso = peso;
                    result.Precio = precio;

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
        public bool EliminarHistoricoChatarra()
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    DB.HistoricoChatarra.RemoveRange(DB.HistoricoChatarra);
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