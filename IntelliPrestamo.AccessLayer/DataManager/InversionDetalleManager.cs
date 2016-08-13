using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamo.AccessLayer.DataManager
{
    public class InversionDetalleManager : BaseManager<Inversion_Detalle>
    {
        public InversionDetalleManager()
            : base()
        {
            
        }
        public List<Inversion_Detalle> GetInversionDetalle(string cedula)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@cedula", cedula)};
            var result = Get("GET_inversion_trans @cedula", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }

            return result;
        }

        public List<Inversion_Detalle> GetInversionDetalle(string cedula, string startdate, string enddate)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@cedula", cedula),
            new SqlParameter("@start_date", startdate),
            new SqlParameter("@end_date", enddate)};
            var result = Get("GET_inversion_trans_by_date @cedula,@start_date,@end_date", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }

            return result;
        }
    }
}
