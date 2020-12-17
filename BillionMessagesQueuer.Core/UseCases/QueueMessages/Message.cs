using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillionMessagesQueuer.Core.UseCases.QueueMessages
{
    public class Message
    {
        public Guid Id { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public Guid InstanceId { get; set; }
        public string Content { get; set; }
    }
}
