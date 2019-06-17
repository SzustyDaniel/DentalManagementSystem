using Common.QueueModels;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
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
        private readonly CloudQueueClient _queueClient;

        public AzureQueueService(IOptions<AzureStorageSettings> settings)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(settings.Value.ConnectionString);
            _queueClient = storageAccount.CreateCloudQueueClient();
        }

        public async Task<EnqueuePositionResult> AddToQueue(EnqueuePosition newItem)
        {
            string queueName = newItem.ServiceType.ToString().ToLower();
            CloudQueue queue = _queueClient.GetQueueReference(queueName);

            string newItemAsJsonString = string.Empty;
            try
            {
                newItemAsJsonString = JObject.FromObject(newItem).ToString();
            }
            catch (Exception)
            {

            }

            CloudQueueMessage message = new CloudQueueMessage(newItemAsJsonString);
            await queue.AddMessageAsync(message);
            await queue.FetchAttributesAsync();

            int? UserNumber = queue.ApproximateMessageCount;
            if (UserNumber.HasValue)
            {
                return new EnqueuePositionResult { UserNumber = UserNumber.Value };
            }
            return null;
        }

        public Task RemoveToQueue(string queueName)
        {
            return null;
        }
    }
}
