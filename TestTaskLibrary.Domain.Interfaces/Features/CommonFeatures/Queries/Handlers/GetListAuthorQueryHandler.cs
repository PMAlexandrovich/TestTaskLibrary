using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Extensions.Queryable;
using TestTaskLibrary.Domain.Application.Features.AuthorFeatures.ViewModels;
using TestTaskLibrary.Domain.Application.Features.BookFeatures.ViewModels;
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Domain.Application.Features.CommonFeatures.Queries.Handlers
{
    public class GetListAuthorQueryHandler : GetListQuery<Author, AuthorViewModel>.GetListQueryCommonHandler
    {
        public GetListAuthorQueryHandler(IGenericRepository<Author> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
