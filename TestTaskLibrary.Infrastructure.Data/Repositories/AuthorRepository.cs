using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestTaskLibrary.Domain.Application.Interfaces;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Infrastructure.Data.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        LibraryContext db;

        public AuthorRepository(LibraryContext db)
        {
            this.db = db;
            GetAll = db.Authors;
        }

        public IQueryable<Author> GetAll { get; }

        public Author Get(int id)
        {
            return db.Authors.Find(id);
        }

        public IEnumerable<Author> GetList()
        {
            return db.Authors.ToList();
        }

        public void Create(Author item)
        {
            db.Authors.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Author book = db.Authors.Find(id);
            if (book != null)
            {
                db.Authors.Remove(book);
                db.SaveChanges();
            }
        }

        public void Update(Author item)
        {
            db.Authors.Update(item);
            db.SaveChanges();
        }
    }
}
