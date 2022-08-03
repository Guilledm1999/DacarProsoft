using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DacarProsoft.Models;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data;
using DacarProsoft.Datos;

namespace DacarProsoft.Controllers
{
    //[HttpGet]
    
    public class AccountController : Controller
    {
        public DaoUsuarios daoUsuarios = new DaoUsuarios();
        public DaoMenu daoMenu = new DaoMenu();



        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
           
            ViewBag.showSuccessAlert = false;
            return View();
        }

      
        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
		
        public ActionResult Verify(Account acc)
        {
            var ini = daoUsuarios.InicioSesion(acc.NombreUsuario, acc.Contrasena);
            int tipoUsu = 0;
            int idUsuario = 0;
            string nombreCompleto = "";
            string usuarioIng = acc.NombreUsuario;
            if (ini.Count > 0)
            {
                foreach (var x in ini)
                {
                    tipoUsu = x.TipoUsuario;
                    idUsuario = x.IdUsuario;
                    nombreCompleto = x.NombreCompleto;
                   
                }

                var menu=daoMenu.AccesosMenu2(tipoUsu);

                Session["Menu"] = menu;
                Session["tipoUsuario"] = tipoUsu;
                Session["idUsuario"] = idUsuario;
                Session["nombreCompleto"] = nombreCompleto;
                Session["usuarioIng"] = usuarioIng;

                Session["usuario"] = ini;
                return RedirectToAction("VistaPrincipal", "Principal");
            }
            else
            {
                ViewBag.showSuccessAlert = true;
                return RedirectToAction("Login", "Account");
            }

        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public bool ConsultarPass(string contrasena)
        {
            try
            {
                var ini = daoUsuarios.InicioSesion(Session["usuarioIng"].ToString(), contrasena);
                if (ini.Count > 0)
                {
                    return true;
                }
                else {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        [HttpPost]
        public bool CambiarPassUser(string contrasena)
        {
            try
            {     
                bool actUser = daoUsuarios.ActualizacionUsuarios(Convert.ToInt32(Session["idUsuario"].ToString()), Session["nombreCompleto"].ToString(), Session["usuarioIng"].ToString(), contrasena, Convert.ToInt32(Session["tipoUsuario"].ToString()));
                if (actUser == true)
                {
                    return true;
                }
                else {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

    }
}


//for (int i = 0; i < dat.Rows.Count; i++)
//{
//    Account account = new Account();
//    account.NombreUsuario = dat.Rows[i]["NombreCompleto"].ToString();
//    account.TipoUsuario = dat.Rows[i]["TipoUsuario"].ToString();
//    usuarios.Add(account);
//}

//if (dat != null)
//{
//    return RedirectToAction("VistaPrincipal", "Principal");
//}
//else
//{      
//    return View("LoginFail");
//}