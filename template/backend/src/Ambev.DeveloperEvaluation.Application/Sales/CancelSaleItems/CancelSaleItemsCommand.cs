using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItems
{

    public record CancelSaleItemsCommand : IRequest<CancelSaleItemsResult>
    {
        /// <summary>
        /// The unique identifier  saleItems to Cancel
        /// </summary>
        public List<Guid> SaleItemsId { get; } = new();

        /// <summary>
        /// The unique identifier  sale ID
        /// </summary>
        public Guid SaleId { get; } = new();


        /// <summary>
        /// Initializes a new instance of CancelSaleCommand
        /// </summary>
        /// <param name="id">The ID of the sale to Cancel</param>
        public CancelSaleItemsCommand(Guid saleId, List<Guid> saleItemsId)
        {
            SaleId = saleId;
            SaleItemsId = saleItemsId;
        }
    }
}