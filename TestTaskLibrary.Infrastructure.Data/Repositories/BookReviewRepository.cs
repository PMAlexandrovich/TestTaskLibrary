using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            GetAll = db.BookReview;
        }

        public IQueryable<BookReview> GetAll { get; }

        public async Task<BookReview> Get(int id)
        {
            return await db.BookReview.FindAsync(id);
        }

        public async Task<IEnumerable<BookReview>> GetList()
        {
            return await db.BookReview.ToListAsync();
        }

        public async Task Create(BookReview item)
        {
            db.BookReview.Add(item);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            BookReview book = await db.BookReview.FindAsync(id);
            if (book != null)
            {
                db.BookReview.Remove(book);
                await db.SaveChangesAsync();
            }
        }

        public async Task Update(BookReview item)
        {
            db.BookReview.Update(item);
            await db.SaveChangesAsync();
        }
    }
}
