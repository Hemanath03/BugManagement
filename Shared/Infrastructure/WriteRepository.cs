using Microsoft.EntityFrameworkCore;
using Shared.Abstractions.Data;
using Shared.Common.EntityHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Infrastructure
{
    public abstract class WriteRepository<T> : IWriteRepository<T>
    where T : class
    {
        protected readonly IApplicationDbContext _dbContext;
        protected readonly DbSet<T> dbSet;

        protected WriteRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            dbSet = dbContext.Set<T>();
        }
        protected virtual async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            if (entity is BaseModel bm)
            {
                bm.CreatedAt = DateTime.UtcNow;
            }

            await dbSet.AddAsync(entity, cancellationToken);
            await SaveAsync(cancellationToken);
        }

        public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            if (entity is BaseModel bm)
            {
                bm.UpdatedAt = DateTime.UtcNow;
            }

            dbSet.Update(entity);
            await SaveAsync(cancellationToken);
        }

        public virtual async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            dbSet.Remove(entity);
            await SaveAsync(cancellationToken);
        }
    }
}
