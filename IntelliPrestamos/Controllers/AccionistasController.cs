using IntelliPrestamo.AccessLayer.DataManager;
using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntelliPrestamos.Controllers
{
    public class AccionistasController : BaseController.BaseController
    {
        private EmpleadoManager manager;
        public ActionResult Manage()
        {
            manager = new EmpleadoManager();
            bussinessInfo();
            var result = manager.GetAllAccionistas();
            #region PreparandoArchivoDescarga
            string fileName = "Accionistas.csv";
            //Areglando lista.
            var dataReport = (from a in result
                              select new
                              {
                                  Cedula = a.Cedula.ToString(),
                                  Nombre = a.Nombre,
                                  Apellidos = a.Apellidos.ToString(),
                                  Monto_Inversion_Actual = a.inversion.Monto_Inversion_Actual,
                                  Monto_Balance = a.inversion.Monto_Balance,
                                  Monto_Prestados = a.inversion.Monto_Prestados,
                                  Monto_Ganancia = a.inversion.Monto_Ganancia
                              }
                ).ToList();
            Create_Excel_Reports(fileName, dataReport);
            #endregion
            return View(result);
        }

        public ActionResult GetInvestment(string cedula, string view)
        {
            InversionManager managerInvestment = new InversionManager();
            initiateDropDown();
            bussinessInfo();
            var result = managerInvestment.GetInversion(cedula);
            result.Cedula = cedula;
            return View(view, result);
        }
        public ActionResult Get(string Id_Rol, string partial_view)
        {
            InversionManager managerInvestment = new InversionManager();

            var result = managerInvestment.GetInversion(Id_Rol);
            result.Cedula = Id_Rol;
            return PartialView(partial_view, result);
        }
        [HttpPost]
       
        public ActionResult Add(string Cedula, double Monto, int Tipo_Trans, string Comentario)
        {
            InversionManager managerInvestment = new InversionManager();
            initiateDropDown();
            bussinessInfo();
            managerInvestment.SetInversion(Cedula, Monto, Tipo_Trans, 0, Comentario);
            var result = managerInvestment.GetInversion(Cedula);
            return View(result);
        }
        
        public ActionResult Modify(Inversion inversion)
        {
            try
            {
                InversionManager managerInvestment = new InversionManager();
                managerInvestment.SetGanancias(inversion.Cedula, inversion.Monto_Ganancia, inversion.tipo_trans);
                var result = managerInvestment.GetInversion(Cedula);

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