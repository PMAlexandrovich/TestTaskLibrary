using System;
using System.Collections.Generic;
using System.Text;

namespace TestTaskLibrary.Domain.Application.Features.BookFeatures.ViewModels
{
    public class BookWithStatusesViewModel
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }

        public StatusViewModel CurrentStatus { get; set; }

        public List<StatusViewModel> BookStatuses { get; set; }

        public InfoViewModel Info { get; set; }
    }
}
