using FlightBookingSystem.Payments.Application.Commands;
using FlightBookingSystem.Payments.Core.Entities;
using FlightBookingSystem.Payments.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.Payments.Application.Handlers
{
    public class ProcessPaymentHandler : IRequestHandler<ProcessPaymentCommand, Guid>
    {
        private readonly IPaymentRepository _repository;
        public ProcessPaymentHandler(IPaymentRepository repository)
        {
            _repository = repository;
        }
        public async Task<Guid> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                BookingId = request.BookingId,
                Amount = request.Amount,
                PaymentDate = DateTime.UtcNow
            };

            await _repository.ProcessPaymentAsync(payment);

            return payment.Id;
        }
    }
}
