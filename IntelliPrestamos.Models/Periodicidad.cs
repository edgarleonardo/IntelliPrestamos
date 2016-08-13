using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamos.Models
{
    public class Periodicidad : ModelBase
    {
        public int Periodicidad_Id { get; set; }
        public string Periodicidad_Desc { get; set; }
        public int Plazo { get; set; }
        public string Periodicidad_Info { get; set; }
    }
}
