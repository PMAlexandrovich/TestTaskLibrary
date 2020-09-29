using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestTaskLibrary.Domain.Core
{
    public class User : IdentityUser<int>, IUser<int>
    {
        [Required]
        public string FullName { get; set; }

        public List<BookStatus> BookStatuses { get; set; }

        public List<BookReview> BookRaitingComments { get; set; }
    }
}
