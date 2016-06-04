using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Doando.Startup))]
namespace Doando
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
