using Common.QueueModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueService.AzureStorage
{
    public interface IAzureQueueService
    {
        Task<EnqueuePositionResult> AddToQueue(EnqueuePosition newItem);
        Task RemoveToQueue(string queueName);
    }
}
