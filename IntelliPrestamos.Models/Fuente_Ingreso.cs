using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamos.Models
{
    public class Fuente_Ingreso : ModelBase
    {
        public string Cedula { get; set; }
        public string Nombre_Trabajo { get; set; }
        public string Direccion { get; set; }
        public string Cargo { get; set; }
        public string telefono { get; set; }
        public string flota { get; set; }
        public decimal Sueldo_Actual { get; set; }
        public int ID_Banco { get; set; }
        public int Tiene_Int_Banking { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string Fecha_Ingreso { get; set; }

        public Fuente_Ingreso()
        {
            Clave = "";
            Usuario = "";
            ID_Banco = 1;
        }

    }
}
