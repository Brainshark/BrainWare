using Data.Models;
using Data.Services;
using System.Collections.Generic;
using System.Web.Http;

namespace Web.Controllers
{
    // todo add route prefix etc
    public class OrderController : ApiController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IEnumerable<Order> GetOrders(int id = 1)
        {
            return _orderService.GetOrdersForCompany(id);
        }
    }
}
