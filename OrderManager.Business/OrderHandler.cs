using OrderManager.Business.Models;
using OrderManager.Business.OrderQueuing;
using OrderManager.Data;
using OrderManager.Data.Factories;
using System.Threading.Tasks;

namespace OrderManager.Business
{
    public class OrderHandler : IOrderHandler
    {
        private readonly IOrderQueue orderQueue;
        private readonly IOrderRepository orderRepository;
        private readonly IOrderRecordFactory orderRecordFactory;

        public OrderHandler(IOrderQueue orderQueue, IOrderRepository orderRepository, IOrderRecordFactory orderRecordFactory)
        {
            this.orderQueue = orderQueue;
            this.orderRepository = orderRepository;
            this.orderRecordFactory = orderRecordFactory;
        }

        public async Task<IOrderQueuingResult> PlaceOrder(Order order)
        {
            var queuingResult = await orderQueue.EnqueueOrder(order);

            if (queuingResult.QueuingStatus == OrderQueuingStatus.SUCCESS)
            {
                CreateOrderRecord(order);
            }
            return queuingResult;
        }

        private void CreateOrderRecord(Order order)
        {
            var orderRecord = orderRecordFactory.Create();
            orderRecord.Id = order.Id;
            orderRecord.Status = OrderStatus.Received.ToString();
            orderRepository.Add(orderRecord);
            //The other methods of the repository would be used by the queue consumer app.
        }
    }
}
