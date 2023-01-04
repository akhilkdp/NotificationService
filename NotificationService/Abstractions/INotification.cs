using NotificationService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Abstractions
{
    public interface INotificationService
    {
        Task<Notification> CreateNotificationAsync(CategoryType NewsCateogry, string HeadLine, string Description, string OccuringLocation, DateTime OccuringDate);
        Task<bool> SendNotificationAsync(Notification notification);
    }
}
