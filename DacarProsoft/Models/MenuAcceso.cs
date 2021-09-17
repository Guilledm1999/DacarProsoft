using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class MenuAcceso
    {
        public int MenuId { set; get; }
        public int Estado { set; get; }
        public int MenuOpcionesId { set; get; }
        public string Descripcion { set; get; }
        public int EstadoMenu { set; get; }

    }
}