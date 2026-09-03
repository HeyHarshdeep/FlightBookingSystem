using Dapper;
using FlightBookingSystem.Flights.Core.Entities;
using FlightBookingSystem.Flights.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.Flights.Infrastructure.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly IDbConnection _dbConnection;
        public FlightRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task AddFlightAsync(Flight flight)
        {
            const string sql = @"
            INSERT INTO Flights (Id, FlightNumber, Origin, Destination, DepartureTime, ArrivalTime)
            VALUES (@Id, @FlightNumber, @Origin, @Destination, @DepartureTime, @ArrivalTime)";

            await _dbConnection.ExecuteAsync(sql, flight);
        }

        public async Task DeleteFlightAsync(Flight flight)
        {
            const string sql = "DELETE FROM Flights WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, new { Id = flight.Id });
        }

        public async Task<IEnumerable<Flight>> GetFlightAsync()
        {
            const string sql = "SELECT * FROM Flights";
            return await _dbConnection.QueryAsync<Flight>(sql);
        }
    }
}
