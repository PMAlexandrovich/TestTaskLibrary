using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskLibrary.Models.Books
{
    public class AddBookViewModel
    {
        [Required(ErrorMessage = "Поле Автор является обязательным")]
        [Display(Name = "Автор")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Поле Название является обязательным")]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Поле Жанр является обязательным")]
        [Display(Name = "Жанр")]
        public string Genre { get; set; }
    }
}
