using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamos.Models
{
    public class Inversion_Detalle : ModelBase
    {
        public int Version_no { get; set; }
        public string Cedula { get; set; }
        public int Tipo_Transaccion_Id { get; set; }
        public string Tipo_Transaccion_Desc { get; set; }
        public int Pago_id { get; set; }
        public decimal Monto { get; set; }
        public string Comentario { get; set; }
        public string Fecha_Transaccion{ get; set; }
    }
}
