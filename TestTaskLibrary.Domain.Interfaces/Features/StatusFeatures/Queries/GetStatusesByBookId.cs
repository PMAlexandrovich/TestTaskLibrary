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
using TestTaskLibrary.Domain.Interfaces;

namespace TestTaskLibrary.Domain.Application.Features.StatusFeatures.Queries
{
    public class GetStatusesByBookId : IRequest<List<StatusViewModel>>
    {
        public int BookId { get; set; }

        public class GetStatusesByBookIdHandler : IRequestHandler<GetStatusesByBookId, List<StatusViewModel>>
        {
            private readonly IBookStatusesRepository statusesRepository;
            private readonly IMapper mapper;

            public GetStatusesByBookIdHandler(IBookStatusesRepository statusesRepository, IMapper mapper)
            {
                this.statusesRepository = statusesRepository;
                this.mapper = mapper;
            }

            public async Task<List<StatusViewModel>> Handle(GetStatusesByBookId request, CancellationToken cancellationToken)
            {
                var statuses = await statusesRepository.GetAll
                    .Include(s => s.Book)
                    .Include(s => s.User)
                    .Where(s => s.BookId == request.BookId).ToListAsync();

                return mapper.Map<List<StatusViewModel>>(statuses);
            }
        }
    }
}
