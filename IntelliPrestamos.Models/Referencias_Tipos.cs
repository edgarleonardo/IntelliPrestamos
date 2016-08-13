using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamos.Models
{
    public class Referencias_Tipos : ModelBase
    {
        public int Tipo_Referencia_Id { get; set; }
        public string Tipo_Referencia_Desc { get; set; }
        public int Tipo_Referencia_Personal { get; set; }
    }
}
