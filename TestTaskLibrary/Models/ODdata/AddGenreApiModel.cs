using System.ComponentModel.DataAnnotations;

namespace TestTaskLibrary.Models.ODdata
{
    public class AddGenreApiModel
    {
        [Required(ErrorMessage = "Введите название жанра")]
        public string Name { get; set; }
    }
}
