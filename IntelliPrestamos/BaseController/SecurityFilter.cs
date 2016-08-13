using IntelliPrestamo.AccessLayer.DataManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IntelliPrestamos.BaseController
{
    public class SecurityFilter : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            HttpCookie authCookie =
              filterContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            var User = "";
            bool isAuthenticated = false;
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket =
                       FormsAuthentication.Decrypt(authCookie.Value);
                var identity = new GenericIdentity(authTicket.Name, "Forms");
                var principal = new GenericPrincipal(identity, new string[] { authTicket.UserData });
                filterContext.HttpContext.User = principal;
                User = authTicket.Name;
                isAuthenticated = true;
            }

            var Controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var Action = filterContext.ActionDescriptor.ActionName;
            
            bool IsLoginPage = false;
            if (Controller.ToLower() == "pagos" && (Action.ToLower() == "volantesdepago" || Action.ToLower() == "tablaamortizacion"))
            {
                IsLoginPage = true;
            }
            if (Controller.ToLower() == "login")
            {
                IsLoginPage = true;
            }
            if (!IsLoginPage)
            {
                UsuarioManager user = new UsuarioManager();
                var isAccessAllowed = user.AuthorizationRoutes(User, Controller, Action);
                if ((isAccessAllowed == null || isAccessAllowed.Autorizado_Para_Ruta == null || isAccessAllowed.Autorizado_Para_Ruta <= 0))
                {
                    if (isAuthenticated)
                    {
                        filterContext.Result = new RedirectResult("~/Login/ErrorAutorizacion");
                    }
                    else
                    {
                        FormsAuthentication.RedirectToLoginPage();
                    }                    
                }
            }
        }
    }
}