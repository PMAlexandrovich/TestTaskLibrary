using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Models.Books
{
    public class IssueViewModel
    {
        [Required]
        [HiddenInput]
        public int BookId { get; set; }

        [Required]
        public string UserEmail { get; set; }
    }
}
