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
    public class SetUpManager : BaseManager<Datos_Sistema>
    {
        public SetUpManager()
            : base()
        {
            
        }

        public Datos_Sistema Get ()
        {
            var result = Get("GET_DATOS_SISTEMA");
            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
            {
                throw new Exception(Error_Message);
            }
        }       

        public Datos_Sistema Set(Datos_Sistema model)
        {
            var parameters = new SqlParameter[]{
                    new SqlParameter("@NOMBRE_NEGOCIO", model.Nombre_Negocio), 
                    new SqlParameter("@LOGO_NEGOCIO", model.Logo_Negocio),
                    Param_With_Type("@PORC_MORA", SqlDbType.Float, model.Porc_Mora),
                    Param_With_Type("@DIAS_GRACIA_MORA", SqlDbType.Int, model.Dias_Gracia_Mora),
                    new SqlParameter("@DIRECCION", model.Direccion),
                    new SqlParameter("@ICON", model.Icon),
                    new SqlParameter("@SMTP_SERVER", model.Smtp_Server),
                    new SqlParameter("@SMTP_USER", model.Smtp_User),
                    new SqlParameter("@SMTP_PASS", model.Smtp_Pass),
                    Param_With_Type("@SMTP_PORT", SqlDbType.Int, model.Smtp_Port),
                    Param_With_Type("@SMTP_TIMEOUT", SqlDbType.Int, model.Smtp_Timeout),
                    new SqlParameter("@FTP_SERVER", model.Ftp_Server),
                    new SqlParameter("@FTP_PORT", model.Ftp_Port),
                    new SqlParameter("@FTP_USER_NAME", model.Ftp_User_Name),
                    new SqlParameter("@FTP_PASS", model.Ftp_Pass),
                    new SqlParameter("@FTP_ROOT", model.Ftp_Root),
                    Param_With_Type("@FTP_ENABLE", SqlDbType.Int, model.Ftp_Enable),
                    new SqlParameter("@COLOR", model.Color),
                    new SqlParameter("@porc_gastos_cierre_max", model.porc_gastos_cierre_max),
                    new SqlParameter("@gasto_cierre_MIN", model.gasto_cierre_MIN),
                    new SqlParameter("@gasto_cierre_referencia_min", model.gasto_cierre_referencia_min)
            };
            var result = Get(@"SET_DATOS_SISTEMA @NOMBRE_NEGOCIO,@LOGO_NEGOCIO,@PORC_MORA,@DIAS_GRACIA_MORA,@DIRECCION,@ICON,@SMTP_SERVER,@SMTP_USER,@SMTP_PASS,@SMTP_PORT,@SMTP_TIMEOUT,@FTP_SERVER, @FTP_PORT,@FTP_USER_NAME,@FTP_PASS,@FTP_ROOT,@FTP_ENABLE, @COLOR, @porc_gastos_cierre_max, @gasto_cierre_MIN, @gasto_cierre_referencia_min", parameters);
            //@NOMBRE_NEGOCIO, @LOGO_NEGOCIO, @PORC_MORA, @DIAS_GRACIA_MORA, @DIRECCION,@ICON,

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
