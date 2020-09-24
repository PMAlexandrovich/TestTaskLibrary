using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskLibrary.Models
{
    public class AddUserViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "ФИО")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
