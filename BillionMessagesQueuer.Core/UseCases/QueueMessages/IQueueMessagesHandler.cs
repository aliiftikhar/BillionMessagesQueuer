using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BillionMessagesQueuer.Core.UseCases.QueueMessages
{
    public interface IQueueMessagesHandler
    {
        Task Handle(QueueMessagesRequest request);
    }
}
