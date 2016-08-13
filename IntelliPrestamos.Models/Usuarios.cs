using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamos.Models
{
    public class Usuarios : ModelBase
    {
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string Cedula { get; set; }
        public int Id_Rol { get; set; }
        public int Autorizado_Para_Ruta { get; set; }
        public string Email { get; set; }
        public List<Roles> ListRol { get; set; }
        public int SolicitudesPendientes { get; set; }
        public int SolicitudesAprobadas { get; set; }
        public int CuotasPendientes { get; set; }
    }
}
