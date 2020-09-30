using System;
using System.Collections.Generic;
using System.Text;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Domain.Interfaces;

namespace TestTaskLibrary.Domain.Application.Interfaces
{
    public interface IGenreRepository : ICommonRepository<Genre>
    {
    }
}
