using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a sale transaction.
    /// Contains customer, branch, sale items, total amount, and cancellation status.
    /// Tracks domain events for creation and updates.
    /// </summary>
    public class Sale : BaseEntity
    {
        /// <summary>
        /// Gets the sale number (unique identifier for display).
        /// </summary>
        public string SaleNumber { get; private set; }

        /// <summary>
        /// Gets the date of the sale.
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Gets the customer associated with this sale.
        /// </summary>
        public ExternalCustomer Customer { get; private set; }

        /// <summary>
        /// Gets the branch where the sale occurred.
        /// </summary>
        public ExternalBranch Branch { get; private set; }

        private readonly List<SaleItem> _items = [];
        /// <summary>
        /// Gets the collection of items included in this sale.
        /// </summary>
        public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();

        /// <summary>
        /// Gets the total amount of the sale, summing all items.
        /// </summary>
        public decimal TotalAmount => _items.Sum(i => i.Total);

        /// <summary>
        /// Indicates whether the sale has been cancelled.
        /// </summary>
        public bool Cancelled { get; private set; }

        private readonly List<IDomainEvent> _domainEvents = [];
        /// <summary>
        /// Gets the list of domain events associated with this sale.
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        /// <summary>
        /// Initializes a new instance of the Sale class with sale number, date, customer, and branch.
        /// Adds a SaleCreatedEvent to the domain events.
        /// </summary>
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

        /// <summary>
        /// Protected constructor for EF Core and serialization purposes.
        /// </summary>
        protected Sale() { }

        /// <summary>
        /// Adds a new item to this sale.
        /// </summary>
        /// <param name="product">The product to add.</param>
        /// <param name="quantity">The quantity of the product.</param>
        /// <param name="unitPrice">The unit price of the product.</param>
        public void AddItem(ExternalProduct product, int quantity, decimal unitPrice)
        {
            var item = new SaleItem(product, quantity, unitPrice);
            _items.Add(item);
        }

        /// <summary>
        /// Updates the sale with new date, customer, and branch.
        /// Adds a SaleUpdatedEvent to the domain events.
        /// </summary>
        public void Update(DateTime date, ExternalCustomer customer, ExternalBranch branch)
        {
            Date = date;
            Customer = customer;
            Branch = branch;
            _domainEvents.Add(new SaleUpdatedEvent(SaleNumber));
        }

        /// <summary>
        /// Clears all items from the sale.
        /// </summary>
        public void ClearItems() => _items.Clear();

        /// <summary>
        /// Cancels the sale.
        /// </summary>
        public void Cancel() => Cancelled = true;

        /// <summary>
        /// Clears all domain events associated with this sale.
        /// </summary>
        public void ClearEvents() => _domainEvents.Clear();
    }
}
