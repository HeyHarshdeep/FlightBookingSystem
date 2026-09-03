using FlightBookingSystem.Payments.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.Payments.Core.Repositories
{
    public interface IPaymentRepository
    {
        Task ProcessPaymentAsync(Payment payment);
        Task RefundPaymentAsync(Guid id);
    }
}
