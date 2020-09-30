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
    public class BookAdditionalInfosRepository : IBookAdditionalInfosRepository
    {
        LibraryContext db;

        public BookAdditionalInfosRepository(LibraryContext db)
        {
            this.db = db;
            GetAll = db.BookAdditionalInfos;
        }

        public IQueryable<BookAdditionalInfo> GetAll { get; }

        public async Task<BookAdditionalInfo> Get(int id)
        {
            return await db.BookAdditionalInfos.FindAsync(id);
        }

        public async Task<IEnumerable<BookAdditionalInfo>> GetList()
        {
            return await db.BookAdditionalInfos.ToListAsync();
        }

        public async Task Create(BookAdditionalInfo item)
        {
            db.BookAdditionalInfos.Add(item);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            BookAdditionalInfo book = await db.BookAdditionalInfos.FindAsync(id);
            if (book != null)
            {
                db.BookAdditionalInfos.Remove(book);
                await db.SaveChangesAsync();
            }
        }

        public async Task Update(BookAdditionalInfo item)
        {
            db.BookAdditionalInfos.Update(item);
            await db.SaveChangesAsync();
        }
    }
}
