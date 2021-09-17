using System.Collections.Generic;
using System.Web.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.Infrastructure;
using Web.Models;

namespace Web.Controllers
{

    public class OrderController : BaseController
    {
        [System.Web.Mvc.HttpGet]
        public async Task<IEnumerable<Order>> GetOrders(int id = 1)
        {
            var orders = await orderService.GetOrdersForCompany(id);
            return orders;
        }
    }
}
