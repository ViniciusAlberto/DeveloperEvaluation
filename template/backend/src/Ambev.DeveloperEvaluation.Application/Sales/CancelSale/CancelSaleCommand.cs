using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{

    /// <summary>
    /// Command for cancel a sale by their ID
    /// </summary>
    public record CancelSaleCommand : IRequest<CancelSaleResult>
    {
        /// <summary>
        /// The unique identifier  sale to Cancel
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Initializes a new instance of CancelSaleCommand
        /// </summary>
        /// <param name="id">The ID of the sale to Cancel</param>
        public CancelSaleCommand(Guid id)
        {
            Id = id;
        }
    }
}