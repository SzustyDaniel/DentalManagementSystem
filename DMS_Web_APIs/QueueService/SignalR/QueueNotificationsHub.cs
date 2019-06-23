using Common;
using Common.QueueModels;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueService.SignalR
{
    public class QueueNotificationsHub : Hub<IQueueNotificationsHub>
    {
        public async Task AddToGroup(string serviceType)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, serviceType);
        }

        public async Task SendQueueNotificationToGroup(ServiceType serviceType, QueueNotification notification)
        {
            await Clients.Groups(serviceType.ToString()).SendQueueNotificationToGroup(notification);
        }
    }
}
