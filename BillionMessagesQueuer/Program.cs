using BillionMessagesQueuer.Core.Services.QueueService;
using BillionMessagesQueuer.Core.UseCases.QueueMessages;
using BillionMessagesQueuer.Providers.Services.QueueService.AzureStorageQueue;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MessageQueuer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var services = new ServiceCollection();
            ConfigureServices(services, configuration);

            var serviceProvider = services.BuildServiceProvider();

            await QueueMessages(configuration, serviceProvider.GetService<IQueueMessagesHandler>());
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(builder =>
            {
                builder.AddConsole();
            });

            services.Configure<AzureStorageQueueConfiguration>(configuration.GetSection("AzureStorageQueue"));

            services.AddTransient<IQueueService, AzureStorageQueueService>();
            services.AddTransient<IQueueMessagesHandler, QueueMessagesHandler>();
        }

        private static async Task QueueMessages(IConfiguration configuration, IQueueMessagesHandler queueMessagesHandler)
        {
            int numberOfMessages;
            int.TryParse(configuration.GetValue<string>("QueueMessagesConfig:NumberOfMessagesToQueue"), out numberOfMessages);

            var request = new QueueMessagesRequest
            {
                NumberOfMessagesToQueue = numberOfMessages
            };

            await queueMessagesHandler.Handle(request);
        }
    }
}
