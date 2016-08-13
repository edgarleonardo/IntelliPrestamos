using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamo.AccessLayer.DataManager
{
    public class RolesManager : BaseManager<Roles>
    {
        public RolesManager()
            : base()
        {
            
        }

        public List<Roles> Get(int role_id)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@ID_ROL", role_id)};
            var result = Get("GET_ROLES @ID_ROL", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message); 
            }
            else
            {
                return result;
            }
        }

        public Roles Set(Roles model)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@ID_ROL", model.Id_Rol), 
                    new SqlParameter("@NOMBRE_ROL", model.Nombre_Rol),
                    new SqlParameter("@DESCRIPCCION", model.Descripccion),
                    new SqlParameter("@ACTIVO", model.Activo)
            };
            var result = Get(@"SET_ROLES @ID_ROL,@NOMBRE_ROL,@DESCRIPCCION,@ACTIVO", parameters);

            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
            {
                throw new Exception(Error_Message);
            }            
        }
    }
}
