using Common;
using Common.QueueModels;
using NUnit.Framework;
using QueueService.AzureStorage;
using QueueService.AzureStorage.Repository;
using QueueService.Test.Mock;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QueueService.Test
{
    [TestFixture]
    class QueueStorageServiceTests
    {
        public static IQueueRepository CreateQueueRepository()
        {
            return new QueueRepositoryMock();
        }

        [Test]
        public async Task QueueServiceTest_AddToQueue()
        {
            QueueStorageService serviceTest = new QueueStorageService(CreateQueueRepository());

            Assert.ThrowsAsync<ArgumentNullException>( async () => { await serviceTest.AddToQueue(null); });

            EnqueuePosition testItem = new EnqueuePosition { ServiceType = ServiceType.Nurse, UserID = 1313 };
            await serviceTest.AddToQueue(testItem);

        }

        [Test]
        public async Task QueueServiceTest_RemoveFromQueue()
        {

        }
    }
}
