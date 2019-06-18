using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueService.AzureStorage.QueueManagement
{
    public static class InitializeAzureStorage
    {
        public static async Task CreateTablesIfNotExists(IOptions<AzureStorageSettings> settings)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(settings.Value.ConnectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            foreach(var tableName in settings.Value.TablesToCreate)
            {
                await tableClient.GetTableReference(tableName).CreateIfNotExistsAsync();
            }
        }
    }
}
