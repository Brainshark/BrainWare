using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orders.Api.Model;
using Orders.Api.Context;
using Orders.Api.Service;

namespace Orders.Api.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            this._orderService = orderService;
        }

        [HttpGet]
        public async Task<IEnumerable<Order>> GetOrders(int id = 1)
        {
            return await this._orderService.GetOrdersForCompany(id);
        }
    }
}
