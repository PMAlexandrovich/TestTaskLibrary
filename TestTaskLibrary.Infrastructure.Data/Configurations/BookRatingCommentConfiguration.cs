using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Infrastructure.Data.Configurations
{
    class BookRatingCommentConfiguration : IEntityTypeConfiguration<BookRatingComment>
    {
        public void Configure(EntityTypeBuilder<BookRatingComment> builder)
        {
            builder.HasKey(c => c.BookAdditionalInfoId);

            builder.HasOne(c => c.User).WithMany(u => u.BookRaitingComments).HasForeignKey(c => c.UserId);

            builder.HasOne(c => c.BookAdditionalInfo).WithMany(i => i.RatingComments).HasForeignKey(c => c.BookAdditionalInfoId);
        }
    }
}
