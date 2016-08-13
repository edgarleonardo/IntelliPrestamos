using IntelliPrestamo.AccessLayer.AppTools;
using IntelliPrestamo.AccessLayer.DataManager;
using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntelliPrestamos.Controllers
{
    public class DocumentosController : BaseController.BaseController
    {
        private DocumentManager manager;
        //
        // GET: /Cargo/

        public ActionResult Manage()
        {
            bussinessInfo();
            return GetAll("");
        }

        public ActionResult Get(int Id_Rol, string partial_view)
        {
            manager = new DocumentManager();
            var result = manager.GetById(Id_Rol);
            ViewBag.drpFlujo = GetDrpFlujo();
            SystemVariableManager sisVar = new SystemVariableManager();
            ViewBag.VariablesSystema = sisVar.Get();
            return PartialView(partial_view, result);
        }

        public ActionResult DownloadDoc(int solicitud_id, int prestamo_id, string cedula, int document_id)
        {
            manager = new DocumentManager();
            var result = manager.DocumentParseWithVariables(solicitud_id, prestamo_id, cedula, document_id);
            var documentObject = manager.GetById(document_id);
            string loc = Server.MapPath("~/");
            PDFConverter pdf = new PDFConverter();
            string fileName = pdf.GeneratePdfFromString(result, documentObject.Nombre_Documento, /*ConfigurationManager.AppSettings["LocalRoot"]*/loc, ConfigurationManager.AppSettings["RemoteRoot"]);
            return File(fileName, "application/pdf", documentObject.Nombre_Documento+".pdf"); ;
        }
         public ActionResult DownloadHtmlReports(string Action, string Url)
        {
            PDFConverter pdf = new PDFConverter();
            string loc = Server.MapPath("~/");
            string fileName = pdf.GeneratePdfFromUrl(Action, loc /*ConfigurationManager.AppSettings["LocalRoot"]*/, Url, ConfigurationManager.AppSettings["RemoteRoot"] + "/");
            return File(fileName, "application/pdf", Action + ".pdf"); ;
        }
         public ActionResult DownloadImageReports(string fileName)
         {
             string path = Server.MapPath("~/content/") + fileName;
             string extension = Path.GetExtension(path);
             string filename = Path.GetFileName(path);
             return File(path, "application/" + extension, filename); 
         }
        public ActionResult GetAll(string partial_view)
        {
            manager = new DocumentManager();
            var result = manager.Get();
            if (string.IsNullOrEmpty(partial_view))
            {
                return View(result);
            }
            return PartialView(partial_view, result);
        }
        [HttpPost]
        [ValidateInput(false)]        
        public ActionResult Add(Document model)
        {
            try
            {
                manager = new DocumentManager();
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
        
        public ActionResult Delete(Document model)
        {
            try
            {
                manager = new DocumentManager();
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