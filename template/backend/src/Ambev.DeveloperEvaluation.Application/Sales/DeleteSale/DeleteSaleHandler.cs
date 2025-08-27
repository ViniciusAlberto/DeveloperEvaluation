using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Dispatcher;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    /// <summary>
    /// Handler for processing DeleteSaleCommand requests
    /// </summary>
    public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IDomainEventPublisher _eventPublisher;

        /// <summary>
        /// Initializes a new instance of DeleteSaleHandler
        /// </summary>
        public DeleteSaleHandler(ISaleRepository saleRepository, IDomainEventPublisher eventPublisher)
        {
            _saleRepository = saleRepository;
            _eventPublisher = eventPublisher;
        }

        /// <summary>
        /// Handles the DeleteSaleCommand request
        /// </summary>
        public async Task<DeleteSaleResult> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

           
            var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
            if (sale == null)
                throw new KeyNotFoundException($"Sale with ID {request.Id} not found");

            
            await _saleRepository.DeleteAsync(sale.Id, cancellationToken);

        
            await _eventPublisher.PublishAsync(new SaleDeletedEvent(sale.Id));

            return new DeleteSaleResult { Success = true };
        }
    }
}
