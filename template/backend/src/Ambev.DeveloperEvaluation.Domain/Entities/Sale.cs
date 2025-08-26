using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {       
        public string SaleNumber { get; private set; }  
        public DateTime Date { get; private set; }

        public ExternalCustomer Customer { get; private set; }
        public ExternalBranch Branch { get; private set; }

        private readonly List<SaleItem> _items = new();
        public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();

        public decimal TotalAmount => _items.Sum(i => i.Total);
        public bool Cancelled { get; private set; }

        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();      


        public Sale(string saleNumber, DateTime date, ExternalCustomer customer, ExternalBranch branch)
        {
            Id = Guid.NewGuid();
            SaleNumber = saleNumber;
            Date = date;
            Customer = customer;
            Branch = branch;
            Cancelled = false;

             _domainEvents.Add(new SaleCreatedEvent(SaleNumber));
        }

        // Construtor protegido usado pelo EF Core
        protected Sale() { }

        public void AddItem(ExternalProduct product, int quantity, decimal unitPrice)
        {
            var item = new SaleItem(product, quantity, unitPrice);
            _items.Add(item);
        }

        public void Update(DateTime date, ExternalCustomer customer, ExternalBranch branch)
        {
            Date = date;
            Customer = customer;
            Branch = branch;
            _domainEvents.Add(new SaleUpdatedEvent(SaleNumber));
        }

        public void ClearItems() => _items.Clear();

        public void Cancel()
        {
            Cancelled = true;
        }       
        
        public void ClearEvents() => _domainEvents.Clear();

    }
}
