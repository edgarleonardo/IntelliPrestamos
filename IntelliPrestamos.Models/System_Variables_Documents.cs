using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamos.Models
{
    public class System_Variables_Documents : ModelBase
    {
        public int Identification_Loop { get; set; }
        public string Table_Name { get; set; }
        public string Column_Name { get; set; }
    }
}
