using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamos.Models
{
    public class Solicitud : ModelBase
    {
        public int Periodicidad_Id { get; set; }
        public string Periodicidad_Desc { get; set; }
        public string Cedula { get; set; }
        public int Dia_Pago { get; set; }
        public decimal Ingresos_Solicitante { get; set; }
         public decimal Monto_Prestamo { get; set; }
        public string Copia_Cedula { get; set; }
        public string Tarjeta_Cobro { get; set; }
        public int Id_Banco { get; set; }       
        public string Usuario_Int_Banking { get; set; }
        public string Clave_Int_Banking { get; set; }
        public int Id_Solicitud { get; set; }
        public int Status_Id { get; set; }
        public string Status_Desc { get; set; }
        public int Plazo { get; set; }
        public string Creation_Date { get; set; }
        public string Create_By { get; set; }
        public string Update_Date { get; set; }
        public string Update_By { get; set; }
        public decimal Interes { get; set; }
        public string Comentario { get; set; }
        public string Comentario_Credito { get; set; }
        public string Accionista { get; set; }
        public string Cobrador { get; set; }
        public Solicitante solicitante { get; set; }
        public Fuente_Ingreso Fuente_Ingreso { get; set; }
        public List<Referencias_Personales> referencias_Personales { get; set; }
        public List<Referencias_Personales> referencias_Laborales { get; set; }
        public List<Document> Documentos { get; set; }
        public Solicitud()
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
            Copia_Cedula = "";
            Tarjeta_Cobro = "";
            Status_Id = 0;
        }	
    }
}
