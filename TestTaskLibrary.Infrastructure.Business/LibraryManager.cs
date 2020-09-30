using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Interfaces.Managers;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Domain.Interfaces;

namespace TestTaskLibrary.Infrastructure.Business
{
    public class LibraryManager : ILibraryManager
    {
        IBooksRepository booksRepository;
        IBookStatusesRepository statusesRepository;

        public LibraryManager(IBooksRepository booksRepository, IBookStatusesRepository statusesRepository)
        {
            this.booksRepository = booksRepository;
            this.statusesRepository = statusesRepository;
        }

        public async Task<bool> IssueBookAsync(User user,int bookId)
        {
            Book book = await booksRepository.GetAll.Include(b => b.CurrentBookStatus).ThenInclude(s => s.User).FirstOrDefaultAsync(b => b.Id == bookId);
            if(book != null)
            {
                switch (book.CurrentBookStatus.Status)
                {
                    case Status.Free:
                        book.CurrentBookStatus.Status = Status.Issued;
                        book.CurrentBookStatus.User = user;
                        statusesRepository.Update(book.CurrentBookStatus);
                        break;
                    case Status.Booked:
                        if(book.CurrentBookStatus.User == user)
                        {
                            book.CurrentBookStatus.Status = Status.Issued;
                            book.CurrentBookStatus.User = user;
                            statusesRepository.Update(book.CurrentBookStatus);
                            return true;
                        }
                        return false;
                    case Status.Issued:
                        return false;
                    default:
                        return false;
                }
            }
            return false;
        }

        public async Task<bool> TakeAsync(int bookId)
        {
            Book book = await booksRepository.GetAll.Include(b => b.CurrentBookStatus).FirstOrDefaultAsync(b => b.Id == bookId);
            if (book != null)
            {
                book.CurrentBookStatus.Status = Status.Free;
                book.CurrentBookStatus.UserId = null;
                statusesRepository.Update(book.CurrentBookStatus);
                return true;
            }
            return false;
        }

        public async Task<bool> BookAsync(User user,int bookId)
        {
            Book book = await booksRepository.GetAll.Include(b => b.CurrentBookStatus).ThenInclude(s => s.User).FirstOrDefaultAsync(b => b.Id == bookId);
            if (book != null)
            {
                switch (book.CurrentBookStatus.Status)
                {
                    case Status.Free:
                        book.CurrentBookStatus.Status = Status.Booked;
                        book.CurrentBookStatus.User = user;
                        book.CurrentBookStatus.TimeOfStartBook = DateTime.Now;
                        book.CurrentBookStatus.TimeOfEndBook = DateTime.Now + TimeSpan.FromMinutes(2);
                        statusesRepository.Update(book.CurrentBookStatus);
                        break;
                    default:
                        return false;
                }
            }
            return false;
        }

        public async Task<bool> UnbookAsync(int bookId)
        {
            Book book = await booksRepository.GetAll.Include(b => b.CurrentBookStatus).FirstOrDefaultAsync(b => b.Id == bookId);
            if (book != null && book.CurrentBookStatus.Status == Status.Booked)
            {
                book.CurrentBookStatus.Status = Status.Free;
                book.CurrentBookStatus.UserId = null;
                book.CurrentBookStatus.TimeOfEndBook = DateTime.MinValue;
                book.CurrentBookStatus.TimeOfStartBook = DateTime.MinValue;
                statusesRepository.Update(book.CurrentBookStatus);
                return true;
            }
            return false;
        }
    }
}
