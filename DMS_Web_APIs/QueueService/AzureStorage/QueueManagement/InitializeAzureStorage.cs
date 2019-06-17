using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueService.AzureStorage.QueueManagement
{
    public static class InitializeAzureStorage
    {
        public static async Task CreateQueuesIfNotExists(IOptions<AzureStorageSettings> settings)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(settings.Value.ConnectionString);
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            foreach(var queueName in settings.Value.QueueNames)
            {
                await queueClient.GetQueueReference(queueName).CreateIfNotExistsAsync();
            }
        }
    }
}
