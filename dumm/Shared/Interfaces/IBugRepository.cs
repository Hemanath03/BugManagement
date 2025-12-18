using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBugRepository
    {
        Task<Bug?> GetByIdAsync(int id);
        Task<IReadOnlyList<Bug>> GetAllAsync();
    }
}
