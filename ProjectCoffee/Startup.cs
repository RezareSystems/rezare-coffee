using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectCoffee.Startup))]
namespace ProjectCoffee
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
