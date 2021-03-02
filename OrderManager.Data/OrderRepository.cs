using System.Collections.Generic;
using System.Linq;

namespace OrderManager.Data
{
    public class OrderRepository : IOrderRepository
    {
        private List<OrderRecord> OrderRecords;

        public OrderRepository()
        {
            OrderRecords = new List<OrderRecord>();
        }
        public void Add(OrderRecord orderRecord)
        {
            OrderRecords.Add(orderRecord);
        }

        public OrderRecord GetById(int id)
        {
            return OrderRecords.FirstOrDefault(or => or.Id == id);
        }

        public List<OrderRecord> listAll()
        {
            return OrderRecords;
        }

        public void Remove(OrderRecord orderRecord)
        {
            OrderRecords.Remove(orderRecord);
        }

        public void Update(OrderRecord updatedOrderRecord)
        {
            int index = OrderRecords.IndexOf(updatedOrderRecord);
            OrderRecords[index] = updatedOrderRecord;
        }
    }
}
