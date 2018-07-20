using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoFixture;
using Data.Models;
using Data.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Web.Controllers;

namespace Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private HomeController _homeController;
        private Mock<IOrderService> _orderServiceMock = new Mock<IOrderService>();
        private Fixture _fixture => new Fixture();

        public HomeControllerTest()
        {
            _homeController = new HomeController(_orderServiceMock.Object);
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            const int testId = 1;
            var expectedOrders = _fixture.CreateMany<Order>(5).ToList();
            _orderServiceMock.Setup(x => x.GetOrdersForCompany(testId))
                .Returns(expectedOrders);

            HomeController controller = new HomeController(_orderServiceMock.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);

            var orderListModel = result.Model as List<Order>;
            CollectionAssert.AreEquivalent(expectedOrders, orderListModel);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
