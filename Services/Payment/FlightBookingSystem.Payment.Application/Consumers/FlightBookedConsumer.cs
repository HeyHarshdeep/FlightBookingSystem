using FlightBookingSystem.BuildingBlocks.Contracts.EventBus.Messages;
using FlightBookingSystem.Payments.Application.Commands;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.Payments.Application.Consumers
{
    public class FlightBookedConsumer : IConsumer<FlightBookedEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<FlightBookedConsumer> _logger;
        public FlightBookedConsumer(IMediator mediator, ILogger<FlightBookedConsumer> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<FlightBookedEvent> context)
        {
            var flightBookedEvent = context.Message;
            var command = new ProcessPaymentCommand(flightBookedEvent.BookingId, 200.00m);
            try
            {
                await _mediator.Send(command);
            }
            catch (Exception ex)
            {
                // Log the error and swallow to avoid moving the message to the _error queue.
                // Consider publishing a failure event or implementing retry/compensation as needed.
                _logger.LogError(ex, "Failed to process payment for BookingId {BookingId}", flightBookedEvent.BookingId);
            }
        }
    }
}
