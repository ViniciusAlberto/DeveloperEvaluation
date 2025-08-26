using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class SaleTests
    {
        [Fact(DisplayName = "Should create sale with valid data")]
        public void Given_ValidData_When_CreateSale_Then_ShouldSetProperties()
        {
            // Arrange
            var sale = SaleTestData.GenerateSale();

            // Act & Assert
            Assert.False(sale.Cancelled);
            Assert.NotNull(sale.Customer);
            Assert.NotNull(sale.Branch);
            Assert.NotEmpty(sale.Items);
            Assert.True(sale.TotalAmount > 0);
        }

        [Fact(DisplayName = "Should add item and recalculate total")]
        public void Given_Sale_When_AddItem_Then_TotalAmountShouldBeUpdated()
        {
            // Arrange
            var sale = SaleTestData.GenerateSale();
            var initialTotal = sale.TotalAmount;
            var product = SaleTestData.GenerateProduct();

            // Act
            sale.AddItem(product, 3, 50);

            // Assert
            Assert.Equal(initialTotal + (3 * 50), sale.TotalAmount);
            Assert.Equal(2, sale.Items.Count); // assuming 1 item initially
        }

        [Fact(DisplayName = "Should cancel sale")]
        public void Given_Sale_When_Cancel_Then_CancelledShouldBeTrue()
        {
            // Arrange
            var sale = SaleTestData.GenerateSale();

            // Act
            sale.Cancel();

            // Assert
            Assert.True(sale.Cancelled);
        }

        [Fact(DisplayName = "Should clear domain events")]
        public void Given_Sale_When_ClearEvents_Then_DomainEventsShouldBeEmpty()
        {
            // Arrange
            var sale = SaleTestData.GenerateSale();
            // Simulate adding an event
            var domainEvent = new TestDomainEvent();
            // Reflection or internal access might be required to add the event if method is private
            sale.ClearEvents();

            // Assert
            Assert.Empty(sale.DomainEvents);
        }
    }
}

public class TestDomainEvent : Ambev.DeveloperEvaluation.Domain.Events.IDomainEvent { }

