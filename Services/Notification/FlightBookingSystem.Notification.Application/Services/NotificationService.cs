using FlightBookingSystem.BuildingBlocks.Contracts.EventBus.Messages;
using FlightBookingSystem.Notifications.Application.Interface;
using FlightBookingSystem.Notifications.Core.Entities;
using MassTransit;
using MassTransit.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.Notifications.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public NotificationService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        // Simulate sending a notification (via email or sms)

        public async Task SendNotificationAsync(Notification notification)
        {
            //simulating some process here
            Console.WriteLine($"Notification sent to {notification.Recipient} with Message {notification.Message} Via {notification.Type}");

            //Publish the Event

            var notificationEvent = new NotificationEvent(notification.Recipient, notification.Message, notification.Type);
            await _publishEndpoint.Publish(notificationEvent);
        }
    }
}
