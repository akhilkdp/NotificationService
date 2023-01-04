using Microsoft.EntityFrameworkCore;
using NotificationService.Data.Models;

namespace NotificationService.Data
{
    public class NotificationServiceContext : DbContext
    {
        // using an in-memory database for simplicity.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "NotificationsDb");
        }

        public virtual DbSet<Notification> Notifications { get; set; }

    }
}