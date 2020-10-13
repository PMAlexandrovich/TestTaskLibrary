using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Features.BookFeatures.ViewModels;
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Domain.Application.Features.CommonFeatures.Queries.Handlers
{
    public class GetListUserQueryHandler : IRequestHandler<GetListQuery<User, UserViewModel>, List<UserViewModel>>
    {
        private readonly IGenericRepository<User> repository;
        private readonly IMapper mapper;

        public GetListUserQueryHandler(IGenericRepository<User> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<UserViewModel>> Handle(GetListQuery<User, UserViewModel> request, CancellationToken cancellationToken)
        {
            var items = repository.GetAll();

            if (request.PageIndex < 1)
                request.PageIndex = 1;
            if (request.PageSize < 1)
                request.PageSize = 1;

            items = items.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);

            return mapper.Map<List<UserViewModel>>(await items.ToListAsync(cancellationToken));
        }
    }
}
