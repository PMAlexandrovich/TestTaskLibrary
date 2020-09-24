using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestTaskLibrary.Domain.Core
{
    public class BookStatus
    {
        [Key]
        public int Id { get; set; }

        public User User { get; set; }

        public bool IsBooked { get; set; }

        public bool IsIssued { get; set; }
    }
}
