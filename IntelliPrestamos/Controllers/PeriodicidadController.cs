using IntelliPrestamo.AccessLayer.DataManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntelliPrestamos.Controllers
{
    public class PeriodicidadController : BaseController.BaseController
    {
        // GET: /ReferenceTipos/
        public ActionResult Manage()
        {
            bussinessInfo();
            PeriodicidadManager manager = new PeriodicidadManager();
            var result = manager.Get();
            return View(result);
        }
        public ActionResult Get(int Id_Rol, string partial_view)
        {
            PeriodicidadManager manager = new PeriodicidadManager();
            var result = manager.Get(Id_Rol);

            return PartialView(partial_view, result);
        }

        public ActionResult GetAll(string partial_view)
        {
            PeriodicidadManager manager = new PeriodicidadManager();
            var result = manager.Get();
            if (string.IsNullOrEmpty(partial_view))
            {
                return View(result);
            }
            return PartialView(partial_view, result);
        }
	}
}