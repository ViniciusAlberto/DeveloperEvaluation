using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Repository interface for Sale entity operations
    /// </summary>
    public interface ISaleRepository
    {
        /// <summary>
        /// Adds a new sale to the repository
        /// </summary>
        /// <param name="sale">The sale to add</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task AddAsync(Sale sale, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a sale by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the sale</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The sale if found, null otherwise</returns>
        Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all sales in the repository
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A read-only list of all sales</returns>
        Task<IReadOnlyList<Sale>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing sale in the repository
        /// </summary>
        /// <param name="sale">The sale to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a sale from the repository by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the sale to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if the sale was deleted, false if not found</returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a paginated list of sales from the repository
        /// </summary>
        /// <param name="page">The page number (starting at 1)</param>
        /// <param name="pageSize">The number of items per page</param>
        /// <param name="orderBy">The ordering of the results (e.g., "date desc")</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A tuple containing the collection of sales and the total count of sales</returns>
        Task<(IEnumerable<Sale> Sales, int TotalCount)> GetPagedAsync(
            int page,
            int pageSize,
            string orderBy,
            CancellationToken cancellationToken);
    }
}
