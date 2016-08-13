using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamos.Models
{
    public class Document : ModelBase
    {
        public int Id {get;set;}
        public int Flujo_Id {get;set;}
        public string Flujo_desc { get; set; }
        public string Nombre_Documento {get;set;}
        public string Documento {get;set;}
        public int Activo {get;set;}
    }
}
