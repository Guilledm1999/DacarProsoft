using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml;

namespace DacarProsoft.Datos
{


    public class Conexion
    {
        public String Servidor = "DESKTOP-9VAIF7L";
        public String Base = "DacarProsoft";
        public String Usuario = "sa";
        public String Contrasena = "Dacar123";

        public SqlConnection conn;
        public SqlCommand comando;
        public SqlDataAdapter adaptador;
        
        public string servidor = "";
        public string baseDatos = "";
        public string usuario = "";
        public string clave = "";
        public string cadena = "";

        public Conexion()
        {
            conn = new SqlConnection();
            comando = new SqlCommand();
            comando.Connection = conn;
            adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
        }


        //public void ConectarSap()
        //{
            
        //    try
        //    {
        //        cadena = String.Format("Server=DESKTOP-9VAIF7L;database=SBODACARPROD;User Id=sa;Password=Dacar123");

        //        conn = new SqlConnection(cadena);
        //        if (conn.State != System.Data.ConnectionState.Open)
        //        {
        //            //ABRO LA CONEXION
        //            conn.Open();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        public void Conectar()
        {
            try
            {
                //UNA VEZ QUE HE OBTENIDO LOS DATOS DE CONEXION ARMAMOS LA CADENA DE CONEXION
                cadena = String.Format("Data Source={0};Initial Catalog={1};user={2};password={3}", Servidor,Base, Usuario, Contrasena);
                //YA TENEMOS LA CADENA AHORA CONECTAMOS CON LA BASE
                conn.ConnectionString = cadena;
                //POR ULTIMO ABRIMOS LA CONEXION SIEMPRE Y CUANDO NO EXISTA UNA CONEXION ABIERTA
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }
            }
            catch (Exception ex)
            {

               Console.WriteLine(ex.Message);
            }
        }
        public DataTable ConsultarDatos(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                //CONECTO A LA BASE DE DATOS
                Conectar();

                using (conn)
                {
                    //EJECUTO LAS POSIBLES SENTENCIAS SQL
                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = CommandType.Text;
                    comando.CommandText = sql;
                    comando.Connection = conn;

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
        public bool EjecutarSentencia(string sql)
        {
            bool resultado = true;
            try
            {
                //CONECTO A LA BASE DE DATOS
                Conectar();

                using (conn)
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = CommandType.Text;
                    comando.CommandText = sql;
                    comando.Connection = conn;

                    comando.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Console.WriteLine(ex.Message);
            }
            return resultado;
        }
    }
}
