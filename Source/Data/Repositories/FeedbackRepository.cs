using Data.DTOs;
using Data.Interfaces.Repositories;
using System;
using Utility;

namespace Data.Repositories
{
    public class FeedbackRepository : RepositoryBase<Feedback>, IFeedbackRepository
    {
        public bool Add(FeedbackDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                Feedback cm = new Feedback();
                cm.CreatedAt = createdAt;
                cm.IsDeleted = false;
                cm.Message = req.Message;
                cm.Mobile = req.Mobile;
                cm.Email = req.Email;
                cm.Name = req.Name;
                cm.Medium = "Mobile App";
                Add(cm);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool AddForWeb(UpdateCMSDTO req)
        {
            DateTime createdAt = new Constants().IST_DATE_TIME;
            try
            {
                Feedback cm = new Feedback();
                cm.CreatedAt = createdAt;
                cm.IsDeleted = false;
                cm.Message = req.Message;
                cm.Mobile = req.MobileNo;
                cm.Email = req.Email;
                cm.Name = req.UserName;
                cm.Medium = "Web";
                Add(cm);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
