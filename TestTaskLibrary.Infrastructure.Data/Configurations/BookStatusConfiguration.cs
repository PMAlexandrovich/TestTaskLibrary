﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Infrastructure.Data.Configurations
{
    class BookStatusConfiguration : IEntityTypeConfiguration<BookStatus>
    {
        public void Configure(EntityTypeBuilder<BookStatus> builder)
        {
            builder.HasOne(s => s.User).WithMany(u => u.BookStatuses).HasForeignKey(s => s.UserId);

            builder.HasOne(s => s.Book).WithMany(b => b.BookStatuses).HasForeignKey(s => s.BookId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
