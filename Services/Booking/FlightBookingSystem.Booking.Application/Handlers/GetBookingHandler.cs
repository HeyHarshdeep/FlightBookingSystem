using FlightBookingSystem.Bookings.Application.Queries;
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
    public class GetBookingHandler : IRequestHandler<GetBookingQuery, Booking>
    {
        private readonly IBookingRepository _repository;
        public GetBookingHandler(IBookingRepository repository)
        {
            _repository = repository;
        }
        public async Task<Booking> Handle(GetBookingQuery query, CancellationToken cancellationToken)
        {
            return await _repository.GetBookingByIdAsync(query.Id);
        }
    }
}
