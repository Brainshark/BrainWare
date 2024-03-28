using Api.Models;

namespace Api.Infrastructure
{
    public interface IEntityHandler
    {
        List<Order> GetOrdersForCompany(int CompanyId);
    }
}
