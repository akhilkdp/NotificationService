using NotificationService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Data
{
    public interface INotificationRepository
    {
        Task<Notification> SaveNotification(Notification notification);

        Task<Notification> GetNotification(Guid id);

        void DeleteNotification(Guid id);

    }
}
