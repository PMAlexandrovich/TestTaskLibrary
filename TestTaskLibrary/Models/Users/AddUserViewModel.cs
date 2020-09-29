using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Models.Users;

namespace TestTaskLibrary.Models
{
    public class AddUserViewModel
    {
        [Required(ErrorMessage = "Поле Email является обязательным")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле ФИО является обязательным")]
        [Display(Name = "ФИО")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Поле Пароль является обязательным")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле Роль является обязательным")]
        [Display(Name = "Роль")]
        public Users.Role Role { get; set; }
    }
}
