using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskLibrary.Models
{
    public class LogInViewModel
    {
        [Required(ErrorMessage = "Поле Логин является обязательным")]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Поле Пароль является обязательным")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить ?")]
        public bool RememberMe { get; set; }
    }
}
