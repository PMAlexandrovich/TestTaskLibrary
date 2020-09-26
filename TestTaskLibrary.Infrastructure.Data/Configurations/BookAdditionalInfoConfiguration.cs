﻿using Microsoft.EntityFrameworkCore;
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
            builder.HasMany(i => i.RatingComments).WithOne(c => c.BookAdditionalInfo).HasForeignKey(c => c.BookAdditionalInfoId);

        }
    }
}
