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
    public class BooksRepository : IBooksRepository
    {
        private readonly LibraryContext db;

        public BooksRepository(LibraryContext context)
        {
            db = context;
            GetAll = db.Books;
        }

        public IQueryable<Book> GetAll { get; }

        public async Task<Book> Get(int id)
        {
            return await db.Books.FindAsync(id);
        }

        public async Task<IEnumerable<Book>> GetList()
        {
            return await db.Books.ToListAsync();
        }

        public async Task Create(Book item)
        {
            db.Books.Add(item);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            Book book = await db.Books.FindAsync(id);
            if(book != null)
            {
                db.Books.Remove(book);
                await db.SaveChangesAsync();
            }
        }


        public async Task Update(Book item)
        {
            db.Books.Update(item);
            await db.SaveChangesAsync();
        }
    }
}
