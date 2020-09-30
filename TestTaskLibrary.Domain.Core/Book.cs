using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestTaskLibrary.Domain.Core
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        public int AuthorId { get; set; }

        [Required]
        public Author Author { get; set; }

        [Required]
        public string Title { get; set; }

        public int GenreId { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [Required]
        public BookStatus CurrentBookStatus { get; set; }

        //public List<BookStatus> BookStatuses { get; set; }

        public BookAdditionalInfo BookAdditionalInfo { get; set; }
    }
}
