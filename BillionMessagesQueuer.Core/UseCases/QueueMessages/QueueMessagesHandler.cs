using BillionMessagesQueuer.Core.Services.QueueService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BillionMessagesQueuer.Core.UseCases.QueueMessages
{
    public class QueueMessagesHandler : IQueueMessagesHandler
    {
        private readonly ILogger<QueueMessagesHandler> logger;
        private readonly IQueueService queueService;
        public QueueMessagesHandler(ILogger<QueueMessagesHandler> logger, IQueueService queueService)
        {
            this.logger = logger;
            this.queueService = queueService;
        }

        public async Task Handle(QueueMessagesRequest request)
        {
            LogMessage("QueueMessagesRequest started");

            ValidateRequest(request);

            var tasks = new List<Task>();

            var instanceId = Guid.NewGuid(); 
            
            for (int i = 1; i <= request.NumberOfMessagesToQueue; i++)
            {
                var message = new Message
                {
                    Id = Guid.NewGuid(),
                    CreatedOnUtc = DateTime.UtcNow,
                    InstanceId = instanceId,
                    Content = $"Message number: {i} created for instance id: {instanceId}"
                };

                tasks.Add(this.queueService.Queue(message));

                if(i%1000 == 0)
                    LogMessage($"{i} of {request.NumberOfMessagesToQueue} queued");
            }

            LogMessage($"Number of Messages Queued: {tasks.Count}");

            await Task.WhenAll(tasks.ToArray());

            LogMessage("QueueMessagesRequest ended");
        }

        private void LogMessage(string message)
        {
            this.logger.LogInformation($"{DateTime.UtcNow} - {message}");
        }

        private void ValidateRequest(QueueMessagesRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
        }
    }
}
