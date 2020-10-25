using System.ComponentModel.DataAnnotations;

namespace TestTaskLibrary.Models.ODdata
{
    public class AddUserApiModel
    {
        [Required(ErrorMessage = "Поле Email является обязательным")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле ФИО является обязательным")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Поле Пароль является обязательным")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле Роль является обязательным")]
        public string Role { get; set; }
    }
}
