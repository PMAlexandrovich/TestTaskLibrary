﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskLibrary.Views.Authors
{
    public class AuthorEditViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите имя автора")]
        [Display(Name = "Имя автора")]
        public string FullName { get; set; }
    }
}
