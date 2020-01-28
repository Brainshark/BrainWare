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
    public class OrderProductMapperTest
    {
        [Test]
        public void Test_Valid_OrderProduct()
        {
            //Arrange
            var dataReader = new Mock<IDataReader>();
            dataReader.Setup(m => m.FieldCount).Returns(1);

            dataReader.Setup(m => m["order_id"]).Returns(1);
            dataReader.Setup(m => m["product_id"]).Returns(2);
            dataReader.Setup(m => m["price"]).Returns(2.34);
            dataReader.Setup(m => m["quantity"]).Returns(2);
            dataReader.Setup(m => m["product_name"]).Returns("mock");
            dataReader.Setup(m => m["product_price"]).Returns(1.34);

            dataReader.SetupSequence(m => m.Read())
                .Returns(true)
                .Returns(false);

            //Act
            MapperBase<OrderProduct> mapper = new OrderProductMapper();
            Collection<OrderProduct> orderProducts = mapper.MapAll(dataReader.Object);

            //Assert
            Assert.AreEqual(1, orderProducts.Count);
            Assert.AreEqual(1, orderProducts[0].OrderId);
            Assert.AreEqual(2, orderProducts[0].ProductId);
            Assert.AreEqual(2.34, orderProducts[0].Price);
            Assert.AreEqual(2, orderProducts[0].Quantity);
            Assert.IsNotNull(orderProducts[0].Product);
            Assert.AreEqual(orderProducts[0].ProductId, orderProducts[0].Product.Id);
            Assert.AreEqual(1.34, orderProducts[0].Product.Price);
            Assert.AreEqual("mock", orderProducts[0].Product.Name);
        }

        [Test]
        public void Test_OrderProduct_With_Missing_Data()
        {
            //Arrange
            var dataReader = new Mock<IDataReader>();
            dataReader.Setup(m => m.FieldCount).Returns(1);

            dataReader.Setup(m => m["order_id"]).Returns(1);
            dataReader.Setup(m => m["product_id"]).Returns(2);
            dataReader.Setup(m => m["price"]).Returns(DBNull.Value);
            dataReader.Setup(m => m["quantity"]).Returns(2);
            dataReader.Setup(m => m["product_name"]).Returns(DBNull.Value);
            dataReader.Setup(m => m["product_price"]).Returns(1.34);

            dataReader.SetupSequence(m => m.Read())
                .Returns(true)
                .Returns(false);

            //Act
            MapperBase<OrderProduct> mapper = new OrderProductMapper();
            Collection<OrderProduct> orderProducts = mapper.MapAll(dataReader.Object);

            //Assert
            Assert.AreEqual(1, orderProducts.Count);
            Assert.AreEqual(1, orderProducts[0].OrderId);
            Assert.AreEqual(2, orderProducts[0].ProductId);
            Assert.AreEqual(1.34, orderProducts[0].Price);
            Assert.AreEqual(2, orderProducts[0].Quantity);
            Assert.IsNotNull(orderProducts[0].Product);
            Assert.AreEqual(orderProducts[0].ProductId, orderProducts[0].Product.Id);
            Assert.AreEqual(1.34, orderProducts[0].Product.Price);
            Assert.AreEqual("", orderProducts[0].Product.Name);
        }

        [Test]
        public void Test_Order_With_Missing_Fields()
        {
            //Arrange
            var dataReader = new Mock<IDataReader>();
            dataReader.Setup(m => m.FieldCount).Returns(1);

            dataReader.Setup(m => m["order_id"]).Returns(1);
            dataReader.Setup(m => m["product_id"]).Returns(2);
            dataReader.Setup(m => m["price"]).Returns(2.34);
            dataReader.Setup(m => m["product_name"]).Returns("mock");
            dataReader.Setup(m => m["product_price"]).Returns(1.34);

            dataReader.SetupSequence(m => m.Read())
                .Returns(true)
                .Returns(false);

            //Act
            MapperBase<OrderProduct> mapper = new OrderProductMapper();
            Collection<OrderProduct> orderProducts = mapper.MapAll(dataReader.Object);

            //Assert
            Assert.AreEqual(1, orderProducts.Count);
            Assert.IsNull(orderProducts[0]);
        }

        [Test]
        public void Test_Order_With_Wrong_Typed_Data()
        {
            //Arrange
            var dataReader = new Mock<IDataReader>();
            dataReader.Setup(m => m.FieldCount).Returns(1);

            dataReader.Setup(m => m["order_id"]).Returns(1);
            dataReader.Setup(m => m["product_id"]).Returns(2);
            dataReader.Setup(m => m["price"]).Returns(2.34);
            dataReader.Setup(m => m["quantity"]).Returns(2.567);
            dataReader.Setup(m => m["product_name"]).Returns("mock");
            dataReader.Setup(m => m["product_price"]).Returns(1.34);

            dataReader.SetupSequence(m => m.Read())
                .Returns(true)
                .Returns(false);

            //Act
            MapperBase<OrderProduct> mapper = new OrderProductMapper();
            Collection<OrderProduct> orderProducts = mapper.MapAll(dataReader.Object);

            //Assert
            Assert.AreEqual(1, orderProducts.Count);
            Assert.IsNull(orderProducts[0]);
        }

    }
}
