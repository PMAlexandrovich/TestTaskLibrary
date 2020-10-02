using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskLibrary.Models.Reviews
{
    public class CreateReviewViewModel
    {
        public int BookId { get; set; }

        public int Rating { get; set; }

        public string Content { get; set; }
    }
}
