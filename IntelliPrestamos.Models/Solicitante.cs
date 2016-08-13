using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamos.Models
{
    public class Solicitante : ModelBase
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Cedula { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Sector { get; set; }
        public string Email { get; set; }
        public string Opcional { get; set; }
        public string CopiaCedula { get; set; } 
        public Solicitante()
        {
            Nombre ="";
            Apellidos ="";
            Cedula ="";
            Direccion ="";
            Telefono ="";
            Celular ="";
            Sector ="";
            Email ="";
            Opcional ="";
            CopiaCedula = "";
        }
    }
}
