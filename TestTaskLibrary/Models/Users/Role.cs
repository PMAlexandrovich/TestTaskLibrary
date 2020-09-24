using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskLibrary.Models.Users
{
    public enum Role
    {
        [Display(Name = "Клиент")]
        Customer,

        [Display(Name = "Библиотекать")]
        Librarian
    }
}
