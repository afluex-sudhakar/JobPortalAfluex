using System;
using System.Web.Mvc;

namespace Webapp.Handlers
{
    public class CustomErrorHandler : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            Log(filterContext.Exception);
            base.OnException(filterContext);
        }

        private void Log(Exception exception)
        {
            //log exception here..

        }
    }
}