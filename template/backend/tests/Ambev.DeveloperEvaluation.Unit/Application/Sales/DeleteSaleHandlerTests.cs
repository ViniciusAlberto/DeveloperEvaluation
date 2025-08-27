using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Dispatcher;
using Ambev.DeveloperEvaluation.Unit.Domain.Sales.TestData;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class DeleteSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IDomainEventPublisher _eventPublisher;
        private readonly DeleteSaleHandler _handler;

        public DeleteSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _eventPublisher = Substitute.For<IDomainEventPublisher>();
            _handler = new DeleteSaleHandler(_saleRepository, _eventPublisher);
        }

        [Fact]
        public async Task Handle_ShouldDeleteSale_WhenSaleExists()
        {
            // Arrange
            var command = DeleteSaleCommandTestData.GenerateValidCommand();
            var fakeSale = new Sale("SALE123", DateTime.UtcNow, null!, null!);

            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                .Returns(fakeSale);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();            

            await _saleRepository.Received(1).DeleteAsync(fakeSale.Id);
            await _eventPublisher.Received(1).PublishAsync(Arg.Any<SaleDeletedEvent>());
        }

        [Fact]
        public async Task Handle_ShouldThrowKeyNotFound_WhenSaleDoesNotExist()
        {
            // Arrange
            var command = DeleteSaleCommandTestData.GenerateValidCommand();
            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                .Returns((Sale?)null);

            // Act
            var act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"Sale with ID {command.Id} not found");

            await _saleRepository.DidNotReceive().DeleteAsync(Arg.Any<Guid>());
            await _eventPublisher.DidNotReceive().PublishAsync(Arg.Any<SaleDeletedEvent>());
        }

        [Fact]
        public async Task Handle_ShouldThrow_WhenCommandIsInvalid()
        {
            // Arrange
            var command = DeleteSaleCommandTestData.GenerateInvalidCommand();

            // Act
            var act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<FluentValidation.ValidationException>();
        }
    }
}
