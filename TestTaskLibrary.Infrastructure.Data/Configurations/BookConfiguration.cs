﻿using Microsoft.EntityFrameworkCore;
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
            builder.HasOne(b => b.BookStatus).WithOne(s => s.Book).HasForeignKey<BookStatus>(b => b.BookId);
        }
    }
}