using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Webapp.Controllers;

namespace Webapp
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            MvcHandler.DisableMvcResponseHeader = true;

            //HttpCookie myCookie = new HttpCookie("theme");
            //myCookie.Expires = DateTime.Now.AddDays(1);
            //myCookie.Value = "default";
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();

            // Process 404 HTTP errors
            var httpException = exception as HttpException;
            if (httpException != null && httpException.GetHttpCode() == 404)
            {
                Response.Clear();
                Server.ClearError();
                Response.TrySkipIisCustomErrors = true;

                IController controller = new ErrorController();

                var routeData = new RouteData();
                routeData.Values.Add("controller", "error");
                routeData.Values.Add("action", "NotFound");

                var requestContext = new RequestContext(
                     new HttpContextWrapper(Context), routeData);
                controller.Execute(requestContext);
            }
        }
        protected void Application_PreSendRequestHeaders()
        {
            Response.Headers.Remove("Server");
            Response.Headers.Remove("X-Powered-By");
            Response.Headers.Remove("X-AspNet-Version");
            Response.Headers.Remove("X-AspNetMvc-Version");

            //Response.Headers.Set("Cache-Control", "no-cache");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.AppendCacheExtension("no-store, must-revalidate, private");
            Response.AppendHeader("Pragma", "no-cache");
            Response.AppendHeader("Expires", "0");
        }
    }
}