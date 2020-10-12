using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Domain.Application.Features.AuthorFeatures.Commands
{
    public class CreateAuthorCommand : IRequest<int>
    {
        public string FullName { get; set; }

        public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, int>
        {
            private readonly IGenericRepository<Author> repository;

            public CreateAuthorCommandHandler(IGenericRepository<Author> repository)
            {
                this.repository = repository;
            }

            public async Task<int> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
            {
                var newAuthor = new Author(request.FullName);
                await repository.AddAsync(newAuthor);
                return newAuthor.Id;
            }
        }
    }
}
