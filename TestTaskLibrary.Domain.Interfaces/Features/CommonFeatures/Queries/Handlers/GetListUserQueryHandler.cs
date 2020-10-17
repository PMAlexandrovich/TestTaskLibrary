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
    public class GetListUserQueryHandler : GetListQuery<User, UserViewModel>.GetListQueryCommonHandler
    {
        public GetListUserQueryHandler(IGenericRepository<User> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
