using Microsoft.EntityFrameworkCore;
using Modules.BugManagement.Shared.Domain.Entities;
using Modules.BugManagement.Shared.Domain.Enums;
using Shared.Abstractions.Data;
using Shared.Common.Models;
using Shared.Infrastructure;

namespace Modules.BugManagement.Shared.Data.Repositories
{
    public class BugRepository
    : WriteRepository<Bug>
    {
        public BugRepository(IApplicationDbContext dbContext)
            : base(dbContext)
        {
        }


        public async Task<Bug?> GetByIdAsync(int id, CancellationToken ct)
        {
            return await dbSet.FirstOrDefaultAsync(b => b.Id == id, ct);
        }

        public async Task<IReadOnlyList<Bug>> GetAllAsync(CancellationToken ct)
        {
            return await dbSet
                .AsNoTracking()
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<PaginatedList<Bug>> GetPagedAsync(
        SearchRequest request,
        CancellationToken ct)
        {
            var query = dbSet.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(b =>
                    b.Title.Contains(request.Search));
            }

            var totalCount = await query.CountAsync(ct);

            var items = await query
                .OrderByDescending(b => b.CreatedAt)
                .Skip(request.Skip)
                .Take(request.PageSize)
                .ToListAsync(ct);

            return new PaginatedList<Bug>(
                items,
                totalCount,
                request.PageNumber,
                request.PageSize
            );
        }

        public async Task<IReadOnlyList<Bug>> GetByStatusAsync(BugStatus status)
        {
            return await dbSet
                .AsNoTracking()
                .Where(b => b.Status == status)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(string title)
        {
            return await dbSet.AnyAsync(b => b.Title == title);
        }
    }
}
