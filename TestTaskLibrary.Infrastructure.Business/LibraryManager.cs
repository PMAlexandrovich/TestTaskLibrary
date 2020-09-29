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
            Book book = booksRepository.GetAll.Include(b => b.CurrentBookStatus).ThenInclude(s => s.User).FirstOrDefault(b => b.Id == bookId);
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

        public bool Take(int bookId)
        {
            Book book = booksRepository.GetAll.Include(b => b.CurrentBookStatus).FirstOrDefault(b => b.Id == bookId);
            if (book != null)
            {
                book.CurrentBookStatus.Status = Status.Free;
                book.CurrentBookStatus.UserId = null;
                statusesRepository.Update(book.CurrentBookStatus);
                return true;
            }
            return false;
        }

        public bool Book(User user,int bookId)
        {
            Book book = booksRepository.GetAll.Include(b => b.CurrentBookStatus).ThenInclude(s => s.User).FirstOrDefault(b => b.Id == bookId);
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

        public bool Unbook(int bookId)
        {
            Book book = booksRepository.GetAll.Include(b => b.CurrentBookStatus).FirstOrDefault(b => b.Id == bookId);
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
