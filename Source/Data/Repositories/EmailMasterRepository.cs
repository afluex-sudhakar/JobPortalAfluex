using Data.DTOs;
using Data.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Utility;

namespace Data.Repositories
{
    public class EmailMasterRepository : RepositoryBase<EmailMaster>, IEmailMasterRepository
    {
        public bool Add(EmailMasterDTO req)
        {
            try
            {
                DateTime createdAt = new Constants().IST_DATE_TIME;
                var tt = new EmailMaster();
                tt.CreatedAt = createdAt;
                tt.IsDeleted = false;
                tt.IsTemplate = req.IsTemplate;
                tt.Body = req.Body;
                tt.To = req.Email;
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

        public List<EmailMaster> GetAll()
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
