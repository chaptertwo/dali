using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BTAdventure.UI.Startup))]
namespace BTAdventure.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
