using DacarDatos.Datos;
using DacarProsoft.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace DacarProsoft.Datos
{
    public class DaoGarantias
    {

        public List<GarantiaDetalle> ReporteGeneralPedidosExterior(DateTime FechaInicio, DateTime FechaFin)
        {
          
            DateTime nuevaFechaFin = FechaFin;
            nuevaFechaFin = nuevaFechaFin.AddDays(1);

            string fechaRegistro = null;

            List<GarantiaDetalle> lst = new List<GarantiaDetalle>();
                using (DacarProsoftEntities DB = new DacarProsoftEntities())
                {

                    var Listado = (from d in DB.IngresoGarantias
                                   where d.RegistroGarantia >= FechaInicio && d.RegistroGarantia <= nuevaFechaFin
                                   select new
                                   {
                                       d.IngresoGarantiaId,
                                       d.Cedula,
                                       d.Nombre,
                                       d.Apellido,
                                       d.Email,
                                       d.Distribuidor,
                                       d.Ciudad,
                                       d.ModeloBateria,
                                       d.NumeroBateria,
                                       d.NumeroGarantia,
                                       d.RegistroGarantia,

                                   });
                    foreach (var x in Listado)
                {
                    DateTime fecha = Convert.ToDateTime(x.RegistroGarantia, CultureInfo.InvariantCulture);
                    //FechaCargaLista = fechaCargaLista.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    fechaRegistro = fecha.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                    lst.Add(new GarantiaDetalle
                        {
                           IngresoGarantiaId=x.IngresoGarantiaId,
                           Cedula=x.Cedula,
                           Nombre=x.Nombre,
                           Apellido=x.Apellido,
                           Email=x.Email,
                           Distribuidor=x.Distribuidor,
                           Ciudad=x.Ciudad,
                           ModeloBateria=x.ModeloBateria,
                           NumeroBateria=x.NumeroBateria,
                           NumeroGarantia=x.NumeroGarantia,
                           RegistroGarantia= fechaRegistro

                    });
                    }
                    return lst;

                }

        }
    }
}