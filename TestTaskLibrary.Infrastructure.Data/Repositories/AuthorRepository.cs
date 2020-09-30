using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Interfaces;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Infrastructure.Data.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryContext db;

        public AuthorRepository(LibraryContext db)
        {
            this.db = db;
            GetAll = db.Authors;
        }

        public IQueryable<Author> GetAll { get; }

        public async Task<Author> Get(int id)
        {
            return await db.Authors.FindAsync(id);
        }

        public async Task<IEnumerable<Author>> GetList()
        {
            return await db.Authors.ToListAsync();
        }

        public async Task Create(Author item)
        {
            db.Authors.Add(item);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            Author book = await db.Authors.FindAsync(id);
            if (book != null)
            {
                db.Authors.Remove(book);
                await db.SaveChangesAsync();
            }
        }

        public async Task Update(Author item)
        {
            db.Authors.Update(item);
            await db.SaveChangesAsync();
        }
    }
}
