using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.Bookings.Application.Commands
{
    public record CreateBookingCommand(Guid FlightId, string PassengerName, string SeatNumber) : IRequest<Guid>;

}
