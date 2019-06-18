using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueService.AzureStorage.QueueManagement
{
    public class AzureStorageSettings
    {
        public string ConnectionString { get; set; }
        public List<string> TablesToCreate { get; set; }
    }
}
