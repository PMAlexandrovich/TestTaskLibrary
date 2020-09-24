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
                if(book.BookStatus == null)
                {
                    book.BookStatus = new BookStatus() { User = user, IsBooked = false, IsIssued = true };
                    booksRepository.Update(book);
                    return true;
                }
                else
                {
                    if(book.BookStatus.IsBooked && book.BookStatus.User == user)
                    {
                        book.BookStatus.IsBooked = false;
                        book.BookStatus.IsIssued = true;
                        statusesRepository.Update(book.BookStatus);
                        return true;
                    }
                }
                
            }
            return false;
        }

        public bool Take(int bookId)
        {
            Book book = booksRepository.Books.Include(b => b.BookStatus).FirstOrDefault(b => b.Id == bookId);
            if (book != null && book.BookStatus != null)
            {
                statusesRepository.Delete(book.BookStatus.Id);
                return true;
            }
            return false;
        }

        public bool Book(User user,int bookId)
        {
            Book book = booksRepository.Books.Include(b => b.BookStatus).ThenInclude(s => s.User).FirstOrDefault(b => b.Id == bookId);
            if (book != null)
            {
                if (book.BookStatus == null)
                {
                    book.BookStatus = new BookStatus() { User = user, IsBooked = true, IsIssued = false };
                    booksRepository.Update(book);
                    return true;
                }
            }
            return false;
        }

        public bool Unbook(int bookId)
        {
            Book book = booksRepository.Books.Include(b => b.BookStatus).FirstOrDefault(b => b.Id == bookId);
            if (book != null && book.BookStatus != null)
            {
                statusesRepository.Delete(book.BookStatus.Id);
                return true;
            }
            return false;
        }
    }
}
