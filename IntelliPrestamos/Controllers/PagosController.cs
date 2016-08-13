using IntelliPrestamo.AccessLayer.AppTools;
using IntelliPrestamo.AccessLayer.DataManager;
using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntelliPrestamos.Controllers
{
    public class PagosController : BaseController.BaseController
    {
        private PrestamosManager manager;
        private CuotasManager CuoManager;
        private PagoManager PagoManager;
        private MoraManager moraManager;
        //
        // GET: /Pagos/
        public ActionResult Manage()
        {
            try
            {
                Prestamos loan = new Prestamos();
                initiateDropDownSolicitud();
                bussinessInfo();
                return View(loan);
            }
            catch(Exception ex)
            {
                Set_Message("Ha Ocurrido Un Error: " + ex.Message);
            }
            return View();
        }
        public ActionResult Pendientes()
        {
            try
            {
                manager = new PrestamosManager();
                initiateDropDownSolicitud();
                bussinessInfo();
                var result = manager.GetByLoanPending(Cedula);
                return View(result);
            }
            catch(Exception ex)
            {
                Set_Message("Ha Ocurrido Un Error: " + ex.Message);
            }
            return View();
        }
        public ActionResult Notificaciones()
        {
            try
            {
                manager = new PrestamosManager();
                initiateDropDownSolicitud();
                bussinessInfo();
                var result = manager.GetByLoanPendingAll();
                foreach(Prestamos prestamo in result)
                {
                    UsuarioManager empleado = new UsuarioManager();
                    if (prestamo.Cobrador != null)
                    {
                        var usuario = empleado.GetBy(prestamo.Cobrador);
                        sendMail(usuario.Email, "Pago Prestamo Pendiente #" + prestamo.Prestamo_Id.ToString(),
                            "El prestamo #" +
                            prestamo.Prestamo_Id.ToString() + " presenta atrasos.");
                    }

                    if (prestamo.Accionista != null)
                    {
                        var usuario = empleado.GetBy(prestamo.Accionista);
                        sendMail(usuario.Email, "Pago Prestamo Pendiente #" + prestamo.Prestamo_Id.ToString(),
                            "El prestamo #" +
                            prestamo.Prestamo_Id.ToString() + " presenta atrasos.");
                    }
                }
                return Content("Notificaciones Enviados...");
            }
            catch(Exception ex)
            {
                Set_Message("Ha Ocurrido Un Error: " + ex.Message);
            }
            return Content("");
        }
        
        public ActionResult Incobrable(int prestamo_id)
        {
            try
            {
                manager = new PrestamosManager();
                manager.Del(prestamo_id);
            }
            catch(Exception ex)
            {
                return Content(ex.Message);
            }
            return Content("Datos Guardados Exitosamente");
        }
        public ActionResult GetPrestamos(int prestamo_id, string partial_view)
        {
            manager = new PrestamosManager();
            var result = manager.Get( prestamo_id);
            return PartialView(partial_view, result);
        }
        public ActionResult GetAll(string cedula, int solicitud_id, int prestamo_id,string accionista,string partial_view)
        {
            manager = new PrestamosManager();
            bussinessInfo();
            var result = manager.GetPayment(cedula, solicitud_id, prestamo_id, Cedula);
            return PartialView(partial_view, result);
        }

        public ActionResult GetCuotaAndPayments(int Id_Rol, string partial_view)
        {
            CuoManager = new CuotasManager();
            PagoManager = new PagoManager();
            moraManager = new MoraManager();
            manager = new PrestamosManager();
            var result = CuoManager.Get(Id_Rol);
            List<CuotaPagoViewModel> CuotaPago = new List<CuotaPagoViewModel>();
            int counter = 0;
            foreach (Cuotas cuota in result)
            {
                CuotaPagoViewModel viewModel = new CuotaPagoViewModel();
                viewModel.cuota = cuota;
                viewModel.pago = PagoManager.Get(cuota.Prestamos_Id,cuota.Numero_Cuota);
                viewModel.mora = moraManager.Get(cuota.Prestamos_Id, cuota.Numero_Cuota); 
                CuotaPago.Add(viewModel);
                counter++;
            }
            ViewBag.Prestamos = manager.Get(Id_Rol); 
            return PartialView(partial_view, CuotaPago);
        }
        [HttpPost]       
        public ActionResult SetCuotaPayments(int id_prestamo, int numero_cuotas, decimal monto, int mora_status)
        {
            string error = "";
            try
            {
                PagoManager = new PagoManager();
                PagoManager.Set(id_prestamo, numero_cuotas, monto, mora_status);
                manager = new PrestamosManager();
                UsuarioManager empleado = new UsuarioManager();
                var resultPrestamo = manager.Get(id_prestamo);
                var Accionista = empleado.GetBy(resultPrestamo.Accionista);
                bussinessInfo();
                sendMail(Accionista.Email, "Pago Cuota Prestamo #" + id_prestamo.ToString() + " cuota #" + numero_cuotas.ToString(), "Se ha hecho un pago al numero de prestamo #" + id_prestamo.ToString() + " por el monto de " + monto.ToString() + ".");
            }
            catch(Exception ex)
            {
                error = ex.Message;
            }
            return Content(error);
        }

        
        [HttpPost]        
        public ActionResult SetCuotaPaymentsExtra(int id_prestamo, decimal monto, int afecta_prestamos)
        {
            string error = "";
            try
            {
                PagoManager = new PagoManager();
                PagoManager.SetExtra(id_prestamo, monto, afecta_prestamos);
                manager = new PrestamosManager();
                UsuarioManager empleado = new UsuarioManager();
                var resultPrestamo = manager.Get(id_prestamo);
                var Accionista = empleado.GetBy(resultPrestamo.Accionista);
                bussinessInfo();
                sendMail(Accionista.Email, "Pago Extraordinario Prestamo " + id_prestamo.ToString(), "Se ha hecho un pago al numero de prestamo #" + id_prestamo.ToString() + " por el monto de " + monto.ToString() + ".");
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return Content(error);
        }
        public ActionResult VolantesDePago( int prestamo_id, int numero_cuota, int pago_id)
        {
            PagoManager = new PagoManager();
            bussinessInfo();
            var result = PagoManager.GetPays(prestamo_id, numero_cuota, pago_id);
            return View("VolantesDePago", result);
        }
        public ActionResult TablaAmortizacion(int prestamo_id)
        {
            TablaAmortizacionManager tabla = new TablaAmortizacionManager();
            bussinessInfo();
            var result = tabla.Get(prestamo_id);
            return View("TablaAmortizacion", result);
        }
        
	}
}