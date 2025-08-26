using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem : BaseEntity
    {
        public ExternalProduct Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Discount { get; private set; }
        public decimal Total => (Quantity * UnitPrice) - Discount;

        private SaleItem() { }

        public SaleItem(ExternalProduct product, int quantity, decimal unitPrice )
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");
            if (quantity > 20)
                throw new InvalidOperationException("Cannot sell more than 20 identical items.");

            Product = product;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = CalculateDiscount(quantity, unitPrice); 
        }

        private decimal CalculateDiscount(decimal quantity, decimal unitPrice)
        {
            if (quantity < 4) return 0;
            if (quantity >= 4 && quantity < 10) return quantity * unitPrice * 0.1m;
            if (quantity >= 10 && quantity <= 20) return quantity * unitPrice * 0.2m;
            return 0;
        }
    }
}
