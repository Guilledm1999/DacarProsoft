using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class MatrizInventarioAlmacenes
    {
        public int Id { set; get; }
        public string Polaridad { set; get; }
        public string Modelo { set; get; }
        public string Referencia { set; get; }
        public int DacarStDanin { set; get; }
        public int DacarBpDanin { set; get; }
        public int DacarTxDanin { set; get; }
        public int TotalDanin { set; get; }
        public int DacarStTanca { set; get; }
        public int DacarBpTanca { set; get; }
        public int DacarTxTanca { set; get; }
        public int TotalTanca { set; get; }
        public int DacarStRendon { set; get; }
        public int DacarBpRendon { set; get; }
        public int DacarTxRendon { set; get; }
        public int TotalRendon { set; get; }
        public int DacarStSambo { set; get; }
        public int DacarBpSambo { set; get; }
        public int DacarTxSambo { set; get; }
        public int TotalSambo { set; get; }
        public int DacarStQuito { set; get; }
        public int DacarBpQuito { set; get; }
        public int DacarTxQuito { set; get; }
        public int TotalQuito { set; get; }
        public string LineaReferencia { set; get; }

    }

    public class MatrizInventarioPlanta
    {

        public string Modelo { set; get; }
        public string Referencia { set; get; }
        public string Polaridad { set; get; }
        public int DacarStSelladas { set; get; }
        public int DacarBpSelladas { set; get; }
        public int DacarTxSelladas { set; get; }
        public int TeknoSelladas { set; get; }
        public int KaiserSelladas { set; get; }
        public int TotalSelladas { set; get; }
        public int DacarStCarga { set; get; }
        public int DacarBpCarga { set; get; }
        public int DacarTxCarga { set; get; }
        public int TeknoCarga { set; get; }
        public int KaiserCarga { set; get; }
        public int TotalCarga { set; get; }
        public int DacarStCd { set; get; }
        public int DacarBpCd { set; get; }
        public int DacarTxCd { set; get; }
        public int TeknoCd { set; get; }
        public int KaiserCd { set; get; }
        public int TotalCd { set; get; }
        public int Magnum { set; get; }
        public int Linea { set; get; }
        public string LineaReferencia { set; get; }
        public int Devolucion { set; get; }


    }
}