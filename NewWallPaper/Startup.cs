using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewWallPaper.Startup))]
namespace NewWallPaper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
