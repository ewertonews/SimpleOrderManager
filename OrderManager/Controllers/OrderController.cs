using Microsoft.AspNetCore.Mvc;
using OrderManager.Business;
using OrderManager.Business.Models;
using OrderManager.Business.OrderQueuing;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderHandler orderHandler;

        public OrderController(IOrderHandler orderHandler)
        {
            this.orderHandler = orderHandler;
        }

        [HttpPost]
        [Route("place")]
        public async Task<IActionResult> Post(Order order)
        {

            var orderPlacementResult = await orderHandler.PlaceOrder(order);
            if (orderPlacementResult.QueuingStatus == OrderQueuingStatus.INVALIDORDER)
            {
                return new BadRequestObjectResult(orderPlacementResult);
            }
            if (orderPlacementResult.QueuingStatus == OrderQueuingStatus.GENERALERROR)
            {
                //TODO: log error
                return StatusCode(500, orderPlacementResult);
            }
            return Created(string.Empty, orderPlacementResult);
        }
    }
}
