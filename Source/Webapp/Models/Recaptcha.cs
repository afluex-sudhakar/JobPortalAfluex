using System;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace Webapp.Models
{
    public class Recaptcha
    {
        public static bool Validate(string response)
        {
            //string Response = HttpContext.Current.Request.QueryString["g-recaptcha-response"];//Getting Response String Append to Post Method
            bool Valid = false;
            //Request to Google Server
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create
            (" https://www.google.com/recaptcha/api/siteverify?secret=6Leai1AaAAAAAD4OF72y8QtkHVKV1a1ZamFV6Ics&response=" + response);
            try
            {
                //Google recaptcha Response
                using (WebResponse wResponse = req.GetResponse())
                {

                    using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                    {
                        string jsonResponse = readStream.ReadToEnd();

                        JavaScriptSerializer js = new JavaScriptSerializer();
                        MyObject data = js.Deserialize<MyObject>(jsonResponse);// Deserialize Json

                        Valid = Convert.ToBoolean(data.success);
                    }
                }

                return Valid;
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }
    }
    public class MyObject
    {
        public string success { get; set; }
    }

    public class mymodel
    {
        public string Mobile { get; set; }
        public string captcharesponse { get; set; }
    }
}