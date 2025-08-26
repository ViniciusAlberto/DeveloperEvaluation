namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleResult
    {
        public Guid Id { get; set; }
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string CustomerId { get; set; } = string.Empty;
        public string BranchId { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public List<GetSaleItemResult> Items { get; set; } = new();
    }

    public class GetSaleItemResult
    {
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
    }
}