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
using TestTaskLibrary.Domain.Application.Interfaces.Repositories;
using TestTaskLibrary.Domain.Core;

namespace TestTaskLibrary.Domain.Application.Features.CommonFeatures.Queries.Handlers
{
    public class GetListBookQueryHandler : IRequestHandler<GetListQuery<Book, BookViewModel>, List<BookViewModel>>
    {
        private readonly IGenericRepository<Book> repository;
        private readonly IMapper mapper;

        public GetListBookQueryHandler(IGenericRepository<Book> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<BookViewModel>> Handle(GetListQuery<Book, BookViewModel> request, CancellationToken cancellationToken)
        {
            var books = repository.GetAll()
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Include(b => b.CurrentBookStatus)
                    .ThenInclude(s => s.User)
                .Include(b => b.BookAdditionalInfo)
                    .ThenInclude(i => i.Reviews).AsQueryable();

            if (request.PageIndex < 1)
                request.PageIndex = 1;
            if (request.PageSize < 1)
                request.PageSize = 1;

            books = books.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);

            return mapper.Map<List<BookViewModel>>(await books.ToListAsync(cancellationToken));
        }
    }
}
