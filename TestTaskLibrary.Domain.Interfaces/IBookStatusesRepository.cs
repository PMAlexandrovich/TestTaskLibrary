using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Domain.Interfaces
{
    public interface IBookStatusesRepository
    {
        IQueryable<BookStatus> Statuses { get; set; }
        IEnumerable<BookStatus> GetBookStatusList();
        BookStatus GetBookStatus(int id);
        void Create(BookStatus item);
        void Update(BookStatus item);
        void Delete(int id);
    }
}
