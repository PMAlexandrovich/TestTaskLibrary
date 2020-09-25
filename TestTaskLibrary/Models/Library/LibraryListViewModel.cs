using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskLibrary.Models.Library
{
    public class LibraryListViewModel
    {
        public IEnumerable<BookItemViewModel> Books { get; set; }

        public string Search { get; set; }

        public FieldSearchType SearchType { get; set; }
    }
}
