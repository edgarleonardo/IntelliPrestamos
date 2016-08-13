using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamos.Models
{
    public class Transaction : ModelBase
    {
        public int Tipo_Transaccion_Id { get; set; }
        public string Tipo_Transaccion_Desc { get; set; }
        public int Tipo_Capital { get; set; }
        public int Tipo_Pago_Capital { get; set; }
    }
}
