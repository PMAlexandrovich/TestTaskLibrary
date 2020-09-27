using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Domain.Interfaces;

namespace TestTaskLibrary.Infrastructure.Data.Repositories
{
    public class BookReviewRepository : IBookReviewsRepository
    {
        LibraryContext db;

        public BookReviewRepository(LibraryContext db)
        {
            this.db = db;
            BookReviews = db.BookReview;
        }

        public IQueryable<BookReview> BookReviews { get; set; }

        public BookReview Get(int id)
        {
            return db.BookReview.Find(id);
        }

        public IEnumerable<BookReview> GetList()
        {
            return db.BookReview.ToList();
        }

        public void Create(BookReview item)
        {
            db.BookReview.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            BookReview book = db.BookReview.Find(id);
            if (book != null)
            {
                db.BookReview.Remove(book);
                db.SaveChanges();
            }
        }

        public void Update(BookReview item)
        {
            db.BookReview.Update(item);
            db.SaveChanges();
        }
    }
}
