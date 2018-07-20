using Data.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data;

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
            var dataReader = new Mock<IDataReader>();

            var paramDict = new Dictionary<string, string> {
                { "companyId", companyId.ToString() }
            };
            _dbService.Setup(x => x.ExecuteReader(It.IsAny<string>(), paramDict))
                .Returns(dataReader.Object);

            // WHEN we get orders
            var ordersResult = _orderService.GetOrdersForCompany(companyId);

            // THEN we should recieve the expected list of orders
        }
    }
}
