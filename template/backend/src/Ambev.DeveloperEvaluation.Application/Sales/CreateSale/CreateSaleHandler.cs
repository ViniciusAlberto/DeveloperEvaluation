using Ambev.DeveloperEvaluation.Application.Dispatcher;
using Ambev.DeveloperEvaluation.Domain.Clients;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Validation.Sale;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    namespace Sales.Application.Sales.CreateSale
    {
        public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
        {
            private readonly ISaleRepository _saleRepository;
            private readonly IProductClient _productClient;
            private readonly IMapper _mapper;
            private readonly IDomainEventPublisher _eventPublisher;

            public CreateSaleHandler(ISaleRepository saleRepository, IProductClient productClient, IMapper mapper, IDomainEventPublisher eventPublisher)
            {
                _saleRepository = saleRepository;
                _productClient = productClient;
                _mapper = mapper;
                _eventPublisher = eventPublisher;
            }

            public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
            {
                var customer = _mapper.Map<ExternalCustomer>(request.Customer);
                var branch = _mapper.Map<ExternalBranch>(request.Branch);

                var sale = new Sale(request.SaleNumber, request.Date, customer, branch);

                foreach (var itemDto in request.Items)
                {
                    // Consulta externa do produto
                    var productInfo = await _productClient.GetByIdAsync(itemDto.ProductExternalId);
                    if (productInfo == null)
                        throw new InvalidOperationException($"Product {itemDto.ProductExternalId} not found.");

                    var product = _mapper.Map<ExternalProduct>(productInfo);

                    sale.AddItem(product, itemDto.Quantity, itemDto.UnitPrice);
                }
                
                var validator = new SaleValidator();
                var result = validator.Validate(sale);
                if (!result.IsValid)
                    throw new FluentValidation.ValidationException(result.Errors);

                await _saleRepository.AddAsync(sale);

                foreach (var domainEvent in sale.DomainEvents)
                    await _eventPublisher.PublishAsync(domainEvent);

                sale.ClearEvents();


                return _mapper.Map<CreateSaleResult>(sale);

                 
            }
        }
    }

}
