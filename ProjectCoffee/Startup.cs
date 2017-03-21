using Hangfire;
using Microsoft.Owin;
using Owin;
using System.Web.Configuration;

[assembly: OwinStartupAttribute(typeof(ProjectCoffee.Startup))]
namespace ProjectCoffee
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            GlobalConfiguration.Configuration
            .UseSqlServerStorage(WebConfigurationManager.ConnectionStrings["HangfireContext"].ToString());

            app.UseHangfireServer();
            app.UseHangfireDashboard();
        }
    }
}
