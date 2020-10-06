using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;

namespace TestTaskLibrary.Infrastructure.Data.Repositories
{
    public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly LibraryContext context;
        private readonly DbSet<TEntity> dbSet;

        public EFGenericRepository(LibraryContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
            GetAll = dbSet;
        }

        public IQueryable<TEntity> GetAll { get; }

        public async Task Add(TEntity item)
        {
            dbSet.Add(item);
            await context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<TEntity> items)
        {
            dbSet.AddRange(items);
            await context.SaveChangesAsync();
        }

        public async Task Remove(TEntity item)
        {
            dbSet.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task Update(TEntity item)
        {
            dbSet.Update(item);
            await context.SaveChangesAsync();
        }
    }
}
