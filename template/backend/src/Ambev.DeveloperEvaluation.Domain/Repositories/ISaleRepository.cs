using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository
    {
        // Create / Add
        Task AddAsync(Sale sale, CancellationToken cancellationToken = default);
        // Read
        Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);   
        Task<IReadOnlyList<Sale>> GetAllAsync(CancellationToken cancellationToken = default);     
        Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default);
        // Delete / Remove
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
