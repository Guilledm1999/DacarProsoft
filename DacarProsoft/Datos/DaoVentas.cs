using DacarDatos.Datos;
using DacarProsoft.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DacarProsoft.Datos
{
    public class DaoVentas
    {

        public List<Ventas> ListadoVentasPorAnio(String anio)
        {
            List<Ventas> lst = new List<Ventas>();
            if (anio == "--Seleccione--") {
                return null;
            }
            int ano = int.Parse(anio);
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {

                var ListadoVentas = from d in DB.vReporteGeneralVentasJO
                                       where d.Anio == ano orderby d.Secuencia descending
                                    select new
                                       {
                                           d.Secuencia,
                                           d.Cuenta,
                                           d.Nombre_Cuenta,
                                           d.Anio,
                                           d.Mes,
                                           d.Fecha,
                                           d.Mercado,
                                           d.Vendedor,
                                           d.Cliente,
                                           d.Clie_tipo,
                                           d.Cliente_Linea,
                                           d.Cliente_Clase,
                                           d.prod_linea,
                                           d.Prod_clase,
                                           d.Prod_grupo,
                                           d.Producto,
                                           d.Marca,
                                           d.ModeloBC,
                                           d.Cantidad,
                                           d.Precio,
                                           d.Parcial,
                                           d.porc_desc,
                                           d.Descuento,
                                           d.base0,
                                           d.Subtotal,
                                           d.impuesto,
                                           d.Total,
                                           d.Costo_uni,
                                           d.Costo_tot,
                                           d.Peso_uni,
                                           d.Peso_tot,
                                           d.Tipo_de_Nc
                                       };

                foreach (var x in ListadoVentas)
                {
                    lst.Add(new Ventas
                    {
                        Secuencia = x.Secuencia,
                        Cuenta=x.Cuenta,
                        Nombre_Cuenta=x.Nombre_Cuenta,
                        Anio=x.Anio,
                        Mes=x.Mes,
                        Fecha=x.Fecha,
                        Mercado=x.Mercado,
                        Vendedor=x.Vendedor,
                        Cliente=x.Cliente,
                        Clie_tipo=x.Clie_tipo,
                        Cliente_Linea=x.Cliente_Linea,
                        Cliente_Clase=x.Cliente_Clase,
                        prod_linea=x.prod_linea,
                        Prod_clase=x.Prod_clase,
                        Prod_grupo=x.Prod_grupo,
                        Producto=x.Producto,
                        Marca=x.Marca,
                        ModeloBC=x.ModeloBC,
                        Cantidad=x.Cantidad,
                        Precio=x.Precio,
                        Parcial=x.Parcial,
                        porc_desc=x.porc_desc,
                        Descuento=x.Descuento,
                        base0=x.base0,
                        Subtotal=x.Subtotal,
                        impuesto=x.impuesto,
                        Total=x.Total,
                        //Costo_uni= Math.Round((double)x.Costo_uni,2),
                        Costo_uni = x.Costo_uni,
                        Costo_tot = x.Costo_tot,
                        Peso_uni=x.Peso_uni,
                        Peso_tot=x.Peso_tot,
                        Tipo_de_Nc=x.Tipo_de_Nc
                    });
                }
                return lst;
            }





        }

        public List<vReporteGeneralVentasJO> ListadoVentasTotales()
        {

            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                var ListadoVentas = DB.vReporteGeneralVentasJO.ToList();
                return ListadoVentas;
            }

        }

        public List<SelectListItem> ConsultarAniosVentas()
        {
            DateTime fechaActual = DateTime.Now;

            List<SelectListItem> anios = new List<SelectListItem>();
            int ano = Convert.ToInt32(fechaActual.Year);
            int i = 0;

            for (i=0;i<=3;i++) {
                anios.Add(new SelectListItem() { Text = Convert.ToString(ano - i), Value = Convert.ToString(ano - i) });

            }
            return anios;

        }

        public List<Meses> ConsultarMesesVentas()
        {

            List<Meses> lst = new List<Meses>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                var ListadoMeses = from d in DB.Meses
                                   orderby d.Orden ascending
                                   select new
                                   {
                                       d.Nombre,
                                       d.Orden
                                   };

                foreach (var x in ListadoMeses)
                {
                    lst.Add(new Meses
                    {
                        Nombre = x.Nombre,
                        Orden = x.Orden
                     });
                }
                return lst;
            }

        }

        public List<Ventas> ListadoVentasPorMeses(String anio , String Mes)
        {
            List<Ventas> lst = new List<Ventas>();
            int ano = Convert.ToInt32(anio);

            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {

                     var ListadoVentas = from d in DB.vReporteGeneralVentasJO
                                        where d.Anio == ano && d.Mes == Mes
                                        select new
                                        {
                                            d.Secuencia,
                                            d.Cuenta,
                                            d.Nombre_Cuenta,
                                            d.Anio,
                                            d.Mes,
                                            d.Fecha,
                                            d.Mercado,
                                            d.Vendedor,
                                            d.Cliente,
                                            d.Clie_tipo,
                                            d.Cliente_Linea,
                                            d.Cliente_Clase,
                                            d.prod_linea,
                                            d.Prod_clase,
                                            d.Prod_grupo,
                                            d.Producto,
                                            d.Marca,
                                            d.ModeloBC,
                                            d.Cantidad,
                                            d.Precio,
                                            d.Parcial,
                                            d.porc_desc,
                                            d.Descuento,
                                            d.base0,
                                            d.Subtotal,
                                            d.impuesto,
                                            d.Total,
                                            d.Costo_uni,
                                            d.Costo_tot,
                                            d.Peso_uni,
                                            d.Peso_tot,
                                            d.Tipo_de_Nc
                                        };
                
                foreach (var x in ListadoVentas)
                {
                    lst.Add(new Ventas
                    {
                        Secuencia = x.Secuencia,
                        Cuenta = x.Cuenta,
                        Nombre_Cuenta = x.Nombre_Cuenta,
                        Anio = x.Anio,
                        Mes = x.Mes,
                        Fecha = x.Fecha,
                        Mercado = x.Mercado,
                        Vendedor = x.Vendedor,
                        Cliente = x.Cliente,
                        Clie_tipo = x.Clie_tipo,
                        Cliente_Linea = x.Cliente_Linea,
                        Cliente_Clase = x.Cliente_Clase,
                        prod_linea = x.prod_linea,
                        Prod_clase = x.Prod_clase,
                        Prod_grupo = x.Prod_grupo,
                        Producto = x.Producto,
                        Marca = x.Marca,
                        ModeloBC = x.ModeloBC,
                        Cantidad = x.Cantidad,
                        Precio = x.Precio,
                        Parcial = x.Parcial,
                        porc_desc = x.porc_desc,
                        Descuento = x.Descuento,
                        base0 = x.base0,
                        Subtotal = x.Subtotal,
                        impuesto = x.impuesto,
                        Total = x.Total,
                        Costo_uni = x.Costo_uni,
                        Costo_tot = x.Costo_tot,
                        Peso_uni = x.Peso_uni,
                        Peso_tot = x.Peso_tot,
                        Tipo_de_Nc = x.Tipo_de_Nc
                    });
                }
                return lst;
            }


        }
        public List<Ventas> ListadoVentasPorAnio(String anio, String Mes)
        {
            List<Ventas> lst = new List<Ventas>();
            int ano = Convert.ToInt32(anio);

            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {

                var ListadoVentas = from d in DB.vReporteGeneralVentasJO
                                    where d.Anio == ano
                                    select new
                                    {
                                        d.Secuencia,
                                        d.Cuenta,
                                        d.Nombre_Cuenta,
                                        d.Anio,
                                        d.Mes,
                                        d.Fecha,
                                        d.Mercado,
                                        d.Vendedor,
                                        d.Cliente,
                                        d.Clie_tipo,
                                        d.Cliente_Linea,
                                        d.Cliente_Clase,
                                        d.prod_linea,
                                        d.Prod_clase,
                                        d.Prod_grupo,
                                        d.Producto,
                                        d.Marca,
                                        d.ModeloBC,
                                        d.Cantidad,
                                        d.Precio,
                                        d.Parcial,
                                        d.porc_desc,
                                        d.Descuento,
                                        d.base0,
                                        d.Subtotal,
                                        d.impuesto,
                                        d.Total,
                                        d.Costo_uni,
                                        d.Costo_tot,
                                        d.Peso_uni,
                                        d.Peso_tot,
                                        d.Tipo_de_Nc
                                    };

                foreach (var x in ListadoVentas)
                {
                    lst.Add(new Ventas
                    {
                        Secuencia = x.Secuencia,
                        Cuenta = x.Cuenta,
                        Nombre_Cuenta = x.Nombre_Cuenta,
                        Anio = x.Anio,
                        Mes = x.Mes,
                        Fecha = x.Fecha,
                        Mercado = x.Mercado,
                        Vendedor = x.Vendedor,
                        Cliente = x.Cliente,
                        Clie_tipo = x.Clie_tipo,
                        Cliente_Linea = x.Cliente_Linea,
                        Cliente_Clase = x.Cliente_Clase,
                        prod_linea = x.prod_linea,
                        Prod_clase = x.Prod_clase,
                        Prod_grupo = x.Prod_grupo,
                        Producto = x.Producto,
                        Marca = x.Marca,
                        ModeloBC = x.ModeloBC,
                        Cantidad = x.Cantidad,
                        Precio = x.Precio,
                        Parcial = x.Parcial,
                        porc_desc = x.porc_desc,
                        Descuento = x.Descuento,
                        base0 = x.base0,
                        Subtotal = x.Subtotal,
                        impuesto = x.impuesto,
                        Total = x.Total,
                        Costo_uni = x.Costo_uni,
                        Costo_tot = x.Costo_tot,
                        Peso_uni = x.Peso_uni,
                        Peso_tot = x.Peso_tot,
                        Tipo_de_Nc = x.Tipo_de_Nc
                    });
                }
                return lst;
            }


        }

    }
}