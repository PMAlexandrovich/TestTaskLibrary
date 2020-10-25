using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskLibrary.Models.ODdata
{
    public class AddAuthorApiModel
    {
        [Required(ErrorMessage = "Введите имя автора")]
        public string Name { get; set; }
    }
}
