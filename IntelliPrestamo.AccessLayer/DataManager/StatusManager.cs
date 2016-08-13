using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamo.AccessLayer.DataManager
{
    public class StatusManager : BaseManager<StatusTable>
    {
        public StatusManager()
            : base()
        {
            
        }

        public List<StatusTable> Get(int flujo_id)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@flujo_id", flujo_id)};
            var result = Get("GET_STATUS @flujo_id", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            return result;
        }
    }
}
