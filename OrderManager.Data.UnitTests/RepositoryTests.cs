using NUnit.Framework;

namespace OrderManager.Data.UnitTests
{
    public class RepositoryTests
    {
        //If I had a real DB, I would created a MockOrderRepository with a list inside.
        private IOrderRepository repository = new OrderRepository();

        [SetUp]
        public void Setup()
        {
            var item1 = new OrderRecord()
            {
                Id = 1,
                Status = "Received"
            };

            var item2 = new OrderRecord()
            {
                Id = 2,
                Status = "Received"
            };
            var item3 = new OrderRecord()
            {
                Id = 3,
                Status = "Received"
            };

            repository.Add(item1);
            repository.Add(item2);
            repository.Add(item3);
        }

        [Test]
        public void ListAllShoutReturnAllItems()
        {           

            var items = repository.listAll();
            Assert.That(items.Count >= 3);
        }

        [Test]
        public void GetByIdShouldReturnCorrectOrderRecordItem()
        {
            var orderRecord = repository.GetById(3);
            Assert.That(orderRecord, Is.Not.Null);
            Assert.That(orderRecord.Id, Is.EqualTo(3));
        }

        [Test]
        public void AddShouldAddItemToRecordsList()
        {
            var item4 = new OrderRecord()
            {
                Id = 4,
                Status = "Received"
            };

            repository.Add(item4);
            Assert.That(repository.listAll().Contains(item4));
        }

        [Test]
        public void RemoveShouldRemoveItemSuccefully()
        {
            var item3 = new OrderRecord()
            {
                Id = 3,
                Status = "Received"
            };
            repository.Remove(item3);
            Assert.That(!repository.listAll().Contains(item3));
        }

        [Test]
        public void UpdateShouldUpdateItemSuccessfully()
        {
            var item4 = new OrderRecord()
            {
                Id = 4,
                Status = "Received"
            };            

            repository.Add(item4);

            string newStatus = "In Progress";
            item4.Status = newStatus;
            repository.Update(item4);
            var updatedOrderRecord = repository.GetById(4);

            Assert.That(updatedOrderRecord.Status, Is.EqualTo(newStatus));
        }

    }
}