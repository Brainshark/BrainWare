using Data.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            var sql =
                @"SELECT
                    c.name, 
                    o.description, 
                    o.order_id,
                    op.price OrderPrice, 
                    op.order_id, 
                    op.product_id, 
                    op.quantity, 
                    p.name ProductName, 
                    p.price ProductPrice
                    FROM [order] o
                    INNER JOIN [company] c
                        ON c.company_id = o.company_id
                    INNER JOIN [orderproduct] op
                        ON op.order_id = o.order_id
                    INNER JOIN [product] p
                        ON p.product_id = op.product_id
                    WHERE o.company_id = @companyId";

            var paramDict = new Dictionary<string, string> {
                { "companyId", companyId.ToString() }
            };
            using (var reader = _dbService.ExecuteReader(sql, paramDict))
            {
                var orderDict = new Dictionary<int, Order>();
                while (reader.Read())
                {
                    var record = (IDataRecord)reader;
                    var orderId = int.Parse(record["order_id"].ToString());
                    var productId = int.Parse(record["product_id"].ToString());
                    if (!orderDict.ContainsKey(orderId))
                    {
                        var order = new Order()
                        {
                            CompanyName = record["name"].ToString(),
                            Description = record["description"].ToString(),
                            OrderId = int.Parse(record["order_id"].ToString()),
                            OrderTotal = ComputerOrderTotalPerRow(record),
                            OrderProducts = new List<OrderProduct>
                            {
                                GetOrderProduct(record)
                            }
                        };

                        orderDict.Add(orderId, order);
                    }
                    else
                    {
                        orderDict[orderId].OrderTotal += ComputerOrderTotalPerRow(record);
                        orderDict[orderId].OrderProducts.Add(GetOrderProduct(record));
                    }

                }
                reader.Close();

                var orders = orderDict.Values.ToList();
                return orders;
            }
            
        }

        private decimal ComputerOrderTotalPerRow(IDataRecord record)
        {
            var quantity = int.Parse(record["quantity"].ToString());
            var price = decimal.Parse(record["OrderPrice"].ToString());

            return price * quantity;
        }

        private OrderProduct GetOrderProduct(IDataRecord record)
        {
            return new OrderProduct
            {
                OrderId = int.Parse(record["order_id"].ToString()),
                ProductId = int.Parse(record["product_id"].ToString()),
                Product = new Product
                {
                    Name = record["ProductName"].ToString(),
                    Price = decimal.Parse(record["ProductPrice"].ToString())
                },
                Quantity = int.Parse(record["quantity"].ToString()),
                Price = decimal.Parse(record["OrderPrice"].ToString())
            };
        }
    }
}