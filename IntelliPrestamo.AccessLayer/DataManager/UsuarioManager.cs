using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamo.AccessLayer.DataManager
{
    public class UsuarioManager : BaseManager<Usuarios>
    {
        public UsuarioManager()
            : base()
        {
            
        }

        public List<Usuarios> GetAll()
        {
            var result = Get("get_usuario");
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message); 
            }
            else
            {
                return result;
            }
        }
        
            public Usuarios GetAuthentication(string usuario, string pass)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@USUARIO", usuario),
            new SqlParameter("@PASS", pass)};
            var result = Get("AUTHENTICATE_user @USUARIO, @PASS", parameters);
            if (result == null  || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            else
            {
                Usuarios cargo = new Usuarios();
                if (result.Count > 0)
                {
                    cargo=result[0];
                } 
                return cargo;
            }
        }
        public Usuarios GetBy(string cedula)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@CEDULA", cedula)};
            var result = Get("get_usuario_by @CEDULA", parameters);
            if (result == null  || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            else
            {
                Usuarios cargo = new Usuarios();
                if (result.Count > 0)
                {
                    cargo=result[0];
                } 
                return cargo;
            }
        }
            public Usuarios GetUserByUserName(string Usuario)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@USUARIO", Usuario)
            };
            var result = Get(@"get_usuario_by_User @USUARIO", parameters);

            if (Error_Message != null && Error_Message.Trim() != "")
            {
                throw new Exception(Error_Message);
            }
            else
            {
                Usuarios cargo = new Usuarios();
                if (result.Count > 0)
                {
                    cargo = result[0];
                }
                return cargo;
            }
        }
        public Usuarios AuthorizationRoutes(string Usuario,string Controller, string Action)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@USUARIO", Usuario), 
                    new SqlParameter("@CONTROLLER", Controller),
                    new SqlParameter("@ACTION", Action)
            };
            var result = Get(@"USER_AUTH_ROUTES @USUARIO,@CONTROLLER,@ACTION", parameters);

            if (Error_Message != null && Error_Message.Trim() != "")
            {
                throw new Exception(Error_Message);
            }
            else
            {
                Usuarios cargo = new Usuarios();
                if (result.Count > 0)
                {
                    cargo = result[0];
                }
                return cargo;
            }
        }
        public void Set(Usuarios model)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@USUARIO", model.Usuario), 
                    new SqlParameter("@CLAVE", model.Clave),
                    new SqlParameter("@CEDULA", model.Cedula),
                    new SqlParameter("@ID_ROL", model.Id_Rol),
                    new SqlParameter("@email", model.Email)                    
            };
            var result = Get(@"Set_usuario @USUARIO,@CLAVE,@CEDULA,@ID_ROL,@email", parameters);
            if (result != null && result.Count > 0 && result[0].errorMessage.Trim() != "")
            {
                Error_Message = result[0].errorMessage;
            }
            if (Error_Message != null && Error_Message.Trim() != "")
            {
                throw new Exception(Error_Message);
            }             
        }
    }
}
