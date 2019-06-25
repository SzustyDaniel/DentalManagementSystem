using Common;
using Common.QueueModels;
using NUnit.Framework;
using QueueService.AzureStorage;
using QueueService.AzureStorage.Entities.QueueStorageEntities;
using QueueService.AzureStorage.Repository;
using QueueService.Test.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueService.Test
{
    [TestFixture]
    class QueueStorageServiceTests
    {
        public static QueueRepositoryMock CreateQueueRepository()
        {
            return new QueueRepositoryMock();
        }

        [Test]
        public async Task QueueServiceTest_AddToQueue()
        {
            QueueRepositoryMock repository = CreateQueueRepository();
            IQueueStorageService serviceTest = new QueueStorageService(repository);

            Assert.ThrowsAsync<ArgumentNullException>( async () => { await serviceTest.AddToQueue(null); });

            EnqueuePosition testItem = new EnqueuePosition { ServiceType = ServiceType.Nurse, UserID = 1313 };
            var result = await serviceTest.AddToQueue(testItem);
            var queueItem = repository._queueNurse.FirstOrDefault(i => i.UserID == 1313);
            Assert.AreEqual(queueItem.UserNumber, result.UserNumber);
        }

        [Test]
        public async Task QueueServiceTest_RemoveFromQueue()
        {

        }
    }
}
