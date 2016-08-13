using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamos.Models
{
    public class Datos_Sistema : ModelBase
    {
        public String Nombre_Negocio { get; set; }
        public String Logo_Negocio { get; set; }
        public double Porc_Mora { get; set; }
        public int?  Dias_Gracia_Mora { get; set; }
        public string File_Name { get; set; }
        public string Direccion { get; set; }

        public decimal porc_gastos_cierre_max { get; set; }
        public int gasto_cierre_MIN { get; set; }
        public decimal gasto_cierre_referencia_min { get; set; }
         
        public string Icon { get; set; }
        public string Icon_file { get; set; }
        
        public string Smtp_Server { get; set; }
        public string Smtp_User { get; set; }

        public string Smtp_Pass { get; set; }
        public int Smtp_Port { get; set; }
        public int? Smtp_Timeout { get; set; }
        public string Ftp_Server { get; set; }

        public string Ftp_Port { get; set; }
        public string Ftp_User_Name { get; set; }
        public string Ftp_Pass { get; set; }
        public string Ftp_Root { get; set; }

        public int? Ftp_Enable { get; set; }
        public string Color { get; set; }
    }
}
