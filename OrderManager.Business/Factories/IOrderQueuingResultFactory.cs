using OrderManager.Business.OrderQueuing;

namespace OrderManager.Business.Factories
{
    public interface IOrderQueuingResultFactory
    {
        IOrderQueuingResult Create();
    }
}