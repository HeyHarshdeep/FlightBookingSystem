using FlightBookingSystem.Flights.Application.Queries;
using FlightBookingSystem.Flights.Core.Entities;
using FlightBookingSystem.Flights.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.Flights.Application.Handlers
{
    public class GetAllFlightsHandler : IRequestHandler<GetAllFlightsQuery, IEnumerable<Flight>>
    {
        private readonly IFlightRepository _repository;

        public GetAllFlightsHandler(IFlightRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Flight>> Handle(GetAllFlightsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetFlightAsync();
        }
    }
}
