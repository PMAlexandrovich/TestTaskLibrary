using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskLibrary.Models.Genres
{
    public class GenreAddViewModel
    {
        [Display(Name = "Название жанра")]
        [Required(ErrorMessage = "Введите название жанра")]
        public string Name { get; set; }
    }
}
