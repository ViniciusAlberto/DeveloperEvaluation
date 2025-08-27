using Ambev.DeveloperEvaluation.Application.Dispatcher;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Ambev.DeveloperEvaluation.Unit.Domain.Sales.TestData;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;
using ValidationException = FluentValidation.ValidationException;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class CancelSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IDomainEventPublisher _eventPublisher;
        private readonly CancelSaleHandler _handler;

        public CancelSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _eventPublisher = Substitute.For<IDomainEventPublisher>();
            _handler = new CancelSaleHandler(_saleRepository, _eventPublisher);
        }

        [Fact]
        public async Task Handle_ShouldCancelSale_WhenSaleExists()
        {
           
            var command = CancelSaleCommandTestData.GenerateValidCommand();
            
            var sale = new Sale("SALE123", DateTime.UtcNow, null!, null!);

            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                .Returns(sale);
            
            var result = await _handler.Handle(command, CancellationToken.None);

            
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            sale.Cancelled.Should().BeTrue();

            await _saleRepository.Received(1).UpdateAsync(sale, Arg.Any<CancellationToken>());
            await _eventPublisher.Received(1).PublishAsync(Arg.Is<SaleCancelledEvent>(e => e.SaleId == sale.Id));
        }

        [Fact]
        public async Task Handle_ShouldThrowKeyNotFound_WhenSaleDoesNotExist()
        {           
            var command = CancelSaleCommandTestData.GenerateValidCommand();
            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                .Returns((Sale?)null);

           
            var act = async () => await _handler.Handle(command, CancellationToken.None);
           
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"Sale with ID {command.Id} not found");

            await _saleRepository.DidNotReceive().UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
            await _eventPublisher.DidNotReceive().PublishAsync(Arg.Any<SaleCancelledEvent>());
        }

        [Fact]
        public async Task Handle_ShouldThrow_WhenCommandIsInvalid()
        {
           
            var command = CancelSaleCommandTestData.GenerateInvalidCommand();
           
            var act = async () => await _handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ValidationException>();
        }
    }
}
