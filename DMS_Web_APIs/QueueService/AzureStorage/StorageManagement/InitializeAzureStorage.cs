using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;
using QueueService.AzureStorage.Entities.TableStorageEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueService.AzureStorage.StorageManagement
{
    public static class InitializeAzureStorage
    {
        public static async Task CreateStorageIfNotExists(IOptions<AzureStorageSettings> settings)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(settings.Value.ConnectionString);

            await storageAccount.CreateCloudQueueClient().CreateQueueStorage(settings.Value.QueuesToCreate);
            await storageAccount.CreateCloudTableClient().CreateAndSeedTableStorage(settings.Value.NextTable, settings.Value.QueuesToCreate);
        }

        private static async Task CreateAndSeedTableStorage(this CloudTableClient tableClient, string nextTable, List<string> rows)
        {
            CloudTable table = tableClient.GetTableReference(nextTable);
            await table.DeleteIfExistsAsync();
            if (await table.CreateIfNotExistsAsync())
            {
                foreach (var row in rows)
                {
                    TableOperation operation = TableOperation.Insert(new CurrentQueueNumber(row) { NextNumber = 0 });
                    await table.ExecuteAsync(operation);
                };
            }
        }

        private static async Task CreateQueueStorage(this CloudQueueClient queueClient, List<string> queueNames)
        {
            foreach (var name in queueNames)
            {
                var queue = queueClient.GetQueueReference(name);
                await queue.DeleteIfExistsAsync();
                await queue.CreateIfNotExistsAsync();
            }
        }
    }
}
