using MediatR;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Domain.Application.Features.GenreFeatures.Commands
{
    public class CreateGenreCommand : IRequest<int>
    {
        public string Name { get; set; }

        public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, int>
        {
            private readonly IGenericRepository<Genre> repository;

            public CreateGenreCommandHandler(IGenericRepository<Genre> repository)
            {
                this.repository = repository;
            }

            public async Task<int> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
            {
                var newGenre = new Genre(request.Name);
                await repository.AddAsync(newGenre);
                return newGenre.Id;
            }
        }
    }
}
