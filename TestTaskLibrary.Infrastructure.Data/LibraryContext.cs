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

    }
}
