using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Domain.Interfaces;

namespace TestTaskLibrary.Infrastructure.Data.Repositories
{
    public class BookRatingCommentsRepository : IBookRatingCommentsRepository
    {
        LibraryContext db;

        public BookRatingCommentsRepository(LibraryContext db)
        {
            this.db = db;
            BookRatingComments = db.BookRatingComments;
        }

        public IQueryable<BookRatingComment> BookRatingComments { get; set; }

        public BookRatingComment Get(int id)
        {
            return db.BookRatingComments.Find(id);
        }

        public IEnumerable<BookRatingComment> GetList()
        {
            return db.BookRatingComments.ToList();
        }

        public void Create(BookRatingComment item)
        {
            db.BookRatingComments.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            BookRatingComment book = db.BookRatingComments.Find(id);
            if (book != null)
            {
                db.BookRatingComments.Remove(book);
                db.SaveChanges();
            }
        }

        public void Update(BookRatingComment item)
        {
            db.BookRatingComments.Update(item);
            db.SaveChanges();
        }
    }
}
