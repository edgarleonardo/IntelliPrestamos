using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamo.AccessLayer.DataManager
{
    public class SolicitanteManager  : BaseManager<Solicitante>
    {
        public SolicitanteManager()
            : base()
        {
            
        }

        public Solicitante Get(string Cedula)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@cedula", Cedula),
            new SqlParameter("@CELULAR", ""),
            new SqlParameter("@TELEFONO", ""),
            new SqlParameter("@nombre", "")};
            var result = Get("GET_SOLICITANTE_BY @cedula, @CELULAR, @TELEFONO,@nombre", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            else
            {
                Solicitante cargo = new Solicitante();
                if (result.Count > 0)
                {
                    cargo = result[0];
                }
                return cargo;
            }
        }
        public List<Solicitante> Get(string Cedula, string celular, string telefono, string nombre)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@cedula", Cedula),
            new SqlParameter("@CELULAR", celular),
            new SqlParameter("@TELEFONO", telefono),
            new SqlParameter("@nombre", nombre)};
            var result = Get("GET_SOLICITANTE_BY @cedula, @CELULAR, @TELEFONO,@nombre", parameters);
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message);
            }
            return result;
        }
        public void Set(Solicitante model)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@CEDULA", model.Cedula), 
                    new SqlParameter("@NOMBRE", model.Nombre),
                    new SqlParameter("@APELLIDOS", model.Apellidos),
                    new SqlParameter("@DIRECCION", model.Direccion),
                    new SqlParameter("@SECTOR", model.Sector),
                    new SqlParameter("@TELEFONO", model.Telefono), 
                    new SqlParameter("@CELULAR", model.Celular),
                    new SqlParameter("@EMAIL", model.Email),
                    new SqlParameter("@OPCIONAL", ((model.Opcional==null)?"":model.Opcional)),
                        new SqlParameter("@copiaCedula", ((model.CopiaCedula==null)?"":model.CopiaCedula))
            };
            Execute(@"SET_SOLICITANTE @CEDULA,@NOMBRE,@APELLIDOS,@DIRECCION, @SECTOR,@TELEFONO,@CELULAR,@EMAIL,@OPCIONAL, @copiaCedula", parameters);

            if (Error_Message != null && Error_Message.Trim() != "")
            {
                throw new Exception(Error_Message);
            }
        }
    }
}
