using NotificationService.Abstractions;
using NotificationService.Data;
using NotificationService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService
{

    // PushNotifierDecorator adds new functionality of sending push notifications in addition to the existing mail notifer
    // Similarly, we can have SMSNotiferDecorator, SlackNotifierDecorator, etc.. which extends and adds further functionalities

    public class PushNotifierDecorator : INotificationService
    {
        private readonly INotificationRepository _repository;
        private readonly INotificationService _innerNotificationService;

        public PushNotifierDecorator(INotificationService notificationService, INotificationRepository repository)
        {
            _innerNotificationService = notificationService;
            _repository = repository;   
        }

        public async Task<Notification> CreateNotificationAsync(CategoryType NewsCateogry, string HeadLine, string Description, string OccuringLocation, DateTime OccuringDate)
        {
            return await _innerNotificationService.CreateNotificationAsync(NewsCateogry, HeadLine, Description, OccuringLocation, OccuringDate);

            // decorated logic goes here if any..
        }

        public async Task<bool> SendNotificationAsync(Notification notification)
        {
            bool IsNotificationSucceeded = false;

            if (notification == null)
            {
                return IsNotificationSucceeded;
            }

            try
            {
                IsNotificationSucceeded = await _innerNotificationService.SendNotificationAsync(notification).ConfigureAwait(false);

                // decorated logic goes here if any..
                // Push notifications are sent after sending the mail notification is completed.

                await SendPushNotificationAsync("ToAddress", "Subject", "MailMessage");

                _repository.DeleteNotification(notification.NotificationId);

                IsNotificationSucceeded = true;
             
                Console.WriteLine(String.Format("Push Notification Sent: {0}" + Environment.NewLine, IsNotificationSucceeded));
            }
            catch (Exception ex)
            {

            }

            return IsNotificationSucceeded;
        }

        public async Task SendPushNotificationAsync(string toAddress, string Subject, string Message)
        {
            try
            {
                // var message = new PushNotification();
                // execute code for push notification here..
                // task.delay is only for mimicking time taken for push notification 

                await Task.Delay(3000);

            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}
