using Common;
using Common.QueueModels;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json.Linq;
using QueueService.AzureStorage.Entities.QueueStorageEntities;
using QueueService.AzureStorage.Entities.TableStorageEntities;
using QueueService.AzureStorage.StorageManagement;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace QueueService.AzureStorage
{
    public class QueueStorageService  : IQueueStorageService
    {
        private readonly AzureStorageSettings _storageSettings;

        private readonly CloudTableClient _tableClient;
        private readonly CloudQueueClient _queueClient;

        public QueueStorageService(IOptions<AzureStorageSettings> settings)
        {
            _storageSettings = settings.Value;
            var storageAccount = CloudStorageAccount.Parse(_storageSettings.ConnectionString);
            _tableClient = storageAccount.CreateCloudTableClient();
            _queueClient = storageAccount.CreateCloudQueueClient();
        }

        public async Task<EnqueuePositionResult> AddToQueue(EnqueuePosition newItem)
        {
            string serivceType = newItem.ServiceType.ToString().ToLower();

            int nextNumber = await GetAndUpdateNextQueueNumber(serivceType);

            JObject result = JObject.FromObject(new QueueItem { UserID = newItem.UserID, UserNumber = nextNumber });
            CloudQueueMessage queueMessage = new CloudQueueMessage(result.ToString());
            await _queueClient.GetQueueReference(serivceType).AddMessageAsync(queueMessage);

            return new EnqueuePositionResult { UserNumber = nextNumber };
        }

        private async Task<int> GetAndUpdateNextQueueNumber(string serviceType)
        {
            var table = _tableClient.GetTableReference(_storageSettings.NextTable);

            TableResult result = await table.ExecuteAsync(TableOperation.Retrieve<NextQueueNumber>(serviceType, serviceType));
            if(result is null)
            {

            }
            if(result.HttpStatusCode == (int)HttpStatusCode.NotFound)
            {

            }

            NextQueueNumber current = result.Result as NextQueueNumber;
            current.NextNumber++;

            TableOperation operation = TableOperation.InsertOrReplace(current);
            await table.ExecuteAsync(operation);

            return current.NextNumber;
        }

    }
}
