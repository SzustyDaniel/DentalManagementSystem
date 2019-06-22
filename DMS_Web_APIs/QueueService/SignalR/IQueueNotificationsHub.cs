using Common;
using Common.QueueModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueService.SignalR
{
    public interface IQueueNotificationsHub
    {
        Task SendQueueNotificationToGroup(QueueNotification notification);
    }
}
