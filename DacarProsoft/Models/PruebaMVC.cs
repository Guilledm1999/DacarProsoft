using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class EPruebaMVC
    {
        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public string Correo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string Estado { get; set; }
        public string Genero { get; set; }
        public int Provincia { get; set; }
        public string ProvinciaDes { get; set; }

    }

    public class EProvincia
    {
        public int idProvincia { get; set; }
        public string Descripcion { get; set; }
       
    }

    public class EPieChart
    {
        public string Descripcion { get; set; }
        public float Cantidad { get; set; }
    }

}