using System.Collections.Generic;
using System.Data;
using Data.Models;

namespace Data.Infrastructure
{
    public class OrderService
    {
        public List<Order> GetOrdersForCompany()
        {
            var database = new Database();

            // Get the orders
            var getOrderQuery =
                "SELECT c.name, o.description, o.order_id " +
                "FROM company c " +
                "INNER JOIN [order] o on c.company_id=o.company_id";

            var orderReader = database.ExecuteReader(getOrderQuery);

            var orderList = new List<Order>();
            
            while (orderReader.Read())
            {
                var orderDataRecord = (IDataRecord)orderReader;

                orderList.Add(new Order()
                {
                    CompanyName = orderDataRecord.GetString(0),
                    Description = orderDataRecord.GetString(1),
                    OrderId = orderDataRecord.GetInt32(2),
                    OrderProducts = new List<OrderProduct>()
                });
            }

            orderReader.Close();

            //Get the order products
            var getOrderProductQuery =
                "SELECT op.price, op.order_id, op.product_id, op.quantity, p.name, p.price " +
                "FROM orderproduct op " +
                "INNER JOIN product p on op.product_id=p.product_id";

            var orderProductReader = database.ExecuteReader(getOrderProductQuery);

            var orderProductList = new List<OrderProduct>();

            while (orderProductReader.Read())
            {
                var orderProductDataRecord = (IDataRecord) orderProductReader;

                orderProductList.Add(new OrderProduct()
                {
                    OrderId = orderProductDataRecord.GetInt32(1),
                    ProductId = orderProductDataRecord.GetInt32(2),
                    Price = orderProductDataRecord.GetDecimal(0),
                    Quantity = orderProductDataRecord.GetInt32(3),
                    Product = new Product()
                    {
                        Name = orderProductDataRecord.GetString(4),
                        Price = orderProductDataRecord.GetDecimal(5)
                    }
                });
             }

            orderProductReader.Close();

            // Construct total order list
            foreach (var order in orderList)
            {
                foreach (var orderProduct in orderProductList)
                {
                    if (orderProduct.OrderId != order.OrderId)
                    {
                        order.OrderProducts.Add(orderProduct);
                        order.OrderTotal = order.OrderTotal + (orderProduct.Price * orderProduct.Quantity);
                    }
                }
            }

            return orderList;
        }
    }
}