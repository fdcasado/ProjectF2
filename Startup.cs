using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectF2.Startup))]
namespace ProjectF2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
