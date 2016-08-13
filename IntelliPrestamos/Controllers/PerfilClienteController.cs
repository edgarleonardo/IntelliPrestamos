using IntelliPrestamo.AccessLayer.DataManager;
using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntelliPrestamos.Controllers
{
    public class PerfilClienteController : BaseController.BaseController
    {
        //
        // GET: /PerfilCliente/
        public ActionResult Manage(string Cedula = "")
        {
            bussinessInfo();
            var result = new PerfilClienteViewModel();
            if (Cedula != "")
            {
                PrestamosManager prestamos = new PrestamosManager();
                SolicitudManager solicitud = new SolicitudManager();
                try
                {
                    initiateDropDownSolicitud();
                    FuenteIngresoManager manager_fuente_ingreso = new FuenteIngresoManager();
                    SolicitanteManager solicitante_Manager = new SolicitanteManager();
                    ReferenciasManager referencias_manager = new ReferenciasManager();
                    result.solicitante = solicitante_Manager.Get(Cedula);
                    result.Fuente_Ingreso = manager_fuente_ingreso.Get(Cedula);
                    result.referencias_Personales = referencias_manager.Get(Cedula);
                    result.Prestamos = prestamos.GetByLoan(Cedula);
                    // Solicitudes aprobadas
                    result.SolicitudAprobadas = solicitud.GetSolicitudByAprobadas(Cedula);
                    // Solicitudes aprobadas
                    result.SolicitudPendiente = solicitud.GetSolicitudByNoAprobadas(Cedula);
                }
                catch (Exception ex)
                {
                    Set_Message("Ha Ocurrido Un Error: " + ex.Message);
                }
            }
            return View(result);
        }
        public ActionResult Get(string Cedula, string partial_view)
        {
            var result = new PerfilClienteViewModel();

            PrestamosManager prestamos = new PrestamosManager();
            SolicitudManager solicitud = new SolicitudManager();
            try
            {
                initiateDropDownSolicitud();
                FuenteIngresoManager manager_fuente_ingreso = new FuenteIngresoManager();
                SolicitanteManager solicitante_Manager = new SolicitanteManager();
                ReferenciasManager referencias_manager = new ReferenciasManager();
                    result.solicitante = solicitante_Manager.Get(Cedula);
                    result.Fuente_Ingreso = manager_fuente_ingreso.Get(Cedula);
                    result.referencias_Personales = referencias_manager.Get(Cedula);
                    result.Prestamos = prestamos.GetByLoan(Cedula);
                // Solicitudes aprobadas
                result.SolicitudAprobadas = solicitud.GetAccionista(Cedula, 3);
                // Solicitudes aprobadas
                result.SolicitudPendiente = solicitud.GetAccionista(Cedula, 1);
            }
            catch (Exception ex)
            {
                Set_Message("Ha Ocurrido Un Error: " + ex.Message);
            }
            return PartialView(partial_view, result);
        }
        [HttpPost]
        public ActionResult Save(PerfilClienteViewModel model)
        {
            FuenteIngresoManager manager_fuente_ingreso = new FuenteIngresoManager();
            SolicitanteManager solicitante_Manager = new SolicitanteManager();
            ReferenciasManager referencias_manager = new ReferenciasManager();
            try
            {
                bussinessInfo();
                initiateDropDownSolicitud();
                
                model.Fuente_Ingreso.Cedula = model.solicitante.Cedula;
                
                solicitante_Manager.Set(model.solicitante);
                manager_fuente_ingreso.Set(model.Fuente_Ingreso);
                foreach (Referencias_Personales references in model.referencias_Personales)
                {
                    if (references.Cedula == null || references.Cedula.Trim() == "")
                    {
                        references.Cedula = model.solicitante.Cedula;
                    }
                    if (!string.IsNullOrEmpty(references.Nombre) && !string.IsNullOrEmpty(references.Telefono))
                    {
                        referencias_manager.Set(references);
                    }
                }
                Set_Message("Datos Guardados Correctamente");

                
            }
            catch (Exception ex)
            {
                Set_Message("Ha Ocurrido Un Error: " + ex.Message);
            }

            return View("Manage", model);
        }
        
	}
}