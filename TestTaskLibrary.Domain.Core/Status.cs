using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestTaskLibrary.Domain.Core
{
    public enum Status
    {
        [Display(Name = "Доступна")]
        Free,

        [Display(Name = "Забронирована")]
        Booked,

        [Display(Name = "Выдана")]
        Issued
    }
}
