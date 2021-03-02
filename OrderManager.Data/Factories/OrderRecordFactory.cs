namespace OrderManager.Data.Factories
{
    public class OrderRecordFactory : IOrderRecordFactory
    {
        public OrderRecord Create()
        {
            return new OrderRecord();
        }
    }
}
