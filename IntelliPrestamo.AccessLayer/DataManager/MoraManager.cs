using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamo.AccessLayer.DataManager
{
    public class MoraManager: BaseManager<Mora>
    {
        public MoraManager()
            : base()
        {            
        }
        public void Set(int id_prestamo, int numero_cuotas)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@prestamo_id", id_prestamo),
                    new SqlParameter("@numero_cuota", numero_cuotas)
            };
            Execute("SET_Mora  @prestamo_id, @numero_cuota", parameters);
            if (Error_Message != null && !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
        }
        public Mora Get(int prestamos_id, int numero)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@id_prestamo", prestamos_id),
                    new SqlParameter("@numero_id", numero)
            };
            Mora pago = new Mora();
            var result = Get("GET_Mora @id_prestamo,@numero_id", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            else
            {
                if (result != null && result.Count > 0)
                {
                    pago = result[0];
                }
                return pago;
            }
        }
    }
}
