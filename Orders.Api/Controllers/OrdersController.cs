using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orders.Api.Model;
using Orders.Api.Context;
using Orders.Api.Service;
using Orders.Api.ViewModel;

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

        [HttpGet("{id:int}")]
        public List<OrderVM> GetOrders(int id = 1)
        {
            //await Task.Delay(1000);
            //return null;
            //return 10;
            return this._orderService.GetOrdersForCompany(id);
        }
    }
}
