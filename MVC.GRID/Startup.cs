using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC.GRID.Startup))]
namespace MVC.GRID
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
