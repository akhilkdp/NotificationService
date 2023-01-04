using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using NotificationService.Abstractions;
using NotificationService.Data;
using NotificationService.Data.Models;


namespace NotificationService
{
    public enum NotificationType { UnKnown, Email, Push, SMS, Slack, Other };

    public enum CategoryType { Default, Breaking, CurrentAffairs, Environmental, Government, International, Politics, Regional, Sports, Technology, Religion, Weather };

    public class Notifier : INotificationService
    {
        private readonly INotificationRepository _repository;
        public Notifier(INotificationRepository repository)
        {
            //initialize
            _repository = repository;
        }

        // creates a notification based on the news and saves to temp database.
        public async Task<Notification> CreateNotificationAsync(CategoryType NewsCateogry, string HeadLine, string Description, string OccuringLocation, DateTime OccuringDate)
        {
            try
            {
                Notification notification = new Notification(HeadLine, Description, OccuringDate, OccuringLocation, (int)NewsCateogry);

                await _repository.SaveNotification(notification).ConfigureAwait(false);

                Console.WriteLine(String.Format("Added News Notification." + Environment.NewLine +
                                                "NotificationId: {0}," + Environment.NewLine +
                                                "Location: {1}," + Environment.NewLine +
                                                "Date: {2}," + Environment.NewLine +
                                                "Headline: {3}," + Environment.NewLine +
                                                "News: {4}" + Environment.NewLine
                                , notification.NotificationId
                                , notification.OccuringLocation
                                , notification.OccuringDate.ToString()
                                , notification.HeadLine
                                , notification.Description));

                return await _repository.GetNotification(notification.NotificationId);

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // sends the notification based on the type of distribution.
        public async Task<bool> SendNotificationAsync(Notification notification)
        {
            bool IsNotificationSucceeded = false;

            if(notification == null)
            {
                return IsNotificationSucceeded;
            }

            try
            {
                await SendEmailAsync("ToAddress", "Subject", "MailMessage");

                _repository.DeleteNotification(notification.NotificationId);

                IsNotificationSucceeded = true;
                
                Console.WriteLine(String.Format("Mail Notification Sent: {0}" + Environment.NewLine, IsNotificationSucceeded));
            }
            catch (Exception ex)
            {

            }

            return IsNotificationSucceeded;
        }

        public async Task SendEmailAsync(string toEmailAddress, string emailSubject, string emailMessage)
        {
            try
            {
                var message = new MailMessage();
                message.To.Add(toEmailAddress);

                message.Subject = emailSubject;
                message.Body = emailMessage;

                using (var smtpClient = new SmtpClient())
                {
                    await smtpClient.SendMailAsync(message);
                }
            }
            catch(Exception ex)
            {
                return;
            }
        }
    }
}
