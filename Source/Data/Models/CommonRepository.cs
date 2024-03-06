using Data.DTOs;
using System;
using Utility;

namespace Data.Models
{
    public class CommonRepository
    {
       
        public  void AddUserLog(UserLogDTO model, DateTime createdAt, string remark, string error, string data, int? userId)
        {
            var domainName = Util.GetDomainName();
            var IPAddress = Util.GetIPAddress();
            var OSVersion = Util.GetOSVersion();
            try
            {
                using (CareerMitraContainer db = new Data.CareerMitraContainer())
                {
                    UserLog ul = new UserLog();
                    ul.CreatedAt = createdAt;
                    ul.Remark = remark;
                    ul.Data = data;
                    ul.Error = error;
                    if (userId != null) ul.UserId = userId;
                    ul.OS = OSVersion;
                    ul.IsDeleted = false;
                    ul.DeviceId = model.DeviceId == null ? "" : model.DeviceId;
                    ul.Lat = model.Lat;
                    ul.Lng = model.Long;
                    ul.Address = model.Address==null?"":model.Address;
                    ul.DeviceOtherInfo = model.DeviceOtherInfo == null ? "" : model.DeviceOtherInfo;
                    ul.UserAgent = model.UserAgent;
                    ul.Domain = domainName;
                    ul.DeviceType = model.DeviceType;
                    ul.IP = IPAddress;
                    db.UserLogs.Add(ul);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}
