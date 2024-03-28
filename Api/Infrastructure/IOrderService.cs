using Api.Models;

namespace Api.Infrastructure
{
    public interface IOrderService
    {
        List<OrderResult> GetOrdersForCompany(int CompanyId);
    }
}
