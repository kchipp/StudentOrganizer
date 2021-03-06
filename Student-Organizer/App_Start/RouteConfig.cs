﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Student_Organizer
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           
            routes.MapRoute(
                name: "DescribeUS",
                url: "home/description",
                defaults: new { controller = "Home", action = "Description", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "ToRegister",
                url: "account/register",
                defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );
        }
    }
}
