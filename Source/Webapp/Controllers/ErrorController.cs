using System.Web.Mvc;

namespace Webapp.Controllers
{
    public class ErrorController : Controller
    {
        [AllowAnonymous]
        public ActionResult NotFound()
        {
            Response.ContentType = "text/html";
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;

            return View();
        }

        [AllowAnonymous]
        public ActionResult InternalServerError()
        {
            Response.ContentType = "text/html";
            Response.StatusCode = 503;
            Response.TrySkipIisCustomErrors = true;

            return View();
        }



        //[HttpGet]
        //public ActionResult InternalServerError()
        //{
        //    return View();
        //}

        //[HttpGet]
        //public ActionResult NotFound()
        //{
        //    return View();
        //}
    }
}