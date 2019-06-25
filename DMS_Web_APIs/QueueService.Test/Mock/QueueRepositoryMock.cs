using Common;
using QueueService.AzureStorage.Entities.QueueStorageEntities;
using QueueService.AzureStorage.Entities.TableStorageEntities;
using QueueService.AzureStorage.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QueueService.Test.Mock
{
    class QueueRepositoryMock : IQueueRepository
    {
        public Queue<QueueItem> _queueNurse { get; set; } = new Queue<QueueItem>();
        public Queue<QueueItem> _queuePharmacist { get; set; } = new Queue<QueueItem>();

        public int nextNurseNumber { get; set; } = 0;
        public int nextPharmacistNumber { get; set; } = 0;

        public QueueRepositoryMock()
        {
            _queueNurse.Enqueue(new QueueItem { UserID = 1, UserNumber = 1 });
            _queueNurse.Enqueue(new QueueItem { UserID = 2, UserNumber = 2 });
            _queueNurse.Enqueue(new QueueItem { UserID = 3, UserNumber = 3 });
            _queueNurse.Enqueue(new QueueItem { UserID = 4, UserNumber = 4 });
            _queuePharmacist.Enqueue(new QueueItem { UserID = 5, UserNumber = 1 });
            _queuePharmacist.Enqueue(new QueueItem { UserID = 6, UserNumber = 2 });
            _queuePharmacist.Enqueue(new QueueItem { UserID = 7, UserNumber = 3 });
            _queuePharmacist.Enqueue(new QueueItem { UserID = 8, UserNumber = 4 });

            nextNurseNumber = nextPharmacistNumber = 4;
        }

        public Task AddItem(ServiceType serviceType, QueueItem item)
        {
            if(serviceType == ServiceType.Pharmacist)
            {
                _queueNurse.Enqueue(item);
            }
            else if(serviceType == ServiceType.Nurse)
            {
                _queueNurse.Enqueue(item);
            }
            return Task.CompletedTask;
        }

        public Task DeleteItem(ServiceType serviceType)
        {
            return Task.CompletedTask;
        }

        public Task<CurrentQueueNumber> GetCurrentNumber(ServiceType serviceType)
        {
            CurrentQueueNumber current = new CurrentQueueNumber();
            if (serviceType == ServiceType.Nurse)
                current.NextNumber = nextNurseNumber;
            if (serviceType == ServiceType.Nurse)
                current.NextNumber = nextPharmacistNumber;

            return Task.FromResult(current);
        }

        public Task<QueueItem> GetNextItem(ServiceType serviceType)
        {
            QueueItem item = new QueueItem();
            if (serviceType == ServiceType.Nurse)
                item = _queueNurse.Dequeue();
            if (serviceType == ServiceType.Pharmacist)
                item = _queuePharmacist.Dequeue();

            return Task.FromResult(item);
        }

        public Task UpadteNextNumber(CurrentQueueNumber next, ServiceType serviceType)
        {
            if (serviceType == ServiceType.Nurse)
                nextNurseNumber++;
            if (serviceType == ServiceType.Pharmacist)
                nextPharmacistNumber++;

            return Task.CompletedTask;
        }
    }
}
