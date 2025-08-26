using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository
    {
        // Create / Add
        Task AddAsync(Sale sale, CancellationToken cancellationToken = default);

        // Read
        Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Sale?> GetBySaleNumberAsync(string saleNumber, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Sale>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Sale>> GetByCustomerAsync(string customerExternalId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Sale>> GetByBranchAsync(string branchExternalId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Sale>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

        // Update
        Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default);

        // Delete / Remove
        Task DeleteAsync(Sale sale, CancellationToken cancellationToken = default);
    }
}
