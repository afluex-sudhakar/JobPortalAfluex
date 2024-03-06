using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Webapp.Handlers
{
    public class AntiForgeryTokenHandler : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            bool isCsrf = true;
            CookieHeaderValue cookie = request.Headers
              .GetCookies(AntiForgeryConfig.CookieName)
              .FirstOrDefault();
            if (cookie != null)
            {
                //Stream requestBufferedStream = request.Content.ReadAsStreamAsync().Result;
                //requestBufferedStream.Position = 0;
                //NameValueCollection myform = request.Content.ReadAsFormDataAsync().Result;
                try
                {
                    //AntiForgery.Validate(cookie[AntiForgeryConfig.CookieName].Value,
                    // myform[AntiForgeryConfig.CookieName]);
                    AntiForgery.Validate(cookie[AntiForgeryConfig.CookieName].Value, request.Headers.GetValues("X-RVT").First());
                    isCsrf = false;
                }
                catch (Exception ex)
                {
                    return request.CreateResponse(HttpStatusCode.Forbidden);
                }
            }
            if (isCsrf)
            {
                return request.CreateResponse(HttpStatusCode.Forbidden);
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}