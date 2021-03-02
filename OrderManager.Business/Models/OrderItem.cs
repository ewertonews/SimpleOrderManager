using System;

namespace OrderManager.Business.Models
{
    public class OrderItem : IComparable
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string KitchenArea { get; set; }
        public string ItemDescription { get; set; }
        public decimal ItemPrice { get; set; }
        public bool Ready { get; set; }

        public int CompareTo(object otherOrderItem)
        {
            if (Id < ((OrderItem)otherOrderItem).Id) return -1;
            if (Id == ((OrderItem)otherOrderItem).Id) return 0;
            return 1;
        }
    }
}
