using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TransitASP.Startup))]
namespace TransitASP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
