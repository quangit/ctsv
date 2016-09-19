namespace WEBPCTSV
{
    using System;
    using System.IO;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            // Code that runs on application startup
            this.Application["OnlineVisitors"] = 0;
            this.Application["Visitors"] = 0;
        }

        void Application_End(object sender, EventArgs e)
        {
            // Code that runs on application shutdown
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends.
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer
            // or SQLServer, the event is not raised.
            this.Application.Lock();
            this.Application["OnlineVisitors"] = (int)this.Application["OnlineVisitors"] - 1;
            this.Application.UnLock();
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Start Session , couting visiter
            int count_visit = 0;

            // Check exist file count_visit (save visit counter)
            if (System.IO.File.Exists(this.Server.MapPath("~/count_visit.txt")) == false)
            {
                count_visit = 1;
            }

            // Else not exist file
            else
            {
                // Create file save visit counter
                StreamReader read = new System.IO.StreamReader(this.Server.MapPath("~/count_visit.txt"));
                count_visit = int.Parse(read.ReadLine());
                read.Close();

                // increase visit variable
                count_visit++;
            }

            // Lock wwebsite
            this.Application.Lock();

            // Application count_visit to Visitors vaiable
            this.Application["Visitors"] = count_visit;

            // Unlock Website
            this.Application.UnLock();

            // Save data to file  count_visit.txt
            StreamWriter writer = new System.IO.StreamWriter(this.Server.MapPath("~/count_visit.txt"));
            writer.WriteLine(count_visit);
            writer.Close();

            // Code that runs when a new session is started
            this.Application.Lock();
            this.Application["OnlineVisitors"] = (int)this.Application["OnlineVisitors"] + 1;
            this.Application.UnLock();
        }
    }
}