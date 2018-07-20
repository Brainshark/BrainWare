using Data.Services;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOrderService _orderService;

        public HomeController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public ActionResult Index()
        {
            const int companyId = 1;
            var orders = _orderService.GetOrdersForCompany(companyId);

            ViewBag.Title = "Home Page";
            return View(orders);
        }
    }
}
