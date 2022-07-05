using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HRGDReguestsystem_IS.Startup))]
namespace HRGDReguestsystem_IS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
