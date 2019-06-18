using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueService.AzureStorage.Entities.TableStorageEntities
{
    public class NextQueueNumber : TableEntity
    {
        public int NextNumber { get; set; }

        public NextQueueNumber()
        {

        }

        public NextQueueNumber(string serviceType)
        {
            PartitionKey = serviceType;
            RowKey = serviceType;
        }
    }
}
