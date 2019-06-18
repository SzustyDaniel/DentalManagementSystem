using Common.QueueModels;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json.Linq;
using QueueService.AzureStorage.QueueManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueService.AzureStorage
{
    public class AzureQueueService  : IAzureQueueService
    {
        private readonly CloudTableClient _tableClient;

        public AzureQueueService(IOptions<AzureStorageSettings> settings)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(settings.Value.ConnectionString);
            _tableClient = storageAccount.CreateCloudTableClient();
        }

        public async Task<EnqueuePositionResult> AddToQueue(EnqueuePosition newItem)
        {
            string newItemAsJsonString = string.Empty;
            try
            {
                newItemAsJsonString = JObject.FromObject(newItem).ToString();
            }
            catch (Exception)
            {

            }

            TableEntity entity = new TableEntity()
            {
                
            };

            string tableName = newItem.ServiceType.ToString().ToLower();
            CloudTable table = _tableClient.GetTableReference(tableName);

            

            return null;
        }

        public Task RemoveToQueue(string queueName)
        {
            return null;
        }
    }
}
