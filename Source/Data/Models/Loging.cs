using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using Utility.Enums;

namespace Data.Models
{
    public class Loging
    {
        public void SaveLogForWeb(string Remark,string Error,string Data,string UserAgent)
        {
            var domainName = Util.GetDomainName();
            var IPAddress = Util.GetIPAddress();
            var OSVersion = Util.GetOSVersion();
            DateTime createdAt = new Constants().IST_DATE_TIME;
            UserLog ul = new UserLog();
            using (CareerMitraContainer db = new Data.CareerMitraContainer())
            {
                ul.Remark = Constants.HTTPSTATUS_FAILED + createdAt.ToShortDateString();
                ul.Error = Error;
                ul.Data = Data; 
                ul.CreatedAt = createdAt;
                ul.OS = OSVersion;
                ul.IsDeleted = false;
                ul.DeviceId = "";
                ul.Lat = "";
                ul.Lng = "";
                ul.Address = "";
                ul.DeviceOtherInfo = "";
                ul.UserAgent = UserAgent;
                ul.Domain = domainName;
                ul.DeviceType = DeviceType.Web;
                ul.IP = IPAddress;
                db.UserLogs.Add(ul);
                db.SaveChanges();
            }
        }
    }
}
