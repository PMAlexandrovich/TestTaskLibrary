using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TestTaskLibrary.Domain.Core;
using System;
using Microsoft.EntityFrameworkCore;

namespace TestTaskLibrary.Infrastructure.Data
{
    public class LibraryContext:IdentityDbContext<User>
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<BookStatus> BookStatuses { get; set; }

        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BookStatus>().Property(s => s.Status).HasConversion(v => v.ToString(), v => (Status)Enum.Parse(typeof(Status), v));
        }

    }
}
