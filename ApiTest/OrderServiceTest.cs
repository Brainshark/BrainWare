using Api.Infrastructure;
using Api.Models;
using Moq;

namespace ApiTest
{
    [TestClass]
    public class OrderServiceTest
    {
        [TestMethod]
        public void GetOrdersForCompany_ShouldReturn_OrderResults()
        {
            // Arrange
            var testEntityHandler = new Mock<IEntityHandler>();

            List<Order> testOrders = PrepareTestOrders();

            testEntityHandler.Setup(handler => 
                handler.GetOrdersForCompany(It.IsAny<int>()))
                .Returns(testOrders);

            var testOrderService = new OrderService(testEntityHandler.Object);

            // Act
            List<OrderResult> testResult = testOrderService.GetOrdersForCompany(1);

            //Assert
            decimal testOrderTotal = 
                testOrders.Sum(o => o.Orderproducts.Sum(op => op.Quantity * op.Price)).Value;
            Order? testFirstOrder = testOrders.FirstOrDefault();
            OrderResult? testfirstOrderResult = testResult.FirstOrDefault();

            Assert.IsNotNull(testResult);
            Assert.AreEqual(testOrders.Count, testResult.Count());
            Assert.AreEqual(testOrderTotal, testResult.Sum(r => r.OrderTotal));
            Assert.AreEqual(testFirstOrder!.Description, testfirstOrderResult!.Description);
            Assert.AreEqual(testFirstOrder.Orderproducts.Count, testfirstOrderResult.OrderProducts.Count);
        }

        private List<Order> PrepareTestOrders() 
        {
            var orders = new List<Order>
            {
                new Order
                {
                    OrderId = 1,
                    Description = "Test Order 1",
                    CompanyId = 1,
                    Company = new Company { CompanyId = 1, Name = "Test Company" },
                    Orderproducts  = new List<Orderproduct> 
                    {
                        new Orderproduct 
                        {
                            OrderId = 1,
                            Price = 3,
                            Quantity = 2,
                            ProductId = 1,
                            Product = new Product { ProductId = 1, Price = 3, Name = "Product 1" }
                        },
                                                new Orderproduct
                        {
                            OrderId = 2,
                            Price = 4,
                            Quantity = 3,
                            ProductId = 2,
                            Product = new Product { ProductId = 2, Price = 4, Name = "Product 2" }
                        }
                    }
                },
                new Order
                {
                    OrderId = 2,
                    Description = "Test Order 2",
                    CompanyId = 1,
                    Company = new Company { CompanyId = 1, Name = "Test Company" },
                    Orderproducts  = new List<Orderproduct>
                    {
                        new Orderproduct
                        {
                            OrderId = 1,
                            Price = 4,
                            Quantity = 2,
                            ProductId = 1,
                            Product = new Product { ProductId = 1, Price = 3, Name = "Product 1" }
                        }
                    }
                }
            };

            return orders;
        }
    }
}