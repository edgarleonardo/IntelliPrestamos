using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamos.Models
{
    public class Mora : ModelBase
    {
        public int Prestamos_Id { get; set; }
        public int Solicitud_Id { get; set; }
        public string Cedula { get; set; }
        public decimal Monto { get; set; }
        public int Numero_Cuota { get; set; }
        public string Fecha_Creacion { get; set; }
        public string Fecha_Pago { get; set; }
        public int Status_Id { get; set; }
        public string Status_Desc { get; set; }
    }
}
