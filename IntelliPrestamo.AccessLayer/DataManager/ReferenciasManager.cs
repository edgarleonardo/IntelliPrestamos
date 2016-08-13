using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamo.AccessLayer.DataManager
{
    public class ReferenciasManager : BaseManager<Referencias_Personales>
    {
        public ReferenciasManager()
            : base()
        {
            
        }

        public List<Referencias_Personales> Get(string Cedula)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@cedula", Cedula)};
            var result = Get("GET_REFERENCIAS_PERSONALES @cedula", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            return result;
        }
        public void Set(Referencias_Personales model)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@NOMBRE", model.Nombre), 
                    new SqlParameter("@APELLIDO", model.Apellido),
                    new SqlParameter("@TELEFONO", model.Telefono),
                    new SqlParameter("@TIPO_REFERENCIA_ID", model.Tipo_Referencia_Id),
                    new SqlParameter("@version_no", model.Version_No), 
                    new SqlParameter("@CEDULA", model.Cedula)
            };
            Execute(@"SET_REFERENCIAS_PERSONALES @NOMBRE,@APELLIDO,@TELEFONO, @TIPO_REFERENCIA_ID,@version_no,@CEDULA", parameters);

            if (Error_Message != null && Error_Message.Trim() != "")
            {
                throw new Exception(Error_Message);
            }
        }
    }
}
