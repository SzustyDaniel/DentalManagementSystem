using Common;
using Microsoft.WindowsAzure.Storage.Queue;
using QueueService.AzureStorage.Entities.QueueStorageEntities;
using QueueService.AzureStorage.Entities.TableStorageEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueService.AzureStorage.Repository
{
    public interface IQueueRepository
    {
        Task DeleteItem(ServiceType serviceType);

        Task<QueueItem> GetNextItem(ServiceType serviceType);

        Task AddItem(ServiceType serviceType, QueueItem item);

        Task<CurrentQueueNumber> GetCurrentNumber(ServiceType serviceType);

        Task UpadteNextNumber(CurrentQueueNumber next, ServiceType serviceType);
    }
}
