using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Domain.Interfaces;

namespace TestTaskLibrary.Infrastructure.Data
{
    public class BooksRepository : IBooksRepository
    {
        private readonly LibraryContext db;

        public BooksRepository(LibraryContext context)
        {
            db = context;
            Books = db.Books;
        }

        public IQueryable<Book> Books { get; set; }

        public Book GetBook(int id)
        {
            return db.Books.Find(id);
        }

        public IEnumerable<Book> GetBookList()
        {
            return db.Books.ToList();
        }

        public void Create(Book item)
        {
            item.BookStatus = new BookStatus();
            db.Books.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Book book = db.Books.Find(id);
            if(book != null)
            {
                db.Books.Remove(book);
                db.SaveChanges();
            }
        }


        public void Update(Book item)
        {
            db.Books.Update(item);
            db.SaveChanges();
        }
    }
}
