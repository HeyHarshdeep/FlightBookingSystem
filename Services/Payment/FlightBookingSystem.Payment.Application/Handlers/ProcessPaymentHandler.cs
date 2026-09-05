using FlightBookingSystem.BuildingBlocks.Contracts.EventBus.Messages;
using FlightBookingSystem.Payments.Application.Commands;
using FlightBookingSystem.Payments.Core.Entities;
using FlightBookingSystem.Payments.Core.Repositories;
using MassTransit;
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
        private readonly IPublishEndpoint _publishEndpoint;
        public ProcessPaymentHandler(IPaymentRepository repository, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _publishEndpoint = publishEndpoint;
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

            //Publish PaymentProcessEvent
            await _publishEndpoint.Publish(new PaymentProcessedEvent(
                payment.Id,
                payment.BookingId,
                payment.Amount,
                payment.PaymentDate
                ));

            return payment.Id;
        }
    }
}
