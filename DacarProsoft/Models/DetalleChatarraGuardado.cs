using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class DetalleChatarraGuardado
    {
        public int ChatarraDetalleId { set; get; }
        public int ChatarraId { set; get; }
        public int DocEntry { set; get; }
        public string ItemCode { set; get; }
        public string Description { set; get; }
        public int Cantidad { set; get; }
        public decimal PesoTeoricoUnitario { set; get; }
        public decimal PesoTeoricoTotal { set; get; }
        public decimal PesoNetoTipo { set; get; }
        public decimal PesoTeoricoAjustado { set; get; }
        public decimal PesoTeoricoAjustadoTotal { set; get; }
        public decimal DesviacionIndividual { set; get; }
    }
}