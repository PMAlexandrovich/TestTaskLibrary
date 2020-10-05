using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Domain.Interfaces;

namespace TestTaskLibrary.Infrastructure.Data.Repositories
{
    public class BookStatusesRepository : IBookStatusesRepository
    {
        private readonly LibraryContext db;

        public BookStatusesRepository(LibraryContext context)
        {
            db = context;
            GetAll = db.BookStatuses;
        }

        public IQueryable<BookStatus> GetAll { get; }

        public async Task<BookStatus> Get(int id)
        {
            return await db.BookStatuses.FindAsync(id);
        }

        public async Task<IEnumerable<BookStatus>> GetList()
        {
            return await db.BookStatuses.ToListAsync();
        }

        public async Task Create(BookStatus item)
        {
            item.StatusSetAt = DateTime.Now;
            db.BookStatuses.Add(item);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            BookStatus book = await db.BookStatuses.FindAsync(id);
            if (book != null)
            {
                db.BookStatuses.Remove(book);
                await db.SaveChangesAsync();
            }
        }


        public async Task Update(BookStatus item)
        {
            db.Update(item);
            await db.SaveChangesAsync();
        }
    }
}
