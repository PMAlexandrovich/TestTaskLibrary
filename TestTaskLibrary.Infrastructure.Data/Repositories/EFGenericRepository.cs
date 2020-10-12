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
        }

        public IQueryable<TEntity> GetAll()
        {
            return dbSet;
        }

        public async Task AddAsync(TEntity item)
        {
            dbSet.Add(item);
            await context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> items)
        {
            dbSet.AddRange(items);
            await context.SaveChangesAsync();
        }

        public async Task RemoveAsync(TEntity item)
        {
            dbSet.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity item)
        {
            dbSet.Update(item);
            await context.SaveChangesAsync();
        }
    }
}
