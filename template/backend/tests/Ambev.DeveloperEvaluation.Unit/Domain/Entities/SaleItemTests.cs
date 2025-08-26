using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class SaleItemTests
    {
        [Fact(DisplayName = "Should create sale item with valid quantity")]
        public void Given_ValidQuantity_When_CreateSaleItem_Then_ShouldSetProperties()
        {
            // Arrange
            var product = SaleTestData.GenerateProduct();

            // Act
            var item = new SaleItem(product, 2, 100);

            // Assert
            Assert.Equal(2, item.Quantity);
            Assert.Equal(100, item.UnitPrice);
            Assert.Equal(0, item.Discount);
            Assert.Equal(200, item.Total);
        }

        [Fact(DisplayName = "Should throw exception for invalid quantity")]
        public void Given_QuantityZeroOrNegative_When_CreateSaleItem_Then_ShouldThrow()
        {
            // Arrange
            var product = SaleTestData.GenerateProduct();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new SaleItem(product, 0, 100));
            Assert.Throws<ArgumentException>(() => new SaleItem(product, -1, 100));
        }

        [Fact(DisplayName = "Should throw exception for quantity greater than 20")]
        public void Given_QuantityGreaterThan20_When_CreateSaleItem_Then_ShouldThrow()
        {
            // Arrange
            var product = SaleTestData.GenerateProduct();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => new SaleItem(product, 21, 100));
        }

        [Fact(DisplayName = "Should calculate discount correctly")]
        public void Given_Quantity_When_CreateSaleItem_Then_DiscountShouldBeCorrect()
        {
            // Arrange
            var product = SaleTestData.GenerateProduct();

            // Act
            var itemNoDiscount = new SaleItem(product, 2, 100);
            var item10Percent = new SaleItem(product, 5, 100);
            var item20Percent = new SaleItem(product, 15, 100);

            // Assert
            Assert.Equal(0, itemNoDiscount.Discount);
            Assert.Equal(50, item10Percent.Discount); // 5*100*0.1
            Assert.Equal(300, item20Percent.Discount); // 15*100*0.2
        }
    }

}
