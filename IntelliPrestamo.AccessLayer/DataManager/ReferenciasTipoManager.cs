using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamo.AccessLayer.DataManager
{
    public class ReferenciasTipoManager : BaseManager<Referencias_Tipos>
    {
        public ReferenciasTipoManager()
            : base()
        {
            
        }
        public List<Referencias_Tipos> Get()
        {
            var result = Get("GET_REFERENCIAS_TIPOS");
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            else
            {
                return result;
            }
        }

        public Referencias_Tipos Get(int referencia_id)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@TIPO_REFERENCIA_ID", referencia_id)};
            var result = Get("GET_REFERENCIAS_TIPOS_BY @TIPO_REFERENCIA_ID", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            else
            {
                Referencias_Tipos cargo = new Referencias_Tipos();
                if (result.Count > 0)
                {
                    cargo = result[0];
                }
                return cargo;
            }
        }
        public void Set(Referencias_Tipos model)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@TIPO_REFERENCIA_ID", model.Tipo_Referencia_Id), 
                    new SqlParameter("@TIPO_REFERENCIA_DESC", model.Tipo_Referencia_Desc),
                    new SqlParameter("@TIPO_REFERENCIA_PERSONAL", model.Tipo_Referencia_Personal)
            };
            Execute(@"SET_REFERENCIAS_TIPOS_BY @TIPO_REFERENCIA_ID,@TIPO_REFERENCIA_DESC,@TIPO_REFERENCIA_PERSONAL", parameters);

            if (Error_Message != null && Error_Message.Trim() != "")
            {
                throw new Exception(Error_Message);
            }
        }

        public void Del(Referencias_Tipos model)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@TIPO_REFERENCIA_ID", model.Tipo_Referencia_Id)
            };
            Execute(@"DEL_CARGOS @TIPO_REFERENCIA_ID", parameters);

            if (Error_Message != null && Error_Message.Trim() != "")
            {
                throw new Exception(Error_Message);
            }
        }
    }
}
