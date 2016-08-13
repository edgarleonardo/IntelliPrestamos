using IntelliPrestamos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliPrestamo.AccessLayer.DataManager
{
    public class ColorManager : BaseManager<Color>
    {
        public ColorManager()
            : base()
        {
            
        }

        public List<Color> Get()
        {
            var result = Get("GET_COLORES_SISTEMA");
            if (result == null)
            {
                throw new Exception(Error_Message);
            }
            return result;

        }
    }
}
