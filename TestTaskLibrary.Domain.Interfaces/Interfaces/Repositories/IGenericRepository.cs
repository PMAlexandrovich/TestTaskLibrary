using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskLibrary.Domain.Application.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetByIdAsync(int id);
        Task AddAsync(TEntity item);
        Task AddRangeAsync(IEnumerable<TEntity> items);
        Task RemoveAsync(TEntity item);
        Task UpdateAsync(TEntity item);
    }
}
