using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Models
{
    public class EventoMesCalendario
    {

        public int idEvento { get; set; }
        public int groupId { get; set; }
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string descripcion { get; set; }
        public string color { get; set; }
        public string textColor { get; set; }
        public string fechaPedido { get; set; }
        public string fechaDespacho { get; set; }
        public string fechaZarpe { get; set; }
        public string destino { get; set; }
        public int Booking { get; set; }
        public string BookinText { get; set; }
        public string cardCode { get; set; }


    }
}