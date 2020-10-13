using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestTaskLibrary.Domain.Application.Features.BookFeatures.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Display(Name = "Полное имя")]
        public string FullName { get; set; }
    }
}
