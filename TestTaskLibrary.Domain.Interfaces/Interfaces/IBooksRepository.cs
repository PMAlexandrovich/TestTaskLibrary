using System;
using System.Collections.Generic;
using System.Linq;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Domain.Interfaces
{
    public interface IBooksRepository : ICommonRepository<Book>
    {
        IQueryable<Book> Books { get; set; }
        
    }
}
