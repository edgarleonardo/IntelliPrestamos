using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamos.Models
{
    public class PerfilViewModel
    {
        public Empleado empleado { get; set; }
        public List<Solicitud> SolicitudPendiente { get; set; }
        public List<Solicitud> SolicitudAprobadas { get; set; }
        public List<Prestamos> Prestamos { get; set; }
        public Inversion inversiones { get; set; }
        public List<Inversion_Detalle> inversiones_detalles { get; set; }
    }
}
