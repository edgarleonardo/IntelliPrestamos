using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamo.AccessLayer.DataManager
{
    public class FlujoManager  : BaseManager<Flujo>
    {
        public FlujoManager()
            : base()
        {
            
        }

        public List<Flujo> Get()
        {
            var result = Get("GET_FLUJO");
            if (result == null)
            {
                throw new Exception(Error_Message);
            }
            return result;

        }
    }
}
