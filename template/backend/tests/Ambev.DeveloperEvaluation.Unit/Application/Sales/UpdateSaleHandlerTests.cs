using Ambev.DeveloperEvaluation.Application.Dispatcher;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Clients;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Unit.Domain.Sales.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class UpdateSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductClient _productClient;
        private readonly IMapper _mapper;
        private readonly IDomainEventPublisher _eventPublisher;
        private readonly UpdateSaleHandler _handler;

        public UpdateSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _productClient = Substitute.For<IProductClient>();
            _mapper = Substitute.For<IMapper>();
            _eventPublisher = Substitute.For<IDomainEventPublisher>();

            _handler = new UpdateSaleHandler(
                _saleRepository,
                _productClient,
                _mapper,
                _eventPublisher
            );
        }

        [Fact]
        public async Task Handle_ValidCommand_UpdatesSaleAndPublishesEvent()
        {
            // Arrange
            var command = UpdateSaleCommandTestData.GenerateValidCommand();

            var sale = new Sale("SALE123", DateTime.UtcNow,
                new ExternalCustomer("cust-1", "Cliente Teste"),
                new ExternalBranch("branch-1", "Filial Teste"));

            _saleRepository.GetByIdAsync(command.SaleId, Arg.Any<CancellationToken>())
                           .Returns(sale);

            var product = new ExternalProduct("prod-1", "Produto Teste");
            var productDto = new ProductDto("prod-1", "Produto Teste");
            _productClient.GetByIdAsync(Arg.Any<string>()).Returns(productDto);
            _mapper.Map<ExternalProduct>(product).Returns(product);
            _mapper.Map<ExternalCustomer>(command.Customer).Returns(new ExternalCustomer(command.Customer.ExternalId, command.Customer.Name));
            _mapper.Map<ExternalBranch>(command.Branch).Returns(new ExternalBranch(command.Branch.ExternalId, command.Branch.Name));
            _mapper.Map<UpdateSaleResult>(sale).Returns(new UpdateSaleResult { Id = sale.Id, SaleNumber = sale.SaleNumber });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(sale.Id);

            await _saleRepository.Received(1).UpdateAsync(sale);
            await _eventPublisher.Received(1).PublishAsync(Arg.Any<SaleUpdatedEvent>());
        }

        [Fact]
        public async Task Handle_InvalidCommand_ThrowsValidationException()
        {
            // Arrange
            var command = UpdateSaleCommandTestData.GenerateInvalidCommand();

            var sale = new Sale("SALE123", DateTime.UtcNow,
                new ExternalCustomer("cust-1", "Cliente Teste"),
                new ExternalBranch("branch-1", "Filial Teste"));

            _saleRepository.GetByIdAsync(command.SaleId, Arg.Any<CancellationToken>())
                           .Returns(sale);

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task Handle_SaleNotFound_ThrowsKeyNotFoundException()
        {
            // Arrange
            var command = UpdateSaleCommandTestData.GenerateValidCommand();

            _saleRepository.GetByIdAsync(command.SaleId, Arg.Any<CancellationToken>())
                           .Returns((Sale)null!);

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"Sale with ID {command.SaleId} not found");
        }

        [Fact]
        public async Task Handle_ProductNotFound_ThrowsInvalidOperationException()
        {
            // Arrange
            var command = UpdateSaleCommandTestData.GenerateCommandWithMissingProduct();

            var sale = new Sale("SALE123", DateTime.UtcNow,
                new ExternalCustomer("cust-1", "Cliente Teste"),
                new ExternalBranch("branch-1", "Filial Teste"));

            _saleRepository.GetByIdAsync(command.SaleId, Arg.Any<CancellationToken>())
                           .Returns(sale);

            _productClient.GetByIdAsync("MISSING_PRODUCT").Returns((ProductDto)null!);

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Product MISSING_PRODUCT not found.");
        }
    }
}
