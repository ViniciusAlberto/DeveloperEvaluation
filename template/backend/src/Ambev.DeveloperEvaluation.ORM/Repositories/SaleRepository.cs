using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Implementation of ISaleRepository using Entity Framework Core
    /// </summary>
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of SaleRepository
        /// </summary>
        /// <param name="context">The database context</param>
        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new sale to the database
        /// </summary>
        /// <param name="sale">The sale to add</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task AddAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            await _context.Sales.AddAsync(sale, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Retrieves a sale by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the sale</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The sale if found, null otherwise</returns>
        public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Sales
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        /// <summary>
        /// Retrieves all sales from the database
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A read-only list of all sales</returns>
        public async Task<IReadOnlyList<Sale>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Sales
                .Include(s => s.Items)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Updates an existing sale in the database
        /// </summary>
        /// <param name="sale">The sale to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Deletes a sale by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the sale to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if deleted, false if not found</returns>
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var sale = await _context.Sales.FindAsync(new object[] { id }, cancellationToken);
            if (sale == null)
                return false;

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        /// <summary>
        /// Retrieves a paginated list of sales from the database
        /// </summary>
        /// <param name="page">The page number (starting at 1)</param>
        /// <param name="pageSize">The number of items per page</param>
        /// <param name="orderBy">The ordering of results (e.g., "Date desc")</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A tuple containing the collection of sales and the total count of sales</returns>
        public async Task<(IEnumerable<Sale> Sales, int TotalCount)> GetPagedAsync(
           int page,
           int pageSize,
           string orderBy,
           CancellationToken cancellationToken)
        {
            var query = _context.Sales.AsQueryable();

            var totalCount = await query.CountAsync(cancellationToken);

            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                var parts = orderBy.Split(' ');
                var property = parts[0];
                var direction = parts.Length > 1 ? parts[1].ToLower() : "asc";

                query = direction == "desc"
                    ? query.OrderByDescending(e => EF.Property<object>(e, property))
                    : query.OrderBy(e => EF.Property<object>(e, property));
            }
            else
            {
                query = query.OrderBy(e => e.Id);
            }

            var sales = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (sales, totalCount);
        }
    }
}
