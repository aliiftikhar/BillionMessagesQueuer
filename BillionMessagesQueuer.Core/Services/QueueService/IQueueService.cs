using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BillionMessagesQueuer.Core.Services.QueueService
{
    public interface IQueueService
    {
        Task Queue<T>(T message);
    }
}
