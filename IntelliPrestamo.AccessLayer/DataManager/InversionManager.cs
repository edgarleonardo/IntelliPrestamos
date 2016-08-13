using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamo.AccessLayer.DataManager
{
    public class InversionManager : BaseManager<Inversion>
    {
        public InversionManager()
            : base()
        {
            
        }
        public Inversion GetInversion(string cedula)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@cedula", cedula)};
            var result = Get("GET_inversion @cedula", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            else
            {
                Inversion cargo = new Inversion();
                if (result.Count > 0)
                {
                    cargo = result[0];
                    InversionDetalleManager invDet = new InversionDetalleManager();
                    cargo.Inv_Detalle = invDet.GetInversionDetalle(cedula);
                }
                return cargo;
            }
        }
        public void SetGanancias(string cedula, decimal monto, string tipo_transaccion_id)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@cedula", cedula),
                    new SqlParameter("@monto", monto),
                    new SqlParameter("@tipo_transaccion_id", tipo_transaccion_id)
            };
            var result = Get("SET_ganancias_trans @cedula, @monto, @tipo_transaccion_id", parameters);
            if (result != null && result.Count > 0 && result[0].errorMessage != "")
            {
                Error_Message = result[0].errorMessage;
            }
            if (!string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }

        }
        public void SetInversion(string cedula, double monto, int tipo_transaccion_id, int pago_id, string comentario)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@cedula", cedula),
            new SqlParameter("@monto", monto),
            new SqlParameter("@tipo_transaccion_id", tipo_transaccion_id),
            new SqlParameter("@pago_id", pago_id), 
            new SqlParameter("@comentario", comentario)
            };
            var result = Get("SET_inversion_trans @cedula, @monto, @tipo_transaccion_id,@pago_id,@comentario", parameters);
            if (result != null && result.Count > 0 && result[0].errorMessage != "")
            {
                Error_Message = result[0].errorMessage;
            }
            if (!string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            
        }
    }
}
