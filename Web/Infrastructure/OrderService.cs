using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Infrastructure
{
    using System.Collections.ObjectModel;
    using System.Data;
    using BL.Readers;
    using Models;

    public class OrderService
    {
        public Collection<Order> GetOrdersForCompany(int CompanyId)
        {
            // Get the orders
            OrderReader orderReader = new OrderReader();
            KeyValuePair<string, object> param = new KeyValuePair<string, object>("@companyId", CompanyId);
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(param);
            Collection<Order> orders = orderReader.Execute(parameters);

            //Get the order products
            OrderProductReader orderProductReader = new OrderProductReader();
            Collection<OrderProduct> orderProducts = orderProductReader.Execute(parameters);

            foreach (var order in orders)
            {
                var ops = orderProducts.Where(orderProduct => orderProduct.OrderId == order.Id);
                decimal orderTotal = 0;
                foreach (var orderproduct in ops)
                {
                    order.OrderProducts.Add(orderproduct);
                    orderTotal += (orderproduct.Price * orderproduct.Quantity);
                }
                order.OrderTotal = orderTotal;
            }

            return orders;
        }
    }
}