using System;
using System.ComponentModel.DataAnnotations;

namespace TestTaskLibrary.Domain.Core
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public BookStatus BookStatus { get; set; }
    }
}
