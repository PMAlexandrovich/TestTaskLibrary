using System;
using System.Collections.Generic;
using System.Linq;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Domain.Interfaces
{
    public interface IBooksRepository
    {
        IQueryable<Book> Books { get; set; }
        IEnumerable<Book> GetBookList();
        Book GetBook(int id);
        void Create(Book item);
        void Update(Book item);
        void Delete(int id);
    }
}
