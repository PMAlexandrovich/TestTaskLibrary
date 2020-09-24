using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Domain.Interfaces;

namespace TestTaskLibrary.Infrastructure.Business
{
    public class LibraryManager
    {
        IBooksRepository booksRepository;
        IBookStatusesRepository statusesRepository;

        public LibraryManager(IBooksRepository booksRepository, IBookStatusesRepository statusesRepository)
        {
            this.booksRepository = booksRepository;
            this.statusesRepository = statusesRepository;
        }

        public bool IssueBook(User user,int bookId)
        {
            Book book = booksRepository.Books.Include(b => b.BookStatus).ThenInclude(s => s.User).FirstOrDefault(b => b.Id == bookId);
            if(book != null)
            {
                switch (book.BookStatus.Status)
                {
                    case Status.Free:
                        book.BookStatus.Status = Status.Issued;
                        book.BookStatus.User = user;
                        statusesRepository.Update(book.BookStatus);
                        break;
                    case Status.Booked:
                        if(book.BookStatus.User == user)
                        {
                            book.BookStatus.Status = Status.Issued;
                            book.BookStatus.User = user;
                            statusesRepository.Update(book.BookStatus);
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

        public bool Take(int bookId)
        {
            Book book = booksRepository.Books.Include(b => b.BookStatus).FirstOrDefault(b => b.Id == bookId);
            if (book != null)
            {
                book.BookStatus.Status = Status.Free;
                book.BookStatus.User = null;
                statusesRepository.Update(book.BookStatus);
                return true;
            }
            return false;
        }

        public bool Book(User user,int bookId)
        {
            Book book = booksRepository.Books.Include(b => b.BookStatus).ThenInclude(s => s.User).FirstOrDefault(b => b.Id == bookId);
            if (book != null)
            {
                switch (book.BookStatus.Status)
                {
                    case Status.Free:
                        book.BookStatus.Status = Status.Booked;
                        book.BookStatus.User = user;
                        statusesRepository.Update(book.BookStatus);
                        break;
                    default:
                        return false;
                }
            }
            return false;
        }

        public bool Unbook(int bookId)
        {
            Book book = booksRepository.Books.Include(b => b.BookStatus).FirstOrDefault(b => b.Id == bookId);
            if (book != null && book.BookStatus.Status == Status.Booked)
            {
                book.BookStatus.Status = Status.Free;
                book.BookStatus.User = null;
                statusesRepository.Update(book.BookStatus);
                return true;
            }
            return false;
        }
    }
}
