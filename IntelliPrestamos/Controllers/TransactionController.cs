using IntelliPrestamo.AccessLayer.DataManager;
using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntelliPrestamos.Controllers
{
    public class TransactionController : BaseController.BaseController
    {
        private TransactionManager manager;
        //
        // GET: /Transaction/
        public ActionResult Manage()
        {
            bussinessInfo();
            manager = new TransactionManager();
            var result = manager.GetAll();
            return View(result);
        }
        public ActionResult Get(int Id_Rol, string partial_view)
        {
            manager = new TransactionManager();
            var result = manager.GetBy(Id_Rol);

            return PartialView(partial_view, result);
        }

        public ActionResult GetAll( string partial_view)
        {
            manager = new TransactionManager();
            var result = manager.GetAll();

            return PartialView(partial_view, result);
        }
        public ActionResult Add(Transaction model)
        {
            try
            {
                manager = new TransactionManager();
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