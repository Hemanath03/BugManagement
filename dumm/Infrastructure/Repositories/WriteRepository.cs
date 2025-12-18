using Application.Common.Data;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class WriteRepository<T>(
    IApplicationDbContext dbContext
) : IWriteRepository<T>
    where T : class
    {
        protected readonly IApplicationDbContext _dbContext = dbContext;
        protected readonly DbSet<T> dbSet = dbContext.Set<T>();

        protected async Task SaveAsync(CancellationToken cancellationToken = default)
            => await _dbContext.SaveChangesAsync(cancellationToken);

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await dbSet.AddAsync(entity, cancellationToken);
            await SaveAsync(cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            dbSet.Update(entity);
            await SaveAsync(cancellationToken);
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            dbSet.Remove(entity);
            await SaveAsync(cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await dbSet.AddRangeAsync(entities, cancellationToken);
            await SaveAsync(cancellationToken);
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            dbSet.UpdateRange(entities);
            await SaveAsync(cancellationToken);
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            dbSet.RemoveRange(entities);
            await SaveAsync(cancellationToken);
        }
    }
}
