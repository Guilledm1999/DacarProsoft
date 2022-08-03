using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class ReporteChatarraConDesviacion
    {
        public Nullable<int> Id { get; set; }

        public Nullable<int> N_Documento { get; set; }
        public string Pedido { get; set; }
        public string Identificador { get; set; }
        public string Cliente { get; set; }
        public string Tipo_Cliente { get; set; }
        public string Cliente_Linea { get; set; }
        public string Cliente_Clase { get; set; }
        public string Tipo_Ingreso { get; set; }
        public Nullable<decimal> Cantidad { get; set; }
        public Nullable<decimal> Peso_Teorico { get; set; }
        public Nullable<decimal> Peso_Real { get; set; }
        public Nullable<decimal> Precio { get; set; }

        public Nullable<decimal> Desviacion { get; set; }
        public string Bodega { get; set; }
        public string Vendedor { get; set; }
        public string Comentarios { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string FechaRegistro { get; set; }
        public int DocEntry { get; set; }
        public int FechaRegistro2 { get; set; }


    }
}