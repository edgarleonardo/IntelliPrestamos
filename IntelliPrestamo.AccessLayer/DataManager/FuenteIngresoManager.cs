using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamo.AccessLayer.DataManager
{
    public class FuenteIngresoManager : BaseManager<Fuente_Ingreso>
    {
        public FuenteIngresoManager()
            : base()
        {
            
        }

        public Fuente_Ingreso Get(string Cedula)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@cedula", Cedula)};
            var result = Get("GET_FUENTE_INGRESOS @cedula", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            else
            {
                Fuente_Ingreso cargo = new Fuente_Ingreso();
                if (result.Count > 0)
                {
                    cargo = result[0];
                }
                return cargo;
            }
        }

        public void Set(Fuente_Ingreso model)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@CEDULA", model.Cedula), 
                    new SqlParameter("@DIRECCION", model.Direccion),
                    new SqlParameter("@CARGO", model.Cargo),
                    new SqlParameter("@TELEFONO", model.telefono),
                    new SqlParameter("@FLOTA", model.flota),
                    new SqlParameter("@FECHA_INGRESO", model.Fecha_Ingreso), 
                    new SqlParameter("@SUELDO_ACTUAL", model.Sueldo_Actual),
                    new SqlParameter("@ID_BANCOS", model.ID_Banco),
                    new SqlParameter("@TIENE_INT_BANKING", model.Tiene_Int_Banking),
                    new SqlParameter("@USUARIO", model.Usuario),
                    new SqlParameter("@CLAVE", model.Clave),
                    new SqlParameter("@Nombre_Trabajo", model.Nombre_Trabajo)
            };
            Execute(@"SET_FUENTE_INGRESOS @CEDULA,@DIRECCION,@CARGO,@TELEFONO, @FLOTA,@FECHA_INGRESO,@SUELDO_ACTUAL,@ID_BANCOS,@TIENE_INT_BANKING,@USUARIO,@CLAVE,@Nombre_Trabajo", parameters);

            if (Error_Message != null && Error_Message.Trim() != "")
            {
                throw new Exception(Error_Message);
            }
        }
    }
}
