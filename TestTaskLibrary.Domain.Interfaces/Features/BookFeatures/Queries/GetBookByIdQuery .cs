using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Core;
using TestTaskLibrary.Domain.Interfaces;

namespace TestTaskLibrary.Domain.Application.Features.BookFeatures.Queries
{
    public class GetBookByIdQuery : IRequest<Book>
    {
        public int Id { get; set; }

        public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book>
        {
            private IBooksRepository repository;

            public GetBookByIdQueryHandler(IBooksRepository repository)
            {
                this.repository = repository;
            }

            public async Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
            {
                var book = await repository.Books
                    .Include(b => b.BookAdditionalInfo)
                        .ThenInclude(i => i.Reviews)
                            .ThenInclude(r => r.User)
                    .Include(b => b.BookStatus).FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

                return book;
            }
        }
    }
}
