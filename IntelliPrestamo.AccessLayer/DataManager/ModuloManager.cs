using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamo.AccessLayer.DataManager
{
    public class ModuloManager : BaseManager<Modulos>
    {
        public ModuloManager()
            : base()
        {
            
        }

        public List<Modulos> Get(int role_id)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@ID_ROL", role_id)};
            var result = Get("GET_MODULOS @ID_ROL", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            else
            {
                if (result != null)
                {
                    foreach (Modulos modulos in result)
                    {
                        if (modulos.Selected_Int == 1)
                        {
                            modulos.Selected = true;
                        }
                        else
                        {
                            modulos.Selected = false;
                        }
                    }
                }
                return result;
            }
        }
        public List<Modulos> GetUserModule(string cedula)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@CEDULA", cedula)};
            var result = Get("Get_usuario_Role @CEDULA", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            else
            {
                return result;
            }
        }
        public void Set(int role_id, int modulo_id, int isSelected)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@ID_ROL", role_id), 
                    new SqlParameter("@ID_MODULO", modulo_id),
                    new SqlParameter("@SELECTED", isSelected)
            };
            Execute(@"SET_MODULOS_ROLES @ID_ROL,@ID_MODULO, @SELECTED", parameters);

            if (Error_Message != null && Error_Message.Trim() != "")
            {
                throw new Exception(Error_Message);
            }                        
        }
    }
}
