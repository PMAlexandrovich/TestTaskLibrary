using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Infrastructure.Data.Configurations
{
    class BookAdditionalInfoConfiguration : IEntityTypeConfiguration<BookAdditionalInfo>
    {
        public void Configure(EntityTypeBuilder<BookAdditionalInfo> builder)
        {
            builder.HasKey(i => i.BookId);
            builder.HasMany(i => i.Reviews).WithOne(c => c.BookAdditionalInfo).HasForeignKey(c => c.BookAdditionalInfoId);

        }
    }
}
