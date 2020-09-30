using MediatR;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTaskLibrary.Domain.Core;
using Microsoft.EntityFrameworkCore;
using TestTaskLibrary.Domain.Interfaces;
using AutoMapper;
using TestTaskLibrary.Domain.Application.Features.BookFeatures.ViewModels;

namespace TestTaskLibrary.Domain.Application.Features.BookFeatures.Queries
{
    public class GetBooksQuery : IRequest<List<BookViewModel>>
    {
        public int? Id { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }

        public string Title { get; set; }

        public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<BookViewModel>>
        {
            private readonly IBooksRepository booksRepository;

            private readonly IMapper mapper;

            public GetBooksQueryHandler(IBooksRepository booksRepository, IMapper mapper)
            {
                this.booksRepository = booksRepository;
                this.mapper = mapper;
            }

            public async Task<List<BookViewModel>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
            {
                var books = booksRepository.GetAll
                    .Include(b => b.Author)
                    .Include(b => b.Genre)
                    .Include(b => b.CurrentBookStatus)
                        .ThenInclude(s => s.User)
                    .Include(b => b.BookAdditionalInfo)
                        .ThenInclude(i => i.Reviews).AsQueryable();

                if(request.Id != null)
                {
                    books = books.Where(b => b.Id == request.Id);
                }

                if (!string.IsNullOrEmpty(request.Author))
                {
                    books = books.Where(b => b.Author.FullName == request.Author);
                }

                if (!string.IsNullOrEmpty(request.Genre))
                {
                    books = books.Where(b => b.Genre.Name == request.Genre);
                }

                if (!string.IsNullOrEmpty(request.Title))
                {
                    books = books.Where(b => b.Title == request.Title);
                }

                return mapper.Map<List<BookViewModel>>(await books.ToListAsync(cancellationToken));
            }
        }
    }
}
