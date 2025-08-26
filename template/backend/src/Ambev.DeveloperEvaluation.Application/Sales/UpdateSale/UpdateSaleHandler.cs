using Ambev.DeveloperEvaluation.Application.Dispatcher;
using Ambev.DeveloperEvaluation.Domain.Clients;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Validation.Sale;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductClient _productClient;
        private readonly IMapper _mapper;
        private readonly IDomainEventPublisher _eventPublisher;

        public UpdateSaleHandler(
            ISaleRepository saleRepository,
            IProductClient productClient,
            IMapper mapper,
            IDomainEventPublisher eventPublisher)
        {
            _saleRepository = saleRepository;
            _productClient = productClient;
            _mapper = mapper;
            _eventPublisher = eventPublisher;
        }

        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {            
            var sale = await _saleRepository.GetByIdAsync(request.SaleId, cancellationToken);
            if (sale == null)
                throw new KeyNotFoundException($"Sale with ID {request.SaleId} not found");
            
            sale.Update(request.Date,
                _mapper.Map<ExternalCustomer>(request.Customer),
                _mapper.Map<ExternalBranch>(request.Branch));
            
            sale.ClearItems();
            foreach (var itemDto in request.Items)
            {
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
            
            await _saleRepository.UpdateAsync(sale);
            
            await _eventPublisher.PublishAsync(new SaleUpdatedEvent(sale.SaleNumber));

            sale.ClearEvents();
            
            return _mapper.Map<UpdateSaleResult>(sale);
        }
    }
}
