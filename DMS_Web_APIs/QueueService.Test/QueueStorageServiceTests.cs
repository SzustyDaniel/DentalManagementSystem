using NUnit.Framework;
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
            
        }

        [Test]
        public async Task QueueServiceTest_RemoveFromQueue()
        {

        }
    }
}
