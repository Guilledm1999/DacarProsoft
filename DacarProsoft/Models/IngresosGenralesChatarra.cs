using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class IngresosGenralesChatarra
    {
        public int NumeroDocumento { set; get; }
        public string Fecha { set; get; }
        public string Identificacion { set; get; }
        public string Cliente { set; get; }
        public string GroupName { set; get; }
        public string ClienteLinea { set; get; }
        public string ClienteClase { set; get; }
        public string TipoIngreso { set; get; }
        public string Comentarios { set; get; }
        public string BodegaId { set; get; }
        public string FechaRegistro { set; get; }
        public string MesRegistro { set; get; }
        public string Descripcion { set; get; }
        public int Cantidad { set; get; }
        public decimal PesoTeoricoTotalAjustado { set; get; }
        public decimal PesoTotalAjustado { set; get; }
        public decimal DiferenciaPesos { set; get; }


    }
}