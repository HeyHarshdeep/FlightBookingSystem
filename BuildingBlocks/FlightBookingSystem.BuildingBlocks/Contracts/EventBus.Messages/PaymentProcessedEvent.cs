using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.BuildingBlocks.Contracts.EventBus.Messages
{
    public record PaymentProcessedEvent(Guid PaymentId, Guid BookingId, decimal Amount, DateTime PaymentDate);
}
