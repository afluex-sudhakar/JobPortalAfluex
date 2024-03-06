using Newtonsoft.Json.Converters;
using System.Globalization;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Webapp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/careermitra/{action}/{id}",
                defaults: new { controller = "WebAPI", id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new IsoDateTimeConverter() { Culture = new CultureInfo("en-GB") });
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            //config.MessageHandlers.Add(new ApiKeyHandler());

        }
    }
}
