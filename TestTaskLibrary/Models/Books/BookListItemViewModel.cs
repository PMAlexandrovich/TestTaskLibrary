using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskLibrary.Models.Books
{
    public class BookListItemViewModel
    {
        
        public int Id { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }

        public string Status { get; set; }
    }
}
