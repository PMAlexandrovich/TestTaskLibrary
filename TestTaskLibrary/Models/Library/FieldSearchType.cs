using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskLibrary.Models.Library
{
    public enum FieldSearchType
    {
        [Display(Name = "Автор")]
        Author,

        [Display(Name = "Название")]
        Title,

        [Display(Name = "Жанр")]
        Genre
    }
}
