using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class AccesosMenu
    {
        public int idAcceso { set; get; }
        public string TipoUsuario { set; get; }
        public string MenuDescr { set; get; }
        public string EstadoMenu { set; get; }
    }
}