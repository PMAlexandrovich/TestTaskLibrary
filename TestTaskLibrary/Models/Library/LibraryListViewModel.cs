using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Features.BookFeatures.ViewModels;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Models.Library
{
    public class LibraryListViewModel
    {
        public List<BookViewModel> Books { get; set; }

        public string Search { get; set; }

        public FieldSearchType SearchType { get; set; }

        public int PageIndex { get; set; }
    }
}
