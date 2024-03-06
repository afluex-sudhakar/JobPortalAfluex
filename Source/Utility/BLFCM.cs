using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace Utility
{
    public class BLFCM
    {

        public string SendMessage(string[] fcmId, string title, string body)
        {
            string serverKey = "AAAAdvrGdqQ:APA91bE9RuE42VzD4FADOqREDW6ZHuNZ_GetRI7VX33FE9JdZqmtVhlYGTRPeotOCyXMGM7XhO3jh11ASdbvJbpD6uAGNGD4yQwv6kmvFLUXv1nwudMubLsR6_GOTgMTSqb0vTWDYTJd";

            try
            {
                var result = "-1";
                var webAddr = "https://fcm.googleapis.com/fcm/send";

                var to = JsonConvert.SerializeObject(fcmId);
                //var regID = "e05DcgDDmxE:APA91bHNHCGcoJelburXbJiDieYHYT_lxWoU_a5t6G6mnh8m6f_hGwOQzwX68TpJqd8VrVnb0E3NbDHxkKlo9jzezD1E_X8ktCyxHkmdx9n8yCahGqRPa970u3IXilb7ka5iXkIFdxEX"; //for testing purpose (enter fcm id hard coded)

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add("Authorization:key=" + serverKey);
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    //string json = "{\"to\": \"" + regID + "\",\"notification\": {\"title\": \"New deal\",\"body\": \"20% deal!\"},\"priority\":10}"; //single recipient
                    //string json = "{\"registration_ids\":[\"e05DcgDDmxE:APA91bHNHCGcoJelburXbJiDieYHYT_lxWoU_a5t6G6mnh8m6f_hGwOQzwX68TpJqd8VrVnb0E3NbDHxkKlo9jzezD1E_X8ktCyxHkmdx9n8yCahGqRPa970u3IXilb7ka5iXkIFdxEX\"],\"notification\":{\"title\":\"New deal\",\"body\":\"20% deal!\"},\"priority\":10}"; //multiple recipient
                    string json = "{\"registration_ids\":" + to + ",\"notification\": {\"title\": \"" + title + "\",\"body\": \"" + body + "\"},\"priority\":10}";
                    //registration_ids, array of strings -  to, single recipient
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }

                return result;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.ToString());
                return ex.Message;
            }
        }
    }
}
