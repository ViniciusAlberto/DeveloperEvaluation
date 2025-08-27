namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    /// <summary>
    /// Represents the response for updating a Sale.
    /// Contains the sale details after the update operation.
    /// </summary>
    public class UpdateSaleResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sale.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the sale number.
        /// </summary>
        public string SaleNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date of the sale.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the customer.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the branch.
        /// </summary>
        public Guid BranchId { get; set; }

        /// <summary>
        /// Gets or sets the total amount of the sale.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Indicates whether the sale has been cancelled.
        /// </summary>
        public bool Cancelled { get; set; }

        /// <summary>
        /// Gets or sets the list of sale items in the sale.
        /// </summary>
        public List<UpdateSaleItemResponse> Items { get; set; } = new();
    }

    /// <summary>
    /// Represents a single sale item in an updated sale response.
    /// </summary>
    public class UpdateSaleItemResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier of the product.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quantity of the product sold.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the subtotal for this item (Quantity * UnitPrice - Discount).
        /// </summary>
        public decimal Subtotal { get; set; }

        /// <summary>
        /// Indicates whether this sale item has been cancelled.
        /// </summary>
        public bool Cancelled { get; set; }
    }
}
