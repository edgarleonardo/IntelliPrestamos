using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamo.AccessLayer.DataManager
{
    public class PrestamosManager  : BaseManager<Prestamos>
    {
        public PrestamosManager()
            : base()
        {
            
        }
        public void Del(int id_prestamo)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@id_prestamo", id_prestamo)};
            var result = Get("DEL_Prestamos_Incobrables_BY @id_prestamo", parameters);
            if (result != null && result.Count > 0 && result[0].errorMessage.Trim() != "")
            {
                Error_Message = result[0].errorMessage;
            }
            if (Error_Message != null && !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }

        }
        public Prestamos Get(int id_prestamo)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@prestamo_id", id_prestamo)};
            var result = Get("GET_prestamos_By_Id @prestamo_id", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            else
            {
                Prestamos cargo = new Prestamos();
                if (result.Count > 0)
                {
                    cargo = result[0];
                }
                return cargo;
            }
        }

        public List<Prestamos> GetByAccionistaLoan(string Cedula, string startdate, string enddate)
        {
            var parameters = new SqlParameter[]{new SqlParameter("@cedula", Cedula),
            new SqlParameter("@start_date", startdate),
            new SqlParameter("@end_date", enddate)
            };
            var result = Get("GET_prestamos_By_Accionista_date @cedula, @start_date, @end_date", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            return result;
        }
        public List<Prestamos> GetByAccionistaLoan(string Cedula)
        {
            var parameters = new SqlParameter[]{new SqlParameter("@cedula", Cedula)
            };
            var result = Get("GET_prestamos_By_Accionista @cedula", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            return result;
        }
        public List<Prestamos> GetByLoanPending(string Cedula)
        {
            var parameters = new SqlParameter[]{new SqlParameter("@cedula", Cedula)
            };
            var result = Get("GET_prestamos_A_Pagar @cedula", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            return result;
        }
        public List<Prestamos> GetByLoan(string Cedula)
        {
            var parameters = new SqlParameter[]{new SqlParameter("@cedula", Cedula)
            };
            var result = Get("GET_prestamos_By_Cedula @cedula", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            return result;
        }
        public List<Prestamos> GetByLoanPendingAll()
        {
            var result = Get("GET_prestamos_A_Pagar_All");
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            return result;
        }
        public List<Prestamos> GetPayment(string Cedula, int id_solicitud, int id_prestamo, string accionista)
        {
            var parameters = new SqlParameter[]{
                new SqlParameter("@id_solicitud", id_solicitud),
                new SqlParameter("@id_prestamo", id_prestamo),
                    new SqlParameter("@cedula", Cedula),            
            new SqlParameter("@accionista", accionista)
            };
            var result = Get("GET_prestamos_For_Payment @id_solicitud, @id_prestamo, @cedula, @accionista", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            return result;
        }
        public List<Prestamos> Get(string Cedula, int id_solicitud, int id_prestamo, string aprobado_por, int tipo_sol, int periodicidad)
        {
            var parameters = new SqlParameter[]{
                new SqlParameter("@ID_Solicitud", id_solicitud),
                new SqlParameter("@id_prestamo", id_prestamo),
                    new SqlParameter("@cedula", Cedula),            
            new SqlParameter("@aprobado_por", aprobado_por),
            new SqlParameter("@tipo_solicitud", tipo_sol),
            new SqlParameter("@periodicidad_id", periodicidad)};
            var result = Get("GET_prestamos @ID_Solicitud, @id_prestamo, @cedula, @tipo_solicitud, @periodicidad_id", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            return result;
        }
        public Prestamos Set(int solicitud_id,  string updateBy)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@id_solicitud", solicitud_id), 
                    new SqlParameter("@CREATE_BY",updateBy)
                    
            };
            var result = Get(@"SET_prestamos @id_solicitud,@CREATE_BY", parameters);
            if (result != null && result.Count > 0 && result[0].errorMessage.Trim() != "")
            {
                Error_Message = result[0].errorMessage;
            }
            if (Error_Message != null && Error_Message.Trim() != "")
            {
                throw new Exception(Error_Message);
            }
            Prestamos cargo = new Prestamos();
            if (result.Count > 0)
            {
                cargo = result[0];
            }
            return cargo;
        }
    }
}
