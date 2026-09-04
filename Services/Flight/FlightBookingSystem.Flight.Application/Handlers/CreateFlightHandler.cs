using FlightBookingSystem.Flights.Application.Commands;
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
    public class CreateFlightHandler : IRequestHandler<CreateFlightCommand, Guid>
    {
        private readonly IFlightRepository _repository;

        public CreateFlightHandler(IFlightRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateFlightCommand request, CancellationToken cancellationToken)
        {
            var flight = new Flight
            {
                Id = Guid.NewGuid(),
                FlightNumber = request.FlightNumber,
                Origin = request.Origin,
                Destination = request.Destination,
                DepartureTime = request.DepartureTime,
                ArrivalTime = request.ArrivalTime
            };

            await _repository.AddFlightAsync(flight);
            return flight.Id;
        }
    }
}
