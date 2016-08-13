using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamos.Models
{
    public class Inversion : ModelBase
    {
        public string Cedula { get; set; }
        public decimal Monto_Inversion_Actual { get; set; }
        public decimal Monto_Prestados { get; set; }
        public decimal Monto_Balance { get; set; }
        public decimal Monto_Ganancia { get; set; }
        public string tipo_trans { get; set; }
        public List<Inversion_Detalle> Inv_Detalle { get; set; }

        public Inversion()
        {
            Inv_Detalle = new List<Inversion_Detalle>();

            Monto_Inversion_Actual = 0;
            Monto_Prestados = 0;
            Monto_Balance = 0;
            Monto_Ganancia = 0;
        }
    }
}
