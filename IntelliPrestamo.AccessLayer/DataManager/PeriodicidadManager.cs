using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamo.AccessLayer.DataManager
{
    public class PeriodicidadManager : BaseManager<Periodicidad>
    {
        public PeriodicidadManager()
            : base()
        {
            
        }

        public List<Periodicidad> Get()
        {
            var result = Get("GET_PERIODICIDAD");
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message); 
            }
            else
            {
                return result;
            }
        }

        public Periodicidad Get(int periodicidad_id)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@PERIODICIDAD_ID", periodicidad_id)};
            var result = Get("GET_PERIODICIDAD_BY @PERIODICIDAD_ID", parameters);
            if (result == null  || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            else
            {
                Periodicidad cargo = new Periodicidad();
                if (result.Count > 0)
                {
                    cargo=result[0];
                } 
                return cargo;
            }
        }

    }
}
