using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamos.Models
{
    public class StatusTable : ModelBase
    {
        public int status_id {get;set;}
        public string status_desc {get;set;}
        public int flujo_id {get;set;}
    }
}
