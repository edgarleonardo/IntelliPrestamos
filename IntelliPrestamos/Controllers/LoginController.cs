using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IntelliPrestamo.AccessLayer.DataManager;
using IntelliPrestamos.Models;
using System.Web.Security;
using System.Configuration;

namespace IntelliPrestamos.Controllers
{
    public class LoginController : BaseController.BaseController
    {
        //
        // GET: /Login/
        
        public ActionResult Login(string ReturnUrl = "")
        {
            bussinessInfo();
            if (getCookies())
            {
                if (string.IsNullOrEmpty(ReturnUrl))
                {
                    ReturnUrl = ConfigurationManager.AppSettings["AuthDefaultUrl"].ToString() + Cedula;
                }

                Response.Redirect(ReturnUrl);
            }
            return View();
        }
        public ActionResult Logout()
        {
            bussinessInfo();
            if (getCookies())
            {
                FormsAuthentication.SignOut();
            }
            return View("Login");
        }
        public ActionResult ErrorAutorizacion()
        {
            bussinessInfo();
            return View();
        }
        
	}
}