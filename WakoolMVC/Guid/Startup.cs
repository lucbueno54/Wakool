using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Guid.Startup))]
namespace Guid
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
