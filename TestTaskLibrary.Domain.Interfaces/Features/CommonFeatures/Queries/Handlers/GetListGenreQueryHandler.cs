using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Features.GenreFeatures.ViewModels;
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Domain.Application.Features.CommonFeatures.Queries.Handlers
{
    public class GetListGenreQueryHandler : GetListQuery<Genre, GenreViewModel>.GetListQueryCommonHandler
    {
        public GetListGenreQueryHandler(IGenericRepository<Genre> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
