using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskLibrary.Domain.Application.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll { get;}
        Task Add(TEntity item);
        Task AddRange(IEnumerable<TEntity> items);
        Task Remove(TEntity item);
        Task Update(TEntity item);
    }
}
