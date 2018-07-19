using Data.Models;
using Data.Services;
using System.Collections.Generic;
using System.Web.Http;

namespace Web.Controllers
{
    // todo add route prefix etc
    public class OrderController : ApiController
    {
        [HttpGet]
        public IEnumerable<Order> GetOrders(int id = 1)
        {
            var data = new OrderService();

            return data.GetOrdersForCompany(id);
        }
    }
}
