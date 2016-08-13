using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamos.Models
{
    public class Prestamos : ModelBase
    {
        public int Prestamo_Id { get; set; }
        public int Id_Solicitud { get; set; }
        public string Cedula { get; set; }
        public decimal Monto_Aprobado { get; set; }
        public string Web_Int_Banking { get; set; }
        public int Periodicidad_Id { get; set; }
        public string Periodicidad_Desc { get; set; }
        public int Dia_De_Pago { get; set; }
        public int Plazo { get; set; }
        public int Status_Id { get; set; }
        public string Status_Desc { get; set; }
        public decimal Capital_Pagados { get; set; }
        public decimal Intereses_Pagados { get; set; }
        public decimal Intereses_Generados { get; set; }
        public decimal Balance_General { get; set; }
        public string Fecha_Cancelacon { get; set; }
        public string Fecha_Aprobacion { get; set; }
        public string Fecha_Vencimiento { get; set; }
        public string Fecha_Desembolso { get; set; }
        public decimal Interes { get; set; }
        public string updated_by { get; set; }
        public string aprobado_por { get; set; }
        public int desembolsado { get; set; }
        public decimal cuota { get; set; }
        public string Accionista { get; set; }
        public string Cobrador { get; set; }        
        public int cuotas_pagadas { get; set; }
        public int cuotas_restantes { get; set; }
        public List<Document> Documentos { get; set; }
        public List<Tabla_Amortizacion> amortizacion { get; set; }
    }
}
