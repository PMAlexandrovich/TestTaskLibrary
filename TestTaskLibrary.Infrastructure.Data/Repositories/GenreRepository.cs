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
    public class GenreRepository : IGenreRepository
    {
        private readonly LibraryContext db;

        public GenreRepository(LibraryContext db)
        {
            this.db = db;
            GetAll = db.Genres;
        }

        public IQueryable<Genre> GetAll { get; }

        public async Task<Genre> Get(int id)
        {
            return await db.Genres.FindAsync(id);
        }

        public async Task<IEnumerable<Genre>> GetList()
        {
            return await db.Genres.ToListAsync();
        }

        public async Task Create(Genre item)
        {
            db.Genres.Add(item);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            Genre book = await db.Genres.FindAsync(id);
            if (book != null)
            {
                db.Genres.Remove(book);
                await db.SaveChangesAsync();
            }
        }

        public async Task Update(Genre item)
        {
            db.Genres.Update(item);
            await db.SaveChangesAsync();
        }
    }
}
