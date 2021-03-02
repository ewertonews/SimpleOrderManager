using System.Collections.Generic;

namespace OrderManager.Business.Models
{
    public class Order
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
