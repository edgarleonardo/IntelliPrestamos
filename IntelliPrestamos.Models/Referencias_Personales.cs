using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamos.Models
{
    public class Referencias_Personales : ModelBase
    {
        public string Nombre {get; set;}
        public string Apellido {get; set;}
        public string Telefono {get; set;}
        public int Tipo_Referencia_Id {get; set;}
        public string Cedula {get; set;}
        public int Version_No { get; set; }

        public Referencias_Personales()
        {
            Nombre = "";
            Apellido = "";
            Telefono = "";
            Tipo_Referencia_Id = 1;
            Cedula = "";
            Version_No = 0;
        }
    }
}
