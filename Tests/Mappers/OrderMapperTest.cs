using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BL.Mappers;
using Models;
using Moq;
using NUnit.Framework;

namespace Tests.Mappers
{
    [TestFixture]
    public class OrderMapperTest
    {
        [Test]
        public void Test_Valid_Order() {
            //Arrange
            var dataReader = new Mock<IDataReader>();
            dataReader.Setup(m => m.FieldCount).Returns(1);

            dataReader.Setup(m => m["order_id"]).Returns(1);
            dataReader.Setup(m => m["company_id"]).Returns(2);
            dataReader.Setup(m => m["description"]).Returns("mock");
            dataReader.SetupSequence(m => m.Read())
                .Returns(true)
                .Returns(false);

            //Act
            MapperBase<Order> mapper = new OrderMapper();
            Collection<Order> orders = mapper.MapAll(dataReader.Object);

            //Assert
            Assert.AreEqual(1, orders.Count);
            Assert.AreEqual(1, orders[0].Id);
            Assert.AreEqual(2, orders[0].CompanyId);
            Assert.AreEqual("mock", orders[0].Description);
        }

        [Test]
        public void Test_Order_With_Missing_Data()
        {
            //Arrange
            var dataReader = new Mock<IDataReader>();
            dataReader.Setup(m => m.FieldCount).Returns(1);

            dataReader.Setup(m => m["order_id"]).Returns(1);
            dataReader.Setup(m => m["company_id"]).Returns(DBNull.Value);
            dataReader.Setup(m => m["description"]).Returns(DBNull.Value);

            dataReader.SetupSequence(m => m.Read())
                .Returns(true)
                .Returns(false);

            //Act
            MapperBase<Order> mapper = new OrderMapper();
            Collection<Order> orders = mapper.MapAll(dataReader.Object);

            //Assert
            Assert.AreEqual(1, orders.Count);
            Assert.AreEqual(1, orders[0].Id);
            Assert.AreEqual(0, orders[0].CompanyId);
            Assert.AreEqual("", orders[0].Description);
        }

        [Test]
        public void Test_Order_With_Missing_Fields()
        {
            //Arrange
            var dataReader = new Mock<IDataReader>();
            dataReader.Setup(m => m.FieldCount).Returns(1);

            dataReader.Setup(m => m["order_id"]).Returns(1);
            dataReader.Setup(m => m["description"]).Returns("mock");

            dataReader.SetupSequence(m => m.Read())
                .Returns(true)
                .Returns(false);

            //Act
            MapperBase<Order> mapper = new OrderMapper();
            Collection<Order> orders = mapper.MapAll(dataReader.Object);

            //Assert
            Assert.AreEqual(1, orders.Count);
            Assert.IsNull(orders[0]);
        }

        [Test]
        public void Test_Order_With_Wrong_Typed_Data()
        {
            //Arrange
            var dataReader = new Mock<IDataReader>();
            dataReader.Setup(m => m.FieldCount).Returns(1);

            dataReader.Setup(m => m["order_id"]).Returns(1);
            dataReader.Setup(m => m["company_id"]).Returns("asdfg");
            dataReader.Setup(m => m["description"]).Returns("mock");

            dataReader.SetupSequence(m => m.Read())
                .Returns(true)
                .Returns(false);

            //Act
            MapperBase<Order> mapper = new OrderMapper();
            Collection<Order> orders = mapper.MapAll(dataReader.Object);

            //Assert
            Assert.AreEqual(1, orders.Count);
            Assert.IsNull(orders[0]);
        }

    }
}
