using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Domain.Interfaces
{
    interface IBookAdditionalInfosRepository : ICommonRepository<BookAdditionalInfo>
    {
        public IQueryable<BookAdditionalInfo> BookAdditionalInfos { get; set; }
    }
}
