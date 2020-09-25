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

        public int BookId { get; set; }

        public Book Book { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        [Required]
        [Display(Name = "Статус")]
        public Status Status { get; set; }

        public DateTime TimeOfEndBook { get; set; }

        public DateTime TimeOfStartBook { get; set; }
    }
}
