using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamo.AccessLayer.DataManager
{
    public class CargoManager: BaseManager<Cargo>
    {
        public CargoManager()
            : base()
        {
            
        }

        public List<Cargo> Get()
        {
            var result = Get("GET_CARGOS");
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message); 
            }
            else
            {
                return result;
            }
        }

        public Cargo Get(int cargo_id)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@ID_CARGO", cargo_id)};
            var result = Get("GET_CARGOS_BY @ID_CARGO", parameters);
            if (result == null  || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            else
            {
                Cargo cargo = new Cargo();
                if (result.Count > 0)
                {
                    cargo=result[0];
                } 
                return cargo;
            }
        }
        public void Set(Cargo model)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@ID_CARGO", model.Id_Cargo), 
                    new SqlParameter("@NOMBRE", model.Nombre),
                    new SqlParameter("@ESGERENCIAL", model.EsGerencial),
                    new SqlParameter("@ESACCIONISTA", model.EsAccionista),
                    new SqlParameter("@ACTIVO", model.Activo)
            };
            Execute(@"SET_CARGOS @ID_CARGO,@NOMBRE,@ESGERENCIAL,@ESACCIONISTA, @ACTIVO", parameters);

            if (Error_Message != null && Error_Message.Trim() != "")
            {
                throw new Exception(Error_Message);
            }             
        }

        public void Del(Cargo model)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@ID_CARGO", model.Id_Cargo)
            };
            Execute(@"DEL_CARGOS @ID_CARGO", parameters);

            if (Error_Message != null && Error_Message.Trim() != "")
            {
                throw new Exception(Error_Message);
            }            
        }
    }
    
}
