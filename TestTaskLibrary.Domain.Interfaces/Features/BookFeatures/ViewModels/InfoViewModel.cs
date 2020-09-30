using System;
using System.Collections.Generic;
using System.Text;

namespace TestTaskLibrary.Domain.Application.Features.BookFeatures.ViewModels
{
    public class InfoViewModel
    {
        public BookViewModel Book { get; set; }

        public List<ReviewViewModel> Reviews { get; set; }
    }
}
