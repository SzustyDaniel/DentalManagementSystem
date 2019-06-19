using Common.QueueModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueService.AzureStorage
{
    public interface IQueueStorageService
    {
        Task<EnqueuePositionResult> AddToQueue(EnqueuePosition item);

        Task<DequeuePositionResult> RemoveFromQueue(DequeuePosition item);
    }
}
