using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents an item within a sale transaction.
    /// Includes product details, quantity, pricing, discount, and cancellation status.
    /// </summary>
    public class SaleItem : BaseEntity
    {
        /// <summary>
        /// Gets the product associated with this sale item.
        /// </summary>
        public ExternalProduct Product { get; private set; }

        /// <summary>
        /// Gets the quantity of the product in this item.
        /// Must be greater than zero and not exceed 20.
        /// </summary>
        public int Quantity { get; private set; }

        /// <summary>
        /// Gets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; private set; }

        /// <summary>
        /// Gets the discount applied to this item.
        /// Calculated based on quantity and unit price.
        /// </summary>
        public decimal Discount { get; private set; }

        /// <summary>
        /// Gets the total amount for this item after discount.
        /// </summary>
        public decimal Total => (Quantity * UnitPrice) - Discount;

        /// <summary>
        /// Indicates whether this sale item has been cancelled.
        /// </summary>
        public bool IsCancelled { get; private set; }

        /// <summary>
        /// Private constructor for EF Core and serialization purposes.
        /// </summary>
        private SaleItem() { }

        /// <summary>
        /// Initializes a new instance of the SaleItem class with product, quantity, and unit price.
        /// </summary>
        /// <param name="product">The product being sold.</param>
        /// <param name="quantity">The quantity of the product (1-20).</param>
        /// <param name="unitPrice">The unit price of the product.</param>
        /// <exception cref="ArgumentException">Thrown if quantity is less than or equal to zero.</exception>
        /// <exception cref="InvalidOperationException">Thrown if quantity exceeds 20.</exception>
        public SaleItem(ExternalProduct product, int quantity, decimal unitPrice)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");
            if (quantity > 20)
                throw new InvalidOperationException("Cannot sell more than 20 identical items.");

            Product = product ?? throw new ArgumentNullException(nameof(product));
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = CalculateDiscount(quantity, unitPrice);
            IsCancelled = false;
        }

        /// <summary>
        /// Cancels this sale item.
        /// Throws an exception if the item is already cancelled.
        /// </summary>
        public void Cancel()
        {
            if (IsCancelled)
                throw new InvalidOperationException("Sale item is already cancelled.");

            IsCancelled = true;
        }
        private static decimal CalculateDiscount(decimal quantity, decimal unitPrice)
        {
            if (quantity < 4) return 0;
            if (quantity >= 4 && quantity < 10) return quantity * unitPrice * 0.1m;
            if (quantity >= 10 && quantity <= 20) return quantity * unitPrice * 0.2m;
            return 0;
        }      
    }
}
