using System.Collections.Generic;
using System.Web.Http;
using Data.Models;
using Data.Infrastructure;

namespace Web.Controllers
{
    using System.Web.Mvc;
    
    public class OrderController : ApiController
    {
        [HttpGet]
        public IEnumerable<Order> GetOrders()
        {
            var orderService = new OrderService();

            return orderService.GetOrdersForCompany();
        }
    }
}
