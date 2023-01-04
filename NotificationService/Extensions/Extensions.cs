using Microsoft.Extensions.DependencyInjection;
using NotificationService.Abstractions;
using NotificationService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Extensions
{
    public static class Extensions
    {
        public static void AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<INotificationService, Notifier>();
            
            serviceCollection.AddScoped<INotificationRepository, InMemoryStore>();

        }
    }
}
