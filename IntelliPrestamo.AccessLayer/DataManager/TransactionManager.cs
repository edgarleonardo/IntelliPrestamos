using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamo.AccessLayer.DataManager
{
    public class TransactionManager : BaseManager<Transaction>
    {
        public TransactionManager()
            : base()
        {
            
        }
        public List<Transaction> GetTransaccionesCapital()
        {
            var result = Get("GET_tipo_transaccion_capital");
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            else
            {
                return result;
            }
        }
        
        public List<Transaction> GetAll()
        {
            var result = Get("GET_tipo_transaccion");
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message); 
            }
            else
            {
                return result;
            }
        }

        public Transaction GetBy(int transaccion_id)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@tipo_transaccion_id", transaccion_id)};
            var result = Get("GET_tipo_transaccion_by @tipo_transaccion_id", parameters);
            if (result == null  || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            else
            {
                Transaction resultDat = new Transaction();
                if (result != null && result.Count > 0)
                {
                    resultDat = result[0];
                }
                return resultDat;
            }
        }
        public void Set(Transaction model)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@tipo_transaccion_id", model.Tipo_Transaccion_Id), 
                    new SqlParameter("@tipo_transaccion_desc", model.Tipo_Transaccion_Desc),
                    new SqlParameter("@tipo_pago_capital", model.Tipo_Pago_Capital),
                    new SqlParameter("@tipo_capital", model.Tipo_Capital)
            };
            Execute(@"SET_tipo_transaccion @tipo_transaccion_id,@tipo_transaccion_desc,@tipo_pago_capital,@tipo_capital", parameters);

            if (Error_Message != null && Error_Message.Trim() != "")
            {
                throw new Exception(Error_Message);
            }  
        }
    }
}
