using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IntelliPrestamos.Startup))]
namespace IntelliPrestamos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
