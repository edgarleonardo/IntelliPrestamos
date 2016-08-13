using System.Web;
using System.Web.Mvc;

namespace IntelliPrestamos
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new IntelliPrestamos.BaseController.SecurityFilter());
        }
    }
}
