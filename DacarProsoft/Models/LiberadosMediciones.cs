using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class LiberadosMediciones
    {
        public int DocEntry { set; get; }
        public int PackingId { set; get; }
        public int MedicionId { set; get; }
        public int NumeroDocumento { set; get; }
        public string NumeroOrden { set; get; }
        public string NombreCliente { set; get; }
        //public string FechaLiberacion { set; get; }
        //public string FechaDocumento { set; get; }
        public int NumeroMedicion { set; get; }
        public string NumeroLote { set; get; }
        public string Modelo { set; get; }
        public decimal Voltaje { set; get; }
        public bool Nivel { set; get; }
        public bool Acabado { set; get; }
        public bool Limpieza { set; get; }
        public decimal CCA { set; get; }
        public string FechaMedicion { set; get; }
    }
}