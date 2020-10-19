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

namespace TestTaskLibrary.Domain.Application.Features.CommonFeatures.Queries
{
    public class GetListQuery<TEntity,TViewModel> : IRequest<List<TViewModel>> where TEntity : class
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string SearchField { get; set; }

        public string Search { get; set; }

        public string SortField { get; set; }

        public class GetListQueryCommonHandler : IRequestHandler<GetListQuery<TEntity, TViewModel>, List<TViewModel>> 
        {
            protected readonly IGenericRepository<TEntity> repository;
            protected readonly IMapper mapper;

            public GetListQueryCommonHandler(IGenericRepository<TEntity> repository, IMapper mapper)
            {
                this.repository = repository;
                this.mapper = mapper;
            }

            public async Task<List<TViewModel>> Handle(GetListQuery<TEntity, TViewModel> request, CancellationToken cancellationToken)
            {
                var items = repository.GetAll().ProjectTo<TViewModel>(mapper.ConfigurationProvider);

                if (request.PageIndex < 1)
                    request.PageIndex = 1;
                if (request.PageSize < 1)
                    request.PageSize = 1;



                if (!string.IsNullOrEmpty(request.SearchField) && !string.IsNullOrEmpty(request.Search))
                {
                    items = items.Search(request.SearchField, request.Search);
                }

                if (!string.IsNullOrEmpty(request.SortField))
                {
                    items = items.OrderBy(request.SortField);
                }

                items = items.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);

                return await items.ToListAsync(cancellationToken);
            }
        }
    }
}
