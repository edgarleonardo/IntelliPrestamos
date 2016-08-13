using IntelliPrestamo.AccessLayer.DataManager;
using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntelliPrestamos.Controllers
{
    public class SetUpController : BaseController.BaseController
    {
        private SetUpManager manager = null;
        private RolesManager Rol = null;
        private ModuloManager Mod = null;
        //
        // GET: /SetUp/
        public ActionResult Index()
        {
            var result = bussinessInfo();
            ViewBag.DrpColor = GetDrpColor();
            return View(result);
        }

        [HttpPost]
        public ActionResult Index(Datos_Sistema model)
        {
            try
            {

                bussinessInfo();
                ViewBag.DrpColor = GetDrpColor();
                manager = new SetUpManager();
                model.Logo_Negocio = Upload_File("fileUpload", "logo");
                model.Icon = Upload_File("fileUploadIcon", "icon");
                var result = manager.Set(model);
                Set_Message("Datos Guardados Correctamente");
                return View(result);
            }
            catch (Exception ex)
            {
                Set_Message("Ha Ocurrido Un Error: " + ex.Message);
            }
            return View(model);
        }

        public ActionResult Roles()
        {
            bussinessInfo();
            return GetRoles("");
        }
        public ActionResult GetRoles(string partial_view)
        {
            Rol = new RolesManager();
            var result = Rol.Get(0);
            #region PreparandoArchivoDescarga
            string fileName = "RoleInfo.csv";            
                //Areglando lista.
                var dataReport = (from a in result
                                  select new
                                  {
                                      Id_Rol = a.Id_Rol.ToString(),
                                      Nombre_Rol = a.Nombre_Rol,
                                      Descripccion = a.Descripccion,
                                      Activo = a.Activo.ToString()
                                  }
                    ).ToList();
                Create_Excel_Reports(fileName, dataReport);
            #endregion
            if (string.IsNullOrEmpty(partial_view))
            {
                return View(result);
            }
            return PartialView(partial_view, result);
        }
        public ActionResult GetSingleRole(int Id_Rol, string partial_view)
        {
            Rol = new RolesManager();
            Mod = new ModuloManager();
            var result = Rol.Get(Id_Rol);
            var result_view = new Roles(); 
            if (result != null && result.Count > 0)
            {
                result_view = result[0];
                result_view.ListRol = Mod.Get(Id_Rol);
            }
            else
            {
                result_view.ListRol = Mod.Get(0);
            }

            return PartialView(partial_view, result_view);
        }

        public ActionResult Delete(Roles model)
        {
            try
            {
                Rol = new RolesManager();
                Mod = new ModuloManager();
                model.Activo = 0;
                var result = Rol.Set(model);

                ViewBag.Success = "Datos Actualizados Satisfactoriamente";
                return Content("Datos Actualizados Satisfactoriamente");
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return Content(ViewBag.Error);
        }
        public ActionResult Add(Roles model)
        {
            try
            {
                Rol = new RolesManager();
                Mod = new ModuloManager();
                var result = Rol.Set(model);

                if (result != null)
                {
                    foreach (Modulos modulos in model.ListRol)
                    {
                        if (modulos.Selected)
                        {
                            Mod.Set(result.Id_Rol, modulos.Id_Modulos, 1);
                        }
                        else
                        {
                            Mod.Set(result.Id_Rol, modulos.Id_Modulos, 0);
                        }
                    }
                }
                ViewBag.Success = "Datos Guardados/Actualizados Satisfactoriamente";
                return Content("Datos Guardados/Actualizados Satisfactoriamente");
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return Content(ViewBag.Error);
        }
        
        
	}
}