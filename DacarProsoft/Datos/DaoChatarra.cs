using DacarDatos.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DacarProsoft.Datos
{
    public class DaoChatarra
    {
        public List<TipoDocumento> ConsultarTipoDocumento()
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoTipoDocumento = DB.TipoDocumento.ToList();
                return ListadoTipoDocumento;
            }
        }

        public List<TipoIngreso> ConsultarTipoIngreso()
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoTipoIngreso = DB.TipoIngreso.ToList();
                return ListadoTipoIngreso;
            }
        }
        public List<Chatarra> ConsultarChatarra()
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoChatarra = DB.Chatarra.ToList();
                return ListadoChatarra;
            }
        }
        public List<ChatarraDetalle> ConsultarChatarraDetalle()
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoChatarraDetalle = DB.ChatarraDetalle.ToList();
                return ListadoChatarraDetalle;
            }
        }
        public List<Modelos> ConsultarModelos()
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoModelos = DB.Modelos.ToList();
                return ListadoModelos;
            }
        }
        public List<Bodega> ConsultarBodega()
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoBodega = DB.Bodega.ToList();
                return ListadoBodega;
            }
        }
        public List<Marcas> ConsultarMarcas()
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoMarcas = DB.Marcas.ToList();
                return ListadoMarcas;
            }
        }
        public List<Vehiculos> ConsultarVehiculos()
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoVehiculos = DB.Vehiculos.ToList();
                return ListadoVehiculos;
            }
        }
        public List<Vendedores> ConsultarVendedor()
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoVendedores = DB.Vendedores.ToList();
                return ListadoVendedores;
            }
        }

      

    }
}
