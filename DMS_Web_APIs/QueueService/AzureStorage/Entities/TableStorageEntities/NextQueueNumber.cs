using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueService.AzureStorage.Entities.TableStorageEntities
{
    public class CurrentQueueNumber : TableEntity
    {
        public int NextNumber { get; set; }

        public CurrentQueueNumber()
        {

        }

        public CurrentQueueNumber(string serviceType)
        {
            PartitionKey = serviceType;
            RowKey = serviceType;
        }
    }
}
