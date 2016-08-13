using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamo.AccessLayer.DataManager
{
    public class BancosManager: BaseManager<Bancos>
    {
        public BancosManager()
            : base()
        {
            
        }

        public List<Bancos> Get()
        {
            var result = Get("GET_BANCOS");
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message); 
            }
            else
            {
                return result;
            }
        }

        public Bancos Get(int banco_id)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@ID_BANCOS", banco_id)};
            var result = Get("GET_BANCOS_BY @ID_BANCOS", parameters);
            if (result == null  || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            else
            {
                Bancos cargo = new Bancos();
                if (result.Count > 0)
                {
                    cargo=result[0];
                } 
                return cargo;
            }
        }
        public void Set(Bancos model)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@ID_BANCOS", model.Id_Bancos), 
                    new SqlParameter("@NOMBRE_BANCO", model.Nombre_Banco),
                    new SqlParameter("@WEB_BANCO", model.Web_Banco),
                    new SqlParameter("@WEB_INT_BANKING", model.Web_Int_Banking)
            };
            Execute(@"SET_BANCOS @ID_BANCOS,@NOMBRE_BANCO,@WEB_BANCO,@WEB_INT_BANKING", parameters);

            if (Error_Message != null && Error_Message.Trim() != "")
            {
                throw new Exception(Error_Message);
            }             
        }

    }
}
