using IntelliPrestamo.AccessLayer.AppTools;
using IntelliPrestamo.AccessLayer.DataManager;
using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IntelliPrestamos.BaseController
{
    public class BaseController : Controller
    {
        protected string Cedula = "";
        /// <summary>
        /// Este metodo valida los permisos del usuario que accede
        /// </summary>
        /// <param name="userName">Nombre Usuario</param>
        /// <param name="LoginActivo">Valida si esta activo</param>
        protected void security(string userName, bool LoginActivo)
        {
            ViewBag.Title = "Informaciones De La Empresa";
        }
        protected bool getCookies()
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            var User = "";
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket =
                       FormsAuthentication.Decrypt(authCookie.Value);
                User = authTicket.Name;
                UsuarioManager userio = new UsuarioManager();
                Usuarios usuario = userio.GetUserByUserName(User);
                ViewBag.UserName = usuario.Usuario;
                ViewBag.SolicitudesPendientes = usuario.SolicitudesPendientes;
                ViewBag.CuotasPendientes = usuario.CuotasPendientes;
                ViewBag.SolicitudesAprobadas = usuario.SolicitudesAprobadas;
                Cedula = ViewBag.LoggedId = usuario.Cedula;
                ModuloManager modulo = new ModuloManager();
                ViewBag.PermissionsList = modulo.GetUserModule(usuario.Cedula);
                return true;
            }
            return false;
        }
        private string UserSmtp = "";
        private string Pass = "";
        private string ServerSmtp = "";
        private int port = 0;
        protected Datos_Sistema bussinessInfo()
        {
            SetUpManager manager = new SetUpManager();
            var result = manager.Get();
            ViewBag.Name = result.Nombre_Negocio;
            this.UserSmtp = result.Smtp_User;
            this.Pass = result.Smtp_Pass;
            this.ServerSmtp = result.Smtp_Server;
            this.port = result.Smtp_Port;
            ViewBag.Url_Image = "~/content/" + result.Logo_Negocio;
            ViewBag.Url_Icon = "~/content/" + result.Icon;
            ViewBag.ColorLeft = result.Color;
            getCookies();
            return result;
        }
        protected void sendMail(string mailTo, string Subject,string Message)
        {
            MailSender mail = new MailSender(this.ServerSmtp, this.UserSmtp, this.Pass, this.port);
            mail.SendMail(Message, Subject, mailTo, ConfigurationManager.AppSettings["MailFrom"]);
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string userName, string password, string ReturnURL)
        {
            UsuarioManager user = new UsuarioManager();
            Usuarios usuario = user.GetAuthentication(userName, password);
            bussinessInfo();
            if (usuario == null || usuario.Cedula == null || usuario.Cedula == "")
            {
                Set_Message("Usuario o Clave Incorrectas.");
                return View();
            }
            else if (getCookies())
            {
                if (string.IsNullOrEmpty(ReturnURL))
                {
                    ReturnURL = ConfigurationManager.AppSettings["AuthDefaultUrl"].ToString() + usuario.Cedula;
                }

                Response.Redirect(ReturnURL);
                return View();
            }
            else
            {
                var authTicket = new FormsAuthenticationTicket(1, usuario.Usuario,
                DateTime.Now, DateTime.Now.AddMinutes(int.Parse(ConfigurationManager.AppSettings["TimeOutMinute"].ToString())), true, usuario.Id_Rol.ToString());
                string cookieContents = FormsAuthentication.Encrypt(authTicket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieContents)
                {
                    Expires = authTicket.Expiration,
                    Path = FormsAuthentication.FormsCookiePath
                };
                Response.Cookies.Add(cookie);
                ViewBag.UserName = usuario.Usuario;
                ViewBag.LoggedId = usuario.Cedula;
                if (string.IsNullOrEmpty(ReturnURL))
                {
                    ReturnURL = ConfigurationManager.AppSettings["AuthDefaultUrl"].ToString() + usuario.Cedula;
                }

                Response.Redirect(ReturnURL);

                return View();
            }
        }
        protected void Create_Excel_Reports<T>(string fileName, IList<T> data)
        {
            string fiInfo = DateTime.Now.Millisecond.ToString() + fileName;
            if (System.IO.File.Exists(Server.MapPath("~/TmpFiles/") + fiInfo))
            {
                System.IO.File.Delete(Server.MapPath("~/TmpFiles/") + fiInfo);
            }
            DataTable tbReport = ConvertToDataTable<T>(data);
            ExcelFileCreation excel = new ExcelFileCreation();
            excel.CreateCSVFile(ref tbReport, Server.MapPath("~/TmpFiles/") + fiInfo);//Generando Archivo de Excel  
                 
            //, Server.MapPath("../TmpFiles/")
            ViewBag.DownloadLink = fiInfo;
        }
        private DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
        public ActionResult Ayuda()
        {
            bussinessInfo();
            return View();
        }
        public ActionResult GetInvestmentAcc(string cedula)
        {
            InversionManager managerInvestment = new InversionManager();
            initiateDropDown();
            bussinessInfo();
            var result = managerInvestment.GetInversion(cedula);
            result.Cedula = cedula;
            return Json(result, JsonRequestBehavior.AllowGet); ;
        }
        protected IEnumerable<SelectListItem> GetDrpColor()
        {
            List<SelectListItem> ls = new List<SelectListItem>();
            try
            {
                ColorManager _man = new ColorManager();

                var result = _man.Get();
                foreach (var temp in result)
                {
                    ls.Add(new SelectListItem() { Text = temp.Color_Desc.ToString(), Value = temp.Color_Code.ToString() });
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message.ToString();
            }
            return ls;
        }
        protected IEnumerable<SelectListItem> GetDrpStatusSolicitud()
        {
            List<SelectListItem> ls = new List<SelectListItem>();
            try
            {
                StatusManager _man = new StatusManager();

                var result = _man.Get(1);
                foreach (var temp in result)
                {
                    ls.Add(new SelectListItem() { Text = temp.status_desc.ToString(), Value = temp.status_id.ToString() });
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message.ToString();
            }
            return ls;
        }
        protected void initiateDropDownSolicitud()
        {
            ViewBag.GetDrpPeriodicidad = GetDrpPeriodicidad();
            ViewBag.GetDrpBanco = GetDrpBanco();
            ViewBag.GetDrpRelacion = GetDrpRelacion();
            ViewBag.DrpSolicitudStatus = GetDrpStatusSolicitud();
            ViewBag.DrpAccionistas = GetDrpAccionistas();
            ViewBag.DrpSup = GetDrpSup();
        }
        protected IEnumerable<SelectListItem> GetDrpRelacion()
        {
            List<SelectListItem> ls = new List<SelectListItem>();
            try
            {
                ReferenciasTipoManager _man = new ReferenciasTipoManager();

                var result = _man.Get();
                foreach (var temp in result)
                {
                    ls.Add(new SelectListItem() { Text = temp.Tipo_Referencia_Desc.ToString(), Value = temp.Tipo_Referencia_Id.ToString() });
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message.ToString();
            }
            return ls;
        }
        protected IEnumerable<SelectListItem> GetDrpPeriodicidad()
        {
            List<SelectListItem> ls = new List<SelectListItem>();
            try
            {
                PeriodicidadManager _man = new PeriodicidadManager();

                var result = _man.Get();
                foreach (var temp in result)
                {
                    ls.Add(new SelectListItem() { Text = temp.Periodicidad_Info.ToString(), Value = temp.Periodicidad_Id.ToString() });
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message.ToString();
            }
            return ls;
        }
        protected IEnumerable<SelectListItem> GetDrpBanco()
        {
            List<SelectListItem> ls = new List<SelectListItem>();
            try
            {
                BancosManager _man = new BancosManager();

                var result = _man.Get();
                foreach (var temp in result)
                {
                    ls.Add(new SelectListItem() { Text = temp.Nombre_Banco.ToString(), Value = temp.Id_Bancos.ToString() });
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message.ToString();
            }
            return ls;
        }
        protected IEnumerable<SelectListItem> GetDrpCargo()
        {
            List<SelectListItem> ls = new List<SelectListItem>();
            try
            {
                CargoManager _man = new CargoManager();

                var result = _man.Get();
                foreach (var temp in result)
                {
                    ls.Add(new SelectListItem() { Text = temp.Nombre.ToString(), Value = temp.Id_Cargo.ToString() });
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message.ToString();
            }
            return ls;
        }
        protected IEnumerable<SelectListItem> GetDrpAccionistas()
        {
            List<SelectListItem> ls = new List<SelectListItem>();
            try
            {
                EmpleadoManager _man = new EmpleadoManager();

                var result = _man.GetAllAccionistas();
                foreach (var temp in result)
                {
                    ls.Add(new SelectListItem() { Text = temp.Nombre.ToString()+" "+temp.Apellidos, Value = temp.Cedula.ToString() });
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message.ToString();
            }
            return ls;
        }
        protected void initiateDropDown()
        {
            ViewBag.DrpCargo = GetDrpCargo();
            ViewBag.DrpSup = GetDrpSup();
            ViewBag.DrpRol = GetDrpRol();
            ViewBag.DrpTipoTrans = GetDrpTipoDeTrans();
        }
            protected IEnumerable<SelectListItem> GetDrpFlujo()
        {
            List<SelectListItem> ls = new List<SelectListItem>();
            try
            {
                FlujoManager _man = new FlujoManager();

                var result = _man.Get();
                foreach (var temp in result)
                {
                    ls.Add(new SelectListItem() { Text = temp.flujo_desc.ToString() , Value = temp.flujo_id.ToString() });
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message.ToString();
            }
            return ls;
        }
        protected IEnumerable<SelectListItem> GetDrpTipoDeTrans()
        {
            List<SelectListItem> ls = new List<SelectListItem>();
            try
            {
                TransactionManager _man = new TransactionManager();

                var result = _man.GetTransaccionesCapital();
                foreach (var temp in result)
                {
                    ls.Add(new SelectListItem() { Text = temp.Tipo_Transaccion_Desc.ToString() , Value = temp.Tipo_Transaccion_Id.ToString() });
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message.ToString();
            }
            return ls;
        }
        protected IEnumerable<SelectListItem> GetDrpSup()
        {
            List<SelectListItem> ls = new List<SelectListItem>();
            try
            {
                EmpleadoManager _man = new EmpleadoManager();

                var result = _man.GetActive();
                foreach (var temp in result)
                {
                    ls.Add(new SelectListItem() { Text = temp.Nombre.ToString() + " " + temp.Apellidos, Value = temp.Cedula.ToString() });
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message.ToString();
            }
            return ls;
        }
        protected IEnumerable<SelectListItem> GetDrpRol()
        {
            List<SelectListItem> ls = new List<SelectListItem>();
            try
            {
                RolesManager _man = new RolesManager();

                var result = _man.Get(0);
                foreach (var temp in result)
                {
                    ls.Add(new SelectListItem() { Text = temp.Nombre_Rol.ToString(), Value = temp.Id_Rol.ToString() });
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message.ToString();
            }
            return ls;
        }
        protected string Upload_File(string Id_Element, string File_Name)
        {
            var file = Request.Files[Id_Element];
            var extension = Path.GetExtension(file.FileName).Replace(".", "");
            if (extension != null && extension != "")
            {
                var fileName = File_Name + "." + extension;

                string path = Path.Combine(Server.MapPath("~/content/"), fileName.ToString());

                file.SaveAs(path);
                return fileName;
            }
            return "";
        }
        protected void Set_Message(string message)
        {
            ViewBag.Message = message;
        }
        
    }
}