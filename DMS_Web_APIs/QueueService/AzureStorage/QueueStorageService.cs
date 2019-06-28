using Common.QueueModels;
using QueueService.AzureStorage.Entities.QueueStorageEntities;
using QueueService.AzureStorage.Entities.TableStorageEntities;
using QueueService.AzureStorage.Repository;
using System;
using System.Threading.Tasks;

namespace QueueService.AzureStorage
{
    public class QueueStorageService : IQueueStorageService
    {
        private readonly IQueueRepository _repository;

        public QueueStorageService(IQueueRepository repository)
        {
            _repository = repository;
        }

        public async Task<EnqueuePositionResult> AddToQueue(EnqueuePosition item)
        {
            if(item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            CurrentQueueNumber currentNumber = await _repository.GetCurrentNumber(item.ServiceType);

            currentNumber.NextNumber++;
            await _repository.UpadteNextNumber(currentNumber, item.ServiceType);

            QueueItem newItem = new QueueItem()
            {
                UserID = item.UserID,
                UserNumber = currentNumber.NextNumber
            };

            await _repository.AddItem(item.ServiceType, newItem);

            return new EnqueuePositionResult { UserNumber = newItem.UserNumber };
        }

        public async Task<DequeuePositionResult> RemoveFromQueue(DequeuePosition item)
        {
            if(item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            try
            {
                QueueItem nextItem = await _repository.GetNextItem(item.ServiceType);

                if (nextItem is null)
                {
                    return null;
                }

                return new DequeuePositionResult
                {
                    CustomerID = nextItem.UserID,
                    CustomerNumberInQueue = nextItem.UserNumber
                };
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}
