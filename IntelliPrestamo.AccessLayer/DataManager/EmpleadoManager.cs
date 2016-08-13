using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamo.AccessLayer.DataManager
{
    public class EmpleadoManager: BaseManager<Empleado>
    {
        public EmpleadoManager()
            : base()
        {
            
        }
        
            public List<Empleado> GetActive()
        {
            var result = Get("GET_EMPLOYEE_ACTIVE");
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message); 
            }
            else
            {
                return result;
            }
        }
        public List<Empleado> GetAll()
        {
            var result = Get("GET_EMPLOYEE");
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message); 
            }
            else
            {
                return result;
            }
        }

        public List<Empleado> GetAllAccionistas()
        {
            var result = Get("GET_EMPLOYEE_ACCIONISTA");
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            else
            {
                foreach(Empleado emp in result)
                {
                    InversionManager invma = new InversionManager();
                    emp.inversion = invma.GetInversion(emp.Cedula);
                    if (emp.inversion == null)
                    {
                        emp.inversion = new Inversion();
                    }
                }
                return result;
            }
        }
        public Empleado GetBy(string cedula)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@CEDULA", cedula)};
            var result = Get("GET_EMPLOYEE_BY @CEDULA", parameters);
            if (result == null  || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            else
            {
                Empleado cargo = new Empleado();
                if (result.Count > 0)
                {
                    UsuarioManager user = new UsuarioManager();
                    cargo=result[0];
                    cargo.User = user.GetBy(cedula);
                } 
                return cargo;
            }
        }
        public void Set(Empleado model)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@NOMBRE", model.Nombre), 
                    new SqlParameter("@APELLIDOS", model.Apellidos),
                    new SqlParameter("@CEDULA", model.Cedula),
                    new SqlParameter("@FECHA_NACIMIENTO", model.Fecha_Nacimiento),
                    new SqlParameter("@DIRECCION", model.Direccion),                    
                    new SqlParameter("@TELEFONO", model.Telefono),
                    new SqlParameter("@CELULAR", model.Celular),
                     Param_With_Type("@CARGO_ID", SqlDbType.Int, model.Cargo_Id),
                    new SqlParameter("@ID_SUPERVISOR", model.Id_Supervisor),
                    new SqlParameter("@FECHA_SALIDA", model.Fecha_Salida),
                    new SqlParameter("@FECHA_INGRESO", model.Fecha_Ingreso)
            };
            Execute(@"SET_EMPLOYEE @NOMBRE,@APELLIDOS,@CEDULA,@FECHA_NACIMIENTO,@DIRECCION, @TELEFONO,@CELULAR, @CARGO_ID, @ID_SUPERVISOR, @FECHA_SALIDA, @FECHA_INGRESO", parameters);

            if (Error_Message != null && Error_Message.Trim() != "")
            {
                throw new Exception(Error_Message);
            }   
            else
            {
                UsuarioManager user = new UsuarioManager();
                if (model.User.Cedula == null)
                {
                    model.User.Cedula = model.Cedula;
                }
                
                user.Set(model.User);
            }
        }
    }
}
