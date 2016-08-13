using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamos.Models
{
    public class Bancos : ModelBase
    {
        public int Id_Bancos { get; set; }
        public string Nombre_Banco { get; set; }
        public string Web_Banco { get; set; }
        public string Web_Int_Banking { get; set; }
    }
}
