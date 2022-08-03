using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class MaestrosGenerales
    {
        public int MaestrosUtilitariosId { set; get; }
        public String Descripcion { set; get; }
        public String Valor { set; get; }
        public String fechaCreacion { set; get; }
        public String fechaActualizacion { set; get; }
        public bool estado { set; get; }
    }
}