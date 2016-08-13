using IntelliPrestamo.AccessLayer.DataManager;
using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntelliPrestamos.Controllers
{
    public class PerfilController : BaseController.BaseController
    {
        private EmpleadoManager manager;
        //
        // GET: /Perfil/
        public ActionResult Manage(string cedula, string FechaInicio ="", string FechaFin="")
        {
            var result = new PerfilViewModel();
            manager = new EmpleadoManager();
            InversionManager inversiones = new InversionManager();
            InversionDetalleManager inversionesDetalles = new InversionDetalleManager();
            PrestamosManager prestamos = new PrestamosManager();
            SolicitudManager solicitud = new SolicitudManager();
            try
            {

                bussinessInfo();
                 result.empleado = manager.GetBy(cedula);
                 result.inversiones = inversiones.GetInversion(cedula);
                 result.inversiones_detalles = inversionesDetalles.GetInversionDetalle(cedula, FechaInicio, FechaFin);
                 result.Prestamos = prestamos.GetByAccionistaLoan(cedula, FechaInicio, FechaFin);
                 // Solicitudes aprobadas
                 result.SolicitudAprobadas = solicitud.GetAccionista(cedula, 3, FechaInicio, FechaFin);
                 // Solicitudes aprobadas
                 result.SolicitudPendiente = solicitud.GetAccionista(cedula, 1, FechaInicio, FechaFin);
            }
            catch(Exception ex)
            {
                Set_Message("Ha Ocurrido Un Error: " + ex.Message);
            }
            return View(result);
        }
	}
}