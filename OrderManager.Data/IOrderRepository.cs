namespace OrderManager.Data
{
    public interface IOrderRepository
    {
        void Add(OrderRecord item);
        OrderRecord GetById(int id);
        System.Collections.Generic.List<OrderRecord> listAll();
        void Remove(OrderRecord item);
        void Update(OrderRecord item);
    }
}