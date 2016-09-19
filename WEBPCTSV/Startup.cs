using Microsoft.Owin;

[assembly: OwinStartup(typeof(WEBPCTSV.Startup))]

namespace WEBPCTSV
{
    using Hangfire;

    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            GlobalConfiguration.Configuration.UseSqlServerStorage("DSAContext");

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}