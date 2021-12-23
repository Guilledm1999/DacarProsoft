using DacarDatos.Datos;
using DacarProsoft.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DacarProsoft.Datos
{
    public class ConexionAccess
    {
        //var ruta=ObtenerRutaAccess();

        private static string conec ;
        OleDbConnection cn;
        public bool open(string Base)
        {
            try
            {
                var ruta = ObtenerRutaAccess();

                //conec = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\\\\21.0.1.152\\VisuaLCN\\data\\27-75.mdb;Persist Security Info=False;";
                conec = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+ruta+Base+";Persist Security Info=False;";

                cn = new OleDbConnection(conec);
                cn.Open();
                Console.WriteLine("Conexión válida");
                return true;
            }
            catch (Exception ex){
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        public bool close()
        {
            try
            {
                //conec = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\\\\21.0.1.152\\VisuaLCN\\data\\27-75.mdb;Persist Security Info=False;";
                //cn = new OleDbConnection(conec);
                cn.Close();
                Console.WriteLine("Conexión Cerrada");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        public List<ModelAccessData_O> AccessData_O()
        {
            List<ModelAccessData_O> valores = new List<ModelAccessData_O>();

            OleDbDataAdapter da = new OleDbDataAdapter();
            DataSet ds = new DataSet();

            da.SelectCommand = new OleDbCommand("select * FROM Data_O ", cn);

            OleDbDataReader reader = da.SelectCommand.ExecuteReader();          

            while (reader.Read())
            {
                valores.Add(new ModelAccessData_O
                {
                    Current= Convert.ToDecimal(reader["Current"].ToString()),
                    Power = Convert.ToDecimal(reader["Power"].ToString()),
                    StepTime = Convert.ToInt32(reader["Step Time"].ToString()),
                    TestUnique = Convert.ToInt32(reader["Test Unique"].ToString()),
                    TotalTime = Convert.ToInt32(reader["Total Time"].ToString()),
                    Voltaje = Convert.ToDecimal(reader["Voltage"].ToString()),
                });
            }
            return valores;
        }


        public List<SelectListItem> ConsultarFechasBaseDeDatos()
        {
            List<SelectListItem> lst = new List<SelectListItem>();

            OleDbDataAdapter da = new OleDbDataAdapter();
            DataSet ds = new DataSet();

            da.SelectCommand = new OleDbCommand("select * FROM Test ORDER BY Date DESC", cn);

            OleDbDataReader reader = da.SelectCommand.ExecuteReader();

            while (reader.Read())
            {
                lst.Add(new SelectListItem
                {
                    Value = reader["Test Unique"].ToString(),
                    Text = reader["Date"].ToString()
                  
                });
            }
            return lst;
        }

        public List<SelectListItem> ConsultarDetalleRegistrosLcnAccess(int testUnique)
        {
            List<SelectListItem> lst = new List<SelectListItem>();

            OleDbDataAdapter da = new OleDbDataAdapter();
            DataSet ds = new DataSet();

            da.SelectCommand = new OleDbCommand("select * FROM Test ORDER BY Date DESC", cn);

            OleDbDataReader reader = da.SelectCommand.ExecuteReader();

            while (reader.Read())
            {
                lst.Add(new SelectListItem
                {
                    Value = reader["Test Unique"].ToString(),
                    Text = reader["Date"].ToString()

                });
            }
            return lst;
        }

        public string ObtenerRutaAccess()
        {
            string result="";
            using (DacarProsoftEntities DB = new DacarProsoftEntities())
            {
                try
                {
                    var Listado = (from d in DB.RutaBasesAccessCalidad
                                   select new
                                   {
                                       d.RutaFisica
                                   }).FirstOrDefault();


                    result = Listado.RutaFisica;

                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return result;
                }
            }
        }
    }
}