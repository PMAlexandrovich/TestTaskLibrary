using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Domain.Interfaces;

namespace TestTaskLibrary.Infrastructure.Data
{
    public class BookStatusesRepository: IBookStatusesRepository
    {
        private readonly LibraryContext db;

        public BookStatusesRepository(LibraryContext context)
        {
            db = context;
            Statuses = db.BookStatuses;
        }

        public IQueryable<BookStatus> Statuses { get; set; }

        public BookStatus GetBookStatus(int id)
        {
            return db.BookStatuses.Find(id);
        }

        public IEnumerable<BookStatus> GetBookStatusList()
        {
            return db.BookStatuses.ToList();
        }

        public void Create(BookStatus item)
        {
            db.BookStatuses.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            BookStatus book = db.BookStatuses.Find(id);
            if (book != null)
            {
                db.BookStatuses.Remove(book);
                db.SaveChanges();
            }
        }


        public void Update(BookStatus item)
        {
            db.SaveChanges();
        }
    }
}
