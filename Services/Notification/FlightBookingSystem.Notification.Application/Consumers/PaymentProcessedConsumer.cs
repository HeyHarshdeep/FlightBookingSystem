using FlightBookingSystem.BuildingBlocks.Contracts.EventBus.Messages;
using FlightBookingSystem.Notifications.Application.Commands;
using MassTransit;
using MediatR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.Notifications.Application.Consumers
{
    public class PaymentProcessedConsumer : IConsumer<PaymentProcessedEvent>
    {
        private readonly IMediator _mediator;
        public PaymentProcessedConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(ConsumeContext<PaymentProcessedEvent> context)
        {
            var paymentProcessedEvent = context.Message;
            var message = $"Payment of {paymentProcessedEvent.Amount} for BookingId: {paymentProcessedEvent.BookingId} was processed successfully";

            var command = new SendNotificationCommand("harsh@singh.com", message, "Email");

            await _mediator.Send(command);

        }
    }
}
