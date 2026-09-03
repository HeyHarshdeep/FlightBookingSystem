using Dapper;
using FlightBookingSystem.Bookings.Core.Entities;
using FlightBookingSystem.Bookings.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.Bookings.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IDbConnection _dbConnection;
        public BookingRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task AddBookingAsync(Booking booking)
        {
            const string sql = @"
        INSERT INTO Bookings (Id, FlightId, PassengerName, SeatNumber, BookingDate)
        VALUES (@Id, @FlightId, @PassengerName, @SeatNumber, @BookingDate)";

            _dbConnection.ExecuteAsync(sql, booking);

        }

        public async Task<Booking> GetBookingByIdAsync(Guid id)
        {
            const string sql = "SELECT * FROM Bookings WHERE Id = @Id";
            return await _dbConnection.QuerySingleOrDefaultAsync<Booking>(sql, new { Id = id });
        }
    }
}
