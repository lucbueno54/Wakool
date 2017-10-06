using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Guid
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{entity}",
                defaults: new { controller = "Home", action = "Index", entity = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "DefaultInserAlterDelit",
                url: "{controller}/{action}/{entity}/{ID}",
                defaults: new { controller = "Home", action = "Edit", id = UrlParameter.Optional },
                constraints: new { id = @"\d+" }
                );
        }
    }
}
