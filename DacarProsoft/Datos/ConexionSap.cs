using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DacarProsoft.Datos
{
    public class ConexionSap
    {
        private SqlConnection conexionBDL = new SqlConnection("Server=DESKTOP-9VAIF7L;database=SBODACARPROD;User Id=sa;Password=Dacar123");
        public SqlConnection AbrirConexionSap()
        {
            if (conexionBDL.State == ConnectionState.Closed)
                conexionBDL.Open();
            return conexionBDL;
        }
        public SqlConnection CerrarConexionSap()
        {
            if (conexionBDL.State == ConnectionState.Open)
                conexionBDL.Close();
            return conexionBDL;
        }
    }
}