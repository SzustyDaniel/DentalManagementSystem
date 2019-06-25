using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json.Linq;
using QueueService.AzureStorage.Entities.QueueStorageEntities;
using QueueService.AzureStorage.Entities.TableStorageEntities;
using QueueService.AzureStorage.StorageManagement;

namespace QueueService.AzureStorage.Repository
{
    public class QueueRepository : IQueueRepository
    {
        private readonly AzureStorageSettings _options;
        private readonly CloudTableClient _tableClient;
        private readonly CloudQueueClient _queueClient;

        public QueueRepository(IOptions<AzureStorageSettings> options)
        {
            _options = options.Value;
            var storageAccount = CloudStorageAccount.Parse(_options.ConnectionString);
            _tableClient = storageAccount.CreateCloudTableClient();
            _queueClient = storageAccount.CreateCloudQueueClient();
        }

        public async Task AddItem(ServiceType serviceType ,QueueItem item)
        {
            CloudQueueMessage newMessage = new CloudQueueMessage(JObject.FromObject(item).ToString());
            await _queueClient.GetQueueReference(serviceType.ToString().ToLower()).AddMessageAsync(newMessage);
        }

        public async Task DeleteItem(ServiceType serviceType)
        {
            CloudQueue queue = _queueClient.GetQueueReference(serviceType.ToString().ToLower());
            await queue.DeleteMessageAsync(await queue.GetMessageAsync());
        }

        public async Task<QueueItem> GetNextItem(ServiceType serviceType)
        {
            string service = serviceType.ToString().ToLower();
            CloudQueueMessage item = await _queueClient.GetQueueReference(service).GetMessageAsync();
            if(item is null)
            {
                throw new NullReferenceException();
            }
            return JObject.Parse(item.AsString).ToObject<QueueItem>();
        }

        public async Task<CurrentQueueNumber> GetCurrentNumber(ServiceType serviceType)
        {
            string service = serviceType.ToString().ToLower();

            TableResult result = null;
            try
            {
                TableOperation operation = TableOperation.Retrieve<CurrentQueueNumber>(service, service);
                result = await _tableClient.GetTableReference(_options.NextTable).ExecuteAsync(operation);
            }
            catch(Exception e)
            {
                throw new Exception("Error in retrive operation", e);
            }
            return result.Result as CurrentQueueNumber;
        }

        public async Task UpadteNextNumber(CurrentQueueNumber next, ServiceType serviceType)
        {
            string service = serviceType.ToString();
            try
            {
                await _tableClient.GetTableReference(_options.NextTable).ExecuteAsync(TableOperation.InsertOrReplace(next));
            }
            catch(Exception e)
            {
                throw new Exception("", e);
            }
        }
    }
}
