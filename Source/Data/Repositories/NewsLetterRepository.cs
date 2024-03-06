using Data.DTOs;
using Data.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utility;
using Utility.Enums;

namespace Data.Repositories
{
    public class NewsLetterRepository : RepositoryBase<NewsLetter>, INewsLetterRepository
    {
        public bool Add(NewsLetterDTO req)
        {
            try
            {
                var domainName = Util.GetDomainName();
                var IPAddress = Util.GetIPAddress();
                var OSVersion = Util.GetOSVersion();
                DateTime createdAt = new Constants().IST_DATE_TIME;
                var a = GetAll(x => x.Email == req.Email && x.IsDeleted == false).FirstOrDefault();
                if (a != null)
                {

                    UserLog ul = new UserLog();
                    ul.Remark = Constants.LOG_USER_REGISTRATION_ATTEMPT_FAILED + createdAt.ToShortDateString();
                    ul.Data = new Security().Serialize<NewsLetterDTO>(req);
                    ul.Error = Constants.LOG_USER_REGISTRATION_MOBILENUMBER_ALREADYUSED;
                    ul.CreatedAt = createdAt;
                    ul.OS = OSVersion;
                    ul.IsDeleted = false;
                    ul.DeviceId = req.DeviceId == null ? "" : req.DeviceId;
                    ul.Lat = req.Lat;
                    ul.Lng = req.Long;
                    ul.Address = req.Address;
                    ul.DeviceOtherInfo = req.DeviceOtherInfo;
                    ul.UserAgent = "";
                    ul.Domain = domainName;
                    ul.DeviceType = "";
                    ul.IP = IPAddress;
                    db.UserLogs.Add(ul);
                    db.SaveChanges();
                    return false;
                }
                else
                {

                    var tt = new NewsLetter();
                    tt.Email = req.Email;
                    tt.CreatedAt = createdAt;
                    tt.IsDeleted = false;
                    Add(tt);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
    }
}
