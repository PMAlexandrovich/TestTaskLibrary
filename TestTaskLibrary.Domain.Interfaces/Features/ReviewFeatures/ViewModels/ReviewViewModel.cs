using System;
using System.Collections.Generic;
using System.Text;

namespace TestTaskLibrary.Domain.Application.Features.BookFeatures.ViewModels
{
    public class ReviewViewModel
    {
        public int Id { get; set; }

        public InfoViewModel BookAdditionalInfo { get; set; }

        public UserViewModel User { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }
    }
}
