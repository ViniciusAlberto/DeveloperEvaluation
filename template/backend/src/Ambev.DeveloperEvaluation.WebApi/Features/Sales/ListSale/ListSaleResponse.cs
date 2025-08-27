namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSale
{
    /// <summary>
    /// Represents the response for a paginated list of sales.
    /// </summary>
    public class ListSaleResponse
    {
        /// <summary>
        /// Gets or sets the collection of sales in the current page.
        /// </summary>
        public IEnumerable<SaleDto> Data { get; set; } = new List<SaleDto>();

        /// <summary>
        /// Gets or sets the total number of sales across all pages.
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Gets or sets the current page number.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the total number of pages available.
        /// </summary>
        public int TotalPages { get; set; }
    }

    /// <summary>
    /// Represents a single sale in the list of sales.
    /// </summary>
    public class SaleDto
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
        /// Gets or sets the collection of items in this sale.
        /// </summary>
        public List<SaleItemDto> Items { get; set; } = new();
    }

    /// <summary>
    /// Represents a single item in a sale.
    /// </summary>
    public class SaleItemDto
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
