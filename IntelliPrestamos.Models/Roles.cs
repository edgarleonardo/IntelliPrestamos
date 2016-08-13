using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamos.Models
{
    public class Roles : ModelBase
    {
        public int Id_Rol { get; set; }
        public string Nombre_Rol { get; set; }
        public int Activo { get; set; }
        public string Descripccion { get; set; }
        public List<Modulos> ListRol { get; set; }

        public Roles ()
        {
            ListRol = new List<Modulos>();
        }
    }
}
