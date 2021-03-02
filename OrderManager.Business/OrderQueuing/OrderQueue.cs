using OrderManager.Business.Exceptions;
using OrderManager.Business.Factories;
using OrderManager.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManager.Business.OrderQueuing
{
    public class OrderQueue : IOrderQueue
    {
        private IQueuingHandler<OrderItem> queuingHandler;
        private readonly IOrderQueuingResultFactory queuingResultFactory;

        public OrderQueue(IQueuingHandler<OrderItem> queuingHandler, IOrderQueuingResultFactory queuingResultFactory)
        {
            this.queuingHandler = queuingHandler;
            this.queuingResultFactory = queuingResultFactory;
        }

        public async Task<IOrderQueuingResult> EnqueueOrder(Order order)
        {
            var queuingResult = queuingResultFactory.Create();
            try
            {
                ValidateOrder(order);

                var enqueuingTasks = order.OrderItems.Select(async orderItem =>
                {
                    await queuingHandler.AddItemToQueue(orderItem.KitchenArea, orderItem);
                });

                await Task.WhenAll(enqueuingTasks);

                queuingResult.Message = "Order placed";
                queuingResult.QueuingStatus = OrderQueuingStatus.SUCCESS;
                return queuingResult;
            }
            catch (InvalidOrderException ivo)
            {
                queuingResult.Message = $"Failed to place order item: {ivo.Message}";
                queuingResult.QueuingStatus = OrderQueuingStatus.INVALIDORDER;
                return queuingResult;
            }
            catch (Exception ex)
            {
                await RemoveItemsFromQueueAsync(order.OrderItems);
                queuingResult.Message = $"Failed to place order item: {ex.Message}";
                queuingResult.QueuingStatus = OrderQueuingStatus.GENERALERROR;
                return queuingResult;
            }
        }

        private static void ValidateOrder(Order order)
        {
            if (order.OrderItems.Count == 0 || !Enum.IsDefined(typeof(OrderStatus), order.Status))
            {
                throw new InvalidOrderException("Invalid order item");
            }
        }

        private async Task RemoveItemsFromQueueAsync(List<OrderItem> orderItems)
        {
            //The method RemoveItemFromQueue can create much garbage in the heap memory and give much work
            //to the garbage collector, but I am counting it won't be called frequently. 
            //There is a better way to handle queueItems removal.
            foreach (var item in orderItems)
            {
                await queuingHandler.RemoveItemFromQueue(item.KitchenArea, item);
            }
        }
    }
}
