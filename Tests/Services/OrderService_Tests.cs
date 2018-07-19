using Data.Services;
using NUnit.Framework;

namespace Tests.Services
{
    [TestFixture]
    public class OrderService_Tests
    {
        private IDbService _dbService;

        public OrderService_Tests()
        {
            _dbService = new DbService();
        }

        public void GetOrdersForCompany_ReturnsExpectedOrder()
        {
            // GIVEN I have an order
            const string companyInsertSql = @"INSERT INTO company (name, company_id)
            VALUES ('test name', 1)";

            _dbService.ExecuteNonQuery(companyInsertSql);

            // WHEN I get that order

            // THEN I receive the order I expect
        }
    }
}
