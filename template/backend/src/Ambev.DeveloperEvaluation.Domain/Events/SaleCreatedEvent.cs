using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleCreatedEvent : IDomainEvent
    {
        public string SaleId { get; }  


        public SaleCreatedEvent(string saleId)
        {
            SaleId = saleId;                
        }
    }
}
