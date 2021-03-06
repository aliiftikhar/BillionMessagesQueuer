﻿using Azure.Storage.Queues;
using BillionMessagesQueuer.Core.Services.QueueService;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BillionMessagesQueuer.Providers.Services.QueueService.AzureStorageQueue
{
    public class AzureStorageQueueService : IQueueService
    {
        private readonly AzureStorageQueueConfiguration azureStorageQueueConfiguration;
        private readonly QueueClient queueClient;

        public AzureStorageQueueService(IOptions<AzureStorageQueueConfiguration> azureStorageQueueConfiguration)
        {
            this.azureStorageQueueConfiguration = azureStorageQueueConfiguration?.Value ?? throw new ArgumentNullException(nameof(azureStorageQueueConfiguration));

            if (string.IsNullOrWhiteSpace(this.azureStorageQueueConfiguration.ConnectionString))
                throw new ArgumentNullException(nameof(this.azureStorageQueueConfiguration.ConnectionString));

            if (string.IsNullOrWhiteSpace(this.azureStorageQueueConfiguration.QueueName))
                throw new ArgumentNullException(nameof(this.azureStorageQueueConfiguration.QueueName));

            queueClient = new QueueClient(this.azureStorageQueueConfiguration.ConnectionString, this.azureStorageQueueConfiguration.QueueName);
        }

        public async Task Queue<T>(T message)
        {
            var base64EncodeddMessage = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message)));

            await queueClient.SendMessageAsync(base64EncodeddMessage);
        }
    }
}
