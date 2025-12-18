using Application.Common.Data;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BugRepository
    : WriteRepository<Bug>, IBugRepository
    {
        public BugRepository(IApplicationDbContext context)
            : base(context) { }

        public async Task<Bug?> GetByIdAsync(int id)
        {
            return await dbSet.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IReadOnlyList<Bug>> GetAllAsync()
        {
            return await dbSet
                .AsNoTracking()
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }
    }
}
