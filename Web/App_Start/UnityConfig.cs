using Data.Services;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<IOrderService, OrderService>();
            container.RegisterType<IDbService, DbService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}