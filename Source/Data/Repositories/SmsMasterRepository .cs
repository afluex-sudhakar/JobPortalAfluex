using Data.DTOs;
using Data.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Utility;

namespace Data.Repositories
{
    public class SmsMasterRepository : RepositoryBase<SMSMaster>, ISmslMasterRepository
    {
        public bool Add(SmsMasterDTO req)
        {
            try
            {
                DateTime createdAt = new Constants().IST_DATE_TIME;
                var tt = new SMSMaster();
                tt.CreatedAt = createdAt;
                tt.IsDeleted = false;
                tt.IsTemplate = req.IsTemplate;
                tt.Body = req.SMS;
                tt.To = req.Mobile;
                tt.Subject = req.Subject;
                tt.From = Constants.MAIL_FROM_EMAIL;
                Add(tt);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<SMSMaster> GetAll()
        {
            try
            {
                return GetAll(x => x.IsDeleted == false && x.IsTemplate==true).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
     
    }
}
