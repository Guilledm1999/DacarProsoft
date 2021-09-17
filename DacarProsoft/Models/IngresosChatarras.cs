using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class IngresosChatarras
    {
        public int DocEntry { set; get; }
        public int NumeroDocumento { set; get; }
        public string NumeroPedido { set; get; }
        public string FechaIngreso { set; get; }
        public string MesIngreso { set; get; }
        public string CedulaCliente { set; get; }
        public string NombreCliente { set; get; }
        public string GroupCode { set; get; }
        public int CardCode { set; get; }
        public string ClienteClase { set; get; }
        public string ClienteLinea { set; get; }
        public string TipoIngreso { set; get; }
        public string Comments { set; get; }
        public string Bodega { set; get; }
        public decimal PesoTeoricoTotalCalculado { set; get; }
        public decimal PesoBultoIngresado { set; get; }
        public int CantidadTotal { set; get; }
        public decimal PesoAjustadoTotal { set; get; }
        public string Desviacion { set; get; }
        public int ModoIngreso { set; get; }



    }
}