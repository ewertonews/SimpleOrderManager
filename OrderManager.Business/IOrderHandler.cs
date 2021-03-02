using OrderManager.Business.Models;
using OrderManager.Business.OrderQueuing;
using System.Threading.Tasks;

namespace OrderManager.Business
{
    public interface IOrderHandler
    {
        Task<IOrderQueuingResult> PlaceOrder(Order order);
    }
}