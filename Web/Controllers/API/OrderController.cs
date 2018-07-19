using Data.Services;
using System.Web.Http;

namespace Web.Controllers
{
    [RoutePrefix("api/orders")]
    public class OrderController : ApiController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetOrders(int id = 1)
        {
            return Ok(_orderService.GetOrdersForCompany(id));
        }
    }
}
