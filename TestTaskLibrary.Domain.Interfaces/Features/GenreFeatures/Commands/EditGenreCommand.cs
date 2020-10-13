using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Domain.Application.Features.GenreFeatures.Commands
{
    public class EditGenreCommand : IRequest<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public class EditGenreCommandHandler : IRequestHandler<EditGenreCommand, int>
        {
            private readonly IGenericRepository<Genre> repository;

            public EditGenreCommandHandler(IGenericRepository<Genre> repository)
            {
                this.repository = repository;
            }

            public async Task<int> Handle(EditGenreCommand request, CancellationToken cancellationToken)
            {
                var genre = await repository.GetByIdAsync(request.Id);
                if (genre != null)
                {
                    genre.Name = request.Name;
                    await repository.UpdateAsync(genre);
                    return request.Id;
                }
                return default;
            }
        }
    }
}
