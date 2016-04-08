using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NCas.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.MapRoute(
                name: "NCas.Account",
                url: "Account/{action}/{id}",
                defaults: new { controller = "Account", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "NCas.WebApp",
                url: "WebApp/{action}/{id}",
                defaults: new { controller = "WebApp", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
             name: "NCas.VerifyTicket",
             url: "VerifyTicket",
             defaults: new { controller = "Auth", action = "VerifyTicket" }
         );
            routes.MapRoute(
                name: "NCas.Verify",
                url: "Verify",
                defaults: new { controller = "Auth", action = "Verify" }
            );
            routes.MapRoute(
                name: "NCas.Login",
                url: "Login",
                defaults: new { controller = "Auth", action = "Login" }
            );
        }
    }
}
