using System;
using System.ComponentModel.DataAnnotations;

namespace TestTaskLibrary.Domain.Core
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Автор")]
        public string Author { get; set; }

        [Required]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Жанр")]
        public string Genre { get; set; }

        [Required]
        public BookStatus BookStatus { get; set; }

        public BookAdditionalInfo BookAdditionalInfo { get; set; }
    }
}
