using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskLibrary.Models.Books
{
    public class AddBookViewModel
    {
        [Required]
        [Display(Name = "Автор")]
        public string Author { get; set; }

        [Required]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Жанр")]
        public string Genre { get; set; }
    }
}
