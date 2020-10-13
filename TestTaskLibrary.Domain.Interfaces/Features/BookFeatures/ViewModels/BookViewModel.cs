using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Domain.Application.Features.BookFeatures.ViewModels
{
    public class BookViewModel
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

        public string ImageName { get; set; }

        public StatusViewModel CurrentStatus { get; set; }

        public InfoViewModel Info { get; set; }
    }
}
