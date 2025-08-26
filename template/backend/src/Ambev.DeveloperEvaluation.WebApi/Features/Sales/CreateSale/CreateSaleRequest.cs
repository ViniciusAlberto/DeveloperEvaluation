namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequest
    {
        public Guid CustomerId { get; set; }
        public Guid BranchId { get; set; }
        public DateTime Date { get; set; }
        public List<CreateSaleItemRequest> Items { get; set; } = new();
    }

    public class CreateSaleItemRequest
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
