using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Features.AuthorFeatures.ViewModels;
using TestTaskLibrary.Domain.Application.Features.BookFeatures.ViewModels;
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Domain.Application.Features.CommonFeatures.Queries.Handlers
{
    public class GetListAuthorQueryHandler : IRequestHandler<GetListQuery<Author, AuthorViewModel>, List<AuthorViewModel>>
    {
        private readonly IGenericRepository<Author> repository;
        private readonly IMapper mapper;

        public GetListAuthorQueryHandler(IGenericRepository<Author> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<List<AuthorViewModel>> Handle(GetListQuery<Author, AuthorViewModel> request, CancellationToken cancellationToken)
        {
            var items = repository.GetAll();

            if (request.PageIndex < 1)
                request.PageIndex = 1;
            if (request.PageSize < 1)
                request.PageSize = 1;

            items = items.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);

            return mapper.Map<List<AuthorViewModel>>(await items.ToListAsync(cancellationToken));
        }
    }
}
