using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.BuildingBlocks.Contracts.EventBus.Messages
{
    public record FlightBookedEvent(Guid BookingId, Guid FlightId, string PassengerName, string SeatNumber, DateTime BookingDate);

}
