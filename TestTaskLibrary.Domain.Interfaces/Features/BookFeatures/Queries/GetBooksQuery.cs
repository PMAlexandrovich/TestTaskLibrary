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
using Org.BouncyCastle.Ocsp;

namespace TestTaskLibrary.Domain.Application.Features.BookFeatures.Queries
{
    public class GetBooksQuery : IRequest<List<BookViewModel>>
    {
        public int? Id { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }

        public string Title { get; set; }

        public FieldSearchType? SearchType { get; set; }

        public string Search { get; set; }

        public int PageIndex { get; set; } = 1;

        public int OffsetSize { get; set; } = 10;

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

                if (request.Search != null && request.SearchType != null)
                {
                    switch (request.SearchType)
                    {
                        case FieldSearchType.Author:
                            books = books.Where(b => b.Author.FullName.ToLower().Contains(request.Search.ToLower()));
                            break;
                        case FieldSearchType.Title:
                            books = books.Where(b => b.Title.ToLower().Contains(request.Search.ToLower()));
                            break;
                        case FieldSearchType.Genre:
                            books = books.Where(b => b.Genre.Name.ToLower().Contains(request.Search.ToLower()));
                            break;
                        default:
                            break;
                    }
                }

                books = books.Skip((request.PageIndex - 1) * request.OffsetSize).Take(request.OffsetSize);

                return mapper.Map<List<BookViewModel>>(await books.ToListAsync(cancellationToken));
            }
        }
    }
}
