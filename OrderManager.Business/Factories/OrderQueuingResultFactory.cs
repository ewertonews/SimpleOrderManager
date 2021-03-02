using OrderManager.Business.OrderQueuing;

namespace OrderManager.Business.Factories
{
    public class OrderQueuingResultFactory : IOrderQueuingResultFactory
    {
        public IOrderQueuingResult Create()
        {
            return new OrderQueuingResult();
        }
    }
}
