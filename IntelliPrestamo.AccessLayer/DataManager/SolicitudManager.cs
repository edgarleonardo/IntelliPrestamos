using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamo.AccessLayer.DataManager
{
    public class SolicitudManager : BaseManager<Solicitud>
    {
        public SolicitudManager()
            : base()
        {
            
        }
        public void Det(int id_solicitud)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@ID_Solicitud", id_solicitud)};
            Execute("DEL_SOLICITUD_BY @ID_Solicitud", parameters);
            if (Error_Message != null && !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
           
        }
        public Solicitud Get(int tipo_solicitud)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@tipo_solicitud", tipo_solicitud)};
            var result = Get("GET_SOLICITUD @tipo_solicitud", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            else
            {
                Solicitud cargo = new Solicitud();
                if (result.Count > 0)
                {
                    cargo = result[0];
                }
                return cargo;
            }
        }
        public List<Solicitud> Get(string Cedula, int id_solicitud, string usuario_int_bank, int tipo_sol, int periodicidad)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@cedula", Cedula),
            new SqlParameter("@ID_Solicitud", id_solicitud),
            new SqlParameter("@PERIODICIDAD_ID", periodicidad),
            new SqlParameter("@USUARIO_INT_BANKING", usuario_int_bank),
            new SqlParameter("@tipo_solicitud", tipo_sol)};
            var result = Get("GET_SOLICITUD_BY @cedula, @ID_Solicitud, @PERIODICIDAD_ID, @USUARIO_INT_BANKING, @tipo_solicitud", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            return result;
        }
        public List<Solicitud> GetSolicitudByCedula(string Cedula, int tipo_sol)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@cedula", Cedula),
            
            new SqlParameter("@tipo_solicitud", tipo_sol)};
            var result = Get("GET_SOLICITUD_BY_Cedula @cedula, @tipo_solicitud", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            return result;
        }
        public List<Solicitud> GetSolicitudByAprobadas(string Cedula)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@cedula", Cedula)};
            var result = Get("GET_SOLICITUD_BY_AProbadas @cedula", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            return result;
        }
        public List<Solicitud> GetSolicitudByNoAprobadas(string Cedula)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@cedula", Cedula)};
            var result = Get("GET_SOLICITUD_BY_NOAprobadas @cedula", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            return result;
        }
        public List<Solicitud> GetAccionista(string Cedula, int tipo_sol, string startdate, string enddate)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@cedula", Cedula),
            new SqlParameter("@tipo_solicitud", tipo_sol),
            new SqlParameter("@start_date", startdate),
            new SqlParameter("@end_date", enddate)};
            var result = Get("GET_SOLICITUD_BY_Accionista_date @cedula, @tipo_solicitud,@start_date,@end_date", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            return result;
        }
        public List<Solicitud> GetAccionista(string Cedula, int tipo_sol)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@cedula", Cedula),
            new SqlParameter("@tipo_solicitud", tipo_sol)};
            var result = Get("GET_SOLICITUD_BY_Accionista @cedula, @tipo_solicitud", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            return result;
        }
        public List<Solicitud> GetCanceladas()
        {
            var result = Get("GET_SOLICITUD_CANCELADAS");
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            return result;
        }
        public Solicitud Set(Solicitud model)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@PERIODICIDAD_ID", model.Periodicidad_Id), 
                    new SqlParameter("@DIA_PAGO", model.Dia_Pago),
                    new SqlParameter("@CEDULA", model.Cedula),
                    new SqlParameter("@INGRESOS_SOLICITANTE", model.Ingresos_Solicitante),
                    new SqlParameter("@COPIA_CEDULA", model.Copia_Cedula),
                    new SqlParameter("@TARJETA_COBRO", model.Tarjeta_Cobro), 
                    new SqlParameter("@ID_BANCOS", model.Id_Banco),
                    new SqlParameter("@USUARIO_INT_BANKING", model.Usuario_Int_Banking),
                    new SqlParameter("@CLAVE_INT_BANKING", model.Clave_Int_Banking),
                    new SqlParameter("@ID_SOLICITUD", model.Id_Solicitud),
                    new SqlParameter("@STATUS_ID", model.Status_Id),
                    new SqlParameter("@PLAZO", model.Plazo),
                    new SqlParameter("@MONTO_PRESTAMO", model.Monto_Prestamo)     ,
                    new SqlParameter("@CREATE_BY", model.Create_By),
                    new SqlParameter("@interes", model.Interes)  ,
                    new SqlParameter("@comentario", model.Comentario)  ,
                     new SqlParameter("@comentario_credito", model.Comentario_Credito)  ,
                     new SqlParameter("@accionista", model.Accionista),
                     new SqlParameter("@cobrador", model.Cobrador) 
                    
            };
            var result = Get(@"SET_SOLICITUD @PERIODICIDAD_ID,@DIA_PAGO,@CEDULA,@INGRESOS_SOLICITANTE, @COPIA_CEDULA,@TARJETA_COBRO,@ID_BANCOS,@USUARIO_INT_BANKING,@CLAVE_INT_BANKING,@ID_SOLICITUD,@STATUS_ID,@PLAZO,@MONTO_PRESTAMO, @CREATE_BY,@interes,@comentario,@comentario_credito,@accionista,@cobrador", parameters);
            if (result != null && result.Count > 0 && result[0].errorMessage.Trim() != "")
            {
                Error_Message = result[0].errorMessage;
            }
            if (Error_Message != null && Error_Message.Trim() != "")
            {
                throw new Exception(Error_Message);
            }
            Solicitud cargo = new Solicitud();
            if (result.Count > 0)
            {
                cargo = result[0];
            }
            return cargo;
        }
    }
}
