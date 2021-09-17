using DacarDatos.Datos;
using DacarProsoft.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DacarProsoft.Datos
{
    public class DaoCliente
    {

        public List<InformacionClienteSap> ConsultarListaClientes()
        {
            //Char estado = 'Y';
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                List<InformacionClienteSap> infocliente = new List<InformacionClienteSap>();

                var ListadoClientes = from p in DB.OCRD
                                      where p.validFor == "Y"
                                      orderby p.CardName 
                                      select new {
                    p.CardCode,
                    p.CardName,
                    p.Phone1,
                    p.Address,
                    p.Country,
                    p.County,
                    p.City,
                    p.E_Mail,
                    p.CreateDate
                }; 

                foreach (var x in ListadoClientes) {
                    infocliente.Add(new InformacionClienteSap {
                        Cedula=x.CardCode,
                        Nombre=x.CardName,
                        //Telefono=x.Phone1,
                        //Direccion=x.Address,
                        //Pais=x.Country,
                        //Provincia=x.County,
                        //Ciudad=x.City,
                        //Email=x.E_Mail,
                        //FechaInicio=x.CreateDate?? DateTime.Now
                    });
                }

                return infocliente;
            }

        }

        public List<InformacionClienteSap> ConsultarListadoClientes()
        {
            //Char estado = 'Y';
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                List<InformacionClienteSap> infocliente = new List<InformacionClienteSap>();

                var ListadoClientes = from p in DB.OCRD
                                      where p.CardType == "C"
                                      orderby p.CardName
                                      select new
                                      {
                                          p.CardCode,
                                          p.CardName,
                                                                            };

                foreach (var x in ListadoClientes)
                {
                    infocliente.Add(new InformacionClienteSap
                    {
                        Cedula = x.CardCode,
                        Nombre = x.CardName,
                    });
                }
                return infocliente;
            }

        }

        public List<TipoCliente> ConsultarTipoCliente()
        {
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoTipoCliente = DB.TipoCliente.ToList();
                return ListadoTipoCliente;
            }
        }

        //public List<Cliente> ConsultarCliente(String NombreCliente)
        //{

        //    List<Cliente> cliente = new List<Cliente>();
        //    using (DacarProsoftEntities DB = new DacarProsoftEntities())
        //    {
             
        //        var listadoClientes = from p in DB.Cliente
        //                               where p.NombreCliente == NombreCliente
        //                               select new
        //                               {
        //                                 p.ClienteId,
        //                                 p.NombreCliente
        //                               };

        //        foreach (var x in listadoClientes)
        //        {
        //            cliente.Add(new Cliente
        //            {
        //              ClienteId=x.ClienteId,
        //              NombreCliente=x.NombreCliente
        //            });
        //        }
        //        return cliente;
        //    }
        //}

        //public bool ingresarCliente(String NombreCliente, String Ruc, String Telefono, String Direccion,String Pais,String Provincia,String Ciudad,String Email, DateTime FechaInicio)
        //{
        //    using (DacarProsoftEntities DB = new DacarProsoftEntities())
        //    {
        //        try
        //        {
        //            var cliente = new Cliente();
        //            cliente.NombreCliente = NombreCliente;
        //            cliente.Ruc = Ruc;
        //            cliente.Telefono = Telefono;
        //            cliente.Direccion = Direccion;
        //            cliente.Pais = Pais;
        //            cliente.Provincia = Provincia;
        //            cliente.Ciudad = Ciudad;
        //            cliente.Email = Email;
        //            cliente.FechaInicio = FechaInicio;

        //            DB.Cliente.Add(cliente);
        //            DB.SaveChanges();
        //            return true;
        //        }
        //        catch (Exception ex){
        //            Console.WriteLine(ex.Message);
        //            return false;

        //        }
        //    }

        //}

        public List<SelectListItem> ClienteLinea()
        {
            List<SelectListItem> lst2 = new List<SelectListItem>();
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                var ListadoModelos = from d in DB.C_SYP_LINEASN
                                     select new
                                     {
                                         d.Code,
                                         d.Name
                                     };

                foreach (var x in ListadoModelos)
                {
                    string[] partes = (x.Name).Split(' ');
                    string subcadena = partes[1];
                    lst2.Add(new SelectListItem()
                    {
                        Text = subcadena,
                        Value = Convert.ToString(x.Code)
                    });
                }
                return lst2;
            }
        }
        public List<SelectListItem> ClienteClase()
        {
            List<SelectListItem> lst2 = new List<SelectListItem>();
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                var ListadoModelos = from d in DB.C_SYP_CLASESN
                                     select new
                                     {
                                         d.Code,
                                         d.Name
                                     };

                foreach (var x in ListadoModelos)
                {

                    string[] partes = (x.Name).Split(' ');

                    string subcadena = partes[1];

                    lst2.Add(new SelectListItem()
                    {
                        Text = subcadena,
                        Value = Convert.ToString(x.Code)
                    });
                }
                return lst2;
            }
        }

    }
}