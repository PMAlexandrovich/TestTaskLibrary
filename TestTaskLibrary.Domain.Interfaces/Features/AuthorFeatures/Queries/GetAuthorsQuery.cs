using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Application.Features.AuthorFeatures.ViewModels;
using TestTaskLibrary.Domain.Application.Interfaces;
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Domain.Application.Features.AuthorFeatures.Queries
{
    public class GetAuthorsQuery : IRequest<List<AuthorViewModel>>
    {
        public int? Id { get; set; }

        public string FullName { get; set; }

        public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, List<AuthorViewModel>>
        {
            private readonly IGenericRepository<Author> repository;
            private readonly IMapper mapper;

            public GetAuthorsQueryHandler(IGenericRepository<Author> repository, IMapper mapper)
            {
                this.repository = repository;
                this.mapper = mapper;
            }

            public async Task<List<AuthorViewModel>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
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

                return mapper.Map<List<AuthorViewModel>>(await authors.ToListAsync());

            }
        }
    }
}
