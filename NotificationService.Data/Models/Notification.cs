using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Data.Models
{
    public class Notification
    {
        public Guid NotificationId { get; set; }
        public int Category { get; set; }
        public string HeadLine { get; set; }
        public string Description { get; set; }
        public DateTime OccuringDate { get; set; }   
        public string OccuringLocation { get; set; }
        public DateTime CreatedDate { get; set; }

        public Notification(string headLine, string description, DateTime occuringDate, string occuringLocation, int category)
        {
            NotificationId = Guid.NewGuid();
            Category = category;
            HeadLine = headLine;
            Description = description;
            OccuringDate = occuringDate;
            OccuringLocation = occuringLocation;
            CreatedDate = DateTime.UtcNow;
        }
    }
}
