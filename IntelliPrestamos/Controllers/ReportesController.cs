using IntelliPrestamo.AccessLayer.DataManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntelliPrestamos.Controllers
{
    public class ReportesController : BaseController.BaseController
    {
        //
        // GET: /Reportes/
        public ActionResult Ganancias()
        {
            bussinessInfo();
            return View();
        }
        public ActionResult GetGanancias(string cedula = "", int prestamo_id = 0, string accionista = "", string fechaInicio = "", string fechaFin = "", string view = "")
        {
            ReportesManager rp = new ReportesManager();
            var result = rp.GetGanancias(cedula, prestamo_id, accionista, fechaInicio, fechaFin);
            #region PreparandoArchivoDescarga
            string fileName = "Ganancias.csv";
            //Areglando lista.
            var dataReport = (from a in result
                              select new
                              {
                                  Cedula = a.Cedula.ToString(),
                                  Nombre = a.Nombre,
                                  Accinista_Nombre = a.Accinista_Nombre.ToString(),
                                  Monto_Intereses = a.Monto_Intereses.ToString(),
                                  fecha = a.fecha.ToString()
                              }
                ).ToList();
            Create_Excel_Reports(fileName, dataReport);
            #endregion
            return PartialView(view, result);
        }
        public ActionResult Perdidas()
        {
            bussinessInfo();
            return View();
        }
        public ActionResult GetPerdida(string cedula = "", int prestamo_id = 0, string accionista = "", string fechaInicio = "", string fechaFin = "", string view = "")
        {
            ReportesManager rp = new ReportesManager();
            var result = rp.GetPerdidas(cedula, prestamo_id, accionista, fechaInicio, fechaFin);
            #region PreparandoArchivoDescarga
            string fileName = "Perdidas.csv";
            //Areglando lista.
            var dataReport = (from a in result
                              select new
                              {
                                  Cedula = a.Cedula.ToString(),
                                  Nombre = a.Nombre,
                                  Accinista_Nombre = a.Accinista_Nombre.ToString(),
                                  Monto_Intereses = a.Monto_Intereses.ToString()
                              }
                ).ToList();
            Create_Excel_Reports(fileName, dataReport);
            #endregion
            return PartialView(view, result);
        }
        public ActionResult GetPrestamos(string cedula = "", int prestamo_id = 0, string accionista = "", string fechaInicio = "", string fechaFin = "", string view = "")
        {
            ReportesManager rp = new ReportesManager();
            var result = rp.GetPrestamos(cedula, prestamo_id, accionista, fechaInicio, fechaFin);
            #region PreparandoArchivoDescarga
            string fileName = "Prestamos.csv";
            //Areglando lista.
            var dataReport = (from a in result
                              select new
                              {
                                  Cedula = a.Cedula.ToString(),
                                  Nombre = a.Nombre,
                                  Accinista_Nombre = a.Accinista_Nombre.ToString(),
                                  status_desc = a.status_desc.ToString(),
                                  monto_aprobado = a.monto_aprobado.ToString(),
                                  fecha_desembolso = a.fecha_desembolso.ToString(),
                                  fecha_cancelacion = a.fecha_cancelacion.ToString()
                              }
                ).ToList();
            Create_Excel_Reports(fileName, dataReport);
            #endregion
            return PartialView(view, result);
        }
        public ActionResult Prestamos()
        {
            bussinessInfo();
            return View();
        }
	}
}