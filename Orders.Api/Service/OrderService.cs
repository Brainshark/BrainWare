using Orders.Api.Context;
using Orders.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Orders.Api.ViewModel;

namespace Orders.Api.Service
{
    public class OrderService
    {
        private BrainWareContext _context = null;
        public OrderService(BrainWareContext context)
        {
            this._context = context;
        }

        public List<OrderVM> GetOrdersForCompany(int CompanyId)
        {
            try
            {
                var company = this._context.Company
                                    .Include(c => c.Order)
                                        .ThenInclude(o => o.Orderproduct)
                                          .ThenInclude(op => op.Product)
                                            .SingleOrDefault(x => x.CompanyId == CompanyId);
                var orderVMs = new List<OrderVM>();
                decimal orderTotal = 0;
                foreach (var order in company.Order)
                {

                    var orderVM = new OrderVM()
                    {
                        CompanyName = order.Company.Name,
                        Description = order.Description,
                        OrderId = order.OrderId,
                        OrderProducts = new List<OrderProductVM>()
                    };
                    foreach (var orderProduct in order.Orderproduct)
                    {
                        orderVM.OrderProducts.Add(new OrderProductVM()
                        {
                            OrderId = orderVM.OrderId,
                            Price = orderProduct.Price.Value,
                            Quantity = order.Orderproduct.Count,
                            Product = new ProductVM()
                            {
                                Name = orderProduct.Product.Name,
                                Price = orderProduct.Product.Price.Value
                            }
                        });
                        orderTotal = orderTotal + (orderProduct.Quantity * orderProduct.Price.Value);
                    }
                    orderVM.OrderTotal = orderTotal;
                    orderTotal = 0;
                    orderVMs.Add(orderVM);
                }

                return orderVMs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}