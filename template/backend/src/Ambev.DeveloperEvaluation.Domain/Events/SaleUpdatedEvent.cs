using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleUpdatedEvent : IDomainEvent
    {
        public string SaleNumber { get; }
        public SaleUpdatedEvent(string saleNumber)
        {
            SaleNumber = saleNumber;
        }
    }
}
