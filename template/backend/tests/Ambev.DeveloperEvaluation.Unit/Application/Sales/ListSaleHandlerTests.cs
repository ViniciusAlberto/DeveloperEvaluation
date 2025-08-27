using Ambev.DeveloperEvaluation.Application.Sales.ListSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
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
    public class ListSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ListSaleHandler _handler;

        public ListSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
           
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Sale, SaleDto>()
                   .ForMember(dest => dest.Cancelled, opt => opt.MapFrom(src => src.Cancelled))
                   .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

                cfg.CreateMap<SaleItem, SaleItemDto>()
                   .ForMember(dest => dest.Subtotal, opt => opt.MapFrom(src => src.Total))
                   .ForMember(dest => dest.Cancelled, opt => opt.MapFrom(src => src.IsCancelled))
                   .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
            });
            _mapper = config.CreateMapper();

            _handler = new ListSaleHandler(_saleRepository, _mapper);
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsListSaleResult()
        {
           
            var command = ListSaleCommandTestData.GenerateValidCommand();

           
            var sales = new List<Sale>();
            for (int i = 0; i < command.PageSize; i++)
            {
                var customer = new ExternalCustomer($"cust-{i}", $"Customer {i}");
                var branch = new ExternalBranch($"branch-{i}", $"Branch {i}");
                var sale = new Sale($"SALE-{i + 1}", DateTime.UtcNow.AddDays(-i), customer, branch);

             
                var product = new ExternalProduct($"prod-{i}", $"Product {i}");
                sale.AddItem(product, 2, 50m);

                sales.Add(sale);
            }

            _saleRepository
                .GetPagedAsync(command.Page, command.PageSize, command.OrderBy, Arg.Any<CancellationToken>())
                .Returns((sales.AsEnumerable(), sales.Count));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Items.Count().Should().Be(command.PageSize);
            result.CurrentPage.Should().Be(command.Page);
            result.TotalCount.Should().Be(command.PageSize);
            result.TotalPages.Should().Be(1);

            var firstSale = result.Items.First();
            firstSale.Items.First().ProductName.Should().Be("Product 0");
        }

        [Fact]
        public async Task Handle_InvalidCommand_ThrowsValidationException()
        {
           
            var command = ListSaleCommandTestData.GenerateInvalidCommand();

           
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            
            await act.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task Handle_EmptySales_ReturnsEmptyList()
        {
            
            var command = ListSaleCommandTestData.GenerateValidCommand();

            _saleRepository
                .GetPagedAsync(command.Page, command.PageSize, command.OrderBy, Arg.Any<CancellationToken>())
                .Returns((Enumerable.Empty<Sale>(), 0));

            
            var result = await _handler.Handle(command, CancellationToken.None);

            result.Items.Should().BeEmpty();
            result.TotalCount.Should().Be(0);
            result.TotalPages.Should().Be(0);
        }
    }
}
