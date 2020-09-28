using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestTaskLibrary.Domain.Interfaces
{
    public interface ICommonRepository<T>
    {
        IEnumerable<T> GetList();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
