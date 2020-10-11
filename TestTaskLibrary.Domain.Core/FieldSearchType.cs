using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestTaskLibrary.Domain.Core
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
