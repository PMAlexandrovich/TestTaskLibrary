using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestTaskLibrary.Domain.Core
{
    public class User:IdentityUser
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
