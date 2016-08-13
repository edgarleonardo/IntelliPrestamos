using IntelliPrestamo.AccessLayer.DataManager;
using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntelliPrestamos.Controllers
{
    public class UsersController : BaseController.BaseController
    {

        private EmpleadoManager manager;
        //
        // GET: /Users/
        public ActionResult Manage()
        {
            bussinessInfo();
            return Get("", "");
        }
        public ActionResult GetAll(string partial_view)
        {
            manager = new EmpleadoManager();
            var result = manager.GetAll();
            return PartialView(partial_view, result);
        }
        public ActionResult Get(string Id_Rol, string partial_view)
        {
            manager = new EmpleadoManager();
            initiateDropDown();
            if (Id_Rol == "" &&
                partial_view == "")
            {
                var result = manager.GetAll();
                return View(result);
            }
            else
            {
                var result = manager.GetBy(Id_Rol);
                return PartialView(partial_view, result);
            }            
        }
        public ActionResult User(string Cedula)
        {
            bussinessInfo();
            initiateDropDown();
            manager = new EmpleadoManager();
            var result = manager.GetBy(Cedula);
            return View(result);
        }
        
        public ActionResult Add(Empleado model)
        {
            try
            {
                bussinessInfo();
                manager = new EmpleadoManager();
                initiateDropDown();
                /// SI la fecha de salida, asignar espacio en blanco
                if (model.Fecha_Salida == null )
                {
                    model.Fecha_Salida ="";
                }
                manager.Set(model);
                
                ViewBag.Message = "Datos Actualizados Satisfactoriamente";
                return View("User", model);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }

            return View("User", model);
        }
	}
}