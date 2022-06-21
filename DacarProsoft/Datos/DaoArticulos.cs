using DacarDatos.Datos;
using DacarProsoft.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DacarProsoft.Datos
{
    public class DaoArticulos
    {
        public Conexion conexion = new Conexion();

        public DataTable consultarArticulos()
        {

            DataTable dt = new DataTable();
            try
            {
                //ME CONECTO
                conexion.Conectar();
                using (conexion.conn)
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = CommandType.Text;
                    comando.CommandText = "SELECT * FROM[SBODACARPROD].[dbo].[vConsultaArticulos]";
                    comando.Connection = conexion.conn;
                    //CREO EL ADAPTADOR
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = comando;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dt;
        }

        public List<vConsultaArticulos> ConsultarListaArticulos()
        {

            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                var ListadoArticulos = DB.vConsultaArticulos.ToList();
                return ListadoArticulos;
            }

        }
        public List<SelectListItem> ConsultarCategoria()
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
          
                lst = (from d in DB.vConsultaArticulos
                       select new SelectListItem
                       {
                           Value = d.Categoria,
                           Text = d.Categoria
                       }).Distinct().ToList();

                return lst;
            }

        }

        public List<vConsultaArticulos> ListadoArticulosPorCategoria(String Categoria,String SubCategoria)
        {
            List<vConsultaArticulos> lst = new List<vConsultaArticulos>();
            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {

               var ListadoArticulos = from d in DB.vConsultaArticulos
                       where d.Categoria == Categoria && d.SubCategoria == SubCategoria
                      select new
                      {
                         d.Codigo,
                         d.Descripcion,
                         d.Categoria,
                         d.SubCategoria
                      };

                foreach (var x in ListadoArticulos)
                {
                    lst.Add(new vConsultaArticulos
                    {
                        Codigo = x.Codigo,
                        Descripcion = x.Descripcion,
                        Categoria = x.Categoria,
                        SubCategoria = x.SubCategoria
                    });
                }
                return lst;
            }

        }

        //public bool ingresarArticulo(int ClienteId, String CodigoArticulo, String DescripcionArticulo, String CategoriaArticulo, String SubCategoriaArticulo, int CantidadArticulo)
        //{
        //    using (DacarProsoftEntities DB = new DacarProsoftEntities())
        //    {
        //        var articulos = new ArticulosPedidos();
        //         articulos.ClienteId= ClienteId;
        //         articulos.CodigoArticulo= CodigoArticulo;
        //         articulos.DescripcionArticulo= DescripcionArticulo;
        //         articulos.Categoria= CategoriaArticulo;
        //         articulos.SubCategoria= SubCategoriaArticulo;
        //         articulos.Cantidad= CantidadArticulo;
               
        //        DB.ArticulosPedidos.Add(articulos);
        //        DB.SaveChanges();
        //        return true;
        //    }

        //}
        //public List<ArticulosPedidos> ConsultarListaArticulosDePedidos()
        //{

        //    using (DacarProsoftEntities DB = new DacarProsoftEntities())
        //    {
        //        var ListadoArticulos = DB.ArticulosPedidos.ToList();
        //        return ListadoArticulos;
        //    }

        //}
        public List<PesoPrecioArticulo> ConsultarPesos()
        {
            List<Modelos> lst = new List<Modelos>();

            List<PesoPrecioArticulo> lst2 = new List<PesoPrecioArticulo>();

            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                var ListadoModelos = from d in DB.OITM
                                     where d.ItemName.StartsWith("CHA-")
                                     orderby d.ItemCode ascending
                                     select new
                                     {
                                         d.ItemCode,
                                         d.ItemName,
                                         d.SWeight1,
                                         d.U_DAC_PESOPRODUCTO
                                     };

                foreach (var x in ListadoModelos)
                {
                    decimal val = 0;
                    if (x.U_DAC_PESOPRODUCTO == null)
                    {
                        val = 1;
                    }
                    else
                    {
                        val = x.U_DAC_PESOPRODUCTO.Value;
                    }
                    lst2.Add(new PesoPrecioArticulo
                    {
                        CodigoItem = x.ItemCode,
                        ItemName = x.ItemName,
                        PesoArticulo = Decimal.Round(val,2),
                    });
                }
                return lst2;
            }
        }

    }
}