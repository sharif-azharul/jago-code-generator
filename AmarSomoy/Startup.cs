using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AmarSomoy.Startup))]
namespace AmarSomoy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
