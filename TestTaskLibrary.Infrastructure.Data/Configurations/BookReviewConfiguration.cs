using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Infrastructure.Data.Configurations
{
    class BookReviewConfiguration : IEntityTypeConfiguration<BookReview>
    {
        public void Configure(EntityTypeBuilder<BookReview> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.User).WithMany(u => u.BookRaitingComments).HasForeignKey(c => c.UserId);

            //builder.HasOne(c => c.BookAdditionalInfo).WithMany(i => i.Reviews).HasForeignKey(c => c.BookAdditionalInfoId);
        }
    }
}
