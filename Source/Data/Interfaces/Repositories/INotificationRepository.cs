using Data.DTOs;
using System.Collections.Generic;

namespace Data.Interfaces.Repositories
{
    public interface INotificationRepository : IRepositoryBase<Notification>
    {
        bool Add(NotificationDTO req);

        bool Update(NotificationDTO req);

        bool Delete(int id, int userId);

        new List<Notification> GetAll();

        Notification GetDetail(int id, int languageId);

        List<NotificationDTOMobile> GetAllNotification(NotificationRequestDTO para);
    }
}
