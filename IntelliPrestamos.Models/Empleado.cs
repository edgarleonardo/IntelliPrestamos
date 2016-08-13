using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamos.Models
{
    public class Empleado : ModelBase
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Cedula { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Fecha_Nacimiento { get; set; }
        public int Cargo_Id { get; set; }
        public string Cargo_Desc { get; set; }
        public int Id_Empleado { get; set; }
        public string Id_Supervisor { get; set; }
        public string Fecha_Salida { get; set; }

        public string Fecha_Ingreso { get; set; }
        public Usuarios User { get; set; }
        public Inversion inversion { get; set; }
        public Empleado()
        {
            User = new Usuarios();
            inversion = new Inversion();
        }
    }
}
