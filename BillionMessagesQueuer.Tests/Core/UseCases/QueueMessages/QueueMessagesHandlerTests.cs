using BillionMessagesQueuer.Core.Services.QueueService;
using BillionMessagesQueuer.Core.UseCases.QueueMessages;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillionMessagesQueuer.Tests.Core.UseCases.QueueMessages
{
    public class QueueMessagesHandlerTests
    {
        private Mock<ILogger<QueueMessagesHandler>> logger;
        private Mock<IQueueService> queueService;

        [SetUp]
        public void Setup()
        {
            logger = new Mock<ILogger<QueueMessagesHandler>>();
            queueService = new Mock<IQueueService>();
        }

        [Test]
        public void ArgumentNullException_for_null_request()
        {
            var sut = new QueueMessagesHandler(logger.Object, queueService.Object);

            Assert.ThrowsAsync<ArgumentNullException>(() => sut.Handle(null));
        }

        [Test]
        public async Task Handler_should_queue_correct_number_of_messages()
        {
            queueService.Setup(x => x.Queue(It.IsAny<Message>())).Returns(Task.CompletedTask);

            var sut = new QueueMessagesHandler(logger.Object, queueService.Object);

            var request = new QueueMessagesRequest
            {
                NumberOfMessagesToQueue = 10
            };

            await sut.Handle(request);

            queueService.Verify(x => x.Queue(It.IsAny<Message>()), Times.Exactly(10));
        }
    }
}
