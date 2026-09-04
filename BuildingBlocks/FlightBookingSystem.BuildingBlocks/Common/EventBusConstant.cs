using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.BuildingBlocks.Common
{
    public class EventBusConstant
    {
        public const string FlightBookedQueue = "flight-booked-queue";
        public const string PaymentProcessedQueue = "payment-processed-queue";
        public const string NotificationSentQueue = "notification-sent-queue";
    }
}
