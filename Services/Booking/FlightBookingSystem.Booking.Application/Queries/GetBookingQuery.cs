using FlightBookingSystem.Bookings.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.Bookings.Application.Queries
{
    public record GetBookingQuery(Guid Id): IRequest<Booking>;
    
}
