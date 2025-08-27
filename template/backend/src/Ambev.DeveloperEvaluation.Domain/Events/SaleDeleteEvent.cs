using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleDeletedEvent : IDomainEvent
    {
        public Guid SaleId { get; }

        public SaleDeletedEvent(Guid saleId)
        {
            SaleId = saleId;
        }
    }
}