using System;
using System.Collections.Generic;
using System.Text;

namespace TestTaskLibrary.Domain.Core
{
    public class Genre
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public List<Book> Books { get; set; }
    }
}
