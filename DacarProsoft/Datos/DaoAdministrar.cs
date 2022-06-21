using DacarDatos.Datos;
using System;
using System.Collections.Generic;
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
    }
}