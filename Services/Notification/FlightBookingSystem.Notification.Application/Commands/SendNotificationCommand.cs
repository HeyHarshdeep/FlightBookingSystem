using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.Notifications.Application.Commands
{
    public record SendNotificationCommand(string Recipient, string Message, string Type) : IRequest;

}
