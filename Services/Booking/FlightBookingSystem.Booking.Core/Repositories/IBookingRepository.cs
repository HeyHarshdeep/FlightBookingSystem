using FlightBookingSystem.Bookings.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.Bookings.Core.Repositories
{
    public interface IBookingRepository
    {
        Task<Booking> GetBookingByIdAsync(Guid id);
        Task AddBookingAsync(Booking booking);
    }
}
