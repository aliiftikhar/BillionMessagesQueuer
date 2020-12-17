using System;

namespace BillionMessagesQueuer.Core.UseCases.QueueMessages
{
    public class QueueMessagesRequest
    {
        public int NumberOfMessagesToQueue { get; set; }
    }
}