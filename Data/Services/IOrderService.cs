using System.Collections.Generic;
using Data.Models;

namespace Data.Services
{
    public interface IOrderService
    {
        List<Order> GetOrdersForCompany(int companyId);
    }
}