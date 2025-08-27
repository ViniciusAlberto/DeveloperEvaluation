namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleItems
{
    /// <summary>
    /// Request model for cancel a item in sale by ID
    /// </summary>
    public class CancelSaleItemsRequest
    {
        /// <summary>
        /// The list with  unique identifier item in sale to cancel
        /// </summary>
        public required List<Guid> SaleItemsIds { get; set; }

        public Guid SaleId { get; set; }
    }
}
