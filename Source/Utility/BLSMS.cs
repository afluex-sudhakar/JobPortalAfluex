using System;
using System.IO;
using System.Net;

namespace Utility
{
    public static class BLSMS
    {
        public static string SendSMS(string Mobile, string Message,string TempId)
        {
           //string strUrl = "http://smsw.co.in/API/WebSMS/Http/v1.0a/index.php?username=careeruser&password=AY6Pih-lcp3&sender=MSCLCM&to=" + Mobile + "&message=" + Message + "& reqid = 1 & format ={ json}&route_id = 39 & callback =#&unique=0&sendondate='" + DateTime.Now.ToString() + " '";


            string strUrl = "http://smsw.co.in/API/WebSMS/Http/v1.0a/index.php?username=careeruser&password=AY6Pih-lcp3&sender=MSCLCM&to=&to=" + Mobile + "&message=" + Message + ", CareerMitra & reqid=1 &format={json|text}&pe_id=1201162400048358285 &template_id=" + TempId + "&callback =#&unique=0&sendondate='" + DateTime.Now.ToString() + " '";



            //string strUrl = "http://smsw.co.in/API/WebSMS/Http/v1.0a/index.php?username=Afluex&password=f6b7c7-970d7&sender=AFLUEX&to=" + Mobile + "&message=" + Message + "& reqid = 1 & format ={ json}&route_id = 39 & callback =#&unique=0&sendondate='" + DateTime.Now.ToString() + " '";



            //string strUrl = "http://smsw.co.in/API/WebSMS/Http/v1.0a/index.php?username=careeruser&password=AY6Pih-lcp3&sender=MSCLCM&to=" + Mobile + "&message=" + Message + "& reqid = 1 & format ={ json}&route_id = 39 & callback =#&unique=0&sendondate='" + DateTime.Now.ToString() + " '";


            WebRequest request = HttpWebRequest.Create(strUrl);
            // Get the response back  
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = (Stream)response.GetResponseStream();
            StreamReader readStream = new StreamReader(s);
            string dataString = readStream.ReadToEnd();
            response.Close();
            s.Close();
            readStream.Close();
            return dataString;
        }
    }
}
