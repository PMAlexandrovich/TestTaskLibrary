using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Models.Library
{
    public class BookItemViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Автор")]
        public string Author { get; set; }

        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Жанр")]
        public string Genre { get; set; }

        [Display(Name = "Статус")]
        public Status Status { get; set; }

        public User User { get; set; }
    }
}
