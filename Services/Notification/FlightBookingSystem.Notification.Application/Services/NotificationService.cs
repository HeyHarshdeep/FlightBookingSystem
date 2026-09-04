using FlightBookingSystem.Notifications.Application.Interface;
using FlightBookingSystem.Notifications.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.Notifications.Application.Services
{
    public class NotificationService : INotificationService
    {
        // Simulate sending a notification (via email or sms)

        public Task SendNotificationAsync(Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
