using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamos.Models
{
    public class Cuotas : ModelBase
    {
         public int Prestamos_Id { get; set; }
        public int Solicitud_Id { get; set; }
        public string Cedula { get; set; }
        public decimal Monto_Aprobado { get; set; }
        public int Numero_Cuota { get; set; }
        public string Fecha_Cuota { get; set; }
        public string Fecha_Vencimiento { get; set; }
        public int Status_Id { get; set; }
        public decimal Monto_Capital { get; set; }
        public decimal Monto_Intereses { get; set; }
        public decimal Monto { get; set; }
        public string Status_Desc { get; set; }
    }
}
