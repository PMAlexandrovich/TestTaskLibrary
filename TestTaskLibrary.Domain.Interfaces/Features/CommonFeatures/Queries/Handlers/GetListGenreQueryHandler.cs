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
    public class GetListGenreQueryHandler : IRequestHandler<GetListQuery<Genre, GenreViewModel>, List<GenreViewModel>>
    {
        private readonly IGenericRepository<Genre> repository;
        private readonly IMapper mapper;

        public GetListGenreQueryHandler(IGenericRepository<Genre> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<GenreViewModel>> Handle(GetListQuery<Genre, GenreViewModel> request, CancellationToken cancellationToken)
        {
            var items = repository.GetAll();

            if(request.PageIndex < 1)
                request.PageIndex = 1;
            if(request.PageSize < 1)
                request.PageSize = 1;

            items = items.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);

            return mapper.Map<List<GenreViewModel>>(await items.ToListAsync(cancellationToken));
        }
    }
}
