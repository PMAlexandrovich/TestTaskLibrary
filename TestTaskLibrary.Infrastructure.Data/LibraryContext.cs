using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TestTaskLibrary.Domain.Core;
using System;
using Microsoft.EntityFrameworkCore;
using TestTaskLibrary.Infrastructure.Data.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TestTaskLibrary.Infrastructure.Data
{
    public class LibraryContext:IdentityDbContext<User, CustomRole, int>
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<BookStatus> BookStatuses { get; set; }

        public DbSet<BookAdditionalInfo> BookAdditionalInfos { get; set; }

        public DbSet<BookReview> BookReview { get; set; }

        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new BookStatusConfiguration());
            modelBuilder.ApplyConfiguration(new BookAdditionalInfoConfiguration());
            modelBuilder.ApplyConfiguration(new BookReviewConfiguration());
        }

    }
}
