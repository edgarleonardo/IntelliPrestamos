using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamo.AccessLayer.DataManager
{
    public class TablaAmortizacionManager : BaseManager<Tabla_Amortizacion>
    {
        public TablaAmortizacionManager()
            : base()
        {
            
        }

        public List<Tabla_Amortizacion> Get(int prestamo_id)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@prestamo_id", prestamo_id)};
            var result = Get("GET_TABLA_AMORTIZACION @prestamo_id", parameters);
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
