using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamos.Models
{
    public class Cargo : ModelBase
    {
        public int Id_Cargo { get; set; }
        public string Nombre { get; set; }
        public int EsGerencial { get; set; }
        public int EsAccionista { get; set; }
        public int Activo { get; set; }
    }
}
