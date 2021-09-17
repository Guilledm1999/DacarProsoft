using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAPbobsCOM;


namespace DacarProsoft.Datos
{
    public static  class ConexionApiSap
    {
        public static Company myCompany = null;
        public static bool Open()
        {
            try
            {
                bool Respuesta = false;

                Company company = new Company();
                myCompany = company;
                myCompany.Server = "SRVSAPDAC";
                myCompany.DbServerType = BoDataServerTypes.dst_MSSQL2014;
                myCompany.CompanyDB = "SBODACARPRUEBAS";
                myCompany.UserName = "manager";
                myCompany.Password = "Dcs@P15.*";
                myCompany.language = BoSuppLangs.ln_Spanish_La;

                int error = myCompany.Connect();

                if (error == 0)
                {
                    Respuesta = true;
                    Console.WriteLine("Conexion Exitosa a la Base de datos TecnologiasMaster");
                }
                else
                {
                    string descerror = "Error"+ myCompany.GetLastErrorDescription().ToString();
                    Console.WriteLine("Error - " + myCompany.GetLastErrorDescription().ToString());
                }
                return Respuesta;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

    }
}