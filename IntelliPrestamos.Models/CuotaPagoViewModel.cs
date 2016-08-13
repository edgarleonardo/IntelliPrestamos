using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamos.Models
{
    public class CuotaPagoViewModel
    {
        public Cuotas cuota { get; set; }
        public Pagos pago { get; set; }
        public Mora mora { get; set; }
        public Prestamos prestamos { get; set; }
    }
}
