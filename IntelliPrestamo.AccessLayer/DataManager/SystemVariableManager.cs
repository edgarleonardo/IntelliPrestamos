using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamo.AccessLayer.DataManager
{
    public class SystemVariableManager : BaseManager<System_Variables_Documents>
    {
        public SystemVariableManager()
            : base()
        {
            
        }

        public List<System_Variables_Documents> Get()
        {
            var result = Get("GET_SYSTEM_VARIABLE");
            if (result == null || !string.IsNullOrEmpty(Error_Message))
            {
                throw new Exception(Error_Message); 
            }
            else
            {
                return result;
            }
        }        
        
    }
}
