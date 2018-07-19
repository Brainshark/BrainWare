using Data.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Data.Services
{
    public class OrderService : IOrderService
    {
        private readonly IDbService _dbService;

        public OrderService(IDbService dbService)
        {
            _dbService = dbService;
        }

        public IEnumerable<Order> GetOrdersForCompany(int companyId)
        {
            // Get the orders
            // todo tokenize
            var sql1 =
                @"SELECT 
                    c.name, 
                    o.description, 
                    o.order_id 
                    FROM company c 
                    INNER JOIN [order] o 
                        ON c.company_id = o.company_id";

            var reader1 = _dbService.ExecuteReader(sql1);
            var orders = new List<Order>();          
            while (reader1.Read())
            {
                var record1 = (IDataRecord) reader1;
                orders.Add(new Order()
                {
                    CompanyName = record1.GetString(0),
                    Description = record1.GetString(1),
                    OrderId = record1.GetInt32(2),
                    OrderProducts = new List<OrderProduct>()
                });
            }
            reader1.Close();

            //Get the order products
            var sql2 =
                @"SELECT 
                    op.price, 
                    op.order_id, 
                    op.product_id, 
                    op.quantity, 
                    p.name, 
                    p.price 
                    FROM orderproduct op 
                    INNER JOIN product p 
                        ON op.product_id = p.product_id";

            var reader2 = _dbService.ExecuteReader(sql2);
            var orderProducts = new List<OrderProduct>();

            while (reader2.Read())
            {
                var record2 = (IDataRecord)reader2;
                orderProducts.Add(new OrderProduct()
                {
                    OrderId = record2.GetInt32(1),
                    ProductId = record2.GetInt32(2),
                    Price = record2.GetDecimal(0),
                    Quantity = record2.GetInt32(3),
                    Product = new Product()
                    {
                        Name = record2.GetString(4),
                        Price = record2.GetDecimal(5)
                    }
                });
             }

            reader2.Close();

            foreach (var order in orders)
            {
                foreach (var orderproduct in orderProducts)
                {
                    if (orderproduct.OrderId != order.OrderId)
                    {
                        continue;
                    }

                    order.OrderProducts.Add(orderproduct);
                    order.OrderTotal = order.OrderTotal + (orderproduct.Price * orderproduct.Quantity);
                }
            }

            return orders;
        }
    }
}