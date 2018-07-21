using Data.Models;
using Data.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Tests.Services
{
    [TestClass]
    public class OrderService_Tests
    {
        private IOrderService _orderService;
        private Mock<IDbService> _dbService = new Mock<IDbService>();

        public OrderService_Tests()
        {
            _orderService = new OrderService(_dbService.Object);
        }

        [TestMethod]
        public void GetOrdersForCompany_ReceivesCompanyId_ReturnsExpetedOrders()
        {
            // GIVEN we have a company id
            const int companyId = 1;
            var dataReader = CreateDataReader();
            var paramDict = new Dictionary<string, string> {
                { "companyId", companyId.ToString() }
            };
            _dbService.Setup(x => x.ExecuteReader(It.IsAny<string>(), paramDict))
                .Returns(dataReader.Object);

            // WHEN we get orders
            var ordersResult = _orderService.GetOrdersForCompany(companyId)
                .ToList();

            // THEN we should recieve the expected list of orders
            var expectedOrders = new List<Order>
            {
                new Order
                {
                    CompanyName = "test name",
                    Description = "test description",
                    OrderId = 1,
                    OrderTotal = 100.00m,
                    OrderProducts = new List<OrderProduct>
                    {
                        new OrderProduct
                        {
                            OrderId = 1,
                            ProductId = 1,
                            Product = new Product
                            {
                                Name = "test product name",
                                Price = 25.00m
                            },
                            Quantity = 10,
                            Price = 10.00m
                        }
                    }
                }
            };

            CollectionAssert.AreEquivalent(expectedOrders, ordersResult);
        }

        private static Mock<IDataReader> CreateDataReader()
        {
            var dataReader = new Mock<IDataReader>();

            dataReader.Setup(m => m["name"]).Returns("test name");
            dataReader.Setup(m => m["description"]).Returns("test description");
            dataReader.Setup(m => m["order_id"]).Returns(1);
            dataReader.Setup(m => m["OrderPrice"]).Returns(10.00m);
            dataReader.Setup(m => m["product_id"]).Returns(1);
            dataReader.Setup(m => m["quantity"]).Returns(10);
            dataReader.Setup(m => m["ProductName"]).Returns("test product name");
            dataReader.Setup(m => m["ProductPrice"]).Returns(25.00m);

            dataReader.SetupSequence(m => m.Read())
                .Returns(true);
            return dataReader;
        }
    }
}
