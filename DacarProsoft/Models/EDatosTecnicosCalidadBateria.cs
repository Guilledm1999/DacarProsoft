using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class EDatosTecnicosCalidadBateria
    {
        public int DatosTecnicosCalidadBateriasId { get; set; }
        public string Modelo { get; set; }
        public string CAP { get; set; }
        public string RC { get; set; }
        public string CCA { get; set; }
        public string CACeroGrados { get; set; }
        public string CCAMenosDiescochoExpo { get; set; }
        public string C20xDiseno { get; set; }
        public string CapResxDiseno { get; set; }
        public string CCAxDisenoSeparadorFibra { get; set; }
        public string CAxDisenoSeparadorFibra { get; set; }
        public string HCAxDisenoSeparadorFibra { get; set; }
        public string CCAxDisenoSeparadorPE { get; set; }
        public string CAxDisenoSeparadorPE { get; set; }
        public string HCAxDisenoSeparadorPE { get; set; }
        public string CantPlacas { get; set; }
        public string PesoSellada { get; set; }
        public string PesoHumedaKg { get; set; }
        public string Linea { get; set; }
        public string C100 { get; set; }
        public string C10 { get; set; }
        public string C5 { get; set; }
    }
}