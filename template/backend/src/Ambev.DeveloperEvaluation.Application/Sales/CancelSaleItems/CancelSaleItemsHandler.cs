using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Application.Dispatcher;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItems
{
    public class CancelSaleItemsHandler : IRequestHandler<CancelSaleItemsCommand, CancelSaleItemsResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IDomainEventPublisher _eventPublisher;

        public CancelSaleItemsHandler(ISaleRepository saleRepository, IDomainEventPublisher eventPublisher)
        {
            _saleRepository = saleRepository;
            _eventPublisher = eventPublisher;
        }

        public async Task<CancelSaleItemsResult> Handle(CancelSaleItemsCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.SaleId, cancellationToken);
            if (sale == null)
                throw new KeyNotFoundException($"Sale with ID {request.SaleId} not found");

            var cancelledItems = new List<Guid>();

            foreach (var itemId in request.SaleItemsId)
            {
                var item = sale.Items.FirstOrDefault(i => i.Id == itemId);
                if (item != null && !item.IsCancelled)
                {
                    item.Cancel();
                    cancelledItems.Add(itemId);

                    // publica evento para cada item cancelado
                    await _eventPublisher.PublishAsync(new SaleItemCancelledEvent(sale.Id, item.Id));
                }
            }

            await _saleRepository.UpdateAsync(sale, cancellationToken);

            return new CancelSaleItemsResult
            {
                Success = cancelledItems.Any(),
                CancelledItems = cancelledItems
            };
        }
    }
}
