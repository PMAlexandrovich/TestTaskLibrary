using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            BookAdditionalInfos = db.BookAdditionalInfos;
        }

        public IQueryable<BookAdditionalInfo> BookAdditionalInfos { get; set; }

        public BookAdditionalInfo Get(int id)
        {
            return db.BookAdditionalInfos.Find(id);
        }

        public IEnumerable<BookAdditionalInfo> GetList()
        {
            return db.BookAdditionalInfos.ToList();
        }

        public void Create(BookAdditionalInfo item)
        {
            db.BookAdditionalInfos.Add(item);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            BookAdditionalInfo book = db.BookAdditionalInfos.Find(id);
            if (book != null)
            {
                db.BookAdditionalInfos.Remove(book);
                db.SaveChanges();
            }
        }

        public void Update(BookAdditionalInfo item)
        {
            db.BookAdditionalInfos.Update(item);
            db.SaveChanges();
        }
    }
}
