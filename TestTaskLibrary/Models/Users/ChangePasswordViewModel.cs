﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskLibrary.Models
{
    public class ChangePasswordViewModel
    {
        [HiddenInput]
        public string Id { get; set; }

        [Display(Name = "Новый пароль")]
        public string Password { get; set; }
    }
}