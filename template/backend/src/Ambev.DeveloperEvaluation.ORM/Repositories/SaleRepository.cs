using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;

        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            await _context.Sales.AddAsync(sale, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Sale>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Sales
                .Include(s => s.Items)
                .ToListAsync(cancellationToken);
        }

        public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Sales
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<Sale?> GetBySaleNumberAsync(string saleNumber, CancellationToken cancellationToken = default)
        {
            return await _context.Sales
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.SaleNumber == saleNumber, cancellationToken);
        }

        public async Task<IReadOnlyList<Sale>> GetByCustomerAsync(string customerExternalId, CancellationToken cancellationToken = default)
        {
            return await _context.Sales
                .Include(s => s.Items)
                .Where(s => s.Customer.ExternalId == customerExternalId)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Sale>> GetByBranchAsync(string branchExternalId, CancellationToken cancellationToken = default)
        {
            return await _context.Sales
                .Include(s => s.Items)
                .Where(s => s.Branch.ExternalId == branchExternalId)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Sale>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            return await _context.Sales
                .Include(s => s.Items)
                .Where(s => s.Date >= startDate && s.Date <= endDate)
                .ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}