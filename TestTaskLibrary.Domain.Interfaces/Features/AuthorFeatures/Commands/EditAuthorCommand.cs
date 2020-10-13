using MediatR;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Domain.Application.Features.AuthorFeatures.Commands
{
    public class EditAuthorCommand : IRequest<int>
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public class EditAuthorCommandHandler : IRequestHandler<EditAuthorCommand, int>
        {
            private readonly IGenericRepository<Author> repository;

            public EditAuthorCommandHandler(IGenericRepository<Author> repository)
            {
                this.repository = repository;
            }

            public async Task<int> Handle(EditAuthorCommand request, CancellationToken cancellationToken)
            {
                var author = await repository.GetByIdAsync(request.Id);
                if(author != null)
                {
                    author.FullName = request.FullName;
                    await repository.UpdateAsync(author);
                    return request.Id;
                }
                return default;
            }
        }
    }
}
