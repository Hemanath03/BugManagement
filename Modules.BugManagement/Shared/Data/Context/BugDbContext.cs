using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Modules.BugManagement.Shared.Data.Context;
using Modules.BugManagement.Shared.Domain.Entities;
using Shared.Abstractions.Data;
using System.Data.Common;


namespace Modules.BugManagement.Shared.Data.Context
{
    public class BugDbContext
    : DbContext, IApplicationDbContext
    {
        
        public BugDbContext(DbContextOptions<BugDbContext> options)
            : base(options)
        {
        }

        public IEntityType? FindEntityType(Type type)
            => Model.FindEntityType(type);

        public DbConnection GetDbConnection() => Database.GetDbConnection();

        public DbSet<Bug> Bugs { get; set; }

    }

}
//Add - Migration InitialCreate - Context BugDbContext - OutputDir Shared\Migrations\BugDbContextMigrations

//Update-Database -Context BugDbContext
