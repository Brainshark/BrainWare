using System.Web.Http;
using Web.Infrastructure;

namespace Web.Controllers
{
    public class BaseController : ApiController
    {
        //Ideally this database class would have an interface, and allow for swapping between database classes.
        //It would also be injected on startup rather than in the base controller (either natively, or through another IOC framework).

        private readonly Database db = new Database("BrainWareConnectionString");
        public OrderService orderService;
        public BaseController()
        {
            orderService = new OrderService(db);
        }
    }
}
