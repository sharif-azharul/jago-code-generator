using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TelerikMVCUI.Startup))]
namespace TelerikMVCUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
