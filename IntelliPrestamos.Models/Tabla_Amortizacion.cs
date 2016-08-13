using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamos.Models
{
    public class Tabla_Amortizacion : ModelBase
    {
        public int Loan_Id { get; set; }
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public int Period { get; set; }
        public string Pmt_Dt { get; set; }
        public decimal Balance { get; set; }
        public decimal Payment { get; set; }
        public decimal Cur_Interest { get; set; }
        public decimal Cum_Interest { get; set; }
        public decimal Cur_Principle { get; set; }
        public decimal Cum_Principle { get; set; }

    }
}
