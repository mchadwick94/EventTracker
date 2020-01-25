using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EventTracker.Startup))]
namespace EventTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
