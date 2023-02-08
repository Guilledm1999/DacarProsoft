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
    public class DaoPruebaMVC
    {
        /*
       public List<EPruebaMVC> Datos()
       {

           List<EPruebaMVC> lst = new List<EPruebaMVC>();
           using (DacarProsoftEntities DB = new DacarProsoftEntities())
           {

               var list3 = (from Item1 in DB.PruebaMVC
                            join Item2 in DB.ProvinciasEcuador
                            on Item1.Provincia equals Item2.id // join on some property
                            where Item1.Estado =="A"
                            select new EPruebaMVC { 
                            IdPersona = Item1.idPersona,
                            Nombre = Item1.Nombre.Trim(),
                            Cedula = Item1.Cedula.Trim(),
                            Correo = Item1.Correo.Trim(),
                            FechaIngreso = Item1.FechaIngreso,
                            Genero = Item1.Genero,
                            Provincia = Item1.Provincia,
                            ProvinciaDes = Item2.provincia,
                            }).ToList();
               return list3;

           }

       }

       public List<EProvincia> Provincias()
       {
          List <EProvincia> poObject = new List<EProvincia>();


           using (DacarProsoftEntities DB = new DacarProsoftEntities())
           {

               poObject = (from Item1 in DB.ProvinciasEcuador
                            select new EProvincia
                            {
                             idProvincia = Item1.id,
                             Descripcion = Item1.provincia
                            }).ToList();
               return poObject;

           }
       }



       public List<EPieChart> GenerarGeneros()
           {
           List<EPieChart> poObject = new List <EPieChart>();

           using (DacarProsoftEntities DB = new DacarProsoftEntities())
           {


               poObject = DB.PruebaMVC.Where(a => a.Estado =="A").GroupBy(n => n.Genero)
                        .Select(n => new EPieChart
                        {
                            Descripcion = n.Key,
                            Cantidad = n.Count()
                        })
                        .OrderBy(n => n.Descripcion).ToList();
               return poObject;

           }
       }

       public bool Guardar(String nombre, String cedula, String correo, String genero, int provincia )
       {
           using (DacarProsoftEntities DB = new DacarProsoftEntities())
           {
               try
               {
                   var result = new PruebaMVC();
                   if (nombre != null)
                   {
                       result.Nombre = nombre;
                   }
                   if (cedula != null)
                   {
                       result.Cedula = cedula;
                   }
                   if (correo != null)
                   {
                       result.Correo = correo;
                   }
                   if (genero != null)
                   {
                       result.Genero = genero;
                   }
                   if (provincia > 0)
                   {
                       result.Provincia = provincia;
                   }



                   result.FechaIngreso = DateTime.Now;
                   result.Estado = "A"; 

                   DB.PruebaMVC.Add(result);
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

       public bool Actualizar(int IdPersona, String nombre, String cedula, String correo, String genero, int provincia)
       {
           using (DacarProsoftEntities DB = new DacarProsoftEntities())
           {
               try
               {
                   var result = (from a in DB.PruebaMVC
                                where a.idPersona == IdPersona && a.Estado == "A"
                                select a).FirstOrDefault();


                   if (nombre != null)
                   {
                       result.Nombre = nombre;
                   }
                   if (cedula != null)
                   {
                       result.Cedula = cedula;
                   }
                   if (correo != null)
                   {
                       result.Correo = correo;
                   }
                   if (genero != null)
                   {
                       result.Genero = genero;
                   }
                   if (provincia > 0)
                   {
                       result.Provincia = provincia;
                   }
                   result.FechaActualizacion = DateTime.Now;


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

       public bool Eliminar(int PoId)
       {
           using (DacarProsoftEntities DB = new DacarProsoftEntities())
           {
               try
               {
                   var query = (from a in DB.PruebaMVC
                                where a.idPersona == PoId && a.Estado == "A"
                                select a).FirstOrDefault();

                   query.Estado = "E";

                   query.FechaActualizacion = DateTime.Now;

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


       public List<SelectListItem> CargarProvincia()
       {

           List<SelectListItem> lst = new List<SelectListItem>();
           using (DacarProsoftEntities DB = new DacarProsoftEntities())
           {

               var Listado = (from d in DB.ProvinciasEcuador
                              select new
                              {
                                  d.id,
                                  d.provincia

                              }).ToList();
               foreach (var x in Listado)
               {
                   lst.Add(new SelectListItem()
                   {
                       Value = x.id.ToString(),
                         Text = x.provincia,
                   });
               }
               return lst;

           }
           */
    }

}
