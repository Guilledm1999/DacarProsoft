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
        public List<vConsultaArticulos> ConsultarListaABaterias()
        {

            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {
                var ListadoArticulos = DB.vConsultaArticulos.ToList();
                
                return ListadoArticulos;
            }

        }
        public List<MatrizInventarioPlanta> ConsultarListaBateriasPlanta()
        {
            var ListadoArticulos = new List<MatrizInventarioPlanta>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                ListadoArticulos = (from m in DB.MatrizInventarioBaterias
                                    join l in DB.LineasMarcasPropias on m.Linea equals l.Identificador
                                    where m.Estado =="A"
                                    select new MatrizInventarioPlanta()
                                    {   
                                        Modelo = m.Modelo,
                                        Referencia = m.Referencia,
                                        Polaridad= m.Polaridad,
                                        DacarStSelladas = 0,
                                        DacarBpSelladas = 0,
                                        DacarTxSelladas = 0,
                                        TeknoSelladas = 0,
                                        KaiserSelladas = 0,
                                        TotalSelladas = 0,
                                        DacarStCarga = 0,
                                        DacarBpCarga = 0,
                                        DacarTxCarga = 0,
                                        TeknoCarga = 0,
                                        KaiserCarga = 0,
                                        TotalCarga = 0,
                                        DacarStCd = 0,
                                        DacarBpCd = 0,
                                        DacarTxCd = 0,
                                        TeknoCd = 0,
                                        KaiserCd = 0,
                                        TotalCd = 0,
                                        Magnum = 0,
                                        LineaReferencia = l.Referencia,
                                        Devolucion = 0,




                                    }).ToList();
            }

            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {


                var Matriz = DB.Matriz_Inventario_Baterias_RN.ToList();

                foreach (var item in ListadoArticulos)
                {

                    foreach (var Mat in Matriz)
                    {
                        
                            //Selladas
                            //Estandar - Tekno
                            if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "SELLADA" && (Mat.Marca == "ESTANDAR - TEKNO" || Mat.Marca == "MOTO") && Mat.Stock != 0 && item.Polaridad == Mat.Polaridad)
                            {
                                item.DacarStSelladas = item.DacarStSelladas + Decimal.ToInt32(Mat.Stock ?? 0);

                            }
                            //Bp
                            if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "SELLADA" && Mat.Marca == "BOLT" && Mat.Stock != 0 && item.Polaridad == Mat.Polaridad)
                            {
                                item.DacarBpSelladas = item.DacarBpSelladas + Decimal.ToInt32(Mat.Stock ?? 0);
                            }
                            //Taxi- EcoPower
                            if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "SELLADA" && Mat.Marca == "TAXI - ECO" && Mat.Stock != 0 && item.Polaridad == Mat.Polaridad)
                            {
                                item.DacarTxSelladas = item.DacarTxSelladas + Decimal.ToInt32(Mat.Stock ?? 0);
                            }

                            //Kaiser
                            if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "SELLADA" && (Mat.Marca == "KAISER") && Mat.Stock != 0 && item.Polaridad == Mat.Polaridad)
                            {
                                item.KaiserSelladas = item.KaiserSelladas + Decimal.ToInt32(Mat.Stock ?? 0);
                            }
                            //Total
                            if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "SELLADA" && Mat.Stock != 0 && (Mat.Marca == "ESTANDAR - TEKNO" || Mat.Marca == "BOLT" || Mat.Marca == "TAXI - ECO" || Mat.Marca == "KAISER" || Mat.Marca == "MOTO") && item.Polaridad == Mat.Polaridad)
                            {
                                item.TotalSelladas = item.TotalSelladas + Decimal.ToInt32(Mat.Stock ?? 0);
                            }


                            string[] Total = { "ESTANDAR - TEKNO", "BOLT", "TAXI - ECO", "KAISER", "MOTO" };
                            //Carga
                            //Estandar - Tekno
                            if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "CARGADA" && (Mat.Marca == "ESTANDAR - TEKNO" || Mat.Marca == "MOTO") && Mat.Stock != 0 && item.Polaridad == Mat.Polaridad)
                            {
                                item.DacarStCarga = item.DacarStCarga + Decimal.ToInt32(Mat.Stock ?? 0);
                            }
                            //Bp
                            if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "CARGADA" && Mat.Marca == "BOLT" && Mat.Stock != 0 && item.Polaridad == Mat.Polaridad)
                            {
                                item.DacarBpCarga = item.DacarBpCarga + Decimal.ToInt32(Mat.Stock ?? 0);
                            }
                            //Taxi- EcoPower
                            if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "CARGADA" && Mat.Marca == "TAXI - ECO" && Mat.Stock != 0 && item.Polaridad == Mat.Polaridad)
                            {
                                item.DacarTxCarga = item.DacarTxCarga + Decimal.ToInt32(Mat.Stock ?? 0);
                            }

                            //Kaiser
                            if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "CARGADA" && (Mat.Marca == "KAISER") && Mat.Stock != 0 && item.Polaridad == Mat.Polaridad)
                            {
                                item.KaiserCarga = item.KaiserCarga + Decimal.ToInt32(Mat.Stock ?? 0);
                            }
                            /*
                        if (item.Referencia == "DB12" || item.Modelo == "DB12" && Mat.Ubicacion == "CARGADA" && Mat.Marca == "MOTO")
                        {
                            item.TotalCarga = item.TotalCarga + Decimal.ToInt32(Mat.Stock ?? 0);
                        }*/
                        //Total
                        if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "CARGADA" && Mat.Stock != 0 &&  Total.Contains(Mat.Marca) && item.Polaridad == Mat.Polaridad)
                            {
                            
                                item.TotalCarga = item.TotalCarga + Decimal.ToInt32(Mat.Stock ?? 0);
                            }



                            //Terminado Cd
                            //Estandar 
                            if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "TERMINADO" && (Mat.Marca == "Estandar" || Mat.Marca == "Moto" ) && Mat.Stock != 0 && item.Polaridad == Mat.Polaridad)
                            {
                                item.DacarStCd = item.DacarStCd + Decimal.ToInt32(Mat.Stock ?? 0);
                            }
                            // Tekno
                            if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "TERMINADO" && Mat.Marca == "TEKNO" && Mat.Stock != 0 && item.Polaridad == Mat.Polaridad)
                            {
                                item.TeknoCd = item.TeknoCd + Decimal.ToInt32(Mat.Stock ?? 0);
                            }
                            //Bp
                            if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "TERMINADO" && Mat.Marca == "Bolt" && Mat.Stock != 0 && item.Polaridad == Mat.Polaridad)
                            {
                                item.DacarBpCd = item.DacarBpCd + Decimal.ToInt32(Mat.Stock ?? 0);
                            }
                            //Taxi- EcoPower
                            if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "TERMINADO" && (Mat.Marca == "Eco Power" || Mat.Marca == "Taxi") && Mat.Stock != 0 && item.Polaridad == Mat.Polaridad)
                            {
                                item.DacarTxCd = item.DacarTxCd + Decimal.ToInt32(Mat.Stock ?? 0);
                            }

                            //Kaiser
                            if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "TERMINADO" && (Mat.Marca == "Kaiser") && Mat.Stock != 0 && item.Polaridad == Mat.Polaridad)
                            {
                                item.KaiserCd = item.KaiserCd + Decimal.ToInt32(Mat.Stock ?? 0);
                            }
                            //Total
                            if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "TERMINADO" && Mat.Stock != 0 && (Mat.Marca == "Estandar" || Mat.Marca == "TEKNO" || Mat.Marca == "Bolt" || Mat.Marca == "Eco Power" || Mat.Marca == "Kaiser" || Mat.Marca == "Taxi" || Mat.Marca == "Moto") && item.Polaridad == Mat.Polaridad)
                            {
                                item.TotalCd = item.TotalCd + Decimal.ToInt32(Mat.Stock ?? 0);
                            }

                            //Magnum
                            if (item.Referencia == Mat.Refencia && Mat.Stock != 0 && Mat.Marca == "Magnum" && Mat.Ubicacion == "TERMINADO" && item.Polaridad == Mat.Polaridad)
                            {
                                item.Magnum = item.Magnum + Decimal.ToInt32(Mat.Stock ?? 0);
                            }
                        //Magnum
                        if (item.Referencia == Mat.Refencia && Mat.Stock != 0  && Mat.Ubicacion == "DEVUELTO" && item.Polaridad == Mat.Polaridad)
                        {
                            item.Devolucion = item.Devolucion + Decimal.ToInt32(Mat.Stock ?? 0);
                        }








                    }
                }


                return ListadoArticulos;
            }

        }

        public List<MatrizInventarioAlmacenes> ConsultarListaBateriasAlmacenes()
        {
            var ListadoArticulos = new List<MatrizInventarioAlmacenes>();
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                ListadoArticulos = (from m in DB.MatrizInventarioBaterias
                                    join l in DB.LineasMarcasPropias on m.Linea equals l.Identificador
                                    where m.Estado =="A"
                                    select new MatrizInventarioAlmacenes()
                                    {   Modelo = m.Modelo,
                                        Referencia = m.Referencia,
                                        Polaridad  = m.Polaridad,
                                        DacarStDanin = 0,
                                        DacarBpDanin = 0,
                                        DacarTxDanin = 0,
                                        TotalDanin = 0,
                                        DacarStTanca = 0,
                                        DacarBpTanca = 0,
                                        DacarTxTanca = 0,
                                        TotalTanca = 0,
                                        DacarStRendon = 0,
                                        DacarBpRendon = 0,
                                        DacarTxRendon = 0,
                                        TotalRendon = 0,
                                        DacarStSambo = 0,
                                        DacarBpSambo = 0,
                                        DacarTxSambo = 0,
                                        TotalSambo = 0,
                                        DacarStQuito = 0,
                                        DacarBpQuito = 0,
                                        DacarTxQuito = 0,
                                        TotalQuito = 0,
                                        LineaReferencia =  l.Referencia,



                                    }).ToList();
            }

            using (SBODACARPRODEntities1 DB = new SBODACARPRODEntities1())
            {


                var Matriz = DB.Matriz_Inventario_Baterias_RN.ToList();

                foreach (var item in ListadoArticulos)
                {

                    foreach (var Mat in Matriz)
                    {
                        //Danin
                        //Estandar
                        if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "PLAZA DANIN" && (Mat.Marca == "Estandar" || Mat.Marca == "Moto")  && Mat.Stock != 0 && item.Polaridad == Mat.Polaridad )
                        {
                            item.DacarStDanin = item.DacarStDanin + Decimal.ToInt32(Mat.Stock ?? 0);
                        }
                        //Bp
                        if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "PLAZA DANIN" && Mat.Marca == "Bolt" && Mat.Stock != 0 && item.Polaridad == Mat.Polaridad)
                        {
                            item.DacarBpDanin = item.DacarBpDanin + Decimal.ToInt32(Mat.Stock ?? 0);
                        }
                        //Taxi- EcoPower
                        if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "PLAZA DANIN" && (Mat.Marca == "Taxi" || Mat.Marca == "Eco Power") && Mat.Stock != 0 && item.Polaridad == Mat.Polaridad)
                        {
                            item.DacarTxDanin = item.DacarTxDanin + Decimal.ToInt32(Mat.Stock ?? 0);
                        }
                        //Total
                        if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "PLAZA DANIN" && Mat.Stock != 0 && (Mat.Marca == "Taxi" || Mat.Marca == "Eco Power" || Mat.Marca == "Bolt" || Mat.Marca == "Estandar" || Mat.Marca == "Moto") && item.Polaridad == Mat.Polaridad)
                        {
                            item.TotalDanin = item.TotalDanin + Decimal.ToInt32(Mat.Stock ?? 0);
                        }

                        //Tanca
                        //Estandar
                        if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "JUAN TANCA MARENGO" && (Mat.Marca == "Estandar" || Mat.Marca == "Moto") && item.Polaridad == Mat.Polaridad)
                        {
                            item.DacarStTanca = item.DacarStTanca + Decimal.ToInt32(Mat.Stock ?? 0);
                        }
                        //Bp
                        if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "JUAN TANCA MARENGO" && Mat.Marca == "Bolt" && item.Polaridad == Mat.Polaridad)
                        {
                            item.DacarBpTanca = item.DacarBpTanca + Decimal.ToInt32(Mat.Stock ?? 0);
                        }
                        //Taxi- EcoPower
                        if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "JUAN TANCA MARENGO" && (Mat.Marca == "Taxi" || Mat.Marca == "Eco Power") && item.Polaridad == Mat.Polaridad)
                        {
                            item.DacarTxTanca = item.DacarTxTanca + Decimal.ToInt32(Mat.Stock ?? 0);
                        }
                        //Total
                        if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "JUAN TANCA MARENGO" && (Mat.Marca == "Taxi" || Mat.Marca == "Eco Power" || Mat.Marca == "Bolt" || Mat.Marca == "Estandar" || Mat.Marca == "Moto") && item.Polaridad == Mat.Polaridad)
                        {
                            item.TotalTanca = item.TotalTanca + Decimal.ToInt32(Mat.Stock ?? 0);
                        }

                        // Rendon
                        //Estandar
                        if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "GOMEZ RENDON" && (Mat.Marca == "Estandar" || Mat.Marca == "Moto") && item.Polaridad == Mat.Polaridad)
                        {
                            item.DacarStRendon = item.DacarStRendon + Decimal.ToInt32(Mat.Stock ?? 0);
                        }
                        //Bp
                        if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "GOMEZ RENDON" && Mat.Marca == "Bolt" && item.Polaridad == Mat.Polaridad)
                        {
                            item.DacarBpRendon = item.DacarBpRendon + Decimal.ToInt32(Mat.Stock ?? 0);
                        }
                        //Taxi- EcoPower
                        if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "GOMEZ RENDON" && (Mat.Marca == "Taxi" || Mat.Marca == "Eco Power") && item.Polaridad == Mat.Polaridad)
                        {
                            item.DacarTxRendon = item.DacarTxRendon + Decimal.ToInt32(Mat.Stock ?? 0);
                        }
                        //Total
                        if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "GOMEZ RENDON" && (Mat.Marca == "Taxi" || Mat.Marca == "Eco Power" || Mat.Marca == "Bolt" || Mat.Marca == "Estandar" || Mat.Marca == "Moto") && item.Polaridad == Mat.Polaridad)
                        {
                            item.TotalRendon = item.TotalRendon + Decimal.ToInt32(Mat.Stock ?? 0);
                        }
                        //Sambo
                        //Estandar
                        if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "SAMBORONDON" && (Mat.Marca == "Estandar" || Mat.Marca == "Moto") && item.Polaridad == Mat.Polaridad)
                        {
                            item.DacarStSambo = item.DacarStSambo + Decimal.ToInt32(Mat.Stock ?? 0);
                        }
                        //Bp
                        if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "SAMBORONDON" && Mat.Marca == "Bolt" && item.Polaridad == Mat.Polaridad)
                        {
                            item.DacarBpSambo = item.DacarBpSambo + Decimal.ToInt32(Mat.Stock ?? 0);
                        }
                        //Taxi- EcoPower
                        if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "SAMBORONDON" && (Mat.Marca == "Taxi" || Mat.Marca == "Eco Power") && item.Polaridad == Mat.Polaridad)
                        {
                            item.DacarTxSambo = item.DacarTxSambo + Decimal.ToInt32(Mat.Stock ?? 0);
                        }
                        //Total
                        if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "SAMBORONDON" && (Mat.Marca == "Taxi" || Mat.Marca == "Eco Power" || Mat.Marca == "Bolt" || Mat.Marca == "Estandar" || Mat.Marca == "Moto") && item.Polaridad == Mat.Polaridad)
                        {
                            item.TotalSambo = item.TotalSambo + Decimal.ToInt32(Mat.Stock ?? 0);
                        }


                        //Quito
                        //Estandar
                        if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "QUITO" && (Mat.Marca == "Estandar" || Mat.Marca == "Moto") && item.Polaridad == Mat.Polaridad)
                        {
                            item.DacarStQuito = item.DacarStQuito + Decimal.ToInt32(Mat.Stock ?? 0);
                        }
                        //Bp
                        if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "QUITO" && Mat.Marca == "Bolt" && item.Polaridad == Mat.Polaridad)
                        {
                            item.DacarBpQuito = item.DacarBpQuito + Decimal.ToInt32(Mat.Stock ?? 0);
                        }
                        //Taxi- EcoPower
                        if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "QUITO" && (Mat.Marca == "Taxi" || Mat.Marca == "Eco Power") && item.Polaridad == Mat.Polaridad)
                        {
                            item.DacarTxQuito = item.DacarTxQuito + Decimal.ToInt32(Mat.Stock ?? 0);
                        }
                        //Total
                        if (item.Referencia == Mat.Refencia && Mat.Ubicacion == "QUITO" && (Mat.Marca == "Taxi" || Mat.Marca == "Eco Power" || Mat.Marca == "Bolt" || Mat.Marca == "Estandar" || Mat.Marca == "Moto") && item.Polaridad == Mat.Polaridad)
                        {
                            item.TotalQuito = item.TotalQuito + Decimal.ToInt32(Mat.Stock ?? 0);
                        }

                    }
                }


                return ListadoArticulos;
            }

        }

        public List<vConsultaArticulos> ConsultarListaBateriasSelladas()
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