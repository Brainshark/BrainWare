using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure
{
    public class EntityHandler : IEntityHandler
    {
        private readonly BrainWareContext _context;
        public EntityHandler(BrainWareContext context)
        {
            _context = context;
        }

        public List<Order> GetOrdersForCompany(int CompanyId)
        {
            List<Order> orders = _context.Orders
                .Include(o => o.Company)
                .Include(o => o.Orderproducts)
                    .ThenInclude(op => op.Product)
                .Where(e => e.CompanyId == CompanyId)
                .ToList();

            return orders;
        }
    }
}
