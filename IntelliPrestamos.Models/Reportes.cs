using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamos.Models
{
    public class Reportes : ModelBase
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public decimal Monto_Intereses { get; set; }
        public decimal monto_aprobado { get; set; }
        public string Accionista { get; set; }
        public string Accinista_Nombre { get; set; }
        public string fecha { get; set; }
        public int prestamo_id { get; set; }
        public string status_desc { get; set; }
        public string fecha_desembolso { get; set; }
        public string fecha_vencimiento { get; set; }
        public string fecha_cancelacion { get; set; }
    }
}
