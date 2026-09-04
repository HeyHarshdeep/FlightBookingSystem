using FlightBookingSystem.Bookings.Application.Commands;
using FlightBookingSystem.Bookings.Core.Entities;
using FlightBookingSystem.Bookings.Core.Repositories;
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
        public CreateBookingHandler(IBookingRepository repository)
        {
            _repository = repository;
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

            return booking.Id;
        }
    }
}
