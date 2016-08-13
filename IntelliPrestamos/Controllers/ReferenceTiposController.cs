using IntelliPrestamo.AccessLayer.DataManager;
using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntelliPrestamos.Controllers
{
    public class ReferenceTiposController : BaseController.BaseController
    {
        //
        // GET: /ReferenceTipos/
        public ActionResult Manage()
        {
            bussinessInfo();
            ReferenciasTipoManager manager = new ReferenciasTipoManager();
            var result = manager.Get();
            return View(result);
        }
        public ActionResult Get(int Id_Rol, string partial_view)
        {
            ReferenciasTipoManager manager = new ReferenciasTipoManager();
            var result = manager.Get(Id_Rol);

            return PartialView(partial_view, result);
        }

        public ActionResult GetAll(string partial_view)
        {
            ReferenciasTipoManager manager = new ReferenciasTipoManager();
            var result = manager.Get();
            if (string.IsNullOrEmpty(partial_view))
            {
                return View(result);
            }
            return PartialView(partial_view, result);
        }
        public ActionResult Add(Referencias_Tipos model)
        {
            try
            {
                ReferenciasTipoManager manager = new ReferenciasTipoManager();
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
	}
}