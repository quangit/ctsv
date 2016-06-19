using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WEBPCTSV.Models.bean;

namespace WEBPCTSV
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
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
            Application["OnlineVisitors"] = 0;
            Application["Visitors"] = 0;
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
        }
        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
        }
        void Session_Start(object sender, EventArgs e)
        {
            //Start Session , couting visiter
            int count_visit = 0;
            //Check exist file count_visit (save visit counter)
            if (System.IO.File.Exists(Server.MapPath("~/count_visit.txt")) == false)
            {
                count_visit = 1;
            }
            // Else not exist file
            else
            {
                // Create file save visit counter
                System.IO.StreamReader read = new System.IO.StreamReader(Server.MapPath("~/count_visit.txt"));
                count_visit = int.Parse(read.ReadLine());
                read.Close();
                // increase visit variable
                count_visit++;
            }
            // Lock wwebsite
            Application.Lock();
            // Application count_visit to Visitors vaiable
            Application["Visitors"] = count_visit;
            // Unlock Website
            Application.UnLock();
            // Save data to file  count_visit.txt
            System.IO.StreamWriter writer = new System.IO.StreamWriter(Server.MapPath("~/count_visit.txt"));
            writer.WriteLine(count_visit);
            writer.Close();
            // Code that runs when a new session is started
            Application.Lock();
            Application["OnlineVisitors"] = (int)Application["OnlineVisitors"] + 1;
            Application.UnLock();
        }
        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends.
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer
            // or SQLServer, the event is not raised.
            Application.Lock();
            Application["OnlineVisitors"] = (int)Application["OnlineVisitors"] - 1;
            Application.UnLock();
        }
    }
}