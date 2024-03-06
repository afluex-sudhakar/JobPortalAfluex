using System;
using System.Web;
using System.Collections.Generic;
using Utility.Enums;

namespace Data.DTOs
{
    public class NotificationDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleH { get; set; }
        public string Description { get; set; }
        public string DescriptionH { get; set; }
        public string Link { get; set; }
        public string Logo { get; set; }
        public bool IsSchedule { get; set; }
        public string Status { get; set; }
        public bool IsDeleted { get; set; }
        public int NotificationType { get; set; }
        public System.DateTime DateFrom { get; set; }
        public List<Notification> lst { get; set; }
        public int UserId { get; set; }
        public int LanguageId { get; set; }
        public string NotifiedDevice { get; set; }
        public List<UserNotificationDTO> lstNotification { get; set; }
        public string MobileNo { get; set; }
        public string Image { get; set; }
        public HttpPostedFileBase postedImage { get; set; }
    }

    public class UserNotificationDTO
    {
        public int NotificationId { get; set; }
        public int UserDeviceId { get; set; }
        public string Medium { get; set; }
        public string FCMId { get; set; }
        public int? UserId { get; set; }
    }

    public class NotificationReposnseDTO : NotificationDTO
    {
        public string Message { get; set; }

    }
    public class NotificationReposnseDTOMobile
    {
        public List<NotificationDTOMobile> lstNotification { get; set;}
    }
    public class NotificationDTOMobile
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int NotificationId { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
    }
    public class NotificationRequestDTO: UserLogDTO
    {
        public int UserId { get; set; }
        public Language Language { get; set; }
    }
}
