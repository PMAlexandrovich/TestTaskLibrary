using System;
using System.Collections.Generic;
using System.Text;

namespace TestTaskLibrary.Domain.Core
{
    public class BookAdditionalInfo
    {
        public int BookId { get; set; }

        public Book Book { get; set; }

        public List<BookReview> Reviews { get; set; }
    }
}
