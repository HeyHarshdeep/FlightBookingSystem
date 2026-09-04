using FlightBookingSystem.Flights.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.Flights.Application.Queries
{
    public record GetAllFlightsQuery : IRequest<IEnumerable<Flight>>;

}
