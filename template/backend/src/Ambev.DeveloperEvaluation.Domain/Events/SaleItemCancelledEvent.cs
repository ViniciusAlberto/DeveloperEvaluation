using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleItemCancelledEvent : IDomainEvent
    {
        public Guid SaleId { get; }
        public Guid ItemId { get; }

        public SaleItemCancelledEvent(Guid saleId, Guid itemId)
        {
            SaleId = saleId;
            ItemId = itemId;
        }
    }
}
