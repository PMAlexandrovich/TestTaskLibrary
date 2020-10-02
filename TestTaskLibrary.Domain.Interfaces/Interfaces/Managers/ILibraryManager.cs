using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Domain.Application.Interfaces.Managers
{
    public interface ILibraryManager
    {
        Task<bool> IssueBookAsync(User user, int bookId);

        Task<bool> TakeAsync(int bookId);

        Task<bool> BookAsync(User user, int bookId);

        Task<bool> UnbookAsync(User user, int bookId);
    }
}
