using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Infrastructure.Data.Configurations
{
    class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasOne(b => b.CurrentBookStatus).WithOne().HasForeignKey<Book>(b => b.CurrentBookStatusId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.BookAdditionalInfo).WithOne(s => s.Book).HasForeignKey<BookAdditionalInfo>(b => b.BookId);

            builder.HasOne(b => b.Author).WithMany(a => a.Books).HasForeignKey(b => b.AuthorId);

            builder.HasOne(b => b.Genre).WithMany(g => g.Books).HasForeignKey(b => b.GenreId);
        }
    }
}
