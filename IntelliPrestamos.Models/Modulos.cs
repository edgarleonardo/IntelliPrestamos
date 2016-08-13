using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamos.Models
{
    public class Modulos : ModelBase
    {
        public int Id_Modulos { get; set; }
        public string Nombre { get; set; }
        public string Controller { get; set; }
        public string Actions { get; set; }
        public int Activo { get; set; }
        public int Selected_Int { get; set; }
        public bool Selected { get; set; }
    }
}
