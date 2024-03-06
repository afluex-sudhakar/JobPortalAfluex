using System.Web.Mvc;
using System.Web.Routing;

namespace Webapp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                 name: "User Hindi",
                 url: "uhi/{action}/{id}",
                 defaults: new { controller = "UserHI", action = "UserProfile", id = UrlParameter.Optional }
             );

            routes.MapRoute(
                name: "User English",
                url: "uen/{action}/{id}",
                defaults: new { controller = "User", action = "UserProfile", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Hindi",
                url: "hi/{action}/{id}",
                defaults: new { controller = "HomeHindi", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "English",
                url: "en/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "Default",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
          );
        }
    }
}
