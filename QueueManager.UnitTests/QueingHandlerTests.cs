using NUnit.Framework;
using System.Threading.Tasks;

namespace OrderManager.UnitTests
{
    public class QueingHandlerTests
    {
        private IQueuingHandler<DummyQueueItem> queuingHandler;


        [SetUp]
        public void Setup()
        {
            queuingHandler = new QueuingHandler<DummyQueueItem>();
            string queueId1 = "Q1";
            string queueId2 = "Q2";
            queuingHandler.AddItemToQueue(queueId1, new DummyQueueItem { Id = 1 }).Wait();
            queuingHandler.AddItemToQueue(queueId2, new DummyQueueItem { Id = 2 }).Wait();
        }

        [Test]
        public async Task AddItemToQueueShouldAddItemSuccessfully()
        {
            string queueId = "Q4";
            await queuingHandler.AddItemToQueue(queueId, new DummyQueueItem { Id = 1 });
            var records = queuingHandler.QueuesRecord;

            Assert.That(records.ContainsKey(queueId));
        }

        [Test]
        public async Task ProcessItemFromQueueSholdReturnAqueueItem()
        {
            var dequeueResult = await queuingHandler.ProcessItemFromQueue("Q1");

            Assert.That(dequeueResult, Is.Not.Null);
        }

        [Test]
        public async Task ProcessItemFromQueueSholdReturnNull()
        {
            var dequeueResult = await queuingHandler.ProcessItemFromQueue("Q5");

            Assert.That(dequeueResult, Is.Null);
        }


        [Test]
        public async Task GetQueueByIdShouldReturnAqueueWithSomeItem()
        {           
            var resultQueue = await queuingHandler.GetQueueById("Q1");

            Assert.That(resultQueue.Count > 0, Is.True);
        }

        [Test]
        public async Task GetQueueByIdShoulfReturnNullQueue()
        {
            var resultQueue = await queuingHandler.GetQueueById("Q3");

            Assert.That(resultQueue, Is.Null);
        }

        [Test]
        public async Task RemoveItemFromQueueShouldRemoveItemSuccessfully()
        {
            string queueId2 = "Q2";
            queuingHandler.AddItemToQueue(queueId2, new DummyQueueItem { Id = 3 }).Wait();
            var dummyQueueItem = new DummyQueueItem
            {
                Id = 2
            };

            await queuingHandler.RemoveItemFromQueue(queueId2, dummyQueueItem);

            Assert.That(queuingHandler.QueuesRecord[queueId2].Contains(dummyQueueItem), Is.False);
        }
    }
}