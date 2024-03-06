using Data.DTOs;
using Data.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Utility;
using Utility.Enums;

namespace Data.Repositories
{
    public class NotificationRepository : RepositoryBase<Notification>, INotificationRepository
    {
        public bool Add(NotificationDTO req)
        {
            try
            {
                DateTime createdAt = new Constants().IST_DATE_TIME;
                var tt = new Notification();
                if (req.Status == "Sent")
                {
                   
                    foreach(var item in req.lstNotification)
                    {
                        UserNotification un = new UserNotification();
                        un.UserId = item.UserId;
                        un.CreatedAt = createdAt;
                        un.IsDeleted = false;
                        un.UserDeviceId = item.UserDeviceId;
                        un.Medium = "Web";
                        tt.UserNotifications.Add(un);
                    }
                }
                tt.CreatedAt = createdAt;
                tt.Link = req.Link;
                tt.Title = req.Title;
                tt.TitleH = req.TitleH;
                tt.Description = req.Description;
                tt.DescriptionH = req.DescriptionH;
                tt.Status = req.Status;
                tt.Image = req.Image;
                tt.Status = "";
                tt.IsDeleted = false;
                Add(tt);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(NotificationDTO req)
        {
            try
            {
                DateTime createdAt = new Constants().IST_DATE_TIME;
                var d = GetById(req.Id);
                if (d != null)
                {
                    d.ModifiedAt = createdAt;
                    Update(d);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id, int userId)
        {
            try
            {
                var d = GetById(id);
                if (d != null)
                {
                    d.IsDeleted = true;
                    d.ModifiedAt = new Constants().IST_DATE_TIME;
                    Update(d);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Notification> GetAll()
        {
            try
            {
                return GetAll(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Notification GetDetail(int id, int languageId)
        {
            try
            {
                return GetById(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<NotificationDTOMobile> GetAllNotification(NotificationRequestDTO req)
        {
            try
            {
                List<NotificationDTOMobile> res = new List<NotificationDTOMobile>();
                var data = from x in db.UserNotifications
                          where x.IsDeleted == false && x.UserId == req.UserId
                          select x;
                res = data.Select(x => new NotificationDTOMobile
                {
                    Title = req.Language == Language.English ? x.Notification.Title : x.Notification.TitleH,
                    Description = req.Language == Language.English ? x.Notification.Description : x.Notification.DescriptionH,
                    Link = x.Notification.Link,
                    NotificationId = (int)x.NotificationId,
                    Date = x.Notification.CreatedAt,
                    Image = x.Notification.Image != null ? Constants.BASE_URL + "FileUpload/Other/" + x.Notification.Image : ""
                }).ToList();
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
