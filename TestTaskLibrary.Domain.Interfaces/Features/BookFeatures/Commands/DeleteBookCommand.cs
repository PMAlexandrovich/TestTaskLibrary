using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Domain.Application.Features.BookFeatures.Commands
{
    public class DeleteBookCommand : IRequest<int>
    {
        public int Id { get; set; }

        public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, int>
        {
            private readonly IGenericRepository<Book> repository;

            public DeleteBookCommandHandler(IGenericRepository<Book> repository)
            {
                this.repository = repository;
            }

            public async Task<int> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
            {
                var book = await repository.GetAll().FirstOrDefaultAsync(b => b.Id == request.Id);
                await repository.RemoveAsync(book);
                return request.Id;
            }
        }
    }
}
