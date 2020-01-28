using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace BL.Mappers
{
    public class OrderProductMapper : MapperBase<OrderProduct>
    {
        protected override OrderProduct Map(IDataRecord record)
        {
            try
            {
                int productId = (record["product_id"] == DBNull.Value ? 0 : (int)record["product_id"]);
                decimal productPrice = (record["product_price"] == DBNull.Value ? 0 : (decimal)(double)record["product_price"]);
                OrderProduct op = new OrderProduct();
                op.OrderId = (record["order_id"] == DBNull.Value ? 0 : (int)record["order_id"]);
                op.ProductId = productId;
                op.Price = (record["price"] == DBNull.Value ? productPrice : (decimal)(double)record["price"]);
                op.Quantity = (record["quantity"] == DBNull.Value ? 0 : (int)record["quantity"]);
                op.Product = new Product()
                {
                    Id = productId,
                    Name = record["product_name"].ToString(),
                    Price = productPrice
                };
                return op;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error while mapping orderProduct: " + ex.ToString());
                return null;
            }
        }
    }
}
