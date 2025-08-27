using Ambev.DeveloperEvaluation.Application.Dispatcher;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale.Sales.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Clients;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Unit.Domain.Sales.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class CreateSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductClient _productClient;
        private readonly IMapper _mapper;
        private readonly IDomainEventPublisher _eventPublisher;
        private readonly CreateSaleHandler _handler;

        public CreateSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _productClient = Substitute.For<IProductClient>();
            _mapper = Substitute.For<IMapper>();
            _eventPublisher = Substitute.For<IDomainEventPublisher>();

            _handler = new CreateSaleHandler(_saleRepository, _productClient, _mapper, _eventPublisher);
        }
        [Fact(DisplayName = "Handle valid command returns result")]
        public async Task Handle_ValidCommand_ReturnsResult()
        {
            // Arrange
            var command = CreateSaleCommandTestData.GenerateValidCommand();
            var customer = new ExternalCustomer(command.Customer.ExternalId, command.Customer.Name);
            var branch = new ExternalBranch(command.Branch.ExternalId, command.Branch.Name);
            var productDto = new ProductDto(command.Items[0].ProductExternalId, command.Items[0].ProductName);
            var externalProduct = new ExternalProduct(command.Items[0].ProductExternalId, command.Items[0].ProductName);

            _mapper.Map<ExternalCustomer>(command.Customer).Returns(customer);
            _mapper.Map<ExternalBranch>(command.Branch).Returns(branch);
            _productClient.GetByIdAsync(command.Items[0].ProductExternalId).Returns(Task.FromResult(productDto));
            _mapper.Map<ExternalProduct>(productDto).Returns(externalProduct);
            _saleRepository.AddAsync(Arg.Any<Sale>()).Returns(Task.CompletedTask);
            _eventPublisher.PublishAsync(Arg.Any<IDomainEvent>()).Returns(Task.CompletedTask);

            _mapper.Map<CreateSaleResult>(Arg.Any<Sale>()).Returns(ci =>
            {
                var sale = ci.Arg<Sale>();
                return new CreateSaleResult
                {
                    SaleNumber = sale.SaleNumber,
                    CustomerId = sale.Customer.ExternalId,
                    BranchId = sale.Branch.ExternalId,
                    TotalAmount = sale.TotalAmount, // pega direto da entidade
                    Items = sale.Items.Select(i => new CreateSaleItemResult
                    {
                        ProductId = i.Product.ExternalId,
                        ProductName = i.Product.Name,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice,
                        Subtotal = i.Total
                    }).ToList()
                };
            });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.SaleNumber.Should().Be(command.SaleNumber);
            result.Items.Should().HaveCount(command.Items.Count);

            // agora confere com o cálculo real, não com o DTO
            result.TotalAmount.Should().Be(result.Items.Sum(i => i.Subtotal));

            await _saleRepository.Received(1).AddAsync(Arg.Is<Sale>(s =>
                s.SaleNumber == command.SaleNumber &&
                s.Customer.Equals(customer) &&
                s.Branch.Equals(branch) &&
                s.Items.Count == command.Items.Count
            ));

            await _eventPublisher.Received(command.Items.Count).PublishAsync(Arg.Any<IDomainEvent>());
        }

        [Fact(DisplayName = "Handle invalid command throws validation exception")]
        public async Task Handle_InvalidCommand_ThrowsValidationException()
        {
            // Arrange
            var command = CreateSaleCommandTestData.GenerateInvalidCommand();

            // Act
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<FluentValidation.ValidationException>();
        }

        [Fact(DisplayName = "Handle command with missing product throws exception")]
        public async Task Handle_CommandWithMissingProduct_ThrowsInvalidOperationException()
        {
            // Arrange
            var command = CreateSaleCommandTestData.GenerateCommandWithMissingProduct();
            var customer = new ExternalCustomer(command.Customer.ExternalId, command.Customer.Name);
            var branch = new ExternalBranch(command.Branch.ExternalId, command.Branch.Name);

            _mapper.Map<ExternalCustomer>(command.Customer).Returns(customer);
            _mapper.Map<ExternalBranch>(command.Branch).Returns(branch);
            _productClient.GetByIdAsync(command.Items[0].ProductExternalId).Returns(Task.FromResult<ProductDto?>(null));

            // Act
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                     .WithMessage($"Product {command.Items[0].ProductExternalId} not found.");
        }
    }
}

