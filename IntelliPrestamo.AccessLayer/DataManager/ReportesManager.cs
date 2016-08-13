using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamo.AccessLayer.DataManager
{
    public class ReportesManager : BaseManager<Reportes>
    {
        public ReportesManager()
            : base()
        {
            
        }

        public List<Reportes> GetGanancias(string cedula, int prestamo_id, string accionista, string fechaInicio, string fechaFin)
        { 
            var parameters = new SqlParameter[]{
                    new SqlParameter("@CEDULA", cedula),
            new SqlParameter("@ACCIONISTA", accionista),
            new SqlParameter("@PRESTAMO_ID", prestamo_id),
            new SqlParameter("@FECHA_INICIO", fechaInicio),
            new SqlParameter("@FECHA_FIN", fechaFin)};
            var result = Get("GET_GANANCIAS @CEDULA, @ACCIONISTA,@PRESTAMO_ID, @FECHA_INICIO,@FECHA_FIN", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message); 
            }
            else
            {
                return result;
            }
        }
        public List<Reportes> GetPerdidas(string cedula, int prestamo_id, string accionista, string fechaInicio, string fechaFin)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@CEDULA", cedula),
            new SqlParameter("@ACCIONISTA", accionista),
            new SqlParameter("@PRESTAMO_ID", prestamo_id),
            new SqlParameter("@FECHA_INICIO", fechaInicio),
            new SqlParameter("@FECHA_FIN", fechaFin)};
            var result = Get("GET_PRESTAMOS_REPORT_PERDIDAS @CEDULA, @ACCIONISTA,@PRESTAMO_ID, @FECHA_INICIO,@FECHA_FIN", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            else
            {
                return result;
            }
        }
        public List<Reportes> GetPrestamos(string cedula, int prestamo_id, string accionista, string fechaInicio, string fechaFin)
        { 
            var parameters = new SqlParameter[]{
                    new SqlParameter("@CEDULA", cedula),
            new SqlParameter("@ACCIONISTA", accionista),
            new SqlParameter("@PRESTAMO_ID", prestamo_id),
            new SqlParameter("@FECHA_INICIO", fechaInicio),
            new SqlParameter("@FECHA_FIN", fechaFin)};
            var result = Get("GET_PRESTAMOS_REPORT @CEDULA, @ACCIONISTA,@PRESTAMO_ID, @FECHA_INICIO,@FECHA_FIN", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message); 
            }
            else
            {
                return result;
            }
        }
        
    }
}
