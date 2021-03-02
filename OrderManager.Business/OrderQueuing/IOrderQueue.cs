using OrderManager.Business.Models;
using System.Threading.Tasks;

namespace OrderManager.Business.OrderQueuing
{
    public interface IOrderQueue
    {
        Task<IOrderQueuingResult> EnqueueOrder(Order order);
    }
}