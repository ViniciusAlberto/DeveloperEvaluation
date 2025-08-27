using Ambev.DeveloperEvaluation.Application.Dispatcher;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItems;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class CancelSaleItemsHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IDomainEventPublisher _eventPublisher;
        private readonly CancelSaleItemsHandler _handler;

        public CancelSaleItemsHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _eventPublisher = Substitute.For<IDomainEventPublisher>();
            _handler = new CancelSaleItemsHandler(_saleRepository, _eventPublisher);
        }

        [Fact]
        public async Task Handle_ShouldCancelSpecifiedItems_WhenItemsExist()
        {
            // Arrange
            var command = CancelSaleItemsCommandTestData.GenerateValidCommand();

            var sale = new Sale("SALE123", DateTime.UtcNow, null!, null!);

            // Acessa a lista interna de itens via Reflection
            var itemsField = typeof(Sale).GetField("_items", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var itemList = (List<SaleItem>)itemsField!.GetValue(sale)!;

            // Cria SaleItems com IDs do comando
            foreach (var itemId in command.SaleItemsId)
            {
                var product = new ExternalProduct(itemId.ToString(), "Produto");
                var item = new SaleItem(product, 1, 10m);

                // Define o ID do item via Reflection
                typeof(SaleItem).GetProperty("Id")!.SetValue(item, itemId);

                itemList.Add(item);
            }

            _saleRepository.GetByIdAsync(command.SaleId, Arg.Any<CancellationToken>())
                           .Returns(sale);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeTrue();
            result.CancelledItems.Should().BeEquivalentTo(command.SaleItemsId);

            foreach (var id in command.SaleItemsId)
            {
                sale.Items.First(i => i.Id == id).IsCancelled.Should().BeTrue();
            }

            await _saleRepository.Received(1).UpdateAsync(sale, Arg.Any<CancellationToken>());
            await _eventPublisher.Received(command.SaleItemsId.Count)
                                 .PublishAsync(Arg.Any<SaleItemCancelledEvent>());
        }

        [Fact]
        public async Task Handle_ShouldReturnEmpty_WhenItemsDoNotExist()
        {
            // Arrange
            var command = CancelSaleItemsCommandTestData.GenerateNonExistingItemsCommand();

            var sale = new Sale("SALE123", DateTime.UtcNow, null!, null!);
            sale.AddItem(new ExternalProduct("existing-1", "Produto"), 1, 10m);

            _saleRepository.GetByIdAsync(command.SaleId, Arg.Any<CancellationToken>())
                           .Returns(sale);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.CancelledItems.Should().BeEmpty();

            await _saleRepository.Received(1).UpdateAsync(sale, Arg.Any<CancellationToken>());
            await _eventPublisher.DidNotReceive().PublishAsync(Arg.Any<SaleItemCancelledEvent>());
        }

        [Fact]
        public async Task Handle_ShouldThrow_WhenSaleDoesNotExist()
        {
            // Arrange
            var command = CancelSaleItemsCommandTestData.GenerateValidCommand();
            _saleRepository.GetByIdAsync(command.SaleId, Arg.Any<CancellationToken>())
                           .Returns((Sale?)null);

            // Act
            var act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                     .WithMessage($"Sale with ID {command.SaleId} not found");

            await _saleRepository.DidNotReceive().UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
            await _eventPublisher.DidNotReceive().PublishAsync(Arg.Any<SaleItemCancelledEvent>());
        }

        [Fact]
        public async Task Handle_ShouldHandleEmptyCommand()
        {
            // Arrange
            var command = CancelSaleItemsCommandTestData.GenerateInvalidCommand();

            var sale = new Sale("SALE123", DateTime.UtcNow, null!, null!);
            _saleRepository.GetByIdAsync(command.SaleId, Arg.Any<CancellationToken>())
                           .Returns(sale);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Success.Should().BeFalse();
            result.CancelledItems.Should().BeEmpty();

            await _saleRepository.Received(1).UpdateAsync(sale, Arg.Any<CancellationToken>());
            await _eventPublisher.DidNotReceive().PublishAsync(Arg.Any<SaleItemCancelledEvent>());
        }
    }
}
