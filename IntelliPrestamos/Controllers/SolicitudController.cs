using IntelliPrestamo.AccessLayer.DataManager;
using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntelliPrestamos.Controllers
{
    public class SolicitudController : BaseController.BaseController
    {
        private SolicitudManager manager;
        private FuenteIngresoManager manager_fuente_ingreso;
        private SolicitanteManager solicitante_Manager;
        private ReferenciasManager referencias_manager;
        //
        // GET: /Solicitud/
        public ActionResult Add(int id = 0)
        {
            manager = new SolicitudManager();
            manager_fuente_ingreso = new FuenteIngresoManager();
            solicitante_Manager = new SolicitanteManager();
            referencias_manager = new ReferenciasManager();
            
            bussinessInfo();
            initiateDropDownSolicitud();
            var result = new Solicitud();
            if (id != 0)
            {
                var listResult = manager.Get("", id, "", 0, 0);
                if (listResult != null && listResult.Count > 0)
                {
                    result = listResult[0];
                    result.solicitante = solicitante_Manager.Get(result.Cedula);
                    result.Fuente_Ingreso = manager_fuente_ingreso.Get(result.Cedula);
                    result.referencias_Personales = referencias_manager.Get(result.Cedula);
                    DocumentManager doc = new DocumentManager();
                    result.Documentos = doc.Get(int.Parse(ConfigurationManager.AppSettings["FlujoSolicitud"]));
                } 
            }            
            return View(result);
        }
        [HttpPost]
        public ActionResult Solicitante(string Cedula)
        {
            manager = new SolicitudManager();
            manager_fuente_ingreso = new FuenteIngresoManager();
            solicitante_Manager = new SolicitanteManager();
            referencias_manager = new ReferenciasManager();
            bussinessInfo();
            initiateDropDownSolicitud();
            var result = new Solicitud();
            if (Cedula != "")
            {   
                    result.solicitante = solicitante_Manager.Get(Cedula);
                    result.Fuente_Ingreso = manager_fuente_ingreso.Get(Cedula);
                    result.referencias_Personales = referencias_manager.Get(Cedula);
            }
            return Json(result, JsonRequestBehavior.AllowGet);;
        }
        public ActionResult Prestamo(int id = 0)
        {
            return Add(id);
        }
        [HttpPost]
        public ActionResult SetPrestamo(int solicitud_id,  string view)
        {
            PrestamosManager prestamos = new PrestamosManager();
            var result = new Prestamos();
            try
            {
                initiateDropDownSolicitud();
                Datos_Sistema data = bussinessInfo();
                if (solicitud_id != 0)
                {
                    var listResult = prestamos.Set(solicitud_id, ViewBag.LoggedId);
                    if (listResult != null )
                    {
                        result = listResult;
                        result.Intereses_Pagados = result.Monto_Aprobado*(data.porc_gastos_cierre_max/100);
                        if (data.gasto_cierre_MIN <= result.Monto_Aprobado)
                        {
                            result.Intereses_Pagados = data.gasto_cierre_referencia_min;
                        }
                        
                        DocumentManager doc = new DocumentManager();
                        result.Documentos = doc.Get(int.Parse(ConfigurationManager.AppSettings["FlujoPrestamos"]));
                        TablaAmortizacionManager tablManager = new TablaAmortizacionManager();
                        result.amortizacion = tablManager.Get(result.Prestamo_Id);
                    }
                }
                return PartialView(view, result);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMen = ex.Message;
            }
            return PartialView(view, result);
        }
        public ActionResult Autorizar(int id = 0)
        {
            return Add(id);
        }
        public ActionResult Views()
        {
            manager = new SolicitudManager();
            bussinessInfo();
            initiateDropDownSolicitud();
            var result = manager.Get("", 0, "", 1, 0);
            if (result != null && result.Count > 0)
            {
                foreach (Solicitud sol in result)
                {
                    solicitante_Manager = new SolicitanteManager();
                    sol.solicitante = solicitante_Manager.Get(sol.Cedula);
                }
            }
            #region PreparandoArchivoDescarga
            string fileName = "SolicitudCreadas.csv";
            //Areglando lista.
            var dataReport = (from a in result
                              select new
                              {
                                  Cedula = a.Cedula.ToString(),
                                  Monto_Prestamo = a.Monto_Prestamo.ToString(),
                                  Ingresos_Solicitante = a.Ingresos_Solicitante.ToString(),
                                  Periodicidad_Desc = a.Periodicidad_Desc,
                                  Interes = a.Interes,
                                  Plazo = a.Plazo,
                                  Cobrador = a.Cobrador,
                                  Nombre = (a.solicitante.Nombre + " " + a.solicitante.Apellidos)
                              }
                ).ToList();
            Create_Excel_Reports(fileName, dataReport);
            #endregion
            return View(result);
        }
        public ActionResult Manage()
        {
            return Views();
        }
        public ActionResult Aprobadas()
        {
            manager = new SolicitudManager();
            bussinessInfo();
            initiateDropDownSolicitud();
            var result = manager.Get("", 0, "", 3, 0);
            if (result != null && result.Count > 0)
            {
                foreach (Solicitud sol in result)
                {
                    solicitante_Manager = new SolicitanteManager();
                    sol.solicitante = solicitante_Manager.Get(sol.Cedula);
                }
            }
            #region PreparandoArchivoDescarga
            string fileName = "SolicitudAprobadas.csv";
            //Areglando lista.
            var dataReport = (from a in result
                              select new
                              {
                                  Cedula = a.Cedula.ToString(),
                                  Monto_Prestamo = a.Monto_Prestamo.ToString(),
                                  Ingresos_Solicitante = a.Ingresos_Solicitante.ToString(),
                                  Periodicidad_Desc = a.Periodicidad_Desc,
                                  Interes = a.Interes,
                                  Plazo = a.Plazo,
                                  Cobrador = a.Cobrador,
                                  Nombre = (a.solicitante.Nombre+" "+a.solicitante.Apellidos)
                              }
                ).ToList();
            Create_Excel_Reports(fileName, dataReport);
            #endregion
            return View(result);
        }
        public ActionResult Rechazadas()
        {
            manager = new SolicitudManager();
            bussinessInfo();
            initiateDropDownSolicitud();
            var result = manager.GetCanceladas();
            if (result != null && result.Count > 0)
            {
                foreach (Solicitud sol in result)
                {
                    solicitante_Manager = new SolicitanteManager();
                    sol.solicitante = solicitante_Manager.Get(sol.Cedula);
                }
            }
            return View( result);
        }
        public ActionResult GetAll(string partial_view)
        {
            manager = new SolicitudManager();
            
            var result = manager.Get("", 0, "", 1, 0);

            #region PreparandoArchivoDescarga
            string fileName = "SolicitudCreadas.csv";
            //Areglando lista.
            var dataReport = (from a in result
                              select new
                              {
                                  Cedula = a.Cedula.ToString(),
                                  Monto_Prestamo = a.Monto_Prestamo.ToString(),
                                  Ingresos_Solicitante = a.Ingresos_Solicitante.ToString(),
                                  Periodicidad_Desc = a.Periodicidad_Desc,
                                  Interes = a.Interes,
                                  Plazo = a.Plazo,
                                  Cobrador = a.Cobrador
                              }
                ).ToList();
            Create_Excel_Reports(fileName, dataReport);
            #endregion
            return PartialView(partial_view, result);
        }

        public ActionResult Delete(int id_solicitud)
        {
            manager = new SolicitudManager();
            
            var result = new Solicitud();
            try
            {
                if (id_solicitud != 0)
                {
                   manager.Det(id_solicitud);                    
                }
            }
            catch(Exception ex)
            {
                return Content("false");
            }
            return Content("true");
        } 
        public ActionResult GetSingle(int Id_Rol, string partial_view)
        {
            manager = new SolicitudManager();
            bussinessInfo();
            initiateDropDownSolicitud();
            var result = new Solicitud();
            if (Id_Rol != 0)
            {
                var listResult = manager.Get("", Id_Rol, "", 1, 0);
                if (listResult != null && listResult.Count > 0)
                {
                    result = listResult[0];
                    solicitante_Manager = new SolicitanteManager();
                    result.solicitante = solicitante_Manager.Get(result.Cedula);
                } 
            }
            return PartialView(partial_view,result);
        } 
        
        [HttpPost]
        public ActionResult Save(Solicitud model)
        {
            manager = new SolicitudManager();
            manager_fuente_ingreso = new FuenteIngresoManager();
            solicitante_Manager = new SolicitanteManager();
            referencias_manager = new ReferenciasManager();
            try
            {
                bussinessInfo();
                initiateDropDownSolicitud();
                model.solicitante.CopiaCedula = Upload_File("fileUpload", model.solicitante.Cedula);
                var result = new Solicitud();

                model.Fuente_Ingreso.Cedula = model.Cedula = model.solicitante.Cedula;
                model.Ingresos_Solicitante = model.Fuente_Ingreso.Sueldo_Actual;
                solicitante_Manager.Set(model.solicitante);
                manager_fuente_ingreso.Set(model.Fuente_Ingreso);
                foreach(Referencias_Personales references in model.referencias_Personales)
                {
                    if (references.Cedula == null || references.Cedula.Trim() == "")
                    {
                        references.Cedula = model.solicitante.Cedula;
                    }
                    if (!string.IsNullOrEmpty(references.Nombre) &&  !string.IsNullOrEmpty(references.Telefono))
                    {
                        referencias_manager.Set(references);
                    }
                }
                model.Create_By = ViewBag.LoggedId;
                if (model.Comentario == null)
                {
                    model.Comentario = "";
                }
                if (model.Comentario_Credito == null)
                {
                    model.Comentario_Credito = "";
                }
                if (model.Accionista == null)
                {
                    model.Accionista = "";
                }
               
                result = manager.Set(model);
                result.solicitante = solicitante_Manager.Get(model.Cedula);
                result.Fuente_Ingreso = manager_fuente_ingreso.Get(model.Cedula);
                result.referencias_Personales = referencias_manager.Get(model.Cedula);
                DocumentManager doc = new DocumentManager();
                result.Documentos = doc.Get(int.Parse(ConfigurationManager.AppSettings["FlujoSolicitud"]));
                Set_Message("Datos Guardados Correctamente");
                
                return View("Add", result);
            }
            catch (Exception ex) {
                Set_Message("Ha Ocurrido Un Error: "+ex.Message);
            }
            
            return View("Add",model);
        }
        [HttpPost]
        public ActionResult Autorize(Solicitud model)
        {
            manager = new SolicitudManager();
            manager_fuente_ingreso = new FuenteIngresoManager();
            solicitante_Manager = new SolicitanteManager();
            referencias_manager = new ReferenciasManager();
            try
            {
                bussinessInfo();
                initiateDropDownSolicitud();
                var result = new Solicitud();

                model.Fuente_Ingreso.Cedula = model.Cedula = model.solicitante.Cedula;

                solicitante_Manager.Set(model.solicitante);
                manager_fuente_ingreso.Set(model.Fuente_Ingreso);
                foreach(Referencias_Personales references in model.referencias_Personales)
                {
                    if (references.Cedula == null || references.Cedula.Trim() == "")
                    {
                        references.Cedula = model.solicitante.Cedula;
                    }
                    if (!string.IsNullOrEmpty(references.Nombre) &&  !string.IsNullOrEmpty(references.Telefono))
                    {
                        referencias_manager.Set(references);
                    }
                }
                model.Create_By = ViewBag.LoggedId;
                if (model.Comentario == null)
                {
                    model.Comentario = "";
                }
                
                result = manager.Set(model);
                result.solicitante = solicitante_Manager.Get(model.Cedula);
                result.Fuente_Ingreso = manager_fuente_ingreso.Get(model.Cedula);
                result.referencias_Personales = referencias_manager.Get(model.Cedula);
                DocumentManager doc = new DocumentManager();
                result.Documentos = doc.Get(int.Parse(ConfigurationManager.AppSettings["FlujoSolicitud"]));
                if (model.Status_Id == 4)
                {
                    UsuarioManager empleado = new UsuarioManager();
                    var empleadoInfo = empleado.GetBy(model.Create_By);
                    sendMail(empleadoInfo.Email, "Solicitud #" + model.Id_Solicitud.ToString() + " tiene una revision", model.Comentario_Credito);
                }
                Set_Message("Datos Guardados Correctamente");
                
                return View("Autorizar", result);
            }
            catch (Exception ex) {
                Set_Message("Ha Ocurrido Un Error: "+ex.Message);
            }

            return View("Autorizar", model);
        }
        
            
	}
}