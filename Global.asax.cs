﻿using System;

using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using System.Web.SessionState;
using System.Web.Optimization;
namespace WebApplication4
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
               // Code that runs on application startup
               AreaRegistration.RegisterAllAreas();
             
               RouteConfig.RegisterRoutes(RouteTable.Routes);
               BundleConfig.RegisterBundles(BundleTable.Bundles);
          }
    }
}