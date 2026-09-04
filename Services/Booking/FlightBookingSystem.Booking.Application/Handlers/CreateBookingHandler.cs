using FlightBookingSystem.Bookings.Application.Commands;
using FlightBookingSystem.Bookings.Core.Entities;
using FlightBookingSystem.Bookings.Core.Repositories;
using FlightBookingSystem.BuildingBlocks.Contracts.EventBus.Messages;
using MassTransit;
using MassTransit.Transports;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.Bookings.Application.Handlers
{
    public class CreateBookingHandler : IRequestHandler<CreateBookingCommand, Guid>
    {
        private readonly IBookingRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        public CreateBookingHandler(IBookingRepository repository, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _publishEndpoint = publishEndpoint;
        }
        public async Task<Guid> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
        {
            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                FlightId = command.FlightId,
                PassengerName = command.PassengerName,
                SeatNumber = command.SeatNumber,
                BookingDate = DateTime.UtcNow
            };

            await _repository.AddBookingAsync(booking);

            // Publish FlightBookedEvent
            await _publishEndpoint.Publish(new FlightBookedEvent(
                booking.Id,
                booking.FlightId,
                booking.PassengerName,
                booking.SeatNumber,
                booking.BookingDate
                ));

            return booking.Id;
        }
    }
}
