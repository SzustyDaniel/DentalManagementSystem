using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueService.AzureStorage.StorageManagement
{
    public class AzureStorageSettings
    {
        public string ConnectionString { get; set; }
        public string NextTable { get; set; }
        public List<string> QueuesToCreate { get; set; }
    }
}
