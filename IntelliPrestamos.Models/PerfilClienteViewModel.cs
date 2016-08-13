using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamos.Models
{
    public class PerfilClienteViewModel
    {
        public Solicitante solicitante { get; set; }
        public Fuente_Ingreso Fuente_Ingreso { get; set; }
        public List<Referencias_Personales> referencias_Personales { get; set; }
        public List<Referencias_Personales> referencias_Laborales { get; set; }
        public List<Solicitud> SolicitudPendiente { get; set; }
        public List<Solicitud> SolicitudAprobadas { get; set; }
        public List<Prestamos> Prestamos { get; set; }

        public PerfilClienteViewModel()
        {
            solicitante = new Solicitante();
            Fuente_Ingreso = new Fuente_Ingreso();
            referencias_Personales = new List<Referencias_Personales>();
            referencias_Personales.Add(new Referencias_Personales());
            referencias_Personales.Add(new Referencias_Personales());
            referencias_Personales.Add(new Referencias_Personales());
            referencias_Laborales = new List<Referencias_Personales>();
            referencias_Laborales.Add(new Referencias_Personales());
            referencias_Laborales.Add(new Referencias_Personales());
            referencias_Laborales.Add(new Referencias_Personales());
        }
    }
}
