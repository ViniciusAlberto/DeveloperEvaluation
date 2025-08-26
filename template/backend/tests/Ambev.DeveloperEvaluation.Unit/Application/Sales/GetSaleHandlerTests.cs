using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain.Sales.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class GetSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly GetSaleHandler _handler;

        public GetSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetSaleHandler(_saleRepository, _mapper);
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsMappedResult()
        {
            // Arrange
            var command = GetSaleCommandTestData.GenerateValidCommand();

            var sale = new Sale("SALE123", DateTime.UtcNow,
                new ExternalCustomer("cust-1", "Cliente Teste"),
                new ExternalBranch("branch-1", "Filial Teste"));

            var expectedResult = new GetSaleResult { Id = sale.Id, SaleNumber = sale.SaleNumber };

            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                           .Returns(sale);

            _mapper.Map<GetSaleResult>(sale).Returns(expectedResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.SaleNumber.Should().Be(expectedResult.SaleNumber);
            await _saleRepository.Received(1).GetByIdAsync(command.Id, Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task Handle_InvalidCommand_ThrowsValidationException()
        {
            // Arrange
            var command = GetSaleCommandTestData.GenerateInvalidCommand();

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task Handle_SaleNotFound_ThrowsKeyNotFoundException()
        {
            // Arrange
            var command = GetSaleCommandTestData.GenerateValidCommand();

            _saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                           .Returns((Sale)null!);

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"Sale with ID {command.Id} not found");
        }
    }
}
