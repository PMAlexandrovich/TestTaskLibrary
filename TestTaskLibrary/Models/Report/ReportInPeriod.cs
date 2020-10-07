using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskLibrary.Models.Report
{
    public class ReportInPeriod
    {
        [Required]
        [Display(Name = "Начало периода")]
        public DateTime Start { get; set; }

        [Required]
        [Display(Name = "Конец периода")]
        public DateTime End { get; set; }
    }
}
