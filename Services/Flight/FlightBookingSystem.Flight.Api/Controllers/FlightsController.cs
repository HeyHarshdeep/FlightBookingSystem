using FlightBookingSystem.Flights.Application.Commands;
using FlightBookingSystem.Flights.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlightBookingSystem.Flights.Api.Controllers
{
    [ApiController]
    [Route("api/flights")]
    public class FlightsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FlightsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetFlights()
        {
            var flights = await _mediator.Send(new GetAllFlightsQuery());
            return Ok(flights);
        }

        [HttpPost]
        public async Task<IActionResult> AddFlight([FromBody] CreateFlightCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetFlights), new { id }, command);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(Guid id)
        {
            await _mediator.Send(new DeleteFlightCommand(id));
            return NoContent();
        }
    }
}
