﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestTaskLibrary.Domain.Core
{
    public class BookRatingComment
    {
        public int Id { get; set; }

        public int BookAdditionalInfoId { get; set; }
        public BookAdditionalInfo BookAdditionalInfo { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }
    }
}
