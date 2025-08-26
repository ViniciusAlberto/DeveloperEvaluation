using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Command for retrieving a user by their ID
    /// </summary>
    public record GetSaleCommand : IRequest<GetSaleResult>
    {
        /// <summary>
        /// The unique identifier of the user to retrieve
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Initializes a new instance of GetUserCommand
        /// </summary>
        /// <param name="id">The ID of the user to retrieve</param>
        public GetSaleCommand(Guid id)
        {
            Id = id;
        }
    }
}
