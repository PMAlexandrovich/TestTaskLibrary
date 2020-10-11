using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Features.BookFeatures.ViewModels;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Domain.Interfaces;

namespace TestTaskLibrary.Domain.Application.Features.BookFeatures.Queries
{
    public class GetBookByIdQuery : IRequest<BookViewModel>
    {
        public int Id { get; set; }

        public GetBookByIdQuery() { }
        public GetBookByIdQuery(int id)
        {
            Id = id;
        }

        public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookViewModel>
        {
            private readonly IBooksRepository repository;
            private readonly IMapper mapper;

            public GetBookByIdQueryHandler(IBooksRepository repository, IMapper mapper)
            {
                this.repository = repository;
                this.mapper = mapper;
            }

            public async Task<BookViewModel> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
            {
                var book = await repository.GetAll
                    .Include(b => b.Author)
                    .Include(b => b.BookAdditionalInfo)
                        .ThenInclude(i => i.Reviews)
                            .ThenInclude(r => r.User)
                    .Include(b => b.CurrentBookStatus)
                        .ThenInclude(s => s.User)
                    .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

                return mapper.Map<BookViewModel>(book);
            }
        }
    }
}
