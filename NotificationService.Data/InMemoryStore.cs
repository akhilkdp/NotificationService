using NotificationService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Data
{
    public class InMemoryStore : INotificationRepository
    {
        private NotificationServiceContext _context;

        public InMemoryStore()
        {
            // context can be added using dependency injection
            // currently not done that approach here..
        }

        public void DeleteNotification(Guid id)
        {
            try
            {
                if(Guid.Empty == id)
                {
                    return;
                }

                using (_context = new NotificationServiceContext())
                {
                    if (_context.Notifications.Any(n => n.NotificationId.Equals(id)))
                    {
                        Notification item = _context.Notifications.First(n => n.NotificationId == id);

                        _context.Notifications.Remove(item);
                    }

                    _context.SaveChanges();
                }
            }
            catch(Exception ex)
            { 

            }            
        }

        public async Task<Notification> GetNotification(Guid id)
        {
            Notification item = null;
            try
            {
                if (Guid.Empty == id)
                {
                    return item;
                }
                
                using (_context = new NotificationServiceContext())
                {
                    if (_context.Notifications.Any(n => n.NotificationId.Equals(id)))
                    {
                        item = await _context.Notifications.FindAsync(id);
                    }
                }
                
                return item;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Notification> SaveNotification(Notification notification)
        {
            Notification item = notification;
            try
            {
                if (Guid.Empty == item.NotificationId)
                {
                    return null;
                }

                using (_context = new NotificationServiceContext())
                {
                    if (!_context.Notifications.Any(n => n.NotificationId.Equals(item.NotificationId)))
                    {
                        await _context.Notifications.AddAsync(item);
                    }

                    _context.SaveChanges();
                }

                return item;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
