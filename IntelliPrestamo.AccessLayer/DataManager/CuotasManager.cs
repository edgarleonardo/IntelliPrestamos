using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamo.AccessLayer.DataManager
{
    public class CuotasManager : BaseManager<Cuotas>
    {
        public CuotasManager()
            : base()
        {
            
        }
        public List<Cuotas> Get(int prestamos_id)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@id_prestamo", prestamos_id)};
            var result = Get("GET_Cuotas @id_prestamo", parameters);
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
