using AutoFixture;
using Data.Models;
using Data.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using Web.Controllers;

namespace Tests.ApiControllers
{
    [TestClass]
    public class OrderController_Tests
    {
        private OrderController _orderController;
        private Mock<IOrderService> _orderServiceMock = new Mock<IOrderService>();
        private Fixture _fixture => new Fixture();

        public OrderController_Tests()
        {
            _orderController = new OrderController(_orderServiceMock.Object);
        }

        [TestMethod]
        public void GetOrders_ReturnsExpectedResponse()
        {
            // GIVEN we have orders
            const int testId = 1;
            var expectedOrders = _fixture.CreateMany<Order>(5).ToList();
            _orderServiceMock.Setup(x => x.GetOrdersForCompany(testId))
                .Returns(expectedOrders);
            
            // WHEN I get those orders
            var result = _orderController.GetOrders(testId);
            var contentResult = result as OkNegotiatedContentResult<IEnumerable<Order>>;
            var actualOrders = contentResult.Content.ToList();

            // THEN I expect an Ok response with the expected order list
            CollectionAssert.AreEquivalent(expectedOrders, actualOrders);           
        }
    }
}
