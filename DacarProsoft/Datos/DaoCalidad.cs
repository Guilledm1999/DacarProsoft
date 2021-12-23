using DacarDatos.Datos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DacarProsoft.Datos
{
    public class DaoCalidad
    {
        public List<SelectListItem> ConsultarBaseDeDatos()
        {
            List<SelectListItem> lst = new List<SelectListItem>();

            string ruta= ObtenerRutaAccess();

            DirectoryInfo Dir2 = new DirectoryInfo(ruta);

            foreach (var file in Dir2.GetFiles("*", SearchOption.AllDirectories))
            {
                lst.Add(new SelectListItem
                {
                    Value = file.Name,
                    Text= System.IO.Path.GetFileNameWithoutExtension(file.Name)
                });

            }
                return lst;
        }

        public string ObtenerRutaAccess()
        {
            string result = "";
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var Listado = (from d in DB.RutaBasesAccessCalidad
                                   select new
                                   {
                                       d.RutaFisica
                                   }).FirstOrDefault();


                    result = Listado.RutaFisica;

                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return result;
                }
            }
        }
    }
}