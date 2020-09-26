using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Domain.Interfaces
{
    interface IBookRatingCommentsRepository : ICommonRepository<BookRatingComment>
    {
        public IQueryable<BookRatingComment> BookAdditionalInfos { get; set; }
    }
}
