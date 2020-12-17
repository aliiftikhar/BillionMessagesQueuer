using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillionMessagesQueuer.Providers.Services.QueueService.AzureStorageQueue
{
    public class AzureStorageQueueConfiguration
    {
        public string ConnectionString { get; set; }
        public string QueueName { get; set; }
    }
}
