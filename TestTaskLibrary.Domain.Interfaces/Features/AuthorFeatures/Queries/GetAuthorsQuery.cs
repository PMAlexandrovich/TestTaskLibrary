using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Interfaces;
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Domain.Application.Features.AuthorFeatures.Queries
{
    public class GetAuthorsQuery : IRequest<List<Author>>
    {
        public int? Id { get; set; }

        public string FullName { get; set; }

        public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, List<Author>>
        {
            private readonly IGenericRepository<Author> repository;

            public GetAuthorsQueryHandler(IGenericRepository<Author> repository)
            {
                this.repository = repository;
            }

            public async Task<List<Author>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
            {
                var authors = repository.GetAll();
                if(request.Id != null)
                {
                    authors = authors.Where(a => a.Id == request.Id);
                }

                if (string.IsNullOrEmpty(request.FullName))
                {
                    authors = authors.Where(a => a.FullName == request.FullName);
                }

                return await authors.ToListAsync();

            }
        }
    }
}
