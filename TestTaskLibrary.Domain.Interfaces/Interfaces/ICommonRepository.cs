using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskLibrary.Domain.Interfaces
{
    public interface ICommonRepository<T>
    {
        IQueryable<T> GetAll { get; }

        Task<IEnumerable<T>> GetList();

        Task<T> Get(int id);

        Task Create(T item);

        Task Update(T item);

        Task Delete(int id);
    }
}
