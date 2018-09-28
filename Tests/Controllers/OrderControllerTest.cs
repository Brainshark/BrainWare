using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Data.Models;
using Web.Controllers;

namespace Tests.Controllers
{
    [TestClass]
    class OrderControllerTest
    {
        [TestMethod]
        public void GetOrdersForCompany()
        {
            // Arrange
            var controller = new OrderController();

            // Act
            var result = controller.GetOrders();

            // Assert
            Assert.AreEqual(result, GetTestData());
        }

        private List<Order> GetTestData()
        {
            var orderList = new List<Order>
            {
                new Order
                {
                    CompanyName = "BrainWare Company",
                    Description = "Our first order.",
                    OrderId = 1,
                    OrderProducts = new List<OrderProduct>
                    {
                        new OrderProduct
                        {
                            OrderId = 2,
                            Price = 1.23m,
                            Product = new Product
                            {
                                Name = "Pipe fitting",
                                Price = 1.00m
                            },
                            ProductId = 1,
                            Quantity = 10
                        },
                        new Data.Models.OrderProduct
                        {
                            OrderId = 2,
                            Price = 2.00m,
                            Product = new Data.Models.Product
                            {
                                Name = "10\" straight",
                                Price = 2.00m
                            },
                            ProductId = 2,
                            Quantity = 13
                        },
                        new Data.Models.OrderProduct
                        {
                            OrderId = 2,
                            Price = 1.00m,
                            Product = new Data.Models.Product
                            {
                                Name = "Quarter turn",
                                Price = 1.00m
                            },
                            ProductId = 3,
                            Quantity = 3
                        },
                        new Data.Models.OrderProduct
                        {
                            OrderId = 2,
                            Price = 0.90m,
                            Product = new Data.Models.Product
                            {
                                Name = "2\" stright",
                                Price = 1.00m
                            },
                            ProductId = 5,
                            Quantity = 3
                        },
                        new Data.Models.OrderProduct
                        {
                            OrderId = 3,
                            Price = 1.23m,
                            Product = new Data.Models.Product
                            {
                                Name = "Pipe fitting",
                                Price = 1.00m
                            },
                            ProductId = 1,
                            Quantity = 10
                        },
                        new Data.Models.OrderProduct
                        {
                            OrderId = 3,
                            Price = 2.00m,
                            Product = new Data.Models.Product
                            {
                                Name = "10\" straight",
                                Price = 2.00m
                            },
                            ProductId = 2,
                            Quantity = 7
                        },
                        new Data.Models.OrderProduct
                        {
                            OrderId = 3,
                            Price = 0.75m,
                            Product = new Data.Models.Product
                            {
                                Name = "Quarter turn",
                                Price = 1.00m
                            },
                            ProductId = 3,
                            Quantity = 13
                        },
                        new Data.Models.OrderProduct
                        {
                            OrderId = 3,
                            Price = 1.10m,
                            Product = new Data.Models.Product
                            {
                                Name = "5\" straight",
                                Price = 1.00m
                            },
                            ProductId = 4,
                            Quantity = 5
                        },
                        new Data.Models.OrderProduct
                        {
                            OrderId = 3,
                            Price = 0.90m,
                            Product = new Data.Models.Product
                            {
                                Name = "2\" stright",
                                Price = 1.00m
                            },
                            ProductId = 5,
                            Quantity = 3
                        }
                    },
                    OrderTotal = 88.25m
                },
                new Data.Models.Order
                {
                    CompanyName = "BrainWare Company",
                    Description = "Our Second order.",
                    OrderId = 2,
                    OrderProducts = new System.Collections.Generic.List<Data.Models.OrderProduct>
                    {
                        new Data.Models.OrderProduct
                        {
                            OrderId = 1,
                            Price = 1.23m,
                            Product = new Data.Models.Product
                            {
                                Name = "Pipe fitting",
                                Price = 1.00m
                            },
                            ProductId = 1,
                            Quantity = 10
                        },
                        new Data.Models.OrderProduct
                        {
                            OrderId = 1,
                            Price = 1.00m,
                            Product = new Data.Models.Product
                            {
                                Name = "Quarter turn",
                                Price = 1.00m
                            },
                            ProductId = 3,
                            Quantity = 3
                        },
                        new Data.Models.OrderProduct
                        {
                            OrderId = 1,
                            Price = 1.10m,
                            Product = new Data.Models.Product
                            {
                                Name = "5\" straight",
                                Price = 1.00m
                            },
                            ProductId = 4,
                            Quantity = 22
                        },
                        new Data.Models.OrderProduct
                        {
                            OrderId = 3,
                            Price = 1.23m,
                            Product = new Data.Models.Product
                            {
                                Name = "Pipe fitting",
                                Price = 1.00m
                            },
                            ProductId = 1,
                            Quantity = 10
                        },
                        new Data.Models.OrderProduct
                        {
                            OrderId = 3,
                            Price = 2.00m,
                            Product = new Data.Models.Product
                            {
                                Name = "10\" straight",
                                Price = 2.00m
                            },
                            ProductId = 2,
                            Quantity = 7
                        },
                        new Data.Models.OrderProduct
                        {
                            OrderId = 3,
                            Price = 0.75m,
                            Product = new Data.Models.Product
                            {
                                Name = "Quarter turn",
                                Price = 1.00m
                            },
                            ProductId = 3,
                            Quantity = 13
                        },
                        new Data.Models.OrderProduct
                        {
                            OrderId = 3,
                            Price = 1.10m,
                            Product = new Data.Models.Product
                            {
                                Name = "5\" straight",
                                Price = 1.00m
                            },
                            ProductId = 4,
                            Quantity = 5
                        },
                        new Data.Models.OrderProduct
                        {
                            OrderId = 3,
                            Price = 0.90m,
                            Product = new Data.Models.Product
                            {
                                Name = "2\" stright",
                                Price = 1.00m
                            },
                            ProductId = 5,
                            Quantity = 3
                        }
                    },
                    OrderTotal = 83.75m
                },
                new Data.Models.Order
                {
                    CompanyName = "BrainWare Company",
                    Description = "Our third order.",
                    OrderId = 3,
                    OrderProducts = new System.Collections.Generic.List<Data.Models.OrderProduct>
                    {
                        new Data.Models.OrderProduct
                        {
                            OrderId = 1,
                            Price = 1.23m,
                            Product = new Data.Models.Product
                            {
                                Name = "Pipe fitting",
                                Price = 1.00m
                            },
                            ProductId = 1,
                            Quantity = 10
                        },
                        new Data.Models.OrderProduct
                        {
                            OrderId = 1,
                            Price = 1.00m,
                            Product = new Data.Models.Product
                            {
                                Name = "Quarter turn",
                                Price = 1.00m
                            },
                            ProductId = 3,
                            Quantity = 3
                        },
                        new Data.Models.OrderProduct
                        {
                            OrderId = 1,
                            Price = 1.10m,
                            Product = new Data.Models.Product
                            {
                                Name = "5\" straight",
                                Price = 1.00m
                            },
                            ProductId = 4,
                            Quantity = 22
                        },
                        new Data.Models.OrderProduct
                        {
                            OrderId = 2,
                            Price = 1.23m,
                            Product = new Data.Models.Product
                            {
                                Name = "Pipe fitting",
                                Price = 1.00m
                            },
                            ProductId = 1,
                            Quantity = 10
                        },
                        new Data.Models.OrderProduct
                        {
                            OrderId = 2,
                            Price = 2.00m,
                            Product = new Data.Models.Product
                            {
                                Name = "10\" straight",
                                Price = 2.00m
                            },
                            ProductId = 2,
                            Quantity = 13
                        },
                        new Data.Models.OrderProduct
                        {
                            OrderId = 2,
                            Price = 1.00m,
                            Product = new Data.Models.Product
                            {
                                Name = "Quarter turn",
                                Price = 1.00m
                            },
                            ProductId = 3,
                            Quantity = 3
                        },
                        new Data.Models.OrderProduct
                        {
                            OrderId = 2,
                            Price = 0.90m,
                            Product = new Data.Models.Product
                            {
                                Name = "2\" stright",
                                Price = 1.00m
                            },
                            ProductId = 5,
                            Quantity = 3
                        }
                    },
                    OrderTotal = 83.50m
                }
            };

            return orderList;
        }
    }
}
