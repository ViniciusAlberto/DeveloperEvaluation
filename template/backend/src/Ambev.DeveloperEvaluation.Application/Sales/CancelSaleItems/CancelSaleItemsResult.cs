namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItems
{

    /// <summary>
    /// Response model for CancelSaleITems operation
    /// </summary>
    public class CancelSaleItemsResult
    {
        /// <summary>
        /// Indicates whether the cancel was successful
        /// </summary>
        public bool Success { get; set; }

        public List<Guid> CancelledItems { get; set; } = new();
    }
}
