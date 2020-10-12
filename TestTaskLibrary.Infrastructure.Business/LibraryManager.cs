using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Interfaces.Managers;
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Infrastructure.Business
{
    public class LibraryManager : ILibraryManager
    {
        private readonly IGenericRepository<Book> booksRepository;
        private readonly IGenericRepository<BookStatus> statusesRepository;

        public LibraryManager(IGenericRepository<Book> booksRepository, IGenericRepository<BookStatus> statusesRepository)
        {
            this.booksRepository = booksRepository;
            this.statusesRepository = statusesRepository;
        }

        public async Task<bool> IssueBookAsync(User user,int bookId)
        {
            Book book = await booksRepository.GetAll().Include(b => b.CurrentBookStatus).ThenInclude(s => s.User).FirstOrDefaultAsync(b => b.Id == bookId);
            if(book != null)
            {
                switch (book.CurrentBookStatus.Status)
                {
                    case Status.Free:
                        book.CurrentBookStatus = new BookStatus()
                        {
                            Book = book,
                            Status = Status.Issued,
                            User = user,
                            StatusSetAt = DateTime.Now
                        };
                        await booksRepository.UpdateAsync(book);
                        return true;
                    case Status.Booked:
                        if(book.CurrentBookStatus.User == user)
                        {
                            book.CurrentBookStatus = new BookStatus()
                            {
                                Book = book,
                                Status = Status.Issued,
                                User = user,
                                StatusSetAt = DateTime.Now
                            };
                            await booksRepository.UpdateAsync(book);
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
            Book book = await booksRepository.GetAll().Include(b => b.CurrentBookStatus).FirstOrDefaultAsync(b => b.Id == bookId);
            if (book != null)
            {
                book.CurrentBookStatus = new BookStatus()
                {
                    Book = book,
                    Status = Status.Free,
                    UserId = null,
                    StatusSetAt = DateTime.Now
                };
                await booksRepository.UpdateAsync(book);
                return true;
            }
            return false;
        }

        public async Task<bool> BookAsync(User user,int bookId)
        {
            Book book = await booksRepository.GetAll().Include(b => b.CurrentBookStatus).ThenInclude(s => s.User).FirstOrDefaultAsync(b => b.Id == bookId);
            if (book != null)
            {
                switch (book.CurrentBookStatus.Status)
                {
                    case Status.Free:
                        book.CurrentBookStatus = new BookStatus()
                        {
                            Book = book,
                            Status = Status.Booked,
                            User = user,
                            StatusSetAt = DateTime.Now
                        };
                        await booksRepository.UpdateAsync(book);
                        return true;
                    default:
                        return false;
                }
            }
            return false;
        }

        public async Task<bool> UnbookAsync(User user, int bookId)
        {
            Book book = await booksRepository.GetAll().Include(b => b.CurrentBookStatus).FirstOrDefaultAsync(b => b.Id == bookId);
            if (book != null && book.CurrentBookStatus.Status == Status.Booked && book.CurrentBookStatus.User == user)
            {
                book.CurrentBookStatus = new BookStatus()
                {
                    Book = book,
                    Status = Status.Free,
                    User = null,
                    StatusSetAt = DateTime.Now
                };
                await statusesRepository.UpdateAsync(book.CurrentBookStatus);
                return true;
            }
            return false;
        }
    }
}
