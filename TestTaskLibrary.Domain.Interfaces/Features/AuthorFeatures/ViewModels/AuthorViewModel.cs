using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestTaskLibrary.Domain.Application.Features.AuthorFeatures.ViewModels
{
    public class AuthorViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Имя автора")]
        public string FullName { get; set; }
    }
}
