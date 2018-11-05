using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Myth.UI.Startup))]
namespace Myth.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
