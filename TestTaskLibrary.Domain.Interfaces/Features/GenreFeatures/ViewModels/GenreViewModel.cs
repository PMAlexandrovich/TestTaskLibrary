using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestTaskLibrary.Domain.Application.Features.GenreFeatures.ViewModels
{
    public class GenreViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Жанр")]
        public string Name { get; set; }
    }
}
