using FlightBookingSystem.Notifications.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.Notifications.Application.Interface
{
    public interface INotificationService
    {
        Task SendNotificationAsync(Notification notification);
    }
}
