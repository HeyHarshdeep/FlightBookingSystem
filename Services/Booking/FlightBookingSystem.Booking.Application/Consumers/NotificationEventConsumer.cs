using FlightBookingSystem.BuildingBlocks.Contracts.EventBus.Messages;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.Bookings.Application.Consumers
{
    public class NotificationEventConsumer : IConsumer<NotificationEvent>
    {
        public async Task Consume(ConsumeContext<NotificationEvent> context)
        {
            var notificationEvent = context.Message;

            Console.WriteLine($"Received Notification Event: Recipient={notificationEvent.Recipient}, " +
                              $"Message={notificationEvent.Message}, Type={notificationEvent.Type}");

            await Task.CompletedTask;
        }
    }
}
