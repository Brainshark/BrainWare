using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Web.Infrastructure.Util;
using Web.Models;

namespace Web.Infrastructure
{
    public class OrderService
    {
        //This is where an interface would benefit both the database, and the OrderService itself (in the event that we aren't using SQL Server, and to better support mocking frameworks).
        //I opted to remove direct SQL statements in favor of stored procedures (SQL injection potential, as well as better performance on the SQL side leveraging the plan cache).
        //I believe we were also fetching all rows (I may be mistaken), and filtering the results in C#, which isn't ideal.

        private static Database db;
        public OrderService(Database _db)
        {
            db = _db;
        }
        
        public async Task<List<Order>> GetOrdersForCompany(int companyId)
        {
            var orders = new List<Order>();
            try
            {
                var par = new Hashtable
                {
                    { "@CompanyId", companyId }
                };
                var dt = await db.GetDataTableAsync("[dbo].[getOrdersByCompanyId]", par);
                foreach (DataRow r in dt.Rows)
                {
                    var order = new Order
                    {
                        CompanyName = r["name"].ToString(),
                        Description = r["description"].ToString(),
                        OrderId = r["order_id"].ToInt(),
                        OrderProducts = new List<OrderProduct>()
                    };
                    order.OrderProducts = await GetOrderProducts(order.OrderId);
                    order.OrderTotal = order.OrderProducts.Sum(x => x.Price * x.Quantity);
                    orders.Add(order);
                }
            }
            catch (Exception ex)
            {
                //ideally log exception
                Console.WriteLine(ex.Message);
            }
            return orders;
        }

        public async Task<List<OrderProduct>> GetOrderProducts(int orderId)
        {
            var orderProducts = new List<OrderProduct>();
            try
            {
                var par = new Hashtable
            {
                { "@OrderId", orderId }
            };
                var dt = await db.GetDataTableAsync("[dbo].[getProductsByOrderId]", par);
                foreach (DataRow r in dt.Rows)
                {
                    orderProducts.Add(new OrderProduct()
                    {
                        OrderId = r["order_id"].ToInt(),
                        ProductId = r["product_id"].ToInt(),
                        Price = r["opPrice"].ToDec(),
                        Quantity = r["quantity"].ToInt(),
                        Product = new Product()
                        {
                            Name = r["name"].ToString(),
                            Price = r["productPrice"].ToDec()
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                //ideally log exception
                Console.WriteLine(ex.Message);
            }
            return orderProducts;
        }
    }
}