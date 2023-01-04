using Microsoft.Extensions.DependencyInjection;
using NotificationService;
using NotificationService.Abstractions;
using NotificationService.Data;
using System;

namespace NotifierConsoleApp
{
    public class Program
    {
        private static IServiceProvider serviceProvider;
        static async Task Main(string[] args)
        {
            Console.WriteLine("Creating test news notifications");

            INotificationRepository notificationRepository = new InMemoryStore();

            // Email notification service.

            INotificationService mailNotificationService = new Notifier(notificationRepository);

            if(mailNotificationService != null)
            {
                var mailNotification = await mailNotificationService.CreateNotificationAsync(CategoryType.International, "International news test01",
                                                            "test01 description", "NewYork", DateTime.UtcNow);

                var isNotifiedViaMail = await mailNotificationService.SendNotificationAsync(mailNotification);


                // Push notification service.

                INotificationService pushNotificationService = new PushNotifierDecorator(mailNotificationService, notificationRepository);

                if(pushNotificationService != null)
                {
                    // creating a new notification here as the previous notification gets cleared from temp database.

                    var pushNotification = await pushNotificationService.CreateNotificationAsync(CategoryType.Regional, "Regional news test02",
                                                            "test02 description", "Cochin", DateTime.UtcNow);

                    var isNotifiedViaPush = await pushNotificationService.SendNotificationAsync(pushNotification);

                }

            }

        }
    }
}