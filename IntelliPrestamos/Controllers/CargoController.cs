using IntelliPrestamo.AccessLayer.DataManager;
using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntelliPrestamos.Controllers
{
    public class CargoController : BaseController.BaseController
    {
            private CargoManager manager;
        //
        // GET: /Cargo/
        
        public ActionResult Manage()
        {
            bussinessInfo();
            return GetAll("");
        }

        public ActionResult Get(int Id_Rol, string partial_view)
        {
            manager = new CargoManager();
            var result = manager.Get(Id_Rol);

            return PartialView(partial_view, result);
        }

        public ActionResult GetAll(string partial_view)
        {
            manager = new CargoManager();
            var result = manager.Get();
            #region PreparandoArchivoDescarga
            string fileName = "Cargos.csv";
            //Areglando lista.
            var dataReport = (from a in result
                              select new
                              {
                                  Id_Cargo = a.Id_Cargo.ToString(),
                                  Nombre = a.Nombre,
                                  EsAccionista = a.EsAccionista.ToString(),
                                  EsGerencial = a.EsGerencial.ToString()
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
        
        public ActionResult Add(Cargo model)
        {
            try
            {
                manager = new CargoManager();
                manager.Set(model);

                ViewBag.Success = "Datos Actualizados Satisfactoriamente";
                return Content("Datos Actualizados Satisfactoriamente");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return Content(ViewBag.Error);
        }
        public ActionResult Delete(Cargo model)
        {
            try
            {
                manager = new CargoManager();
               manager.Del(model);

                ViewBag.Success = "Datos Actualizados Satisfactoriamente";
                return Content("Datos Actualizados Satisfactoriamente");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return Content(ViewBag.Error);
        }
	}
}