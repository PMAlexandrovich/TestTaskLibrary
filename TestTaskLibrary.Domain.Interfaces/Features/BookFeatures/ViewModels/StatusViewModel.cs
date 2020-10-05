using System;
using System.Collections.Generic;
using System.Text;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Domain.Application.Features.BookFeatures.ViewModels
{
    public class StatusViewModel
    {
        public Status Status { get; set; }

        public UserViewModel User { get; set; }

        public DateTime? StatusSetAt { get; set; }
    }
}
