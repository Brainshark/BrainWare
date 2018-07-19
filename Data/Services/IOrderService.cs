using System.Collections.Generic;
using Data.Models;

namespace Data.Services
{
    public interface IOrderService
    {
        IEnumerable<Order> GetOrdersForCompany(int companyId);
    }
}