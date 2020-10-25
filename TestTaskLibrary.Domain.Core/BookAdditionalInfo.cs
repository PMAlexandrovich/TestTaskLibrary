using System;
using System.Collections.Generic;
using System.Text;

namespace TestTaskLibrary.Domain.Core
{
    public class BookAdditionalInfo
    {
        public int Id { get; set; }

        public Book Book { get; set; }

        public ICollection<BookReview> Reviews { get; set; }
    }
}
