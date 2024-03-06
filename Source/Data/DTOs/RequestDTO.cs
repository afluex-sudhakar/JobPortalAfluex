using System.Net;

namespace Data.DTOs
{
    public class Request
    {
        public string Body { get; set; }
    }
    public class Response
    {
        public string Body { get; set; }
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
