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
                            Quantity = orderVM.OrderProducts.Count,                            
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
            //if (company != null)
            //{
            //    return await company.Order;
            //}

            //return Enumerable.Empty<Order>();



    //        var query = this._context.Company
    //.Join(
    //    this._context.Order,
    //    company => company.CompanyId,
    //    order => order.Company.CompanyId,
    //    (company, order) => new 
    //    {
    //        CompanyName = company.Name,
    //        OrderDescription = order.Description,
    //        InvoiceDate = invoice.Date
    //    }
    //).ToList();


    //        var database = new Database();

    //        // Get the orders
    //        var sql1 =
    //            "SELECT c.name, o.description, o.order_id FROM company c INNER JOIN [order] o on c.company_id=o.company_id";

    //        var reader1 = database.ExecuteReader(sql1);

    //        var values = new List<Order>();
            
    //        while (reader1.Read())
    //        {
    //            var record1 = (IDataRecord) reader1;

    //            values.Add(new Order()
    //            {
    //                CompanyName = record1.GetString(0),
    //                Description = record1.GetString(1),
    //                OrderId = record1.GetInt32(2),
    //                OrderProducts = new List<OrderProduct>()
    //            });

    //        }

    //        reader1.Close();

    //        //Get the order products
    //        var sql2 =
    //            "SELECT op.price, op.order_id, op.product_id, op.quantity, p.name, p.price FROM orderproduct op INNER JOIN product p on op.product_id=p.product_id";

    //        var reader2 = database.ExecuteReader(sql2);

    //        var values2 = new List<OrderProduct>();

    //        while (reader2.Read())
    //        {
    //            var record2 = (IDataRecord)reader2;

    //            values2.Add(new OrderProduct()
    //            {
    //                OrderId = record2.GetInt32(1),
    //                ProductId = record2.GetInt32(2),
    //                Price = record2.GetDecimal(0),
    //                Quantity = record2.GetInt32(3),
    //                Product = new Product()
    //                {
    //                    Name = record2.GetString(4),
    //                    Price = record2.GetDecimal(5)
    //                }
    //            });
    //         }

    //        reader2.Close();

    //        foreach (var order in values)
    //        {
    //            foreach (var orderproduct in values2)
    //            {
    //                if (orderproduct.OrderId != order.OrderId)
    //                    continue;

    //                order.OrderProducts.Add(orderproduct);
    //                order.OrderTotal = order.OrderTotal + (orderproduct.Price * orderproduct.Quantity);
    //            }
    //        }

    //        return values;
        }
    }
}