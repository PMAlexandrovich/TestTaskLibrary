using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestTaskLibrary.Domain.Application.Interfaces;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Infrastructure.Data.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        LibraryContext db;

        public GenreRepository(LibraryContext db)
        {
            this.db = db;
            GetAll = db.Genres;
        }

        public IQueryable<Genre> GetAll { get; }

        public Genre Get(int id)
        {
            return db.Genres.Find(id);
        }

        public IEnumerable<Genre> GetList()
        {
            return db.Genres.ToList();
        }

        public void Create(Genre item)
        {
            db.Genres.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Genre book = db.Genres.Find(id);
            if (book != null)
            {
                db.Genres.Remove(book);
                db.SaveChanges();
            }
        }

        public void Update(Genre item)
        {
            db.Genres.Update(item);
            db.SaveChanges();
        }
    }
}
