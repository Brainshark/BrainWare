namespace Api.Controllers
{
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    [ApiController]
    [Route("api")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("order/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderResult>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetOrders(int id = 1)
        {
            IEnumerable<OrderResult> result = _orderService.GetOrdersForCompany(id);

            if (!result.Any()) 
            {
                return NotFound(new { message = "Orders not found"});
            }

            return Ok(result);
        }
    }
}
