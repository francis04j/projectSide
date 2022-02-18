using Application.Common.Interfaces;
using JustEat.Search.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Brokers.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) {
            //TODO: look into why we do this
            this.Database.Migrate();
            this.Database.EnsureCreated();
        }
        public DbSet<Query> Queries { get; }

        public IQueryable<T> GetItems<T>()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Query> GetQueries()
        {
            return Queries;
        }

        public Task<int> SaveItem<T>(T entity, CancellationToken cancellationToken)
        {
            this.Queries.Add(entity as Query);
            return base.SaveChangesAsync(cancellationToken);  
        }
    }
}
